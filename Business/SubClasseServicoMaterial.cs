using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class SubClasseServicoMaterial : BusinessObject<SubClasseServicoMaterial>, IDescricao, IComparable<SubClasseServicoMaterial>	
	{
		#region Private Members
		private string _descricao; 
		private ClasseServicoMaterial _classeservicomaterial; 
		private bool _flagativo;
	    private string _codigo;
	    private string _especificacao;

	    #endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public SubClasseServicoMaterial()
		{
			_descricao = null;
		    _codigo = null;
			_classeservicomaterial =  null; 
			_flagativo = false; 
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties
        public virtual string Especificacao
        {
            get { return _especificacao; }
            set { _especificacao = value; }
        }

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
		public virtual ClasseServicoMaterial ClasseServicoMaterial
		{
			get { return _classeservicomaterial; }
			set { _classeservicomaterial = value; }
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

        public static Dictionary<int, string> List(int id_classeServicoMaterial)
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
            @"select s.ID, s.Codigo, s.Descricao 
			from SubClasseServicoMaterial s  			
			where s.FlagAtivo = 1
			and s.ClasseServicoMaterial.ID = IsNull(:id_classe, s.ClasseServicoMaterial.ID)  			
			order by s.Descricao");

            query.SetInt32("id_classe", id_classeServicoMaterial);
			return BusinessHelper.ExecuteList(query, "{1} - {2}"); 
		}
		
		public static List<SubClasseServicoMaterial> Select(int id_classeServicoMaterial, string texto)
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
            @"from SubClasseServicoMaterial s inner join fetch s.ClasseServicoMaterial c
            where (s.Codigo like :texto or s.Descricao like :texto)
            and c.ID = IsNull(:id_classeServicoMaterial, c.ID)
			order by s.Codigo");

            query.SetString("texto", "%" + texto + "%");
            query.SetParameter("id_classeServicoMaterial", BusinessHelper.IsNullOrZero(id_classeServicoMaterial), NHibernateUtil.Int32);
			return (List<SubClasseServicoMaterial>)query.List<SubClasseServicoMaterial>();
		}
		
		#endregion

        public virtual int CompareTo(SubClasseServicoMaterial other)
	    {
	        return _descricao.CompareTo(other.Descricao);
	    }

        public override string ToString()
        {
            return string.Format("{0} - {1}", _codigo, _descricao);
        }
	}
}
