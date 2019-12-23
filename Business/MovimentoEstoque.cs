using System;
using System.Collections.Generic;
using System.Data;
using NHibernate;
using Shared.NHibernateDAL;
using Shared.DataAccessHelper;
using Shared.Common;

namespace Marinha.Business
{
	[Serializable]
	public partial class MovimentoEstoque : BusinessObject<MovimentoEstoque>	
	{
		#region Private Members
		
		private OrigemMaterial _origemmaterial; 
		private DateTime _data;
        private decimal _quantidade; 
		private ServicoMaterial _material; 
		private TipoMovimento _tipomovimento;
	    private RequisicaoEstoqueItem _requisicaoEstoqueItem;
	    private EntradaMaterialItem _entradaMaterialItem;
	    private TipoOperacaoEstoque _tipoOperacaoEstoque;
	    private PedidoCotacaoItem _pedidoCotacaoItem;
	    private NotaEntregaMaterialOMFItem _itemOMF;

	    #endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public MovimentoEstoque()
		{
			_origemmaterial = OrigemMaterial.Obtencao; 
			_tipoOperacaoEstoque = TipoOperacaoEstoque.Normal;
			_data = DateTime.MinValue; 
			_quantidade = 0; 
			_material =  null;
		    _requisicaoEstoqueItem = null;
		    _entradaMaterialItem = null;
		    _pedidoCotacaoItem = null;
			_tipomovimento = TipoMovimento.Entrada; 
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties
        public virtual PedidoCotacaoItem PedidoCotacaoItem
        {
            get { return _pedidoCotacaoItem; }
            set { _pedidoCotacaoItem = value; }
        }

        public virtual TipoOperacaoEstoque TipoOperacaoEstoque
        {
            get { return _tipoOperacaoEstoque; }
            set { _tipoOperacaoEstoque = value; }
        }
        
        public virtual EntradaMaterialItem EntradaMaterialItem
        {
            get { return _entradaMaterialItem; }
            set { _entradaMaterialItem = value; }
        }

        public virtual RequisicaoEstoqueItem RequisicaoEstoqueItem
        {
            get { return _requisicaoEstoqueItem; }
            set { _requisicaoEstoqueItem = value; }
        }
        	
		/// <summary>
		/// 
		/// </summary>		
		public virtual OrigemMaterial OrigemMaterial
		{
			get { return _origemmaterial; }
			set { _origemmaterial = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual DateTime Data
		{
			get { return _data; }
			set { _data = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
        public virtual decimal Quantidade
		{
			get { return _quantidade; }
			set { _quantidade = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual ServicoMaterial Material
		{
			get { return _material; }
			set { _material = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual TipoMovimento TipoMovimento
		{
			get { return _tipomovimento; }
			set { _tipomovimento = value; }
		}

	    public virtual NotaEntregaMaterialOMFItem ItemOMF
	    {
	        get { return _itemOMF; }
	        set { _itemOMF = value; }
	    }

	    #endregion 
		
		#region Public Methods
		
		public static List<MovimentoEstoque> Select()
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
			@"from MovimentoEstoque m  			
			order by m.Descricao");
		
			return (List<MovimentoEstoque>)query.List<MovimentoEstoque>();
		}
        
        public static QuantidadeEstoque GetQuantidadeEstoque(int id_material, int id_origemMaterial)
        {
            SQLHelper helper = new SQLHelper();
            object[] param = new object[3];
            param[1] = NullHelper.IsZero(id_material);
            param[2] = NullHelper.IsZero(id_origemMaterial);

            DataTable dt = helper.ExecuteDataTable("MovimentoEstoque_SelectPosicaoEstoque", param);
            if(dt.Rows.Count == 0)
                return new QuantidadeEstoque(0,0,0,(Business.OrigemMaterial)id_origemMaterial);
            else
                return new QuantidadeEstoque(
                    Convert.ToInt32(dt.Rows[0]["QuantidadeEntrada"]),
                    Convert.ToInt32(dt.Rows[0]["QuantidadeSaida"]),
                    Convert.ToInt32(dt.Rows[0]["QuantidadeReservada"]), 
                    (Business.OrigemMaterial)id_origemMaterial);
        }
        #endregion
        
        #region Relatorios
        
        public static DataTable SelectPosicaoEstoque(int id_material, int id_origemMaterial, DateTime dataInicio, DateTime dataFim)
        {
            SQLHelper helper = new SQLHelper();
            object[] param = new object[5];
            param[1] =  NullHelper.IsZero(id_material);
            param[2] = NullHelper.IsZero(id_origemMaterial);
            param[3] = NullHelper.IsNull(dataInicio);
            param[4] = NullHelper.IsNull(dataFim);

            return helper.ExecuteDataTable("MovimentoEstoque_SelectPosicaoEstoque", param);
        }

        public static DataSet SelectMovimentoPorMaterial(int id_material, int id_origemMaterial, DateTime dataInicio, DateTime dataFim)
        {
            SQLHelper helper = new SQLHelper();
            object[] param = new object[5];
            param[1] = NullHelper.IsZero(id_material);
            param[2] = NullHelper.IsZero(id_origemMaterial);
            param[3] = NullHelper.IsNull(dataInicio);
            param[4] = NullHelper.IsNull(dataFim);

            return helper.ExecuteDataSet("MovimentoEstoque_SelectMovimentoPorMaterial", param);
        }
		#endregion

        public static void InsertMovimento(EntradaMaterialItem materialItemEntrada)
        {
            MovimentoEstoque movimento = new MovimentoEstoque();
            movimento.Data = DateTime.Now;
            movimento.EntradaMaterialItem = materialItemEntrada;
            movimento.Material = materialItemEntrada.Material;
            movimento.OrigemMaterial =  materialItemEntrada.EntradaMaterial.OrigemMaterial == OrigemMaterial.Obtencao ? Business.OrigemMaterial.PEP : materialItemEntrada.EntradaMaterial.OrigemMaterial;
            movimento.Quantidade = materialItemEntrada.Quantidade;
            movimento.TipoMovimento = TipoMovimento.Saida;
            movimento.Save();
        }
	}

    public class QuantidadeEstoque
    {
        private readonly int _quantidadeEntrada;
        private readonly int _quantidadeSaida;
        private readonly int _quantidadeReservada;
        private readonly OrigemMaterial _origemMaterial;

        public int QuantidadeEntrada
        {
            get { return _quantidadeEntrada; }
        }

        public int QuantidadeReservada
        {
            get { return _quantidadeReservada; }
        }

        public int QuantidadeSaida
        {
            get { return _quantidadeSaida; }
        }
        
        public int QuantidadeAtual
        {
            get{ return _quantidadeEntrada - _quantidadeSaida;}
        }

        public int QuantidadeDisponivel
        {
            get { return _quantidadeEntrada - _quantidadeSaida - _quantidadeReservada; }
        }
        
        public OrigemMaterial OrigemMaterial
        {
            get { return _origemMaterial; }
        }

        public QuantidadeEstoque(int _quantidadeEntrada, int _quantidadeSaida, int _quantidadeReservada, OrigemMaterial _origemMaterial)
        {
            this._quantidadeEntrada = _quantidadeEntrada;
            this._quantidadeSaida = _quantidadeSaida;
            this._quantidadeReservada = _quantidadeReservada;
            this._origemMaterial = _origemMaterial;
            
        }
    }
}
