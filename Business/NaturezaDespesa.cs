using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class NaturezaDespesa : BusinessObject<NaturezaDespesa>, IDescricao, IComparable<NaturezaDespesa>	
	{
		#region Private Members
		private string _descricao; 
		private string _codigo; 
		private bool _flagativo; 		
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public NaturezaDespesa()
		{
			_descricao = null; 
			_codigo = null; 
			_flagativo = false; 
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties

	    public virtual TipoNaturezaDespesa TipoNaturezaDespesa { get; set; }
        public virtual string Observacao { get; set; }
		
		public virtual string Descricao
		{
			get { return _descricao; }
			set	
			{	
				_descricao = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual string Codigo
		{
			get { return _codigo; }
			set	
			{
				if ( value != null )
					if( value.Length > 15)
						throw new ArgumentOutOfRangeException("Invalid value for Codigo", value, value.ToString());
				
				_codigo = value;
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
            @"select n.ID, n.Codigo, n.Descricao 
			from NaturezaDespesa n  
			where n.FlagAtivo = 1
            and dbo.GetRight(n.Codigo, 2) != '00'
			order by n.Codigo, n.Descricao");
		
			return BusinessHelper.ExecuteList(query, "{1} - {2}"); 
		}

        public static Dictionary<int, string> ListAll()
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"select n.ID, n.Codigo, n.Descricao 
			from NaturezaDespesa n  
			where n.FlagAtivo = 1
			order by n.Codigo, n.Descricao");

            return BusinessHelper.ExecuteList(query, "{1} - {2}");
        }

        public static Dictionary<int, string> ListPai()
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"select n.ID, n.Codigo, n.Descricao 
			from NaturezaDespesa n  
			where n.FlagAtivo = 1
            and dbo.GetRight(n.Codigo, 2) = '00'
			order by n.Codigo, n.Descricao");

            return BusinessHelper.ExecuteList(query, "{1} - {2}");
        }
		
		public static List<NaturezaDespesa> Select()
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
			@"from NaturezaDespesa n  			
			order by n.Descricao");
		
			return (List<NaturezaDespesa>)query.List<NaturezaDespesa>();
		}
		
		#endregion

        public override string ToString()
        {
            return _descricao;
        }

        public virtual int CompareTo(NaturezaDespesa other)
	    {
	        if(other == null) return -1;
	        return _descricao.CompareTo(other._descricao);
	    }

	    public virtual NaturezaDespesa GetPai()
	    {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"from NaturezaDespesa n  			
            where n.Codigo = :codigo");
	        query.SetString("codigo", _codigo.Substring(0, _codigo.Length - 2) + "00");

            return query.UniqueResult<NaturezaDespesa>();
	    }
	}
}
