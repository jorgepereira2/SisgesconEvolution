using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class RequisicaoEstoqueItem : BusinessObject<RequisicaoEstoqueItem>	
	{
		#region Private Members
		private RequisicaoEstoque _requisicaoestoque; 
		private ServicoMaterial _material;
        private decimal _quantidade; 
		private OrigemMaterial _origemmaterial;
        private OrigemMaterial? _origemMaterialDestino;
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public RequisicaoEstoqueItem()
		{
			_requisicaoestoque =  null; 
			_material =  null; 
			_quantidade = 0; 
			_origemmaterial = OrigemMaterial.Obtencao; 
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual RequisicaoEstoque RequisicaoEstoque
		{
			get { return _requisicaoestoque; }
			set { _requisicaoestoque = value; }
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
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual OrigemMaterial OrigemMaterial
		{
			get { return _origemmaterial; }
			set { _origemmaterial = value; }
		}

	    
        //Para casos de transferencia
	    public virtual OrigemMaterial? OrigemMaterialDestino
	    {
	        get { return _origemMaterialDestino; }
	        set { _origemMaterialDestino = value; }
	    }

	    #endregion 
		
	}
}
