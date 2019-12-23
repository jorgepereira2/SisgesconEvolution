using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class ClasseServicoMaterial : BusinessObject<ClasseServicoMaterial>, IDescricao, IComparable<ClasseServicoMaterial>	
	{
		#region Private Members
		private string _descricao; 
		private bool _flagativo;
	    private string _codigo;
	    
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public ClasseServicoMaterial()
		{
			_descricao = null;
		    _codigo = null;
			_flagativo = false; 
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties

        public virtual string Codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        }
        	
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
			@"select c.ID, c.Codigo, c.Descricao 
			from ClasseServicoMaterial c  
			where c.FlagAtivo = 1			
			order by c.Codigo, c.Descricao");

		   	return BusinessHelper.ExecuteList(query, "{1} - {2}"); 
		}
		
		public static List<ClasseServicoMaterial> Select(string texto)
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
			@"from ClasseServicoMaterial c
            where (c.Codigo like :texto or c.Descricao like :texto)            
			order by c.Codigo");

		    query.SetString("texto", "%" + texto + "%");
		
			return (List<ClasseServicoMaterial>)query.List<ClasseServicoMaterial>();
		}
		
		#endregion

	    public virtual int CompareTo(ClasseServicoMaterial other)
	    {
	        return _descricao.CompareTo(other.Descricao);
	    }

        public override string ToString()
        {
            return string.Format("{0} - {1}", _codigo, _descricao);
        }
	}
}
