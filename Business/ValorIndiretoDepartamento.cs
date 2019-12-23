using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class ValorIndiretoDepartamento : BusinessObject<ValorIndiretoDepartamento>	
	{
		#region Private Members
		private Celula _departamento;
        private TipoValor tipovalor; 
		private int _mes; 
		private int _ano; 
		private decimal _valor; 		
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public ValorIndiretoDepartamento()
		{
			
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual Celula Departamento
		{
			get { return _departamento; }
			set { _departamento = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual TipoValor TipoValor
		{
			get { return tipovalor; }
			set { tipovalor = value; }
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
		public virtual int Ano
		{
			get { return _ano; }
			set { _ano = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual decimal Valor
		{
			get { return _valor; }
			set { _valor = value; }
		}
			#endregion 
		
		
		#region Public Methods
		
		
		public static List<ValorIndiretoDepartamento> Select()
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
			@"from ValorIndiretoDepartamento v inner join fetch v.Departamento d  			
			order by v.Ano, v.Mes, d.Descricao");
		
			return (List<ValorIndiretoDepartamento>)query.List<ValorIndiretoDepartamento>();
		}
		
		#endregion
		
		
		
	}
}
