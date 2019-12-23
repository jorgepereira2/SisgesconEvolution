using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class DelineamentoOficina : BusinessObject<DelineamentoOficina>	
	{
		
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public DelineamentoOficina()
		{
		    
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties

	    public virtual Servidor Servidor { get; set; }
        public virtual Celula Oficina { get; set; }
        public virtual DelineamentoOrcamento DelineamentoOrcamento { get; set; }
        public virtual bool Enviado { get; set; }
        public virtual bool FlagRecusado { get; set; }
        public virtual string Justificativa { get; set; }
        // pode ser usado pelo delineador
        public virtual string Comentario { get; set; }
        
		#endregion 
	
        #region Advanced Properties
      

	    #endregion

      
	}
}
