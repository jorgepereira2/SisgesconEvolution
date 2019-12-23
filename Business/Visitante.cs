using System;
using System.Collections.Generic;
using System.Data;
using NHibernate;
using Shared.Common;
using Shared.DataAccessHelper;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
    public class Visitante : BusinessObject<Visitante>
	{
		#region Private Members

	 
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public Visitante()
		{
			
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties

        public virtual string Nome { get; set;}
        public virtual string NomeEmpresa { get; set; }
        public virtual string Identidade { get; set; }
        public virtual string Telefone { get; set; }
        public virtual byte[] Foto { get; set; }

	    #endregion 
		
		
		#region Public Methods
		
		public static List<Visitante> Select()
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
            @"from Visitante v 
			order by v.Nome");

            //query.SetParameter("ano", BusinessHelper.IsNullOrZero(ano), NHibernateUtil.Int32);
            //query.SetString("nome", string.Format("%{0}%", nome));
			return (List<Visitante>)query.List<Visitante>();
		}

        public static Visitante GetByIdentidade(string identidade)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"from Visitante v where v.Identidade = :identidade");
            
            query.SetString("identidade", identidade);
            return query.UniqueResult<Visitante>();
        }

		#endregion

     
	}
}
