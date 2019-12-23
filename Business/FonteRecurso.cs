using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class FonteRecurso : BusinessObject<FonteRecurso>, IDescricao, IComparable<FonteRecurso>	
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
		public FonteRecurso()
		{
			_descricao = null; 
			_codigo = null; 
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
		public virtual string Codigo
		{
			get { return _codigo; }
			set	
			{
				if ( value != null )
					if( value.Length > 20)
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
			@"select f.ID, f.Descricao 
			from FonteRecurso f  
			where f.FlagAtivo = 1
			order by f.Descricao");
		
			return BusinessHelper.ExecuteList(query); 
		}
		
		public static List<FonteRecurso> Select()
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
			@"from FonteRecurso f  			
			order by f.Descricao");
		
			return (List<FonteRecurso>)query.List<FonteRecurso>();
		}
		
		#endregion

        public override string ToString()
        {
            return _descricao;
        }

        public virtual int CompareTo(FonteRecurso other)
        {
            if (other == null) return -1;
            return _descricao.CompareTo(other._descricao);
        }
	}
}
