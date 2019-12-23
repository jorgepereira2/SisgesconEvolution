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
	public partial class MovimentoServidor : BusinessObject<MovimentoServidor>
	{
		#region Private Members
		
        #endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public MovimentoServidor()
		{
		
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties

        public virtual Servidor Servidor { get; set; }
		
        public virtual Servidor ServidorControleEntrada{ get; set; }
        public virtual Servidor ServidorControleSaida { get; set; }

        public virtual Servidor ServidorAutorizacaoSaida { get; set; }

        public virtual DateTime HoraEntrada { get; set; }

        public virtual DateTime? HoraSaida { get; set; }

        public virtual string JustificativaSaida { get; set; }

        #endregion 
		
		
		#region Public Methods

        public static List<MovimentoServidor> Select()
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
            @"from MovimentoServidor e inner join fetch e.Servidor s 
			
			order by e.HoraEntrada");


            return (List<MovimentoServidor>)query.List<MovimentoServidor>();
		}

        public static MovimentoServidor GetLast(int id_servidor)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"from MovimentoServidor e inner join fetch e.Servidor s 
			where s.ID = :id_Servidor
			order by e.HoraEntrada DESC");
            query.SetInt32("id_Servidor", id_servidor);
            query.SetMaxResults(1);

            return query.UniqueResult<MovimentoServidor>();
        }

     
		#endregion
	}
}
