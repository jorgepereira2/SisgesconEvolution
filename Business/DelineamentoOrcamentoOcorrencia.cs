using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class DelineamentoOrcamentoOcorrencia : BusinessObject<DelineamentoOrcamentoOcorrencia>	
	{
		#region Private Members
		private DelineamentoOrcamento _delineamentoorcamento; 
		private Celula _celula; 
		private Servidor _servidor; 
		private bool _flagservicoterceiro; 
		private DateTime _datainicio; 
		private string _descricaoservico; 
		private bool _flagparteequipamento; 
		private string _descricaoparteequipamento; 
		private DateTime _dataprevisaofim; 
		private DateTime? _datafim; 
		private string _descricaoconclusao;
	    private bool _flagConcluido;
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public DelineamentoOrcamentoOcorrencia()
		{
			_delineamentoorcamento =  null; 
			_celula =  null; 
			_servidor =  null; 
			_flagservicoterceiro = false; 
			_datainicio = DateTime.MinValue; 
			_descricaoservico = null; 
			_flagparteequipamento = false; 
			_descricaoparteequipamento = null; 
			_dataprevisaofim = DateTime.MinValue; 
			_datafim = null; 
			_descricaoconclusao = null;
		    _flagConcluido = false;
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties

        public virtual bool FlagConcluido
        {
            get { return _flagConcluido; }
            set { _flagConcluido = value; }
        }
        	
		public virtual DelineamentoOrcamento DelineamentoOrcamento
		{
			get { return _delineamentoorcamento; }
			set { _delineamentoorcamento = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual Celula Celula
		{
			get { return _celula; }
			set { _celula = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual Servidor Servidor
		{
			get { return _servidor; }
			set { _servidor = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual bool FlagServicoTerceiro
		{
			get { return _flagservicoterceiro; }
			set { _flagservicoterceiro = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual DateTime DataInicio
		{
			get { return _datainicio; }
			set { _datainicio = value; }
		}
		
		
		public virtual string DescricaoServico
		{
			get { return _descricaoservico; }
			set	
			{
				if ( value != null )
					if( value.Length > 1000)
						throw new ArgumentOutOfRangeException("Invalid value for DescricaoServico", value, value.ToString());
				
				_descricaoservico = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual bool FlagParteEquipamento
		{
			get { return _flagparteequipamento; }
			set { _flagparteequipamento = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual string DescricaoParteEquipamento
		{
			get { return _descricaoparteequipamento; }
			set	
			{
				if ( value != null )
					if( value.Length > 100)
						throw new ArgumentOutOfRangeException("Invalid value for DescricaoParteEquipamento", value, value.ToString());
				
				_descricaoparteequipamento = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual DateTime DataPrevisaoFim
		{
			get { return _dataprevisaofim; }
			set { _dataprevisaofim = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual DateTime? DataFim
		{
			get { return _datafim; }
			set { _datafim = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual string DescricaoConclusao
		{
			get { return _descricaoconclusao; }
			set	
			{
				if ( value != null )
					if( value.Length > 500)
						throw new ArgumentOutOfRangeException("Invalid value for DescricaoConclusao", value, value.ToString());
				
				_descricaoconclusao = value;
			}
		}
			#endregion 
		
		#region Public Methods
		
		public static List<DelineamentoOrcamentoOcorrencia> Select(int id_celula, string numeroPS, DateTime dataInicio, DateTime dataFim, bool? flagConcluido)
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
            @"select d from DelineamentoOrcamentoOcorrencia d inner join d.DelineamentoOrcamento o inner join fetch d.Celula c
			where c.ID = IsNull(:id_celula, c.ID)  			
			and dbo.BuscaCodigoPS(o.PedidoServico.CodigoInterno, o.PedidoServico.DataEmissao, :numeroPS) = 1
			and dbo.DateIsInBetween(d.DataInicio, :dataInicio, :dataFim) = 1
			and d.FlagConcluido = IsNull(:flagConcluido, d.FlagConcluido)
			order by d.DataInicio DESC");

            query.SetParameter("flagConcluido", BusinessHelper.IsNull(flagConcluido), NHibernateUtil.Boolean);
		    query.SetParameter("id_celula", BusinessHelper.IsNullOrZero(id_celula), NHibernateUtil.Int32);
            query.SetParameter("dataInicio", BusinessHelper.IsNull(dataInicio), NHibernateUtil.DateTime);
            query.SetParameter("dataFim", BusinessHelper.IsNull(dataFim), NHibernateUtil.DateTime);
            query.SetString("numeroPS", string.Format("%{0}%", numeroPS));
			return (List<DelineamentoOrcamentoOcorrencia>)query.List<DelineamentoOrcamentoOcorrencia>();
		}
		
		#endregion

        public override void Save()
        {
            base.Save();
            _flagConcluido = _datafim.HasValue;
        }
	}
}
