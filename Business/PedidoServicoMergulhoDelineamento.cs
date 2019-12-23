using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class PedidoServicoMergulhoDelineamento : BusinessObject<PedidoServicoMergulhoDelineamento>	
	{
		#region Private Members
		private PedidoServicoMergulho _pedidoServicoMergulho; 
		private Celula _celula; 
		private DateTime _data; 
		private string _descricaoservicooficina;
        private DateTime? _dataPrevisaoInicio;
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public PedidoServicoMergulhoDelineamento()
		{
            _dataPrevisaoInicio = null;
			_pedidoServicoMergulho =  null; 
			_celula =  null; 
			_data = DateTime.MinValue; 
			_descricaoservicooficina = null; 
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties

	    public virtual int TempoFainaDiaria { get; set; }
        public virtual int NumeroDias { get; set; }
        public virtual int QuantidadeMergulhadores { get; set; }

        public virtual DateTime? DataPrevisaoInicio
        {
            get { return _dataPrevisaoInicio; }
            set { _dataPrevisaoInicio = value; }
        }
        
		public virtual PedidoServicoMergulho PedidoServicoMergulho
		{
			get { return _pedidoServicoMergulho; }
			set { _pedidoServicoMergulho = value; }
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