using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class PedidoServicoEquipamento : BusinessObject<PedidoServicoEquipamento>
	{
		#region Private Members
		
	    
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
        public PedidoServicoEquipamento()
		{
		   
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties

        public virtual PedidoServico PedidoServico { get; set; }
        public virtual Equipamento Equipamento { get; set; }
        public virtual int Quantidade { get; set; }
        public virtual string DefeitoReclamado { get; set; }
        public virtual string NumeroSerie { get; set; }

	    #endregion 
		
		#region Public Methods
		
		
		
		#endregion
	}
}