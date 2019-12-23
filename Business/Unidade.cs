using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
    public partial class Unidade : BusinessObject<Unidade>, IDescricao	
	{
		#region Private Members
		private string _descricao; 
		private bool _flagativo;
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public Unidade()
		{
			_descricao = null; 
			_flagativo = false; 
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual string Descricao
		{
			get { return _descricao; }
			set	
			{
				if ( value != null )
					if( value.Length > 60)
						throw new ArgumentOutOfRangeException("Invalid value for Descricao", value, value.ToString());
				
				_descricao = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual bool FlagAtivo
		{
			get { return _flagativo; }
			set { _flagativo = value; }
		}
			#endregion 
		
		
		#region Public Methods
		
		public static Dictionary<int, string> List()
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
			@"select u.ID, u.Descricao 
			from Unidade u  
			where u.FlagAtivo = 1
			order by u.Descricao");
		
			return BusinessHelper.ExecuteList(query); 
		}
		
		public static List<Unidade> Select()
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
			@"from Unidade u  			
			order by u.Descricao");
		
			return (List<Unidade>)query.List<Unidade>();
		}
		
		#endregion
		
		
		
	}
}
