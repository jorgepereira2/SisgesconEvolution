using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
    public partial class TipoAtividadeSecundaria : BusinessObject<TipoAtividadeSecundaria>, IDescricao, IComparable<TipoAtividadeSecundaria>	
	{
		#region Private Members
		private string _descricao; 
		private bool _flagativo; 		
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public TipoAtividadeSecundaria()
		{
			_descricao = null; 
			_flagativo = false; 
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties

	    public virtual bool SemTaxas { get; set; }
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
			from TipoAtividadeSecundaria t  
			where t.FlagAtivo = 1
			order by t.Descricao");
		
			return BusinessHelper.ExecuteList(query); 
		}
		
		public static List<TipoAtividadeSecundaria> Select()
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
			@"from TipoAtividadeSecundaria t  			
			order by t.Descricao");
		
			return (List<TipoAtividadeSecundaria>)query.List<TipoAtividadeSecundaria>();
		}
		
		#endregion

        public virtual int CompareTo(TipoAtividadeSecundaria other)
	    {
	        return _descricao.CompareTo(other._descricao);
	    }

        public override string ToString()
        {
            return _descricao;
        }
	}
}
