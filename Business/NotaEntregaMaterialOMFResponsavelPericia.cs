using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class NotaEntregaMaterialOMFResponsavelPericia : BusinessObject<NotaEntregaMaterialOMFResponsavelPericia>	
	{
		#region Private Members
		private string _nome; 
		private string _nip; 
		private string _graduacao; 
		private TipoNotificacao _tiponotificacao; 
		private string _observacao;
	    private NotaEntregaMaterialOMF _omf;
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public NotaEntregaMaterialOMFResponsavelPericia()
		{
			_nome = null; 
			_nip = null; 
			_graduacao = null; 
			_tiponotificacao =  null; 
			_observacao = null; 
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties

        public virtual NotaEntregaMaterialOMF OMF
        {
            get { return _omf; }
            set { _omf = value; }
        }

		public virtual string Nome
		{
			get { return _nome; }
			set	
			{
				if ( value != null )
					if( value.Length > 100)
						throw new ArgumentOutOfRangeException("Invalid value for Nome", value, value.ToString());
				
				_nome = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual string NIP
		{
			get { return _nip; }
			set	
			{
				if ( value != null )
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for NIP", value, value.ToString());
				
				_nip = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual string Graduacao
		{
			get { return _graduacao; }
			set	
			{
				if ( value != null )
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for Graduacao", value, value.ToString());
				
				_graduacao = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual TipoNotificacao TipoNotificacao
		{
			get { return _tiponotificacao; }
			set { _tiponotificacao = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual string Observacao
		{
			get { return _observacao; }
			set	
			{
				if ( value != null )
					if( value.Length > 500)
						throw new ArgumentOutOfRangeException("Invalid value for Observacao", value, value.ToString());
				
				_observacao = value;
			}
		}
			#endregion 
		
		
		#region Public Methods
		
		public static Dictionary<int, string> List()
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
			@"select n.ID, n.Descricao 
			from NotaEntregaMaterialOMFResponsavelPericia n  
			where n.FlagAtivo = 1
			order by n.Descricao");
		
			return BusinessHelper.ExecuteList(query); 
		}
		
		public static List<NotaEntregaMaterialOMFResponsavelPericia> Select()
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
			@"from NotaEntregaMaterialOMFResponsavelPericia n  			
			order by n.Descricao");
		
			return (List<NotaEntregaMaterialOMFResponsavelPericia>)query.List<NotaEntregaMaterialOMFResponsavelPericia>();
		}
		
		#endregion
		
		
		
		
	}
}
