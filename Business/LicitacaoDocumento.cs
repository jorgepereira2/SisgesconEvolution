using System;
using System.Collections.Generic;
using System.Threading;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class LicitacaoDocumento : BusinessObject<LicitacaoDocumento>	
	{
		#region Private Members
		
	   
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public LicitacaoDocumento()
		{
			
		}

        public LicitacaoDocumento(Licitacao licitacao)
	    {
	        Licitacao = licitacao;
	    }

	    #endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties

	    public virtual Licitacao Licitacao { get; set; }
        public virtual byte[] Documento{ get; set; }
        public virtual string Nome { get; set; }
		
        #endregion

	    #region Advanced Properties

	 

	    #endregion
	    
	    #region Select
        
	    #endregion

       
	}
}
