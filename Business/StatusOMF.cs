using System;
using System.Collections;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class StatusOMF : BusinessObject<StatusOMF>, IComparable<StatusOMF>	
	{
		#region Private Members
		private string _descricao;
	    
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public StatusOMF()
		{
			_descricao = null;
		    _responsaveis = new CustomList<Servidor>();
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties
      
		public virtual string Descricao
		{
			get { return _descricao; }
			set	
			{_descricao = value;}
		}
			#endregion

	    #region Collections

	    private ICustomList<Servidor> _responsaveis;
        
        public virtual ICustomList<Servidor> Responsaveis
	    {
	        get { return _responsaveis; }
	        set { _responsaveis = value; }
	    }

	    public virtual StatusOMFEnum StatusOMFEnum
	    {
	        get { return (Business.StatusOMFEnum) this.ID;}
	    }

	    #endregion
		
		#region Public Methods

        public static List<StatusOMF> Select()
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"from StatusOMF s  			
			order by s.ID");

            return (List<StatusOMF>)query.List<StatusOMF>();
        }

        public static Dictionary<int, string> List()
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"select s.ID, s.Descricao 
			from StatusOMF s  			
			order by s.Descricao");

            return BusinessHelper.ExecuteList(query);
        }

        private static List<StatusOMFEnum> _listaStatus;

        private static List<StatusOMFEnum> GetListaStatus()
        {
            if (_listaStatus == null)
            {
                ISession session = NHibernateSessionManager.Instance.GetSession();
                IQuery query = session.CreateQuery(
                @"select s.ID
			    from StatusOMF s  			
			    order by s.ID");
                _listaStatus = new List<StatusOMFEnum>();
                IList list = query.List();
                for(int i = 0; i < list.Count; i++)
                {
                    _listaStatus.Add((Business.StatusOMFEnum)Convert.ToInt32(list[i]));
                }
            }
            return _listaStatus;
        }

        public static StatusOMFEnum GetAnterior(StatusOMFEnum statusAtual)
        {
            
            int index = GetListaStatus().FindIndex(new Predicate<StatusOMFEnum>(delegate(StatusOMFEnum match)
            {
                return match == statusAtual;
            }));
            return GetListaStatus()[index - 1];
        }
       
        
        /// <summary>
        /// Retorna o item seguinte do enum
        /// </summary>
	    public static StatusOMFEnum GetNext(StatusOMFEnum statusAtual)
	    {
	        int index = GetListaStatus().FindIndex(new Predicate<StatusOMFEnum>(delegate(StatusOMFEnum match)
            {
                return match == statusAtual;
            }));
            
           return GetListaStatus()[index + 1];
	    }
		#endregion

	    public static StatusOMF Get(StatusOMFEnum status)
	    {
	        return StatusOMF.Get(Convert.ToInt32(status));
	    }

        public virtual int CompareTo(StatusOMF other)
	    {
	        return Descricao.CompareTo(other.Descricao);
	    }

        public override string ToString()
        {
            return Descricao;
        }
	}
}
