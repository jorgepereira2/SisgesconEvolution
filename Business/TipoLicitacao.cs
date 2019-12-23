using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
    public partial class TipoLicitacao : BusinessObject<TipoLicitacao>, IDescricao, IComparable<TipoLicitacao>	
	{
		#region Private Members
		private string _descricao; 
		private bool _flagativo; 		
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public TipoLicitacao()
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
			@"select t.ID, t.Descricao 
			from TipoLicitacao t  
			where t.FlagAtivo = 1
			order by t.Descricao");
		
			return BusinessHelper.ExecuteList(query); 
		}
		
		public static List<TipoLicitacao> Select()
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
			@"from TipoLicitacao t  			
			order by t.Descricao");
		
			return (List<TipoLicitacao>)query.List<TipoLicitacao>();
		}
		
		#endregion

        public virtual int CompareTo(TipoLicitacao other)
	    {
	        return _descricao.CompareTo(other._descricao);
	    }

        public override string ToString()
        {
            return _descricao;
        }
	}
}
