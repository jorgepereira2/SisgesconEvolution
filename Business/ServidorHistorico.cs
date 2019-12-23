using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class ServidorHistorico : BusinessObject<ServidorHistorico>	
	{
		#region Private Members
		private Servidor _servidor; 
		private DateTime _data; 
		private Celula _celula; 
		private bool _flagFazAtividadeDireta;
        
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
        public ServidorHistorico()
		{
			
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties
			
	
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
			
		public virtual Celula Celula
		{
			get { return _celula; }
			set { _celula = value; }
		}
	
        public virtual bool FlagFazAtividadeDireta
        {
            get { return _flagFazAtividadeDireta; }
            set { _flagFazAtividadeDireta = value; }
        }
		#endregion 
		
		#region Public Methods
		
		
		#endregion
		
    }
}