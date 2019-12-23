using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class RotinaCategoriaServicoDelineamento : BusinessObject<RotinaCategoriaServicoDelineamento>	
	{
		#region Private Members
		
		private Celula _celula; 
		private int _homemhora; 
		private string _descricaoservicooficina;
        
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public RotinaCategoriaServicoDelineamento()
		{
			_celula =  null; 
			_homemhora = 0; 
			_descricaoservicooficina = null; 
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties

	    public virtual RotinaCategoriaServico RotinaCategoriaServico { get; set; }
			
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual Celula Celula
		{
			get { return _celula; }
			set { _celula = value; }
		}
			
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual int HomemHora
		{
			get { return _homemhora; }
			set { _homemhora = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual string DescricaoServicoOficina
		{
			get { return _descricaoservicooficina; }
			set	
			{
				if ( value != null )
					if( value.Length > 1000)
						throw new ArgumentOutOfRangeException("Invalid value for DescricaoServicoOficina", value, value.ToString());
				
				_descricaoservicooficina = value;
			}
		}
			#endregion 
		
		#region Public Methods
		
		
		#endregion
		
    }
}