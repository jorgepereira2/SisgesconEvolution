using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
    public partial class TipoMoeda : BusinessObject<TipoMoeda>, IDescricao, IComparable<TipoMoeda>	
	{
		
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public TipoMoeda()
		{
		
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual string Descricao { get; set;}
			
		public virtual string Sigla { get; set;}
			
		public virtual bool FlagAtivo{ get; set;}
		#endregion 
		
		
		#region Public Methods
		
		public static Dictionary<int, string> List()
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
			@"select s.ID, s.Descricao 
			from TipoMoeda s  
			where s.FlagAtivo = 1
			order by s.Descricao");
		
			return BusinessHelper.ExecuteList(query); 
		}

        
		public static List<TipoMoeda> Select()
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
			@"from TipoMoeda s  			
			order by s.Descricao");
		
			return (List<TipoMoeda>)query.List<TipoMoeda>();
		}

     
		#endregion

        public virtual int CompareTo(TipoMoeda other)
	    {
            if (other == null) return 1;
	        return Descricao.CompareTo(other.Descricao);
	    }

        public override string ToString()
        {
            return Descricao;
        }
	}
}
