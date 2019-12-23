using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class PedidoAquisicaoItem : BusinessObject<PedidoAquisicaoItem>, IPedidoAquisicaoItem	
	{
		#region Private Members
		private PedidoAquisicao _pedidoaquisicao; 
		private ServicoMaterial _servicomaterial; 
		private string _descricao; 
		private int _quantidade; 
		private decimal _valor; 
		private decimal? _valor2; 
		private decimal? _valor3; 
		private decimal? _valor4; 		
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public PedidoAquisicaoItem()
		{
			_pedidoaquisicao =  null; 
			_servicomaterial =  null; 
			_descricao = null; 
			_quantidade = 0; 
			_valor = 0; 
			_valor2 = null; 
			_valor3 = null; 
			_valor4 = null; 
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
		public virtual ServicoMaterial ServicoMaterial
		{
			get { return _servicomaterial; }
			set { _servicomaterial = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual string Descricao
		{
			get { return _descricao; }
			set	
			{
				if ( value != null )
					if( value.Length > 300)
						throw new ArgumentOutOfRangeException("Invalid value for Descricao", value, value.ToString());
				
				_descricao = value;
			}
		}
	
	    public virtual string DescricaoItem
	    {
	        get
	        {
	            Parametro parametro = Parametro.Get();
	            if(parametro.EntradaItemCompraManual)
	                return _descricao;
	            else
	                return _servicomaterial.DescricaoCompleta;
	        }
	    }

	    /// <summary>
		/// 
		/// </summary>		
		public virtual int Quantidade
		{
			get { return _quantidade; }
			set { _quantidade = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual decimal Valor
		{
			get { return _valor; }
			set { _valor = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual decimal? Valor2
		{
			get { return _valor2; }
			set { _valor2 = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual decimal? Valor3
		{
			get { return _valor3; }
			set { _valor3 = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual decimal? Valor4
		{
			get { return _valor4; }
			set { _valor4 = value; }
		}
			#endregion 
			
		#region Advanced Properties
	    public virtual decimal ValorTotal
	    {
	        get
	        {
	            return _valor * _quantidade;
	        }
	    }
	    #endregion

        public override void Save()
        {
            Validar();
            base.Save();
        }

        private void Validar()
        {
            if ((Valor2.HasValue && Valor2 < Valor) ||
              (Valor3.HasValue && Valor3 < Valor) ||
              (Valor4.HasValue && Valor4 < Valor))
                throw new Exception("O Valor principal deve ser inferior aos outros valores cotados.");
        }
	}
}
