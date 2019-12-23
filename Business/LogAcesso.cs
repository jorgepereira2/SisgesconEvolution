using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class LogAcesso : BusinessObject<LogAcesso>	
	{
		#region Private Members
		private Servidor _servidor; 
		private DateTime _data; 
		private string _ip; 
		private string _browser; 		
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public LogAcesso()
		{
			_servidor =  null; 
			_data = DateTime.MinValue; 
			_ip = null; 
			_browser = null; 
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
		public virtual DateTime Data
		{
			get { return _data; }
			set { _data = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual string IP
		{
			get { return _ip; }
			set	
			{
				if ( value != null )
					if( value.Length > 20)
						throw new ArgumentOutOfRangeException("Invalid value for IP", value, value.ToString());
				
				_ip = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual string Browser
		{
			get { return _browser; }
			set	
			{
				if ( value != null )
					if( value.Length > 30)
						throw new ArgumentOutOfRangeException("Invalid value for Browser", value, value.ToString());
				
				_browser = value;
			}
		}
			#endregion 
		
		
		#region Public Methods
		
		public static List<LogAcesso> Select(int id_servidor, DateTime dataInicio, DateTime dataFim)
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
			@"from LogAcesso l inner join fetch l.Servidor s
			where s.ID = IsNull(:id_servidor, s.ID)
			and dbo.DateIsInBetween(l.Data, :dataInicio, :dataFim) = 1
			order by l.Data DESC");

		    query.SetParameter("id_servidor", BusinessHelper.IsNullOrZero(id_servidor), NHibernateUtil.Int32);
            query.SetParameter("dataInicio", BusinessHelper.IsNull(dataInicio), NHibernateUtil.DateTime);
            query.SetParameter("dataFim", BusinessHelper.IsNull(dataFim), NHibernateUtil.DateTime);
			return (List<LogAcesso>)query.List<LogAcesso>();
		}
		
		#endregion
		
		
		
	}
}
