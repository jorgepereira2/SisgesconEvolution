using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class TipoRequisicaoEstoque : BusinessObject<TipoRequisicaoEstoque>, IComparable<TipoRequisicaoEstoque>
	{
		#region Private Members
		private string _descricao; 
		private TipoMovimento _tipomovimento; 		
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public TipoRequisicaoEstoque()
		{
			_descricao = null; 
			_tipomovimento =  Business.TipoMovimento.Entrada; 
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
		public virtual TipoMovimento TipoMovimento
		{
			get { return _tipomovimento; }
			set { _tipomovimento = value; }
		}

        public virtual TipoRequisicaoEstoqueEnum TipoRequisicaoEstoqueEnum
        {
            get { return (Business.TipoRequisicaoEstoqueEnum)this.ID; }
        }
		#endregion 
		
		
		#region Public Methods
		
		public static Dictionary<int, string> List()
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
			@"select t.ID, t.Descricao 
			from TipoRequisicaoEstoque t  			
			order by t.Descricao");
		
			return BusinessHelper.ExecuteList(query); 
		}
		#endregion

        public virtual int CompareTo(TipoRequisicaoEstoque other)
	    {
	        return _descricao.CompareTo(other.Descricao);
	    }

        public override string ToString()
        {
            return _descricao;
        }
	}
}
