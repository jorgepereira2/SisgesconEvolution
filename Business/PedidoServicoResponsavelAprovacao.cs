using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class PedidoServicoResponsavelAprovacao : BusinessObject<PedidoServicoResponsavelAprovacao>	
	{
		#region Private Members
		private Servidor _servidor; 
		private bool _flagautonomia; 		
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public PedidoServicoResponsavelAprovacao()
		{
			_servidor =  null; 
			_flagautonomia = false; 
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties
			
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
		public virtual bool FlagAutonomia
		{
			get { return _flagautonomia; }
			set { _flagautonomia = value; }
		}
			#endregion 
	
		#region Public Methods
		
		public static List<PedidoServicoResponsavelAprovacao> Select()
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
			@"from PedidoServicoResponsavelAprovacao p inner join fetch p.Servidor s");
		
			return (List<PedidoServicoResponsavelAprovacao>)query.List<PedidoServicoResponsavelAprovacao>();
		}
		
		#endregion
		
    }
}