using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
    [Serializable]
    public partial class Modalidade : BusinessObject<Modalidade>, IDescricao, IComparable<Modalidade>
    {
        #region Private Members

        private string _descricao;
        private bool _flagativo;
        //private decimal _limiteanual; 	

        #endregion

        #region Default ( Empty ) Class Constuctor

        /// <summary>
        /// default constructor
        /// </summary>
        public Modalidade()
        {
            _descricao = null;
            _flagativo = false;
            //_limiteanual = 0; 
        }

        #endregion // End of Default ( Empty ) Class Constuctor

        #region Public Properties

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

        public virtual bool FlagAtivo
        {
            get { return _flagativo; }
            set { _flagativo = value; }
        }

        //public virtual decimal LimiteAnual
        //{
        //    get { return _limiteanual; }
        //    set { _limiteanual = value; }
        //}

        //public virtual ModalidadeEnum ModalidadeEnum
        //{
        //    get { return (ModalidadeEnum)ID; }
        //}

        #endregion

        #region Public Methods

        public static Dictionary<int, string> List()
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"select m.ID, m.Descricao 
			from Modalidade m  
			where m.FlagAtivo = 1
			order by m.Descricao");

            return BusinessHelper.ExecuteList(query);
        }

        public static List<Modalidade> Select()
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"from Modalidade m 			
			order by m.Descricao");

            return (List<Modalidade>)query.List<Modalidade>();
        }

        #endregion

        public virtual int CompareTo(Modalidade other)
        {
            return _descricao.CompareTo(other._descricao);
        }
    }
}



