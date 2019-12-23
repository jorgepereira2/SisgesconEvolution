using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class PedidoServicoAtividadeSecundariaItem : BusinessObject<PedidoServicoAtividadeSecundariaItem>
	{
		#region Private Members
		private PedidoServicoAtividadeSecundaria _pedidoServicoAtividadeSecundaria; 
		private decimal _valor; 
        
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		
		public PedidoServicoAtividadeSecundariaItem()
		{
		
		}
		#endregion 

		#region Public Properties

	    public virtual string DescricaoServico { get; set; }
        public virtual TipoAtividadeSecundaria TipoAtividadeSecundaria { get; set; }

		public virtual PedidoServicoAtividadeSecundaria PedidoServicoAtividadeSecundaria
		{
			get { return _pedidoServicoAtividadeSecundaria; }
			set { _pedidoServicoAtividadeSecundaria = value; }
		}
			
		public virtual decimal Valor
		{
			get { return _valor; }
			set { _valor = value; }
		}
	    #endregion 
		
	
		
		
		
	}
}
