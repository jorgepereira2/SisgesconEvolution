using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class HistoricoPedidoAquisicao : BusinessObject<HistoricoPedidoAquisicao>	
	{
		#region Private Members
		private PedidoAquisicao _pedidoaquisicao; 
		private Servidor _servidor; 
		private DateTime _data; 
		private StatusPedidoAquisicao _statusanterior; 
		private StatusPedidoAquisicao _statusposterior; 
		private string _justificativarecusa;
        
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public HistoricoPedidoAquisicao()
		{
			_pedidoaquisicao =  null; 
			_servidor =  null; 
			_data = DateTime.MinValue; 
			_statusanterior =  null; 
			_statusposterior =  null; 
			_justificativarecusa = null;
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual PedidoAquisicao PedidoAquisicao
		{
			get { return _pedidoaquisicao; }
			set { _pedidoaquisicao = value; }
		}
			
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
		public virtual StatusPedidoAquisicao StatusAnterior
		{
			get { return _statusanterior; }
			set { _statusanterior = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual StatusPedidoAquisicao StatusPosterior
		{
			get { return _statusposterior; }
			set { _statusposterior = value; }
		}

	    public virtual bool FlagRecusado { get; set; }
			
		public virtual string Justificativa
		{
			get { return _justificativarecusa; }
			set	
			{
				if ( value != null )
					if( value.Length > 500)
						throw new ArgumentOutOfRangeException("Invalid value for Justificativa", value, value.ToString());
				
				_justificativarecusa = value;
			}
		}
		#endregion 
		
    }
}