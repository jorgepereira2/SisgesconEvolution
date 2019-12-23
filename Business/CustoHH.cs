using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class CustoHH : BusinessObject<CustoHH>	
	{
		#region Private Members
		private decimal _valorcusto; 
		private DateTime _mesano; 		
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public CustoHH()
		{
			_valorcusto = 0; 
			_mesano = DateTime.MinValue; 
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual decimal ValorCusto
		{
			get { return _valorcusto; }
			set { _valorcusto = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual DateTime MesAno
		{
			get { return _mesano; }
			set { _mesano = value; }
		}
			#endregion 
		
		
		#region Public Methods
		
        public static decimal GetCustoPorData(DateTime data)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"from CustoHH c  			
			where dbo.GetYear(c.MesAno) = :ano
            and dbo.GetMonth(c.MesAno) = :mes");

            query.SetInt32("ano", data.Year);
            query.SetInt32("mes", data.Month);

            CustoHH custo = query.UniqueResult<CustoHH>();
            return custo == null ? 0 : custo.ValorCusto;
        }
	
		
		public static List<CustoHH> Select()
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
			@"from CustoHH c  			
			order by c.MesAno");
		
			return (List<CustoHH>)query.List<CustoHH>();
		}
		
		#endregion

        public override void Save()
        {
            using(TransactionBlock tran = new TransactionBlock())
            {
                ISession session = NHibernateSessionManager.Instance.GetSession();
                IQuery query =
                    session.CreateSQLQuery(
                        "update tb_famod set valorcustoHH = :valor where month(data) = :mes and year(data) = :ano ;  select 1 as result")
                        .AddScalar("result", NHibernateUtil.Int32);
                query.SetDecimal("valor", this.ValorCusto);
                query.SetInt32("mes", this._mesano.Month);
                query.SetInt32("ano", this._mesano.Year);
                query.List();

                base.Save();
                tran.IsValid = true;
            }
        }
		
	}
}
