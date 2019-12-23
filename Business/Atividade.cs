using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class Atividade : BusinessObject<Atividade>, IDescricao, IComparable<Atividade>	
	{
		#region Private Members
		private string _descricao; 
		private bool _flagativo; 
		private bool _flagatividadedireta;
	    private Apropriacao _apropriacao;
	    
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public Atividade()
		{
			_descricao = null; 
			_flagativo = false; 
			_flagatividadedireta = false;
		    _apropriacao = null;
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties

        public virtual Apropriacao Apropriacao
        {
            get { return _apropriacao; }
            set { _apropriacao = value; }
        }
        
		public virtual string Descricao
		{
			get { return _descricao; }
			set	
			{
				if ( value != null )
					if( value.Length > 100)
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
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual bool FlagAtividadeDireta
		{
			get { return _flagatividadedireta; }
			set { _flagatividadedireta = value; }
		}
			#endregion 
		
		#region Public Methods
		
		public static Dictionary<int, string> List()
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
			@"select a.ID, a.Descricao 
			from Atividade a  
			where a.FlagAtivo = 1
			order by a.Descricao");
		
			return BusinessHelper.ExecuteList(query); 
		}
		
		public static List<Atividade> Select()
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
			@"from Atividade a 
			order by a.Descricao");
		
			return (List<Atividade>)query.List<Atividade>();
		}
		#endregion

	    public virtual int CompareTo(Atividade other)
	    {
	        return Descricao.CompareTo(other.Descricao);
	    }
        public override string ToString()
        {
            return _descricao;
        }
	}
}
