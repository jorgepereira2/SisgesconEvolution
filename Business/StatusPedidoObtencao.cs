using System;
using System.Collections;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class StatusPedidoObtencao : BusinessObject<StatusPedidoObtencao>, IComparable<StatusPedidoObtencao>	
	{
		#region Private Members

		private string _descricao;
        private bool _flagVinculoPorDivisao;
        private bool _flagAtivo;
	    
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public StatusPedidoObtencao()
		{
			_descricao = null;
		    _flagVinculoPorDivisao = false;
		    _responsaveis = new CustomList<Servidor>();
            _responsaveisDivisao = new CustomList<StatusPedidoObtencaoDivisao>();
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties

        public virtual bool FlagAtivo
        {
            get { return _flagAtivo; }
            set { _flagAtivo = value; }
        }

        public virtual bool FlagVinculoPorDivisao
        {
            get { return _flagVinculoPorDivisao; }
            set { _flagVinculoPorDivisao = value; }
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

        #endregion

	    #region Collections

	    private ICustomList<Servidor> _responsaveis;
        private ICustomList<StatusPedidoObtencaoDivisao> _responsaveisDivisao;

        public virtual ICustomList<StatusPedidoObtencaoDivisao> ResponsaveisDivisao
        {
            get { return _responsaveisDivisao; }
            set { _responsaveisDivisao = value; }
        }

	    public virtual ICustomList<Servidor> Responsaveis
	    {
	        get { return _responsaveis; }
	        set { _responsaveis = value; }
	    }

	    public virtual StatusPedidoObtencaoEnum StatusPedidoObtencaoEnum
	    {
	        get { return (Business.StatusPedidoObtencaoEnum) this.ID;}
	    }

	    #endregion
		
		#region Public Methods

        public static List<StatusPedidoObtencao> Select()
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"from StatusPedidoObtencao s  		
            WHERE s.FlagAtivo = 1
			order by s.ID");

            return (List<StatusPedidoObtencao>)query.List<StatusPedidoObtencao>();
        }

        public static Dictionary<int, string> List()
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"select s.ID, s.Descricao 
			from StatusPedidoObtencao s  			
            WHERE s.FlagAtivo = 1
			order by s.Descricao");

            return BusinessHelper.ExecuteList(query);
        }

        private static List<StatusPedidoObtencaoEnum> _listaStatus;

        private static List<StatusPedidoObtencaoEnum> GetListaStatus()
        {
            if (_listaStatus == null)
            {
                ISession session = NHibernateSessionManager.Instance.GetSession();
                IQuery query = session.CreateQuery(
                @"select s.ID
			    from StatusPedidoObtencao s  			
                WHERE s.FlagAtivo = 1
			    order by s.ID");
                _listaStatus = new List<StatusPedidoObtencaoEnum>();
                IList list = query.List();
                for(int i = 0; i < list.Count; i++)
                {
                    _listaStatus.Add((Business.StatusPedidoObtencaoEnum)Convert.ToInt32(list[i]));
                }
            }
            return _listaStatus;
        }

        public static StatusPedidoObtencaoEnum GetAnterior(StatusPedidoObtencaoEnum statusAtual)
        {
            int index = GetListaStatus().FindIndex(new Predicate<StatusPedidoObtencaoEnum>(delegate(StatusPedidoObtencaoEnum match)
            {
                return match == statusAtual;
            }));
            return GetListaStatus()[index - 1];
        }
       
		#endregion

		#region Responsaveis Por Divisao
		
		public virtual List<Servidor> GetResponsaveisDivisao(int id_divisao)
		{
		    List<Servidor> list = new List<Servidor>();
		    foreach (StatusPedidoObtencaoDivisao divisao in _responsaveisDivisao)
		    {
		        if(divisao.Celula.ID == id_divisao)
                    list.Add(divisao.Servidor);
		    }
		    return list;
		}
		
		#endregion

	    public static StatusPedidoObtencao Get(StatusPedidoObtencaoEnum status)
	    {
	        return StatusPedidoObtencao.Get(Convert.ToInt32(status));
	    }

        public virtual int CompareTo(StatusPedidoObtencao other)
	    {
	        return Descricao.CompareTo(other.Descricao);
	    }

        public override string ToString()
        {
            return Descricao;
        }
	}
}
