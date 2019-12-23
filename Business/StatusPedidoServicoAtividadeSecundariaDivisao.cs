using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class StatusPedidoServicoAtividadeSecundariaDivisao : BusinessObject<StatusPedidoServicoAtividadeSecundariaDivisao>	
	{
		#region Private Members
		private StatusPedidoServicoAtividadeSecundaria _statusPedidoServicoAtividadeSecundaria; 
		private Celula _celula; 
		private Servidor _servidor; 		
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public StatusPedidoServicoAtividadeSecundariaDivisao()
		{
			_statusPedidoServicoAtividadeSecundaria =  null; 
			_celula =  null; 
			_servidor =  null; 
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual StatusPedidoServicoAtividadeSecundaria StatusPedidoServicoAtividadeSecundaria
		{
			get { return _statusPedidoServicoAtividadeSecundaria; }
			set { _statusPedidoServicoAtividadeSecundaria = value; }
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
			#endregion 
		
		
		#region Public Methods
		
		public static Dictionary<int, string> List()
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
			@"select s.ID, s.Descricao 
			from StatusPedidoServicoAtividadeSecundariaDivisao s  
			where s.FlagAtivo = 1
			order by s.Descricao");
		
			return BusinessHelper.ExecuteList(query); 
		}
		
		public static List<StatusPedidoServicoAtividadeSecundariaDivisao> Select()
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
			@"from StatusPedidoServicoAtividadeSecundariaDivisao s  			
			order by s.Descricao");
		
			return (List<StatusPedidoServicoAtividadeSecundariaDivisao>)query.List<StatusPedidoServicoAtividadeSecundariaDivisao>();
		}
		
		#endregion
		
		
		
	}
}
