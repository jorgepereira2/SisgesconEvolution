using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class ServidorDependente : BusinessObject<ServidorDependente>	
	{
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
        public ServidorDependente()
		{
			
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties
			
	
		public virtual Servidor Servidor{get;set;}
        public virtual string Nome { get; set; }
        public virtual string GrauParentesco { get; set; }
		#endregion 
		
	
		
    }
}