using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class Rotina : BusinessObject<Rotina>, IComparable<Rotina>	
	{
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public Rotina()
		{
			CategoriasServico = new Shared.NHibernateDAL.CustomList<RotinaCategoriaServico>();
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties

	    public virtual string Descricao { get; set; }
        public virtual bool FlagAtivo { get; set; }

        public virtual ICustomList<RotinaCategoriaServico> CategoriasServico { get; set; }
       
		#endregion 
			
        #region Advanced Properties
	    
	    #endregion

        #region Public Methods

        public static Dictionary<int, string> List()
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"select f.ID, f.Descricao 
			from Rotina f  
			where f.FlagAtivo = 1
			order by f.Descricao");

            return BusinessHelper.ExecuteList(query);
        }

        public static List<Rotina> Select(string texto)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"from Rotina c 
			where c.Descricao like :texto			
			order by c.Descricao");

            query.SetString("texto", string.Format("%{0}%", texto));
            return (List<Rotina>)query.List<Rotina>();
        }

        #endregion

	    public virtual int CompareTo(Rotina other)
	    {
	        return Descricao.CompareTo(other.Descricao);
	    }

        public override string ToString()
        {
            return Descricao;
        }
	}
}
