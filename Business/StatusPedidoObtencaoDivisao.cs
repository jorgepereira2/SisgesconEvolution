using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class StatusPedidoObtencaoDivisao : BusinessObject<StatusPedidoObtencaoDivisao>	
	{
		#region Private Members
		private StatusPedidoObtencao _statuspedidoservico; 
		private Celula _celula; 
		private Servidor _servidor; 		
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public StatusPedidoObtencaoDivisao()
		{
			_statuspedidoservico =  null; 
			_celula =  null; 
			_servidor =  null; 
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual StatusPedidoObtencao StatusPedidoObtencao
		{
			get { return _statuspedidoservico; }
			set { _statuspedidoservico = value; }
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
			from StatusPedidoObtencaoDivisao s  
			where s.FlagAtivo = 1
			order by s.Descricao");
		
			return BusinessHelper.ExecuteList(query); 
		}
		
		public static List<StatusPedidoObtencaoDivisao> Select()
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
			@"from StatusPedidoObtencaoDivisao s  			
			order by s.Descricao");
		
			return (List<StatusPedidoObtencaoDivisao>)query.List<StatusPedidoObtencaoDivisao>();
		}
		
		#endregion
		
		
		
	}
}
