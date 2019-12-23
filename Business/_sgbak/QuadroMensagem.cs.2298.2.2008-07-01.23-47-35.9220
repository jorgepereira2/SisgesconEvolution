using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class QuadroMensagem : BusinessObject<QuadroMensagem>	
	{
		#region Private Members
		private string _mensagem; 
		private DateTime _datainicio; 
		private DateTime _datafim; 
		private Servidor _servidor; 
		private string _assinatura; 
		private string _titulo; 		
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public QuadroMensagem()
		{
			_mensagem = null; 
			_datainicio = DateTime.MinValue; 
			_datafim = DateTime.MinValue; 
			_servidor =  null; 
			_assinatura = null; 
			_titulo = null; 
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual string Mensagem
		{
			get { return _mensagem; }
			set	
			{
				if ( value != null )
					if( value.Length > 7000)
						throw new ArgumentOutOfRangeException("Invalid value for Mensagem", value, value.ToString());
				
				_mensagem = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual DateTime DataInicio
		{
			get { return _datainicio; }
			set { _datainicio = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual DateTime DataFim
		{
			get { return _datafim; }
			set { _datafim = value; }
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
		public virtual string Assinatura
		{
			get { return _assinatura; }
			set	
			{
				if ( value != null )
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for Assinatura", value, value.ToString());
				
				_assinatura = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual string Titulo
		{
			get { return _titulo; }
			set	
			{
				if ( value != null )
					if( value.Length > 40)
						throw new ArgumentOutOfRangeException("Invalid value for Titulo", value, value.ToString());
				
				_titulo = value;
			}
		}
			#endregion 
		
		#region Public Methods
		
		public static List<QuadroMensagem> Select(string titulo)
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
			@"from QuadroMensagem q
			where q.Titulo like :titulo  			
			order by q.DataInicio DESC");

		    query.SetString("titulo", string.Format("%{0}%", titulo));
			return (List<QuadroMensagem>)query.List<QuadroMensagem>();
		}

        public static List<QuadroMensagem> SelectAtivos()
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"from QuadroMensagem q
			where dbo.DateIsInBetween(:data, q.DataInicio, q.DataFim) = 1
			order by q.DataInicio DESC");

            query.SetDateTime("data", DateTime.Today);
            return (List<QuadroMensagem>)query.List<QuadroMensagem>();
        }
		
		#endregion
		
    }
}