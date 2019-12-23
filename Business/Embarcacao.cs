using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class Embarcacao : BusinessObject<Embarcacao>, IDescricao, IComparable<Embarcacao>	
	{
		
		#region Default ( Empty ) Class Constuctor
		
		public Embarcacao()
		{
		
		}
		#endregion 

		#region Public Properties

		public virtual string Descricao { get; set;}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual bool FlagAtivo { get; set;}
			#endregion 
		
		#region Public Methods
		
		public static Dictionary<int, string> List()
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
			@"select a.ID, a.Descricao 
			from Embarcacao a  
			where a.FlagAtivo = 1
			order by a.Descricao");
		
			return BusinessHelper.ExecuteList(query); 
		}
		
		public static List<Embarcacao> Select()
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
			@"from Embarcacao a 
			order by a.Descricao");
		
			return (List<Embarcacao>)query.List<Embarcacao>();
		}
		#endregion

	    public virtual int CompareTo(Embarcacao other)
	    {
	        return Descricao.CompareTo(other.Descricao);
	    }
        public override string ToString()
        {
            return Descricao;
        }
	}
}
