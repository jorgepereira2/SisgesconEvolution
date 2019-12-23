using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public class DiaMaximoLancamentoFamod : BusinessObject<DiaMaximoLancamentoFamod>, IComparable<DiaMaximoLancamentoFamod>	
	{
		#region Private Members
		private DateTime _data;
	    private int _ano;
	    private int _mes;

		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
        public DiaMaximoLancamentoFamod()
		{
			
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties

        public virtual int Ano
        {
            get { return _ano; }
            set { _ano = value; }
        }

        public virtual int Mes
        {
            get { return _mes; }
            set { _mes = value; }
        }
        public virtual DateTime Data
        {
            get { return _data; }
            set { _data = value; }
        }
        #endregion

        #region Public Methods

        public static DiaMaximoLancamentoFamod Get(int ano, int mes)
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
            @"from DiaMaximoLancamentoFamod a  
			where a.Mes = :mes
            and a.Ano = :ano");

            query.SetMaxResults(1);
            query.SetInt32("ano", ano);
            query.SetInt32("mes", mes);

			return query.UniqueResult<DiaMaximoLancamentoFamod>(); 
		}

        public static List<DiaMaximoLancamentoFamod> Select()
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
            @"from DiaMaximoLancamentoFamod a 
			order by a.Data");

            return (List<DiaMaximoLancamentoFamod>)query.List<DiaMaximoLancamentoFamod>();
		}
		#endregion

        public virtual int CompareTo(DiaMaximoLancamentoFamod other)
	    {
	        return Data.CompareTo(other.Data);
	    }
        public override string ToString()
        {
            return _data.ToShortDateString();
        }
	}
}
