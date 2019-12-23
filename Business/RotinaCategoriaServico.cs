using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class RotinaCategoriaServico : BusinessObject<RotinaCategoriaServico>
	{
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
        public RotinaCategoriaServico()
		{
			ItensDelineamento = new Shared.NHibernateDAL.CustomList<RotinaCategoriaServicoDelineamento>();
            ItensOrcamento = new Shared.NHibernateDAL.CustomList<RotinaCategoriaServicoItemOrcamento>();
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties

	    public virtual Rotina Rotina { get; set; }
        public virtual CategoriaServico CategoriaServico { get; set; }

        public virtual ICustomList<RotinaCategoriaServicoItemOrcamento> ItensOrcamento { get; set; }
        public virtual ICustomList<RotinaCategoriaServicoDelineamento> ItensDelineamento { get; set; }

       
		#endregion 
			
        #region Advanced Properties
	    
	    #endregion

        #region Public Methods

       

        #endregion

	   
	}
}
