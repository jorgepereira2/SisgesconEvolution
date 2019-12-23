using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public class CotaMaterial : BusinessObject<CotaMaterial>	
	{
		#region Private Members
		private ServicoMaterial _material; 
		private Celula _celula; 
		private int _ano; 
		private int _mes; 
		private decimal _quantidade; 		
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public CotaMaterial()
		{
			
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual ServicoMaterial Material
		{
			get { return _material; }
			set { _material = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual Celula Celula
		{
			get { return _celula; }
			set { _celula = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual int Ano
		{
			get { return _ano; }
			set { _ano = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual int Mes
		{
			get { return _mes; }
			set { _mes = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual decimal Quantidade
		{
			get { return _quantidade; }
			set { _quantidade = value; }
		}

		#endregion 
		
		
		#region Public Methods
		
		public static List<CotaMaterial> Select(int id_material, int id_celula, int ano, int mes)
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
            @"from CotaMaterial c 
                    inner join fetch c.Material m
                    inner join fetch c.Celula cel
            where c.Ano = IsNull(:ano, c.Ano)
            and   c.Mes = IsNull(:mes, c.Mes)
            and   m.ID = IsNull(:id_material, m.ID)  
            and   cel.ID = IsNull(:id_celula, cel.ID)
			order by c.Ano, c.Mes, cel.Descricao");

		    query.SetParameter("id_material", BusinessHelper.IsNullOrZero(id_material), NHibernateUtil.Int32);
            query.SetParameter("id_celula", BusinessHelper.IsNullOrZero(id_celula), NHibernateUtil.Int32);
            query.SetParameter("ano", BusinessHelper.IsNullOrZero(ano), NHibernateUtil.Int32);
            query.SetParameter("mes", BusinessHelper.IsNullOrZero(mes), NHibernateUtil.Int32);
			return (List<CotaMaterial>)query.List<CotaMaterial>();
		}
		
		#endregion

        public static int GetCota(int id_material, int id_celula, int ano, int mes)
        {
            List<CotaMaterial> list = Select(id_material, id_celula, ano, mes);
            if (list.Count == 0)
                return 0;
            else
                return Convert.ToInt32(list[0].Quantidade);
        }

        public override void Save()
        {
            if(_mes == 0)
            {
                List<CotaMaterial> existentes = Select(_material.ID, _celula.ID, _ano, Int32.MinValue);

                using (TransactionBlock tran = new TransactionBlock())
                {
                    foreach (CotaMaterial cotaMaterial in existentes)
                        cotaMaterial.Delete();

                    for (int i = 1; i < 13; i++)
                    {
                        CotaMaterial cota = new CotaMaterial();
                        cota._ano = this._ano;
                        cota._mes = i;
                        cota._quantidade = _quantidade;
                        cota._celula = this._celula;
                        cota._material = this._material;
                        cota.Save();
                    }

                    tran.IsValid = true;
                }
            }
            else
                base.Save();
        }
		
		
		
	}
}
