using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class Fase : BusinessObject<Fase>, IDescricao, IComparable<Fase>	
	{
		
		#region Default ( Empty ) Class Constuctor
		
		public Fase()
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
			from Fase a  
			where a.FlagAtivo = 1
			order by a.Descricao");
		
			return BusinessHelper.ExecuteList(query); 
		}
		
		public static List<Fase> Select()
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
			@"from Fase a 
			order by a.Descricao");
		
			return (List<Fase>)query.List<Fase>();
		}
		#endregion

	    public virtual int CompareTo(Fase other)
	    {
	        return Descricao.CompareTo(other.Descricao);
	    }
        public override string ToString()
        {
            return Descricao;
        }
	}
}
