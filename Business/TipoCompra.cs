using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class TipoCompra : BusinessObject<TipoCompra>	
	{
		#region Private Members

		private string _descricao; 
		private bool _flagativo; 
		private decimal _limiteanual; 	
	
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public TipoCompra()
		{
			_descricao = null; 
			_flagativo = false; 
			_limiteanual = 0; 
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
		public virtual bool FlagAtivo
		{
			get { return _flagativo; }
			set { _flagativo = value; }
		}
		
		public virtual decimal LimiteAnual
		{
			get { return _limiteanual; }
			set { _limiteanual = value; }
		}

        public virtual TipoCompraEnum TipoCompraEnum
        {
            get { return (TipoCompraEnum)ID; }
        }

		#endregion 
		
		#region Public Methods
		
		public static Dictionary<int, string> List()
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
			@"select t.ID, t.Descricao 
			from TipoCompra t  
			where t.FlagAtivo = 1
			order by t.Descricao");
		
			return BusinessHelper.ExecuteList(query); 
		}
		
		public static List<TipoCompra> Select()
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
			@"from TipoCompra t  			
			order by t.Descricao");
		
			return (List<TipoCompra>)query.List<TipoCompra>();
		}
		
		#endregion
    }
}



