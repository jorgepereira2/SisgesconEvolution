using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class HistoricoPedidoServicoAtividadeSecundaria : BusinessObject<HistoricoPedidoServicoAtividadeSecundaria>	
	{
		#region Private Members
		private PedidoServicoAtividadeSecundaria _PedidoServicoAtividadeSecundaria; 
		private Servidor _servidor; 
		private DateTime _data; 
		private StatusPedidoServicoAtividadeSecundaria _statusanterior; 
		private StatusPedidoServicoAtividadeSecundaria _statusposterior; 
		private string _justificativarecusa;
        private int? _id_delineamentoOrcamento;
        
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public HistoricoPedidoServicoAtividadeSecundaria()
		{
			_PedidoServicoAtividadeSecundaria =  null; 
			_servidor =  null; 
			_data = DateTime.MinValue; 
			_statusanterior =  null; 
			_statusposterior =  null; 
			_justificativarecusa = null;
		    _id_delineamentoOrcamento = null;
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual PedidoServicoAtividadeSecundaria PedidoServicoAtividadeSecundaria
		{
			get { return _PedidoServicoAtividadeSecundaria; }
			set { _PedidoServicoAtividadeSecundaria = value; }
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
		public virtual DateTime Data
		{
			get { return _data; }
			set { _data = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual StatusPedidoServicoAtividadeSecundaria StatusAnterior
		{
			get { return _statusanterior; }
			set { _statusanterior = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual StatusPedidoServicoAtividadeSecundaria StatusPosterior
		{
			get { return _statusposterior; }
			set { _statusposterior = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual string JustificativaRecusa
		{
			get { return _justificativarecusa; }
			set	
			{
				if ( value != null )
					if( value.Length > 500)
						throw new ArgumentOutOfRangeException("Invalid value for Justificativa", value, value.ToString());
				
				_justificativarecusa = value;
			}
		}

        public virtual int? ID_DelineamentoOrcamento
        {
            get { return _id_delineamentoOrcamento; }
            set { _id_delineamentoOrcamento = value; }
        }
		#endregion 
	
	    public virtual string Texto
	    {
	        get
	        {
	            return StatusAnterior == null
	                       ? "Início"
	                       : string.Format("{0} -> {1}", StatusAnterior.Descricao, StatusPosterior.Descricao);
	        }
	    }
		
		#region Public Methods
		
		public static List<HistoricoPedidoServicoAtividadeSecundaria> Select()
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
			@"from HistoricoPedidoServicoAtividadeSecundaria h  			
			order by h.Descricao");
		
			return (List<HistoricoPedidoServicoAtividadeSecundaria>)query.List<HistoricoPedidoServicoAtividadeSecundaria>();
		}
		
		#endregion
		
    }
}