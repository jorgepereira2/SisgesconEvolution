using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class StatusPedidoServicoMergulho : BusinessObject<StatusPedidoServicoMergulho>, IComparable<StatusPedidoServicoMergulho>	
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
		public StatusPedidoServicoMergulho()
		{
			_descricao = null;
		    _flagVinculoPorDivisao = false;
		    _responsaveis = new CustomList<Servidor>();
            _responsaveisDivisao = new CustomList<StatusPedidoServicoMergulhoDivisao>();
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
        private ICustomList<StatusPedidoServicoMergulhoDivisao> _responsaveisDivisao;

        public virtual ICustomList<StatusPedidoServicoMergulhoDivisao> ResponsaveisDivisao
        {
            get { return _responsaveisDivisao; }
            set { _responsaveisDivisao = value; }
        }

	    public virtual ICustomList<Servidor> Responsaveis
	    {
	        get { return _responsaveis; }
	        set { _responsaveis = value; }
	    }

	    public virtual StatusPedidoServicoMergulhoEnum StatusPedidoServicoMergulhoEnum
	    {
	        get { return (Business.StatusPedidoServicoMergulhoEnum) this.ID;}
	    }

	    #endregion

		
		#region Public Methods

        public static List<StatusPedidoServicoMergulho> Select()
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"from StatusPedidoServicoMergulho s  			
			order by s.ID");

            return (List<StatusPedidoServicoMergulho>)query.List<StatusPedidoServicoMergulho>();
        }
		
		public static Dictionary<int, string> List()
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
			@"select s.ID, s.Descricao 
			from StatusPedidoServicoMergulho s  			
			order by s.Descricao");
		
			return BusinessHelper.ExecuteList(query); 
		}

	    private static List<StatusPedidoServicoMergulhoEnum> _listaStatus;
	    
	    private static List<StatusPedidoServicoMergulhoEnum> GetListaStatus()
	    {
	        if(_listaStatus == null)
	        {
                ISession session = NHibernateSessionManager.Instance.GetSession();
                IQuery query = session.CreateQuery(
                @"select s.ID
			    from StatusPedidoServicoMergulho s  			
			    order by s.ID");
                _listaStatus = new List<StatusPedidoServicoMergulhoEnum>();
	            foreach (object id in query.List())
	            {
	                _listaStatus.Add((Business.StatusPedidoServicoMergulhoEnum)Convert.ToInt32(id));
	            }
	        }
	        return _listaStatus;
	    }
	    
	    public static StatusPedidoServicoMergulhoEnum GetAnterior(StatusPedidoServicoMergulhoEnum statusAtual)
	    {
	        int index = GetListaStatus().FindIndex(new Predicate<StatusPedidoServicoMergulhoEnum>(delegate(StatusPedidoServicoMergulhoEnum match)
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

	    public static StatusPedidoServicoMergulho Get(StatusPedidoServicoMergulhoEnum status)
	    {
	        return StatusPedidoServicoMergulho.Get(Convert.ToInt32(status));
	    }

        public virtual int CompareTo(StatusPedidoServicoMergulho other)
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
