using System;
using System.Collections.Generic;
using System.Data;
using NHibernate;
using Shared.Common;
using Shared.DataAccessHelper;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class EntradaMaterial : BusinessObject<EntradaMaterial>	
	{
		#region Private Members
		private Servidor _servidor; 
		private DateTime _data; 
		private DelineamentoOrcamento _delineamentoorcamento;
	    private PedidoObtencao _pedidoObtencao;
	    private OrigemMaterial _origemMaterial;
	    private AutorizacaoCompra _autorizacaoCompra;
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public EntradaMaterial()
		{
		    _autorizacaoCompra = null;
			_servidor =  null; 
			_data = DateTime.MinValue; 
			_delineamentoorcamento =  null;
		    _pedidoObtencao = null;
		    _origemMaterial = Business.OrigemMaterial.PEP;
		    _itens = new CustomList<EntradaMaterialItem>();
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties
        public virtual AutorizacaoCompra AutorizacaoCompra
        {
            get { return _autorizacaoCompra; }
            set { _autorizacaoCompra = value; }
        }
        
        public virtual OrigemMaterial OrigemMaterial
        {
            get { return _origemMaterial; }
            set { _origemMaterial = value; }
        }
        
        public virtual PedidoObtencao PedidoObtencao
        {
            get { return _pedidoObtencao; }
            set { _pedidoObtencao = value; }
        }
        
		public virtual Servidor Servidor
		{
			get { return _servidor; }
			set { _servidor = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual DateTime Data
		{
			get { return _data; }
			set { _data = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual DelineamentoOrcamento DelineamentoOrcamento
		{
			get { return _delineamentoorcamento; }
			set { _delineamentoorcamento = value; }
		}
			#endregion 
		
		#region Collections

	    private ICustomList<EntradaMaterialItem> _itens;

        public virtual ICustomList<EntradaMaterialItem> Itens
	    {
	        get { return _itens; }
	        set { _itens = value; }
	    }
		#endregion
		
		#region Public Methods
    
		
		#endregion
	}
}
