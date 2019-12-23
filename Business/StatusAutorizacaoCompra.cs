using System;
using System.Collections;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class StatusAutorizacaoCompra : BusinessObject<StatusAutorizacaoCompra>, IComparable<StatusAutorizacaoCompra>	
	{
		#region Private Members
		private string _descricao;
	    
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public StatusAutorizacaoCompra()
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
        
        public virtual ICustomList<Servidor> Responsaveis
	    {
	        get { return _responsaveis; }
	        set { _responsaveis = value; }
	    }

	    public virtual StatusAutorizacaoCompraEnum StatusAutorizacaoCompraEnum
	    {
	        get { return (Business.StatusAutorizacaoCompraEnum) this.ID;}
	    }

	    #endregion
		
		#region Public Methods

        public static List<StatusAutorizacaoCompra> Select()
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"from StatusAutorizacaoCompra s  			
			order by s.ID");

            return (List<StatusAutorizacaoCompra>)query.List<StatusAutorizacaoCompra>();
        }

        public static Dictionary<int, string> List()
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"select s.ID, s.Descricao 
			from StatusAutorizacaoCompra s  			
			order by s.Descricao");

            return BusinessHelper.ExecuteList(query);
        }

        private static List<StatusAutorizacaoCompraEnum> _listaStatus;

        private static List<StatusAutorizacaoCompraEnum> GetListaStatus()
        {
            if (_listaStatus == null)
            {
                ISession session = NHibernateSessionManager.Instance.GetSession();
                IQuery query = session.CreateQuery(
                @"select s.ID
			    from StatusAutorizacaoCompra s  			
			    order by s.ID");
                _listaStatus = new List<StatusAutorizacaoCompraEnum>();
                IList list = query.List();
                for(int i = 0; i < list.Count; i++)
                {
                    _listaStatus.Add((Business.StatusAutorizacaoCompraEnum)Convert.ToInt32(list[i]));
                }
            }
            return _listaStatus;
        }

        public static StatusAutorizacaoCompraEnum GetAnterior(StatusAutorizacaoCompraEnum statusAtual)
        {
            if(statusAtual == StatusAutorizacaoCompraEnum.AguardandoAprovacaoEncarregadoObtencao)
                return StatusAutorizacaoCompraEnum.AguardandoAprovacaoEncarregadoObtencao;
                
            int index = GetListaStatus().FindIndex(new Predicate<StatusAutorizacaoCompraEnum>(delegate(StatusAutorizacaoCompraEnum match)
            {
                return match == statusAtual;
            }));
            return GetListaStatus()[index - 1];
        }
       
        
        /// <summary>
        /// Retorna o item seguinte do enum
        /// </summary>
	    public static StatusAutorizacaoCompraEnum GetNext(StatusAutorizacaoCompraEnum statusAtual)
	    {
	        if(statusAtual == StatusAutorizacaoCompraEnum.Finalizado)
	            return StatusAutorizacaoCompraEnum.Finalizado;
            int index = GetListaStatus().FindIndex(new Predicate<StatusAutorizacaoCompraEnum>(delegate(StatusAutorizacaoCompraEnum match)
            {
                return match == statusAtual;
            }));
            
           return GetListaStatus()[index + 1];
	    }
		#endregion

	    public static StatusAutorizacaoCompra Get(StatusAutorizacaoCompraEnum status)
	    {
	        return StatusAutorizacaoCompra.Get(Convert.ToInt32(status));
	    }

        public virtual int CompareTo(StatusAutorizacaoCompra other)
	    {
	        return Descricao.CompareTo(other.Descricao);
	    }

        public override string ToString()
        {
            return Descricao;
        }
	}
}
