using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class TipoPedidoObtencao : BusinessObject<TipoPedidoObtencao>	
	{
		#region Private Members
		private string _descricao; 
		private decimal? _valorlimitepo; 
		private decimal? _valorlimiteano; 		
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public TipoPedidoObtencao()
		{
			_descricao = null; 
			_valorlimitepo = null; 
			_valorlimiteano = null; 
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual string Descricao
		{
			get { return _descricao; }
			set	
			{
				if ( value != null )
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for Descricao", value, value.ToString());
				
				_descricao = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual decimal? ValorLimitePO
		{
			get { return _valorlimitepo; }
			set { _valorlimitepo = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual decimal? ValorLimiteAno
		{
			get { return _valorlimiteano; }
			set { _valorlimiteano = value; }
		}
		
	    public virtual TipoPedidoObtencaoEnum Enum
	    {
	        get{ return (TipoPedidoObtencaoEnum) ID;}
	    }
		#endregion 
		
		#region Public Methods
		
		public static Dictionary<int, string> List()
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
			@"select t.ID, t.Descricao 
			from TipoPedidoObtencao t  			
			order by t.Descricao");
		
			return BusinessHelper.ExecuteList(query); 
		}
		
		public static List<TipoPedidoObtencao> Select()
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
			@"from TipoPedidoObtencao t  			
			order by t.Descricao");
		
			return (List<TipoPedidoObtencao>)query.List<TipoPedidoObtencao>();
		}
		
		#endregion
		
		
		
	}
}
