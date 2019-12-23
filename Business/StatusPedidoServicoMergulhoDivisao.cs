using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class StatusPedidoServicoMergulhoDivisao : BusinessObject<StatusPedidoServicoMergulhoDivisao>	
	{
		#region Private Members
		private StatusPedidoServicoMergulho _statusPedidoServicoMergulho; 
		private Celula _celula; 
		private Servidor _servidor; 		
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public StatusPedidoServicoMergulhoDivisao()
		{
			_statusPedidoServicoMergulho =  null; 
			_celula =  null; 
			_servidor =  null; 
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual StatusPedidoServicoMergulho StatusPedidoServicoMergulho
		{
			get { return _statusPedidoServicoMergulho; }
			set { _statusPedidoServicoMergulho = value; }
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
			from StatusPedidoServicoMergulhoDivisao s  
			where s.FlagAtivo = 1
			order by s.Descricao");
		
			return BusinessHelper.ExecuteList(query); 
		}
		
		public static List<StatusPedidoServicoMergulhoDivisao> Select()
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
			@"from StatusPedidoServicoMergulhoDivisao s  			
			order by s.Descricao");
		
			return (List<StatusPedidoServicoMergulhoDivisao>)query.List<StatusPedidoServicoMergulhoDivisao>();
		}
		
		#endregion
		
		
		
	}
}
