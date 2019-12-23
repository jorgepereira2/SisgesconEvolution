using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class DelineamentoOrcamentoEntradaSingra : BusinessObject<DelineamentoOrcamentoEntradaSingra>	
	{
		#region Private Members
		private Servidor _servidor; 
		private DateTime _data; 
		private DelineamentoOrcamento _delineamentoorcamento; 		
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public DelineamentoOrcamentoEntradaSingra()
		{
			_servidor =  null; 
			_data = DateTime.MinValue; 
			_delineamentoorcamento =  null;
		    _itens = new CustomList<DelineamentoOrcamentoEntradaSingraItem>();
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties
			
		/// <summary>
		/// 
		/// </summary>		
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

	    private ICustomList<DelineamentoOrcamentoEntradaSingraItem> _itens;

        public virtual ICustomList<DelineamentoOrcamentoEntradaSingraItem> Itens
	    {
	        get { return _itens; }
	        set { _itens = value; }
	    }
		#endregion
		
		#region Public Methods

       
		
		#endregion
	}
}
