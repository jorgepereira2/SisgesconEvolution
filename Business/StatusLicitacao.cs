using System;
using System.Collections;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class StatusLicitacao : BusinessObject<StatusLicitacao>, IComparable<StatusLicitacao>	
	{
		#region Private Members

		private string _descricao;
        private bool _flagAtivo;
	    
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public StatusLicitacao()
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

		public virtual bool FlagAtivo
		{
            get { return _flagAtivo; }
			set	{ _flagAtivo = value;}
		}
		
        #endregion

	    #region Collections

	    private ICustomList<Servidor> _responsaveis;
        
        public virtual ICustomList<Servidor> Responsaveis
	    {
	        get { return _responsaveis; }
	        set { _responsaveis = value; }
	    }

	    public virtual StatusLicitacaoEnum StatusLicitacaoEnum
	    {
	        get { return (Business.StatusLicitacaoEnum) this.ID;}
	    }

	    #endregion
		
		#region Public Methods

        public static List<StatusLicitacao> Select()
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"from StatusLicitacao s  
            WHERE s.FlagAtivo = 1
			order by s.ID");

            return (List<StatusLicitacao>)query.List<StatusLicitacao>();
        }

        public static Dictionary<int, string> List()
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"select s.ID, s.Descricao 
			from StatusLicitacao s  			
            WHERE s.FlagAtivo = 1
			order by s.Descricao");

            return BusinessHelper.ExecuteList(query);
        }

        private static List<StatusLicitacaoEnum> _listaStatus;

        private static List<StatusLicitacaoEnum> GetListaStatus()
        {
            if (_listaStatus == null)
            {
                ISession session = NHibernateSessionManager.Instance.GetSession();
                IQuery query = session.CreateQuery(
                @"select s.ID
			    from StatusLicitacao s  			
                WHERE s.FlagAtivo = 1
			    order by s.ID");
                _listaStatus = new List<StatusLicitacaoEnum>();
                IList list = query.List();
                for(int i = 0; i < list.Count; i++)
                {
                    _listaStatus.Add((Business.StatusLicitacaoEnum)Convert.ToInt32(list[i]));
                }
            }
            return _listaStatus;
        }

        public static StatusLicitacaoEnum GetAnterior(StatusLicitacaoEnum statusAtual)
        {
            
            int index = GetListaStatus().FindIndex(new Predicate<StatusLicitacaoEnum>(delegate(StatusLicitacaoEnum match)
            {
                return match == statusAtual;
            }));
            return GetListaStatus()[index - 1];
        }
       
        
        /// <summary>
        /// Retorna o item seguinte do enum
        /// </summary>
	    public static StatusLicitacaoEnum GetNext(StatusLicitacaoEnum statusAtual)
	    {
	        int index = GetListaStatus().FindIndex(new Predicate<StatusLicitacaoEnum>(delegate(StatusLicitacaoEnum match)
            {
                return match == statusAtual;
            }));
            
           return GetListaStatus()[index + 1];
	    }

		#endregion

	    public static StatusLicitacao Get(StatusLicitacaoEnum status)
	    {
	        return StatusLicitacao.Get(Convert.ToInt32(status));
	    }

        public virtual int CompareTo(StatusLicitacao other)
	    {
	        return Descricao.CompareTo(other.Descricao);
	    }

        public override string ToString()
        {
            return Descricao;
        }
	}
}
