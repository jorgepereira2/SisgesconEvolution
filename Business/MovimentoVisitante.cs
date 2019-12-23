using System;
using System.Collections.Generic;
using System.Data;
using NHibernate;
using Shared.Common;
using Shared.DataAccessHelper;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
    /// <summary>
    /// Esta classe foi feita para o controle de bordo
    /// </summary>
	[Serializable]
	public partial class MovimentoVisitante : BusinessObject<MovimentoVisitante>
	{
		#region Private Members
		
        #endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
        public MovimentoVisitante()
		{
		
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties

        public virtual Visitante Visitante { get; set; }
		
        public virtual Servidor ServidorControleEntrada{ get; set; }
        public virtual Servidor ServidorControleSaida { get; set; }
        public virtual DateTime HoraEntrada { get; set; }
        public virtual DateTime? HoraSaida { get; set; }
        
        #endregion 
		
		
		#region Public Methods

        public static List<MovimentoVisitante> Select()
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
            @"from MovimentoVisitante e inner join fetch e.Visitante v 
			
			order by e.HoraEntrada");


            return (List<MovimentoVisitante>)query.List<MovimentoVisitante>();
		}

        public static MovimentoVisitante GetLast(int id_visitante)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"from MovimentoVisitante e inner join fetch e.Visitante s 
			where s.ID = :id_Visitante
			order by e.HoraEntrada DESC");
            query.SetInt32("id_Visitante", id_visitante);
            query.SetMaxResults(1);

            return query.UniqueResult<MovimentoVisitante>();
        }

     
		#endregion
	}
}
