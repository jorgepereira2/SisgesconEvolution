using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class StatusPedidoAquisicaoDivisao : BusinessObject<StatusPedidoAquisicaoDivisao>	
	{
		#region Private Members
		private StatusPedidoAquisicao _statuspedidoservico; 
		private Celula _celula; 
		private Servidor _servidor; 		
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public StatusPedidoAquisicaoDivisao()
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
		public virtual StatusPedidoAquisicao StatusPedidoAquisicao
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
			from StatusPedidoAquisicaoDivisao s  
			where s.FlagAtivo = 1
			order by s.Descricao");
		
			return BusinessHelper.ExecuteList(query); 
		}
		
		public static List<StatusPedidoAquisicaoDivisao> Select()
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
			@"from StatusPedidoAquisicaoDivisao s  			
			order by s.Descricao");
		
			return (List<StatusPedidoAquisicaoDivisao>)query.List<StatusPedidoAquisicaoDivisao>();
		}
		
		#endregion
		
		
		
	}
}
