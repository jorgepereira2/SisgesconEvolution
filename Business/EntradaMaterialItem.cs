using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class EntradaMaterialItem : BusinessObject<EntradaMaterialItem>	
	{
		#region Private Members
		private EntradaMaterial _entradaMaterial; 
		private ServicoMaterial _material; 
		private decimal _quantidade; 		
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public EntradaMaterialItem()
		{
			_entradaMaterial =  null; 
			_material =  null; 
			_quantidade = 0; 
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual EntradaMaterial EntradaMaterial
		{
			get { return _entradaMaterial; }
			set { _entradaMaterial = value; }
		}
			
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
		public virtual decimal Quantidade
		{
			get { return _quantidade; }
			set { _quantidade = value; }
		}
			#endregion 
		
		#region Public Methods
		public virtual IItemServicoMaterial GetItemObtencao()
		{
		    foreach (PedidoObtencaoItem item in EntradaMaterial.PedidoObtencao.Itens)
		    {
		        if(item.ServicoMaterial.ID == this.Material.ID)
		            return item;
		    }

		    foreach (PedidoObtencaoItemPEPRodizio itemPEPRodizio in EntradaMaterial.PedidoObtencao.ItensPEPRodizio)
		    {
                if (itemPEPRodizio.ServicoMaterial.ID == this.Material.ID)
                    return itemPEPRodizio;
		    }
            return new PedidoObtencaoItem();
		}
		
		#endregion
	}
}
