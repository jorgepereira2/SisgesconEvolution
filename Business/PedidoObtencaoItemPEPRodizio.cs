using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class PedidoObtencaoItemPEPRodizio : BusinessObject<PedidoObtencaoItemPEPRodizio>, IItemServicoMaterial	
	{
		#region Private Members
		private PedidoObtencao _pedidoobtencao; 
		private ServicoMaterial _servicomaterial;
        private decimal _quantidade; 
		private OrigemMaterial _origemmaterial; 
		private decimal _quantidadeentregue;
	    private Celula _celula;
	    private string _especificacao;
	    private decimal _valor;
	    private bool _flagCancelado;
	    private DateTime? _dataCancelamento;
	    private Servidor _servidorCancelamento;
	    #endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public PedidoObtencaoItemPEPRodizio()
		{
			_pedidoobtencao =  null; 
			_servicomaterial =  null; 
			_quantidade = 0; 
			_origemmaterial =  Business.OrigemMaterial.PEP; 
			_quantidadeentregue = 0; 
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties
        public virtual Servidor ServidorCancelamento
        {
            get { return _servidorCancelamento; }
            set { _servidorCancelamento = value; }
        }

        public virtual bool FlagCancelado
        {
            get { return _flagCancelado; }
            set { _flagCancelado = value; }
        }

        public virtual DateTime? DataCancelamento
        {
            get { return _dataCancelamento; }
            set { _dataCancelamento = value; }
        }


        /// <summary>
        /// Para qual oficina vai o item
        /// </summary>
        public virtual Celula Celula
        {
            get { return _celula; }
            set { _celula = value; }
        }

        public virtual decimal Valor
        {
            get { return _valor; }
            set { _valor = value; }
        }
	
		public virtual PedidoObtencao PedidoObtencao
		{
			get { return _pedidoobtencao; }
			set { _pedidoobtencao = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual ServicoMaterial ServicoMaterial
		{
			get { return _servicomaterial; }
			set { _servicomaterial = value; }
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
		public virtual OrigemMaterial OrigemMaterial
		{
			get { return _origemmaterial; }
			set { _origemmaterial = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual decimal QuantidadeEntregue
		{
			get { return _quantidadeentregue; }
			set { _quantidadeentregue = value; }
		}

	    public virtual string Especificacao
	    {
	        get { return _especificacao; }
            set{ _especificacao = value;}
	    }


        public virtual Servidor Comprador
        {
            get { return new Servidor(); }
        }

        public virtual Fornecedor Fornecedor
        {
            get { return new Fornecedor(); }
        }
	    #endregion 
		
		#region Public Methods
		
		public static Dictionary<int, string> List()
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
			@"select p.ID, p.Descricao 
			from PedidoObtencaoItemPEPRodizio p  
			where p.FlagAtivo = 1
			order by p.Descricao");
		
			return BusinessHelper.ExecuteList(query); 
		}
		
		public static List<PedidoObtencaoItemPEPRodizio> Select()
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
			@"from PedidoObtencaoItemPEPRodizio p  			
			order by p.Descricao");
		
			return (List<PedidoObtencaoItemPEPRodizio>)query.List<PedidoObtencaoItemPEPRodizio>();
		}
		
		#endregion
		
		public virtual void Cancelar(int id_Servidor, OrigemMaterial origem)
		{
		    this.FlagCancelado = true;
		    this.ServidorCancelamento = Servidor.Get(id_Servidor);
		    this.DataCancelamento = DateTime.Today;
            this.Save();

            _pedidoobtencao.VerificaEntradaFinalizada(origem);
		}
		
	}
}
