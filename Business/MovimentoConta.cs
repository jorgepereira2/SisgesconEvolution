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
    /// Esta classe foi feita para o parsit
    /// </summary>
	[Serializable]
	public partial class MovimentoConta : BusinessObject<MovimentoConta>
	{
		#region Private Members
		
        #endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public MovimentoConta()
		{
		
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties

        public virtual Conta Conta { get; set; }
		
        public virtual Servidor Servidor{ get; set; }

        public virtual DateTime Data { get; set; }

        public virtual decimal Valor { get; set; }

        public virtual string NumeroDocumento { get; set; }

        public virtual string Observacao { get; set; }

        public virtual TipoMovimentoFinanceiro TipoMovimentoFinanceiro { get; set; }

        public virtual TipoOperacaoFinanceira TipoOperacaoFinanceira { get; set; }

        public virtual PedidoAquisicao PedidoAquisicao { get; set; }

        #endregion 
		
		
		#region Public Methods
		
		public static List<MovimentoConta> SelectMovimentoConta(string numeroDocumento)
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
            @"from MovimentoConta e inner join fetch e.Conta c 
			where e.NumeroDocumento like :numeroDocumento			
            and e.TipoMovimentoFinanceiro = 1
			order by e.Data");

		    query.SetString("numeroDocumento", "%" + numeroDocumento + "%");
            return (List<MovimentoConta>)query.List<MovimentoConta>();
		}

        public static List<MovimentoConta> SelectExtrato(int id_conta, DateTime dataInicio, DateTime dataFim)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"from MovimentoConta m inner join fetch m.Conta c
			where c.ID = :id_conta
            and dbo.DateIsInBetween(m.Data, :dataInicio, :dataFim) = 1
            and m.TipoMovimentoFinanceiro != 2
            and dbo.GetYear(m.Data) = :ano
			order by m.Data");

            query.SetInt32("id_conta", id_conta);
            query.SetInt32("ano", dataInicio.Year);
            query.SetDateTime("dataInicio", dataInicio);
            query.SetDateTime("dataFim", dataFim);

            return (List<MovimentoConta>)query.List<MovimentoConta>();
        }

        public static decimal SelectSaldo(int id_conta, int ano, DateTime data)
        {
            SQLHelper helper = new SQLHelper();
            object[] param = new object[4];
            param[1] = id_conta;
            param[2] = ano;
            param[3] = data;
            
            DataSet ds = helper.ExecuteDataSet("MovimentoConta_SelectSaldo", param);
            if (ds.Tables[0].Rows.Count > 0)
                return ds.Tables[0].Rows[0][0] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[0][0]);
            else
                return 0;
        }

        public static DataSet SelectAgrupado(int id_projeto, int id_naturezaDespesa, int id_fonteRecurso, int id_ptres, DateTime dataInicio, DateTime dataFim)
        {
            SQLHelper helper = new SQLHelper();
            object[] param = new object[7];
            param[1] = NullHelper.IsNull(dataInicio);
            param[2] = NullHelper.IsNull(dataFim);
            param[3] = NullHelper.IsZero(id_projeto);
            param[4] = NullHelper.IsZero(id_naturezaDespesa);
            param[5] = NullHelper.IsZero(id_fonteRecurso);
            param[6] = NullHelper.IsZero(id_ptres);


            return helper.ExecuteDataSet("EntradaValores_SelectAgrupado", param);
        }
		
		#endregion

//        public static MovimentoConta GetByAC(int id_ac)
//        {
//            ISession session = NHibernateSessionManager.Instance.GetSession();
//            IQuery query = session.CreateQuery(
//            @"from MovimentoConta e 
//			where e.AutorizacaoCompra.ID = :id_ac");

//            query.SetInt32("id_ac", id_ac);
//            return query.UniqueResult<MovimentoConta>();
//        }



        public static DataSet SelectSaldoAnual(int ano)
        {
            SQLHelper helper = new SQLHelper();
            object[] param = new object[2];
            param[1] = ano;

            return helper.ExecuteDataSet("MovimentoConta_SaldoAnual", param);
        }
	}
}
