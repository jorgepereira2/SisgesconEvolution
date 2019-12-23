using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class StatusPedidoServicoAtividadeSecundaria : BusinessObject<StatusPedidoServicoAtividadeSecundaria>, IComparable<StatusPedidoServicoAtividadeSecundaria>	
	{
		#region Private Members
		private string _descricao;
	    private bool _flagVinculoPorDivisao;
	    private bool _flagSomenteGerente;
	    
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public StatusPedidoServicoAtividadeSecundaria()
		{
			_descricao = null;
		    _flagVinculoPorDivisao = false;
		    _responsaveis = new CustomList<Servidor>();
            _responsaveisDivisao = new CustomList<StatusPedidoServicoAtividadeSecundariaDivisao>();
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties

        public virtual bool FlagSomenteGerente
        {
            get { return _flagSomenteGerente; }
            set { _flagSomenteGerente = value; }
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
        private ICustomList<StatusPedidoServicoAtividadeSecundariaDivisao> _responsaveisDivisao;

        public virtual ICustomList<StatusPedidoServicoAtividadeSecundariaDivisao> ResponsaveisDivisao
        {
            get { return _responsaveisDivisao; }
            set { _responsaveisDivisao = value; }
        }

	    public virtual ICustomList<Servidor> Responsaveis
	    {
	        get { return _responsaveis; }
	        set { _responsaveis = value; }
	    }

	    public virtual StatusPedidoServicoAtividadeSecundariaEnum StatusPedidoServicoAtividadeSecundariaEnum
	    {
	        get { return (Business.StatusPedidoServicoAtividadeSecundariaEnum) this.ID;}
	    }

	    #endregion

		
		#region Public Methods

        public static List<StatusPedidoServicoAtividadeSecundaria> Select()
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"from StatusPedidoServicoAtividadeSecundaria s  			
			order by s.ID");

            return (List<StatusPedidoServicoAtividadeSecundaria>)query.List<StatusPedidoServicoAtividadeSecundaria>();
        }
		
		public static Dictionary<int, string> List()
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
			@"select s.ID, s.Descricao 
			from StatusPedidoServicoAtividadeSecundaria s  			
			order by s.Descricao");
		
			return BusinessHelper.ExecuteList(query); 
		}

	    private static List<StatusPedidoServicoAtividadeSecundariaEnum> _listaStatus;
	    
	    private static List<StatusPedidoServicoAtividadeSecundariaEnum> GetListaStatus()
	    {
	        if(_listaStatus == null)
	        {
                ISession session = NHibernateSessionManager.Instance.GetSession();
                IQuery query = session.CreateQuery(
                @"select s.ID
			    from StatusPedidoServicoAtividadeSecundaria s  			
			    order by s.ID");
                _listaStatus = new List<StatusPedidoServicoAtividadeSecundariaEnum>();
	            foreach (object id in query.List())
	            {
	                _listaStatus.Add((Business.StatusPedidoServicoAtividadeSecundariaEnum)Convert.ToInt32(id));
	            }
	        }
	        return _listaStatus;
	    }
	    
	    public static StatusPedidoServicoAtividadeSecundariaEnum GetAnterior(StatusPedidoServicoAtividadeSecundariaEnum statusAtual)
	    {
	        int index = GetListaStatus().FindIndex(new Predicate<StatusPedidoServicoAtividadeSecundariaEnum>(delegate(StatusPedidoServicoAtividadeSecundariaEnum match)
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
		    return list;
		}
		
		#endregion

	    public static StatusPedidoServicoAtividadeSecundaria Get(StatusPedidoServicoAtividadeSecundariaEnum status)
	    {
	        return StatusPedidoServicoAtividadeSecundaria.Get(Convert.ToInt32(status));
	    }

        public virtual int CompareTo(StatusPedidoServicoAtividadeSecundaria other)
	    {
            if (other == null) return 1;
	        return Descricao.CompareTo(other.Descricao);
	    }

        public override string ToString()
        {
            return Descricao;
        }
	}
}
