using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class ServidorCursoMilitar : BusinessObject<ServidorCursoMilitar>	
	{
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
        public ServidorCursoMilitar()
		{
			
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties
			
	
		public virtual Servidor Servidor{get;set;}
        public virtual string Descricao { get; set; }
		#endregion 
		
	
		
    }
}