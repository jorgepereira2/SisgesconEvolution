using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public class ProvisionadoSiafi : BusinessObject<ProvisionadoSiafi>	
	{
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public ProvisionadoSiafi()
		{
			
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties

	    public virtual string CodigoSiafi { get; set; }
        public virtual int Ano { get; set; }
        public virtual Projeto Projeto { get; set; }
        public virtual NaturezaDespesa NaturezaDespesa { get; set; }

        #endregion

        #region Public Methods

        public static List<ProvisionadoSiafi> Select(int ano)
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
            @"from ProvisionadoSiafi m 
                    inner join fetch m.Projeto p
                    inner join fetch m.NaturezaDespesa n
            where m.Ano = IsNull(:ano, m.Ano)
            order by m.Ano, m.CodigoSiafi");

		    
            query.SetParameter("ano", BusinessHelper.IsNullOrZero(ano), NHibernateUtil.Int32);
            return (List<ProvisionadoSiafi>)query.List<ProvisionadoSiafi>();
		}
		
		#endregion


        //public override void Save()
        //{
        //    if(_mes == 0)
        //    {
        //        List<ProvisionadoSiafi> existentes = Select(_material.ID, _celula.ID, _ano, Int32.MinValue);

        //        using (TransactionBlock tran = new TransactionBlock())
        //        {
        //            foreach (ProvisionadoSiafi ProvisionadoSiafi in existentes)
        //                ProvisionadoSiafi.Delete();

        //            for (int i = 1; i < 13; i++)
        //            {
        //                ProvisionadoSiafi cota = new ProvisionadoSiafi();
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
