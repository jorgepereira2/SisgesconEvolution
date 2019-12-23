using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class EncarregadoCelula : BusinessObject<EncarregadoCelula>	
	{
		#region Private Members
		private Celula _celula; 
		private Servidor _servidor; 
		private DateTime _datainicio; 
		private DateTime? _datafim; 		
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public EncarregadoCelula()
		{
			_celula =  null; 
			_servidor =  null; 
			_datainicio = DateTime.MinValue; 
			_datafim = null; 
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties
			
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
		public virtual Servidor Servidor
		{
			get { return _servidor; }
			set { _servidor = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual DateTime DataInicio
		{
			get { return _datainicio; }
			set { _datainicio = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual DateTime? DataFim
		{
			get { return _datafim; }
			set { _datafim = value; }
		}
			#endregion 
		
		
		#region Public Methods
		
		
		public static List<EncarregadoCelula> Select()
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
			@"from EncarregadoCelula e  			
			order by e.Celula.Descricao");
		
			return (List<EncarregadoCelula>)query.List<EncarregadoCelula>();
		}

        public override void Save()
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"select count(*) from EncarregadoCelula e  			
              where e.Celula.ID = :id_celula
              and e.Servidor.ID = :id_servidor   
              and e.ID != :id_encarregadoCelula  
			");
            query.SetInt32("id_celula", this.Celula.ID);
            query.SetInt32("id_servidor", this.Servidor.ID);
            query.SetInt32("id_encarregadoCelula", this.ID);

            int result = Convert.ToInt32(query.UniqueResult());
            if(result > 0)
                throw new Exception("Este Servidor já é responsável por essa célula.");
            base.Save();
        }
		#endregion
		
		
		
	}
}
