using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class PedidoAquisicao : BusinessObject<PedidoAquisicao>	
	{
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public PedidoAquisicao()
		{
		    _itens = new CustomList<PedidoAquisicaoItem>();
		    _historico = new CustomList<HistoricoPedidoAquisicao>();
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties
        public virtual TipoPedidoAquisicao TipoPedidoAquisicao { get; set;}
        
        public virtual Conta Conta{ get; set;}

        public virtual Meta Meta{ get; set;}
        
        public virtual TipoPagamentoPO? TipoPagamento{ get; set;}
        
        public virtual StatusPedidoAquisicao Status{ get; set;}

        public virtual int Numero{ get; set;}
        	
		public virtual DateTime DataEmissao{ get; set;}
		
		public virtual string Aplicacao{ get; set;}

        public virtual Servidor ServidorCadastro { get; set; }

        public virtual Celula Celula { get; set; }

        public virtual string Observacao { get; set; }

		public virtual Fornecedor Fornecedor{ get; set;}
        public virtual Fornecedor Fornecedor2 { get; set; }
        public virtual Fornecedor Fornecedor3 { get; set; }
        public virtual Fornecedor Fornecedor4 { get; set; }

	    public virtual string NumeroEmpenho { get; set; }

        public virtual bool FlagRecusado { get; set; }

	    #endregion 
	  
	    #region Collections

	    private ICustomList<PedidoAquisicaoItem> _itens;
        private ICustomList<HistoricoPedidoAquisicao> _historico;

        public virtual ICustomList<HistoricoPedidoAquisicao> Historico
        {
            get { return _historico; }
            set { _historico = value; }
        }
        
	    public virtual ICustomList<PedidoAquisicaoItem> Itens
	    {
	        get { return _itens; }
	        set { _itens = value; }
	    }
	    #endregion

        #region Advanced Properties

	    private PedidoAquisicaoManager _manager;
	    public virtual PedidoAquisicaoManager Manager
	    {
	        get
	        {
                if (_manager == null)
                    _manager = PedidoAquisicaoManager.GetManager(this);
	            return _manager;
	        }
	    }
        
        public virtual decimal ValorTotal
        {
            get
            {
                decimal valor = 0;
                foreach (PedidoAquisicaoItem item in _itens)
                {
                    valor += item.ValorTotal;
                }
                return valor;
            }
        }

        public virtual bool PodeSerAlterado
        {
            get
            {
                return  Status == null ||
                        Status.StatusPedidoAquisicaoEnum == StatusPedidoAquisicaoEnum.NaoEnviado ||
                        Status.StatusPedidoAquisicaoEnum == StatusPedidoAquisicaoEnum.Reprovado;
            }
        }

        public virtual HistoricoPedidoAquisicao UltimoHistorico
        {
            get
            {
                if (Historico.Count > 0)
                    return Historico[Historico.Count - 1];
                else
                    return null;
            }
        }
        
	    public virtual bool RequerLicitacao
	    {
	        get
	        {
	            return false;
	            if(TipoPedidoAquisicao == null) return false;
	            if(!TipoPedidoAquisicao.ValorLimitePO.HasValue) return false;
	            return this.ValorTotal > TipoPedidoAquisicao.ValorLimitePO.Value;
	        }
	    }
        #endregion
		
		#region Public Methods
		
		public static List<PedidoAquisicao> Select(string texto, DateTime dataInicio, DateTime dataFim, int id_servidorConsulta, int id_status)
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
            @"from PedidoAquisicao p
                inner join fetch p.Celula c 
                inner join fetch p.Status s
			where p.Numero like :texto
            and s.ID = IsNull(:id_status, s.ID)
			and dbo.DateIsInBetween(p.DataEmissao, :dataInicio, :dataFim) = 1  			
            and  (:FlagPodeVerPAOutraCelula = 1 OR dbo.CelulaPertenceACelula(:CelulaServidor, c.Codigo) = 1)		
			order by p.Numero");

            Servidor servidor = Servidor.Get(id_servidorConsulta);

		    query.SetString("texto", string.Format("%{0}%", texto));
		    query.SetParameter("dataInicio", BusinessHelper.IsNull(dataInicio), NHibernateUtil.DateTime);
            query.SetParameter("dataFim", BusinessHelper.IsNull(dataFim), NHibernateUtil.DateTime);
            query.SetParameter("id_status", BusinessHelper.IsNullOrZero(id_status), NHibernateUtil.Int32);
            query.SetBoolean("FlagPodeVerPAOutraCelula", servidor.FlagPodeVerPAOutraCelula);
            query.SetString("CelulaServidor", servidor.Celula.Codigo);
			return (List<PedidoAquisicao>)query.List<PedidoAquisicao>();
		}

        public static List<PedidoAquisicao> Select(int id_celula, int id_fornecedor, int id_status, int id_tipo, int id_conta, int id_meta, DateTime dataInicio, DateTime dataFim, int id_servidorConsulta)
        {
            Servidor servidor = Servidor.Get(id_servidorConsulta);

            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"from PedidoAquisicao p 
                    inner join fetch p.Celula c 
                    inner join fetch p.Fornecedor f 
                    inner join fetch p.TipoPedidoAquisicao t
                    inner join fetch p.Status s
                    left join fetch p.Conta conta
                    left join fetch p.Meta m
			where c.ID = IsNull(:id_celula, c.ID)
			and   f.ID = IsNull(:id_fornecedor, f.ID)
			and   t.ID = IsNull(:id_tipo, t.ID)
			and   s.ID = IsNull(:id_status, s.ID)
			and   IsNull(conta.ID, -1) = IsNull(:id_conta, IsNull(conta.ID, -1))
			and   IsNull(m.ID, -1) = IsNull(:id_meta, IsNull(m.ID, -1))
			and dbo.DateIsInBetween(p.DataEmissao, :dataInicio, :dataFim) = 1  	
            and  (:FlagPodeVerPAOutraCelula = 1 OR dbo.CelulaPertenceACelula(:CelulaServidor, c.Codigo) = 1)		
			order by p.Numero");

            query.SetParameter("id_celula", BusinessHelper.IsNullOrZero(id_celula), NHibernateUtil.Int32);
            query.SetParameter("id_fornecedor", BusinessHelper.IsNullOrZero(id_fornecedor), NHibernateUtil.Int32);
            query.SetParameter("id_tipo", BusinessHelper.IsNullOrZero(id_tipo), NHibernateUtil.Int32);
            query.SetParameter("id_status", BusinessHelper.IsNullOrZero(id_status), NHibernateUtil.Int32);
            query.SetParameter("id_conta", BusinessHelper.IsNullOrZero(id_conta), NHibernateUtil.Int32);
            query.SetParameter("id_meta", BusinessHelper.IsNullOrZero(id_meta), NHibernateUtil.Int32);
            query.SetParameter("dataInicio", BusinessHelper.IsNull(dataInicio), NHibernateUtil.DateTime);
            query.SetParameter("dataFim", BusinessHelper.IsNull(dataFim), NHibernateUtil.DateTime);
            query.SetBoolean("FlagPodeVerPAOutraCelula", servidor.FlagPodeVerPAOutraCelula);
            query.SetString("CelulaServidor", servidor.Celula.Codigo);
            
            return (List<PedidoAquisicao>)query.List<PedidoAquisicao>();
        }

        public static List<PedidoAquisicao> SelectPedidosRecusados(int id_servidor)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"from PedidoAquisicao p
			where p.ServidorCadastro.ID = :id_servidor
			and p.Status.ID = :statusRecusado
			order by p.Numero");

            query.SetInt32("id_servidor", id_servidor);
            query.SetInt32("statusRecusado", Convert.ToInt32(StatusPedidoAquisicaoEnum.Reprovado));;
            return (List<PedidoAquisicao>)query.List<PedidoAquisicao>();
        }

        /// <summary>
        /// Seleciona os pedidos que estao pendentes da ação de um servidor
        /// </summary>
        public static List<PedidoAquisicao> Select(int id_servidor)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"select distinct p from PedidoAquisicao p inner join p.Status s inner join fetch p.Fornecedor f 
                inner join s.Responsaveis resp
			where resp.ID = :id_servidor
			and s.FlagVinculoPorDivisao = 0									
			order by p.Numero");

            IQuery query2 = session.CreateQuery(
            @"select distinct p from PedidoAquisicao p inner join p.Status s inner join fetch p.Fornecedor f 
                inner join s.ResponsaveisDivisao divisao
			where divisao.Servidor.ID = :id_servidor
			and divisao.Celula.ID = p.Celula.ID
			and s.FlagVinculoPorDivisao = 1
            and s.ID != :id_statusAprovacaoDepartamento
			order by p.Numero");

            query.SetInt32("id_servidor", id_servidor);
            query2.SetInt32("id_servidor", id_servidor);
            query2.SetInt32("id_statusAprovacaoDepartamento", Convert.ToInt32(StatusPedidoAquisicaoEnum.AguardandoAprovacaoEncarregadoDepartamento));

            List<PedidoAquisicao> list1 = (List<PedidoAquisicao>)query.List<PedidoAquisicao>();
            List<PedidoAquisicao> list2 = (List<PedidoAquisicao>)query2.List<PedidoAquisicao>();
            list1.AddRange(list2);
            list1.AddRange(SelectPorDepartamento(id_servidor));

            //list1.Sort(new Comparison<PedidoAquisicao>(delegate(PedidoAquisicao p1, PedidoAquisicao p2)
            //{
            //    return p1.Numero.CompareTo(p2.Numero);
            //}));

            return list1.Where(i => i.Status.StatusPedidoAquisicaoEnum != StatusPedidoAquisicaoEnum.NaoEnviado).OrderBy(i => i.Numero).ToList();
        }

        /// <summary>
        /// Seleciona PA's que estao com o status AguardandoAprovacaoEncarregadoDepartamento e
        /// cujas as divisoes dentro do departamento do servidor
        /// </summary>
        /// <param name="id_servidor"></param>
        /// <returns></returns>
        private static List<PedidoAquisicao> SelectPorDepartamento(int id_servidor)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"select distinct p from PedidoAquisicao p inner join p.Status s 
                inner join s.ResponsaveisDivisao divisao
			where divisao.Servidor.ID = :id_servidor												
			and s.ID = :id_status
			and dbo.CelulaPertenceADepartamento(p.Celula.Codigo, divisao.Celula.Codigo) = 1
			order by p.Numero");

            query.SetInt32("id_status", Convert.ToInt32(StatusPedidoAquisicaoEnum.AguardandoAprovacaoEncarregadoDepartamento));
            query.SetInt32("id_servidor", id_servidor);
            return (List<PedidoAquisicao>)query.List<PedidoAquisicao>();
        }
		#endregion

        public override void Save()
        {
            Validar();
            if (!this.IsPersisted)
                CriarNovo();
            else
                base.Save();
        }

        public virtual void Update()
        {
            base.Save();
        }

	    private void Validar()
	    {
	        if(!this.PodeSerAlterado)
	            throw new Exception("Este PO não pode ser alterado neste estágio.");
	    }

   

	    private void CriarNovo()
        {
            this.DataEmissao = DateTime.Today;
	        this.Status = StatusPedidoAquisicao.Get(StatusPedidoAquisicaoEnum.NaoEnviado);
	        this.Celula = ServidorCadastro.Celula.GetDivisao();
            using (TransactionBlock tran = new TransactionBlock())
            {
                ISession session = NHibernateSessionManager.Instance.GetSession();
                IQuery query = session.CreateQuery(
                    @"select MAX(p.Numero) from PedidoAquisicao p ");

                object result = query.UniqueResult();
                if (result == null)
                    this.Numero = 1;
                else
                    this.Numero = Convert.ToInt32(result) + 1;

                base.Save();

                tran.IsValid = true;
            }
        }
	}
}
