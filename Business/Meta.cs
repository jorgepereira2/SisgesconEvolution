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
	public class Meta : BusinessObject<Meta>	
	{
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public Meta()
		{
			
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties

	    public virtual string Descricao { get; set; }
        public virtual int Ano { get; set; }
        public virtual decimal Valor { get; set; }
        public virtual Conta Conta { get; set; }
        public virtual Celula Celula { get; set; }
        public virtual TipoMoeda TipoMoeda { get; set; }
        public virtual int Quantidade { get; set; }
        public virtual string UnidadeMedida { get; set; }

        #endregion

        #region Public Methods

        public static List<Meta> Select(int ano, int id_conta, int id_celula)
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
            @"from Meta m 
                    inner join fetch m.Conta c                    
                    inner join fetch m.Celula cel 
            where m.Ano = IsNull(:ano, m.Ano)
            and c.ID = IsNull(:id_conta, c.ID)
            and cel.ID = IsNull(:id_celula, cel.ID)
            order by m.Ano, m.Descricao");

		    
            query.SetParameter("ano", BusinessHelper.IsNullOrZero(ano), NHibernateUtil.Int32);
            query.SetParameter("id_conta", BusinessHelper.IsNullOrZero(id_conta), NHibernateUtil.Int32);
            query.SetParameter("id_celula", BusinessHelper.IsNullOrZero(id_celula), NHibernateUtil.Int32);
            return (List<Meta>)query.List<Meta>();
		}

        public static Dictionary<int, string> List()
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"select t.ID, t.Descricao 
			from Meta t  			
			order by t.Descricao");

            return BusinessHelper.ExecuteList(query);
        }

        public static Dictionary<int, string> List(int id_conta)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"select t.ID, t.Descricao 
			from Meta t
            where t.Conta.ID = :id_conta  			
			order by t.Descricao");
            query.SetInt32("id_conta", id_conta);
            return BusinessHelper.ExecuteList(query);
        }

        public static DataSet SelectSaldo(int id_meta, int ano)
        {
            SQLHelper helper = new SQLHelper();
            object[] param = new object[3];
            param[1] = id_meta;
            param[2] = NullHelper.IsZero(ano);

            return helper.ExecuteDataSet("Meta_SelectSaldo", param);
        }

        public static DataSet SelectSaldoPorMeta(int id_ptres, int id_uge, int id_fase, int id_projeto, int id_naturezaDespesa, int ano)
        {
            SQLHelper helper = new SQLHelper();
            object[] param = new object[7];
            param[1] = NullHelper.IsZero(id_ptres); ;
            param[2] = NullHelper.IsZero(id_uge);
            param[3] = NullHelper.IsZero(id_fase);
            param[4] = NullHelper.IsZero(id_projeto);
            param[5] = NullHelper.IsZero(id_naturezaDespesa);
            param[6] = NullHelper.IsZero(ano);

            return helper.ExecuteDataSet("Meta_SelectSaldoPorMeta", param);
        }
		
		#endregion


        //public override void Save()
        //{
        //    if(_mes == 0)
        //    {
        //        List<Meta> existentes = Select(_material.ID, _celula.ID, _ano, Int32.MinValue);

        //        using (TransactionBlock tran = new TransactionBlock())
        //        {
        //            foreach (Meta Meta in existentes)
        //                Meta.Delete();

        //            for (int i = 1; i < 13; i++)
        //            {
        //                Meta cota = new Meta();
        //                cota._ano = this._ano;
        //                cota._mes = i;
        //                cota._quantidade = _quantidade;
        //                cota._celula = this._celula;
        //                cota._material = this._material;
        //                cota.Save();
        //            }

        //            tran.IsValid = true;
        //        }
        //    }
        //    else
        //        base.Save();
        //}


	   
	}
}
