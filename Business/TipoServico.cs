using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
    public partial class TipoServico : BusinessObject<TipoServico>, IDescricao, IComparable<TipoServico>	
	{
		#region Private Members
		private string _descricao; 
		private bool _flagativo; 		
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public TipoServico()
		{
			_descricao = null; 
			_flagativo = false; 
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
			#endregion 
		
		
		#region Public Methods
		
		public static Dictionary<int, string> List()
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
			@"select a.ID, a.Descricao 
			from TipoServico a  
			where a.FlagAtivo = 1
			order by a.Descricao");
		
			return BusinessHelper.ExecuteList(query); 
		}
		
		public static List<TipoServico> Select()
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
			@"from TipoServico a  			
			order by a.Descricao");
		
			return (List<TipoServico>)query.List<TipoServico>();
		}
		
		#endregion

	    public virtual int CompareTo(TipoServico other)
	    {
	        return Descricao.CompareTo(other.Descricao);
	    }

        public override string ToString()
        {
            return Descricao;
        }
	}
}
