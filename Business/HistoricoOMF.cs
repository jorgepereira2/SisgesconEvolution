using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class HistoricoOMF : BusinessObject<HistoricoOMF>	
	{
		#region Private Members
		private Servidor _servidor; 
		private string _descricao; 
		private DateTime _data; 
		private NotaEntregaMaterialOMF _omf;
	    private string _justificativa;
	    private bool _flagReprovado;
	    
	    #endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public HistoricoOMF()
		{
			_servidor =  null; 
			_descricao = null; 
			_data = DateTime.MinValue; 
			_omf =  null;
		    _flagReprovado = false;
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties

        public virtual bool FlagReprovado
        {
            get { return _flagReprovado; }
            set { _flagReprovado = value; }
        }

        public virtual string Justificativa
        {
            get { return _justificativa; }
            set { _justificativa = value; }
        }
        	
		public virtual Servidor Servidor
		{
			get { return _servidor; }
			set { _servidor = value; }
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
		public virtual NotaEntregaMaterialOMF OMF
		{
			get { return _omf; }
			set { _omf = value; }
		}
			#endregion 
		
	
	}
}
