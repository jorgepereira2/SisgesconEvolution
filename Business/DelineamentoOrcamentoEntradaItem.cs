using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class DelineamentoOrcamentoEntradaItem : BusinessObject<DelineamentoOrcamentoEntradaItem>	
	{
		#region Private Members
		private DelineamentoOrcamentoEntrada _delineamentoorcamentoentradasingra; 
		private ServicoMaterial _material; 
		private int _quantidade; 		
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public DelineamentoOrcamentoEntradaItem()
		{
			_delineamentoorcamentoentradasingra =  null; 
			_material =  null; 
			_quantidade = 0; 
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual DelineamentoOrcamentoEntrada DelineamentoOrcamentoEntrada
		{
			get { return _delineamentoorcamentoentradasingra; }
			set { _delineamentoorcamentoentradasingra = value; }
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
		public virtual int Quantidade
		{
			get { return _quantidade; }
			set { _quantidade = value; }
		}
			#endregion 
		
		#region Public Methods
		
		
		#endregion
	}
}
