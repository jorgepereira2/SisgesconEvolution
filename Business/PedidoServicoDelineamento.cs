using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class PedidoServicoDelineamento : BusinessObject<PedidoServicoDelineamento>	
	{
		#region Private Members
		private DelineamentoOrcamento _delineamentoOrcamento; 
		private Celula _celula; 
		private DateTime _data; 
		private int _homemhora; 
		private string _descricaoservicooficina;
        private DateTime? _dataPrevisaoInicio;
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public PedidoServicoDelineamento()
		{
            _dataPrevisaoInicio = null;
			_delineamentoOrcamento =  null; 
			_celula =  null; 
			_data = DateTime.MinValue; 
			_homemhora = 0; 
			_descricaoservicooficina = null; 
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties

        public virtual Servidor ServidorDelineamento { get; set; }

        public virtual DateTime? DataPrevisaoInicio
        {
            get { return _dataPrevisaoInicio; }
            set { _dataPrevisaoInicio = value; }
        }
        
		public virtual DelineamentoOrcamento DelineamentoOrcamento
		{
			get { return _delineamentoOrcamento; }
			set { _delineamentoOrcamento = value; }
		}
			
			
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
		public virtual DateTime Data
		{
			get { return _data; }
			set { _data = value; }
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