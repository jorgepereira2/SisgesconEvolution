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
    public class Conta : BusinessObject<Conta>, IDescricao, IComparable<Conta>		
	{
		#region Private Members

	 
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public Conta()
		{
			
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties

        public virtual string Descricao { get; set;}
        public virtual bool FlagAtivo { get; set; }

	    public virtual Projeto Projeto{ get; set;}
		
		public virtual Fase Fase{ get; set;}
			
		public virtual int Ano{ get; set;}

        public virtual NaturezaDespesa NaturezaDespesa{ get; set;}

        public virtual PTRES PTRES { get; set; }
        public virtual UGE UGE { get; set; }

	    #endregion 
		
		
		#region Public Methods
		
		public static List<Conta> Select()
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
            @"from Conta c 
                    inner join fetch c.Projeto p
                    inner join fetch c.Fase f
                    inner join fetch c.NaturezaDespesa n           
			order by c.Ano, c.Descricao");

            //query.SetParameter("ano", BusinessHelper.IsNullOrZero(ano), NHibernateUtil.Int32);
            //query.SetString("nome", string.Format("%{0}%", nome));
			return (List<Conta>)query.List<Conta>();
		}

        public static Dictionary<int, string> List()
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"from Conta c
                    inner join fetch c.Projeto p
                    inner join fetch c.Fase f
                    inner join fetch c.NaturezaDespesa n    			
                    inner join fetch c.PTRES pt
                    inner join fetch c.UGE u
			order by c.Descricao");

            Dictionary<int, string> list = new Dictionary<int, string>();
            foreach (Conta conta in query.List<Conta>())
            {
                list.Add(conta.ID, string.Format("{0} x {1} x {2} x {3} x {4}", conta.UGE, conta.Projeto.Descricao, conta.Fase.Descricao, conta.NaturezaDespesa.Descricao, conta.PTRES.Descricao ));
            }

            return list;
        }

        public static DataSet SelectSaldo(int id_conta, int ano)
        {
            SQLHelper helper = new SQLHelper();
            object[] param = new object[3];
            param[1] = id_conta;
            param[2] = NullHelper.IsZero(ano);

            return helper.ExecuteDataSet("Conta_SelectSaldo", param);
        }

		#endregion

        public override string ToString()
        {
            return Descricao;
        }

        public virtual int CompareTo(Conta other)
        {
            if (other == null) return -1;
            return Descricao.CompareTo(other.Descricao);
        }
	}
}
