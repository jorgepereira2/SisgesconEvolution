using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class RotinaCategoriaServicoItemOrcamento : BusinessObject<RotinaCategoriaServicoItemOrcamento>
	{
		#region Private Members
		
		private ServicoMaterial _servicomaterial;
        private decimal _quantidade; 
		private OrigemMaterial _origemMaterial;
	    private Celula _celula;
	    private string _observacao;
	    
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
        public RotinaCategoriaServicoItemOrcamento()
		{
		    _celula = null;
			_servicomaterial =  null; 
			_quantidade = 0; 
			_origemMaterial =  OrigemMaterial.Obtencao;
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties

        //Observacao para lancar quando for item de servico
        public virtual string Observacao
        {
            get { return _observacao; }
            set { _observacao = value; }
        }
        
        
        public virtual Celula Celula
        {
            get { return _celula; }
            set { _celula = value; }
        }

        public virtual RotinaCategoriaServico RotinaCategoriaServico { get; set; }
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual ServicoMaterial ServicoMaterial
		{
			get { return _servicomaterial; }
			set { _servicomaterial = value; }
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
			get { return _origemMaterial; }
			set { _origemMaterial = value; }
		}

	    #endregion 
		
		#region Public Methods
		
	
		
		#endregion
		
		
		
	}
}
