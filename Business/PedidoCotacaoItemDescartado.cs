using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class PedidoCotacaoItemDescartado : BusinessObject<PedidoCotacaoItemDescartado>	
	{
		#region Private Members
        private PedidoObtencao _pedidoObtencao; 
		private Servidor _servidor; 
		private string _justificativa;
	    private ServicoMaterial _servicoMaterial;
	    private bool _flagCancelado;
        private string _justificativaCancelamento;
	    private bool _flagReativado;
	    private string _justificativaReativacao;

	    #endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public PedidoCotacaoItemDescartado()
		{
			_pedidoObtencao =  null; 
			_servidor =  null; 
			_justificativa = null; 
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties

        /// <summary>
        /// Indica se o item foi cancelado definitivamente
        /// </summary>
        public virtual bool FlagCancelado
        {
            get { return _flagCancelado; }
            set { _flagCancelado = value; }
        }

        public virtual bool FlagReativado
        {
            get { return _flagReativado; }
            set { _flagReativado = value; }
        }
		public virtual PedidoObtencao PedidoObtencao
		{
			get { return _pedidoObtencao; }
			set { _pedidoObtencao = value; }
		}
        public virtual ServicoMaterial ServicoMaterial
        {
            get { return _servicoMaterial; }
            set { _servicoMaterial  = value; }
        }
			
		/// <summary>
		/// Comprador
		/// </summary>		
		public virtual Servidor Servidor
		{
			get { return _servidor; }
			set { _servidor = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual string Justificativa
		{
			get { return _justificativa; }
			set	
			{
				if ( value != null )
					if( value.Length > 500)
						throw new ArgumentOutOfRangeException("Invalid value for Justificativa", value, value.ToString());
				
				_justificativa = value;
			}
		}

        public virtual string JustificativaCancelamento
        {
            get { return _justificativaCancelamento; }
            set{_justificativaCancelamento = value;}
        }

        public virtual string JustificativaReativacao
        {
            get { return _justificativaReativacao; }
            set { _justificativaReativacao = value; }
        }
		#endregion 
		
		
		#region Public Methods
		
		public static List<PedidoCotacaoItemDescartado> Select(string numeroPO, int id_comprador, bool? flagCancelado, bool? flagReativado)
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
            @"from PedidoCotacaoItemDescartado p
                        inner join fetch p.Servidor s
                        inner join fetch p.PedidoObtencao po  
            where po.Numero like :numeroPO
            and  s.ID = IsNull(:id_comprador, s.ID)			
            and  p.FlagCancelado = IsNull(:flagCancelado, p.FlagCancelado)			
            and  p.FlagReativado = IsNull(:flagReativado, p.FlagReativado)			
			order by po.Numero DESC");

		    query.SetString("numeroPO", "%" + numeroPO + "%");
		    query.SetParameter("id_comprador", BusinessHelper.IsNullOrZero(id_comprador), NHibernateUtil.Int32);
            query.SetParameter("flagCancelado", BusinessHelper.IsNull(flagCancelado), NHibernateUtil.Boolean);
            query.SetParameter("flagReativado", BusinessHelper.IsNull(flagReativado), NHibernateUtil.Boolean);
		
			return (List<PedidoCotacaoItemDescartado>)query.List<PedidoCotacaoItemDescartado>();
		}
		
		#endregion

	    public virtual void Reativar(string justificativa)
	    {
            this.FlagReativado = true;
            this.JustificativaReativacao = justificativa;
            this.Save();

            foreach (PedidoObtencaoItem item in _pedidoObtencao.Itens)
            {
                if (item.ServicoMaterial.ID == _servicoMaterial.ID)
                {
                    item.Save();
                    return;
                }
            }
	    }

	    public virtual void Cancelar(string justificativa)
	    {
	        this.FlagCancelado = true;
	        this.JustificativaCancelamento = justificativa;
            this.Save();

	        foreach (PedidoObtencaoItem item in _pedidoObtencao.Itens)
	        {
	            if(item.ServicoMaterial.ID == _servicoMaterial.ID)
	            {
	                item.Cancelar(_servidor.ID, justificativa, null);
                    return;
	            }
	        }
	    }

        public static PedidoCotacaoItemDescartado GetByPO(int id_po, int id_servicoMaterial)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"from PedidoCotacaoItemDescartado p
            where p.PedidoObtencao.ID = :id_po
            and  p.ServicoMaterial.ID = :id_servicoMaterial");

            query.SetInt32("id_po", id_po);
            query.SetInt32("id_servicoMaterial", id_servicoMaterial);
            query.SetMaxResults(1);
            return query.UniqueResult<PedidoCotacaoItemDescartado>();
        }
	}
}
