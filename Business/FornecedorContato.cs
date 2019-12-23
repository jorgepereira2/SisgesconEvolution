using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class FornecedorContato : BusinessObject<FornecedorContato>	
	{
		#region Private Members
		
		private Fornecedor _fornecedor; 
		private string _nome; 
		private string _setor; 
		private string _telefone; 
		private string _celular; 
		private string _email; 		
		
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public FornecedorContato()
		{
			_fornecedor =  null; 
			_nome = null; 
			_setor = null; 
			_telefone = null; 
			_celular = null; 
			_email = null; 
		}

	    public FornecedorContato(Fornecedor fornecedor)
	    {
	        _fornecedor = fornecedor;
	    }

	    #endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual Fornecedor Fornecedor
		{
			get { return _fornecedor; }
			set { _fornecedor = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual string Nome
		{
			get { return _nome; }
			set	
			{
				if ( value != null )
					if( value.Length > 80)
						throw new ArgumentOutOfRangeException("Invalid value for Nome", value, value.ToString());
				
				_nome = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual string Setor
		{
			get { return _setor; }
			set	
			{
				if ( value != null )
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for Setor", value, value.ToString());
				
				_setor = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual string Telefone
		{
			get { return _telefone; }
			set	
			{
				if ( value != null )
					if( value.Length > 25)
						throw new ArgumentOutOfRangeException("Invalid value for Telefone", value, value.ToString());
				
				_telefone = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual string Celular
		{
			get { return _celular; }
			set	
			{
				if ( value != null )
					if( value.Length > 25)
						throw new ArgumentOutOfRangeException("Invalid value for Celular", value, value.ToString());
				
				_celular = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual string Email
		{
			get { return _email; }
			set	
			{
				if ( value != null )
					if( value.Length > 80)
						throw new ArgumentOutOfRangeException("Invalid value for Email", value, value.ToString());
				
				_email = value;
			}
		}
		#endregion 
		
        public override void Save()
        {
            bool isNew = !IsPersisted;
            
            base.Save();
            
            if(isNew)
                _fornecedor.Contatos.Add(this);
            
        }
	}
}
