using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
    [Serializable]
    public partial class SubNaturezaDespesa : BusinessObject<SubNaturezaDespesa>, IDescricao, IComparable<SubNaturezaDespesa>
    {
        #region Private Members

        private NaturezaDespesa _NaturezaDespesa;
        private string _descricao;
        private bool _flagativo;

        #endregion

        #region Default ( Empty ) Class Constuctor
        
        /// <summary>
        /// default constructor
        /// </summary>
        public SubNaturezaDespesa()
        {
            _NaturezaDespesa = null;
            _descricao = null;
            _flagativo = false;
        }

        #endregion // End of Default ( Empty ) Class Constuctor

        #region Public Properties

        /// <summary>
        /// 
        /// </summary>		
        public virtual NaturezaDespesa NaturezaDespesa
        {
            get { return _NaturezaDespesa; }
            set { _NaturezaDespesa = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual string Descricao
        {
            get { return _descricao; }
            set
            {
                if (value != null)
                    if (value.Length > 50)
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

        public virtual bool Industrial { get; set; }

        #endregion

        #region Public Methods

        public static Dictionary<int, string> List(int id_NaturezaDespesa)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"select s.ID, s.Descricao 
			from SubNaturezaDespesa s  
			where s.FlagAtivo = 1
			and s.NaturezaDespesa.ID = IsNull(:id_NaturezaDespesa, s.NaturezaDespesa.ID)
			order by s.Descricao");

            query.SetParameter("id_NaturezaDespesa", BusinessHelper.IsNullOrZero(id_NaturezaDespesa), NHibernateUtil.Int32);
            return BusinessHelper.ExecuteList(query);
        }

        public static List<SubNaturezaDespesa> Select()
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"from SubNaturezaDespesa s inner join fetch s.NaturezaDespesa t 			
			order by s.Descricao");

            return (List<SubNaturezaDespesa>)query.List<SubNaturezaDespesa>();
        }

        #endregion

        public virtual int CompareTo(SubNaturezaDespesa other)
        {
            return _descricao.CompareTo(other.Descricao);
        }

        public override string ToString()
        {
            return _descricao.ToString();
        }
    }
}
