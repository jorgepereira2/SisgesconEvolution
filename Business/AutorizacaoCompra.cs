using System;
using System.Collections.Generic;
using System.Data;
using NHibernate;
using Shared.Common;
using Shared.DataAccessHelper;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public class AutorizacaoCompra : BusinessObject<AutorizacaoCompra>, ICompra	
	{
		#region Private Members
		private Servidor _servidor; 
		private Fornecedor _fornecedor; 
		private DateTime _dataemissao; 
		private StatusAutorizacaoCompra _status; 
		private Licitacao _licitacao; 
		private Projeto _projeto; 
		private string _observacao;
	    private bool _flagRequerAprovacaoConselhoEconomico;
	    private bool _flagPago;
	    private string _numeroEmpenho;
	    private TipoCompra _tipoCompra;
	    private MotivoCancelamento _motivoCancelamento;
	    private bool _flagEmitida;
	    private string _codigoGestao;
	    private string _lista;
	    private string _ordemBancaria;
	    private string _numeroLancamento;
        private NaturezaDespesa _naturezaDespesa;
	    private FonteRecurso _fonteRecurso;
        private PTRES _ptres;
	    private int _numero;
    	#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public AutorizacaoCompra()
		{
		    _flagRequerAprovacaoConselhoEconomico = false;
			_servidor =  null; 
			_fornecedor =  null; 
			_dataemissao = DateTime.MinValue; 
			_status =  null; 
			_licitacao =  null; 
			_projeto =  null; 
			_observacao = null;
		    _numeroEmpenho = null;
		    _flagPago = false;
		    _itens = new CustomList<PedidoCotacaoItem>();
		    _historico = new CustomList<HistoricoAutorizacaoCompra>();
		    _pagamentos = new CustomList<AutorizacaoCompraPagamento>();
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties
        public virtual int Numero
        {
            get { return _numero;}
            set { _numero = value; }
        }
        public virtual FonteRecurso FonteRecurso
        {
            get { return _fonteRecurso; }
            set { _fonteRecurso = value; }
        }
        public virtual string NumeroLancamento
        {
            get { return _numeroLancamento; }
            set { _numeroLancamento = value; }
        }
        public virtual string OrdemBancaria
        {
            get { return _ordemBancaria; }
            set { _ordemBancaria = value; }
        }
        public virtual string Lista
        {
            get { return _lista; }
            set { _lista = value; }
        }
        public virtual string CodigoGestao
        {
            get { return _codigoGestao; }
            set { _codigoGestao = value; }
        }
        public virtual bool FlagEmitida
        {
            get { return _flagEmitida; }
            set { _flagEmitida = value; }
        }
        public virtual MotivoCancelamento MotivoCancelamento
        {
            get { return _motivoCancelamento; }
            set { _motivoCancelamento = value; }
        }

        public virtual TipoCompra TipoCompra
        {
            get { return _tipoCompra; }
            set { _tipoCompra = value; }
        }
        
        public virtual string NumeroEmpenho
        {
            get { return _numeroEmpenho; }
            set { _numeroEmpenho = value; }
        }
		
        public virtual bool FlagPago
        {
            get { return _flagPago; }
            set { _flagPago = value; }
        }
        
        public virtual bool FlagRequerAprovacaoConselhoEconomico
        {
            get { return _flagRequerAprovacaoConselhoEconomico; }
            set { _flagRequerAprovacaoConselhoEconomico = value; }
        }
        
		public virtual Servidor Servidor
		{
			get { return _servidor; }
			set { _servidor = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual Fornecedor Fornecedor
		{
			get { return _fornecedor; }
			set { _fornecedor = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual DateTime DataEmissao
		{
			get { return _dataemissao; }
			set { _dataemissao = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual StatusAutorizacaoCompra Status
		{
			get { return _status; }
			set { _status = value; }
		}
			
		public virtual Licitacao Licitacao
		{
			get { return _licitacao; }
			set { _licitacao = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual Projeto Projeto
		{
			get { return _projeto; }
			set { _projeto = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual string Observacao
		{
			get { return _observacao; }
			set	
			{
				if ( value != null )
					if( value.Length > 500)
						throw new ArgumentOutOfRangeException("Invalid value for Observacao", value, value.ToString());
				
				_observacao = value;
			}
		}

        public virtual NaturezaDespesa NaturezaDespesa
        {
            get { return _naturezaDespesa; }
            set{ _naturezaDespesa = value;}
        }

        public virtual PTRES PTRES
        {
            get { return _ptres; }
            set { _ptres = value; }
        }
		#endregion 
		
        #region Collections

        private ICustomList<PedidoCotacaoItem> _itens;
	    private ICustomList<HistoricoAutorizacaoCompra> _historico;
        private ICustomList<AutorizacaoCompraPagamento> _pagamentos;
	    
	    public virtual ICustomList<AutorizacaoCompraPagamento> Pagamentos
        {
            get { return _pagamentos; }
            set { _pagamentos = value; }
        }

	    public virtual ICustomList<HistoricoAutorizacaoCompra> Historico
	    {
	        get { return _historico; }
	        set { _historico = value; }
	    }
        public virtual ICustomList<PedidoCotacaoItem> Itens
        {
            get { return _itens; }
            set { _itens = value; }
        }
        #endregion
		
        #region Advanced Properties
        public virtual string CodigoComAno
        {
            get { return string.Format("{0}/{1}", _numero, _dataemissao.Year); }
        }
        
        public virtual decimal ValorTotal
        {
            get
            {
                decimal valor = 0;
                foreach (PedidoCotacaoItem item in _itens)
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
                return _status.StatusAutorizacaoCompraEnum != StatusAutorizacaoCompraEnum.Finalizado;
            }
        }

	    public virtual string TextoPO
	    {
	        get
	        {
	            List<string> list = new List<string>();
	            foreach (PedidoCotacaoItem item in _itens)
	            {
	                foreach (PedidoObtencaoItem obtencaoItem in item.ItensObtencao)
	                {
	                    string po = string.Format("PO {0}", obtencaoItem.PedidoObtencao.CodigoComAno);
	                    if(!list.Contains(po))
	                        list.Add(po);
	                }
	            }

	            string texto = "";
	            foreach (string s in list)
	            {
	                texto += s + ",";
	            }
	            return Shared.Common.Util.RemoveLastChar(texto);
	        }
	    }

        /// <summary>
        /// Lista do POs que originaram esta AC, com ID e numero
        /// </summary>
        public virtual Dictionary<int, string> POs
        {
            get
            {
                Dictionary<int, string> list = new Dictionary<int, string>();
                foreach (PedidoCotacaoItem item in _itens)
                {
                    foreach (PedidoObtencaoItem obtencaoItem in item.ItensObtencao)
                    {

                        if (!list.ContainsKey(obtencaoItem.PedidoObtencao.ID))
                        {
                            string po = string.Format("PO {0}", obtencaoItem.PedidoObtencao.CodigoComAno);
                            list.Add(obtencaoItem.PedidoObtencao.ID, po);
                        }
                    }
                }
                return list;
            }
        }

        /// <summary>
        /// Lista do POs que originaram esta AC, com ID e numero
        /// </summary>
        public virtual Dictionary<int, string> PSs
        {
            get
            {
                Dictionary<int, string> list = new Dictionary<int, string>();
                foreach (PedidoCotacaoItem item in _itens)
                {
                    foreach (PedidoObtencaoItem obtencaoItem in item.ItensObtencao)
                    {
                        if(obtencaoItem.PedidoObtencao.DelineamentoOrcamento == null) continue;
                        if (!list.ContainsKey(obtencaoItem.PedidoObtencao.DelineamentoOrcamento.PedidoServico.ID))
                        {
                            string ps = string.Format("PS {0}", obtencaoItem.PedidoObtencao.DelineamentoOrcamento.PedidoServico.CodigoComAno);
                            list.Add(obtencaoItem.PedidoObtencao.DelineamentoOrcamento.PedidoServico.ID, ps);
                        }
                    }
                }
                return list;
            }
        }

        public virtual string TextoPS
        {
            get
            {
                List<string> list = new List<string>();
                foreach (PedidoCotacaoItem item in _itens)
                {
                    foreach (PedidoObtencaoItem obtencaoItem in item.ItensObtencao)
                    {
                        if(obtencaoItem.PedidoObtencao.DelineamentoOrcamento != null)
                        {
                            string ps = string.Format("PS {0}", obtencaoItem.PedidoObtencao.DelineamentoOrcamento.CodigoComAno.ToString());
                            if (!list.Contains(ps))
                                list.Add(ps);
                        }
                    }
                }

                string texto = "";
                foreach (string s in list)
                {
                    texto += s + ",";
                }
                return Shared.Common.Util.RemoveLastChar(texto);
            }
        }

        public virtual HistoricoAutorizacaoCompra UltimoHistorico
        {
            get
            {
                if (Historico.Count > 0)
                    return Historico[Historico.Count - 1];
                else
                    return null;
            }
        }
        
	    public virtual decimal ValorPago
	    {
	        get
	        {
	            decimal valor = 0;
	            foreach (AutorizacaoCompraPagamento pagamento in _pagamentos)
	            {
	                valor += pagamento.ValorPago;
	            }
	            return valor;
	        }
	    }

        /// <summary>
        /// Chave da combinacao de Projeto, NaturezaDespesa, FonteRecurso e PTRES no formato 12|32|15|43
        /// </summary>
        public virtual string ChaveFinanceiro
        {
            get
            {
                NaturezaDespesa naturezaDespesa = null;
                if (NaturezaDespesa != null)
                    naturezaDespesa = NaturezaDespesa.GetPai();
                if (Projeto != null && naturezaDespesa != null && FonteRecurso != null && PTRES != null)
                    return string.Format("{0}|{1}|{2}|{3}", Projeto.ID, naturezaDespesa.ID, FonteRecurso.ID, PTRES.ID);
                else
                    return "0|0|0|0";
            }
        }
	    #endregion
		
		#region Public Methods

        public static List<AutorizacaoCompra> Select(string texto, DateTime dataInicio, DateTime dataFim, int id_status, int id_comprador)
		{
			ISession session = NHibernateSessionManager.Instance.GetSession();
			IQuery query = session.CreateQuery(
            @"from AutorizacaoCompra a inner join fetch a.Fornecedor f
			where (a.Numero like :texto or f.RazaoSocial like :texto)
			and dbo.DateIsInBetween(a.DataEmissao, :dataInicio, :dataFim) = 1  	  			
			and a.Status = IsNull(:id_status, a.Status)			
			and a.Servidor.ID = IsNull(:id_comprador, a.Servidor.ID)
			order by a.ID");

            query.SetString("texto", string.Format("%{0}%", texto));
            query.SetParameter("dataInicio", BusinessHelper.IsNull(dataInicio), NHibernateUtil.DateTime);
            query.SetParameter("dataFim", BusinessHelper.IsNull(dataFim), NHibernateUtil.DateTime);
            query.SetParameter("id_status", BusinessHelper.IsNullOrZero(id_status), NHibernateUtil.Int32);
            query.SetParameter("id_comprador", BusinessHelper.IsNullOrZero(id_comprador), NHibernateUtil.Int32);
			return (List<AutorizacaoCompra>)query.List<AutorizacaoCompra>();
		}

        /// <summary>
        /// Seleciona os pedidos que estao pendentes da ação de um servidor
        /// </summary>
        public static List<AutorizacaoCompra> Select(int id_servidor, int id_status, int id_comprador, int id_fonteRecurso)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"select distinct a from AutorizacaoCompra a inner join a.Status s 
                inner join s.Responsaveis resp
                inner join fetch a.Servidor c
                left join a.FonteRecurso f
			where resp.ID = :id_servidor
            and c.ID = IsNull(:id_comprador, c.ID)									
			and s.ID = IsNull(:id_status, s.ID)
            and IsNull(f.ID, -1) = IsNull(:id_fonteRecurso, IsNull(f.ID, -1))
			order by a.ID");

            query.SetParameter("id_status", BusinessHelper.IsNullOrZero(id_status), NHibernateUtil.Int32);
            query.SetParameter("id_comprador", BusinessHelper.IsNullOrZero(id_comprador), NHibernateUtil.Int32);
            query.SetParameter("id_fonteRecurso", BusinessHelper.IsNullOrZero(id_fonteRecurso), NHibernateUtil.Int32);
            query.SetInt32("id_servidor", id_servidor);

            return (List<AutorizacaoCompra>)query.List<AutorizacaoCompra>();
        }

        /// <summary>
        /// Seleciona os pedidos que estao pendentes da ação de um servidor
        /// </summary>
        public static List<AutorizacaoCompra> SelectPedidosParaAprovacao(int id_servidor, int id_status)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"select distinct a from AutorizacaoCompra a inner join a.Status s 
                inner join s.Responsaveis resp
                inner join fetch a.Servidor c
                left join a.FonteRecurso f
			where resp.ID = :id_servidor            
			and s.ID IN (:id_status)            
			order by a.ID");

            List<int> status = new List<int>(2);
            if (id_status == 0 || id_status == Convert.ToInt32(StatusAutorizacaoCompraEnum.AguardandoAprovacaoEncarregadoObtencao))
                status.Add(Convert.ToInt32(StatusAutorizacaoCompraEnum.AguardandoAprovacaoEncarregadoObtencao));

            if (id_status == 0 || id_status == Convert.ToInt32(StatusAutorizacaoCompraEnum.AguardandoAprovacaoComandanteGeral))
                status.Add(Convert.ToInt32(StatusAutorizacaoCompraEnum.AguardandoAprovacaoComandanteGeral));


            query.SetParameterList("id_status", status, NHibernateUtil.Int32);
            query.SetInt32("id_servidor", id_servidor);

            return (List<AutorizacaoCompra>)query.List<AutorizacaoCompra>();
        }

        /// <summary>
        /// Seleciona os pedidos que estao pendentes da ação de um servidor
        /// </summary>
        public static List<AutorizacaoCompra> SelectPedidosParaAprovacaoTodos(int id_servidor, int id_status)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"select distinct a 
              from AutorizacaoCompra a inner join a.Status s 
                inner join s.Responsaveis resp
                inner join fetch a.Servidor c
                left join a.FonteRecurso f    
			  order by a.ID");

            List<int> status = new List<int>(2);

            return (List<AutorizacaoCompra>)query.List<AutorizacaoCompra>();
        }


        //public static List<AutorizacaoCompra> SelectPedidosParaAprovacao(int id_servidor, int id_status)
        //{
        //    List<AutorizacaoCompra> list1 = new List<AutorizacaoCompra>();
        //    List<AutorizacaoCompra> list2 = new List<AutorizacaoCompra>();
        //    if(id_status == 0 || id_status == Convert.ToInt32(StatusAutorizacaoCompraEnum.AguardandoAprovacaoEncarregadoObtencao))
        //        list1 = Select(id_servidor, Convert.ToInt32(StatusAutorizacaoCompraEnum.AguardandoAprovacaoEncarregadoObtencao), Int32.MinValue, Int32.MinValue);

        //    if(id_status == 0 || id_status == Convert.ToInt32(StatusAutorizacaoCompraEnum.AguardandoAprovacaoComandanteGeral))
        //        list2 = Select(id_servidor, Convert.ToInt32(StatusAutorizacaoCompraEnum.AguardandoAprovacaoComandanteGeral), Int32.MinValue, Int32.MinValue);
          
        //    list1.AddRange(list2);
        //    return list1;
        //}

        /// <summary>
        /// Seleciona os pedidos que estao pendentes da ação de um servidor
        /// </summary>
        public static List<AutorizacaoCompra> SelectACPendentePagamento(string texto, DateTime dataInicio, DateTime dataFim, bool? pago, int ano)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"select a from AutorizacaoCompra a 
                inner join fetch a.Status s                 
                inner join fetch a.Fornecedor f
			where a.FlagPago = IsNull(:pago, a.FlagPago)
			and (f.RazaoSocial like :texto 
			    or a.Numero like :texto)
			and dbo.DateIsInBetween(a.DataEmissao, :dataInicio, :dataFim) = 1
			and s.ID in (:status)
            and dbo.GetYear(a.DataEmissao) = IsNull(:ano, dbo.GetYear(a.DataEmissao))			
			order by a.ID");

            List<int> status = new List<int>();
            status.Add(Convert.ToInt32(StatusAutorizacaoCompraEnum.Finalizado));
            status.Add(Convert.ToInt32(StatusAutorizacaoCompraEnum.AguardandoEntregaMercadoria));
            query.SetParameterList("status", status);
            query.SetString("texto", "%" + texto + "%");
            query.SetParameter("dataInicio", BusinessHelper.IsNull(dataInicio), NHibernateUtil.DateTime);
            query.SetParameter("dataFim", BusinessHelper.IsNull(dataFim), NHibernateUtil.DateTime);
            query.SetParameter("pago", BusinessHelper.IsNull(pago), NHibernateUtil.Boolean);
            query.SetParameter("ano", BusinessHelper.IsNullOrZero(ano), NHibernateUtil.Int32);

            return (List<AutorizacaoCompra>)query.List<AutorizacaoCompra>();
        }

        public static List<AutorizacaoCompra> Select(int id_status, bool? pago, string texto, DateTime dataInicio, DateTime dataFim, int ano)
        {
            string condicaoStatus = "";
            if (id_status == 0)
                condicaoStatus = "";
            else if (id_status == Int32.MinValue || id_status != Convert.ToInt32(StatusAutorizacaoCompraEnum.Cancelado) && id_status != Convert.ToInt32(StatusAutorizacaoCompraEnum.Reprovado))
                condicaoStatus = string.Format("and s.ID NOT IN ({0},{1})", Convert.ToInt32(StatusAutorizacaoCompraEnum.Cancelado), Convert.ToInt32(StatusAutorizacaoCompraEnum.Reprovado));

            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
                string.Format(
            @"select a from AutorizacaoCompra a 
                inner join fetch a.Status s                 
                inner join fetch a.Fornecedor f
                left join fetch a.Projeto p
                left join fetch a.Licitacao l
			where a.FlagPago = IsNull(:pago, a.FlagPago)
            and dbo.GetYear(a.DataEmissao) = IsNull(:ano, dbo.GetYear(a.DataEmissao))
			and (f.RazaoSocial like :texto 
			    or a.Numero like :texto)
			and dbo.DateIsInBetween(a.DataEmissao, :dataInicio, :dataFim) = 1
			and s.ID = IsNull(:id_status, s.ID)			
            {0}
			order by a.ID", condicaoStatus));

            query.SetParameter("id_status", BusinessHelper.IsNullOrZero(id_status), NHibernateUtil.Int32);
            query.SetParameter("pago", BusinessHelper.IsNull(pago), NHibernateUtil.Boolean);
            query.SetString("texto", "%" + texto + "%");
            query.SetParameter("dataInicio", BusinessHelper.IsNull(dataInicio), NHibernateUtil.DateTime);
            query.SetParameter("dataFim", BusinessHelper.IsNull(dataFim), NHibernateUtil.DateTime);
            query.SetParameter("ano", BusinessHelper.IsNullOrZero(ano), NHibernateUtil.Int32);

            return (List<AutorizacaoCompra>)query.List<AutorizacaoCompra>();
        }

        public static List<AutorizacaoCompra> SelectACAtivas(int id_servidor)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"select a from AutorizacaoCompra a                                  
                inner join fetch a.Servidor s
                
			where s.ID = IsNull(:id_servidor, s.ID)
			and a.Status != :id_statusFinalizado
            and a.Status != :id_statusCancelado
			order by a.ID");

            query.SetParameter("id_servidor", BusinessHelper.IsNullOrZero(id_servidor), NHibernateUtil.Int32);
            query.SetInt32("id_statusFinalizado", Convert.ToInt32(StatusAutorizacaoCompraEnum.Finalizado));
            query.SetInt32("id_statusCancelado", Convert.ToInt32(StatusAutorizacaoCompraEnum.Cancelado));

            return (List<AutorizacaoCompra>)query.List<AutorizacaoCompra>();
        }

        public static List<PedidoCotacaoItem> SelectItens(int id_classeMaterial, int id_subClasseMaterial, int id_materialServico, int id_fornecedor, 
            int id_sjb, int id_fabricante, string descricao, string numeroPO)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"select i from PedidoCotacaoItem i
                inner join fetch i.AutorizacaoCompra a                 
                inner join fetch a.Fornecedor f
                inner join fetch i.ServicoMaterial s
                inner join fetch s.SubClasseServicoMaterial sc
                inner join fetch sc.ClasseServicoMaterial c
			where c.ID = IsNull(:id_classeMaterial, c.ID)
            and sc.ID = IsNull(:id_subClasseMaterial, sc.ID)
            and s.ID = IsNull(:id_servicoMaterial, s.ID)
            and f.ID = IsNull(:id_fornecedor, f.ID)
            and IsNull(s.SJB.ID, -1) = IsNull(:id_sjb, IsNull(s.SJB.ID, -1))
            and IsNull(s.Fabricante.ID, -1) = IsNull(:id_fabricante, IsNull(s.Fabricante.ID, -1))
			and s.Descricao like :texto            
			order by a.ID");

            query.SetParameter("id_classeMaterial", BusinessHelper.IsNullOrZero(id_classeMaterial), NHibernateUtil.Int32);
            query.SetParameter("id_subClasseMaterial", BusinessHelper.IsNullOrZero(id_subClasseMaterial), NHibernateUtil.Int32);
            query.SetParameter("id_servicoMaterial", BusinessHelper.IsNullOrZero(id_materialServico), NHibernateUtil.Int32);
            query.SetParameter("id_fornecedor", BusinessHelper.IsNullOrZero(id_fornecedor), NHibernateUtil.Int32);
            query.SetParameter("id_sjb", BusinessHelper.IsNullOrZero(id_sjb), NHibernateUtil.Int32);
            query.SetParameter("id_fabricante", BusinessHelper.IsNullOrZero(id_fabricante), NHibernateUtil.Int32);
            query.SetString("texto", "%" + descricao + "%");


            List<PedidoCotacaoItem> list = (List<PedidoCotacaoItem>)query.List<PedidoCotacaoItem>();
            
            if(!string.IsNullOrEmpty(numeroPO))
            {
                List<PedidoCotacaoItem> finalList = new List<PedidoCotacaoItem>();
                foreach (PedidoCotacaoItem item in list)
                {
                    if(item.AutorizacaoCompra.POs.ContainsValue(string.Format("PO {0}",numeroPO)))
                        finalList.Add(item);
                }
                return finalList;
            }

            return list;
        }
		#endregion

        #region Salvar

        public override void Save()
        {
            if(!IsPersisted)
                CriarNovo();
            else
                base.Save();
        }

	    public virtual string Tipo
	    {
            get { return "AC"; }
	    }


	    private void CriarNovo()
        {
            this.DataEmissao = DateTime.Today;
            
            using (TransactionBlock tran = new TransactionBlock())
            {
                ISession session = NHibernateSessionManager.Instance.GetSession();
                IQuery query = session.CreateQuery(
                    @"select MAX(a.Numero) from AutorizacaoCompra a where dbo.GetYear(a.DataEmissao) = :ano");
                query.SetInt32("ano", _dataemissao.Year);

                object result = query.UniqueResult();
                if (result == null)
                    this.Numero = 1;
                else
                    this.Numero = Convert.ToInt32(result) + 1;

                base.Save();

                tran.IsValid = true;
            }
        }

        private void Validar()
        {
            if (!this.PodeSerAlterado)
                throw new Exception("Este PO não pode ser alterado neste estágio.");

        }

       

    
	    private PedidoCotacaoItem FindItemPorServicoMaterial(int id)
	    {
	        foreach (PedidoCotacaoItem item in _itens)
	        {
	            if(item.ServicoMaterial.ID == id)
	                return item;
	        }
	        return null;
	    }

	    #endregion
		
		#region Workflow
        public virtual void IrParaProximoStatus(int id_servidor, string justificativa)
        {
            if (!this.IsPersisted)
                throw new Exception("Salve os dados do pedido antes de enviá-lo.");

            if (this.Status.StatusAutorizacaoCompraEnum == StatusAutorizacaoCompraEnum.Finalizado)
                throw new Exception("Este pedido já foi finalizado.");


            StatusAutorizacaoCompraEnum proximoStatus = StatusAutorizacaoCompra.GetNext(_status.StatusAutorizacaoCompraEnum);

            using (TransactionBlock tran = new TransactionBlock())
            {
                AtualizarSaldo();

                HistoricoAutorizacaoCompra historico = GetHistorico(id_servidor, proximoStatus);

                historico.Justificativa = justificativa;
                historico.Save();

                this.Status = StatusAutorizacaoCompra.Get(proximoStatus);
                base.Save();

                this._historico.Add(historico);

                tran.IsValid = true;
            }
        }

        private void AtualizarSaldo()
        {
            EntradaValores entradaValores = new EntradaValores();
            bool atualizarSaldo = false;


            if (Status.StatusAutorizacaoCompraEnum == StatusAutorizacaoCompraEnum.AguardandoAprovacaoComandanteGeral ||
                Status.StatusAutorizacaoCompraEnum == StatusAutorizacaoCompraEnum.AguardandoDesignacaoDivisaoApoio) //nessa etapa a natureza de despesa pode ser alterada
            {
                entradaValores = EntradaValores.GetByAC(this.ID);
                if (entradaValores == null)
                    entradaValores = new EntradaValores();

                entradaValores.TipoMovimentoFinanceiro = TipoMovimentoFinanceiro.SaidaComprometido;
                atualizarSaldo = true;

            }
            else if (Status.StatusAutorizacaoCompraEnum == StatusAutorizacaoCompraEnum.AguardandoNotaEmpenho)
            {
                entradaValores = EntradaValores.GetByAC(this.ID);
                if (entradaValores == null)
                    entradaValores = new EntradaValores();

                entradaValores.TipoMovimentoFinanceiro = TipoMovimentoFinanceiro.SaidaEmpenhado;
                atualizarSaldo = true;
            }


            if (atualizarSaldo && this.Projeto != null && this.NaturezaDespesa != null &&
                this.PTRES != null && this.FonteRecurso != null)
            {
                entradaValores.AutorizacaoCompra = this;
                entradaValores.Servidor = this.Servidor;
                entradaValores.Data = DateTime.Today;
                entradaValores.NaturezaDespesa = this.NaturezaDespesa.GetPai();
                entradaValores.FonteRecurso = this.FonteRecurso;
                entradaValores.PTRES = this.PTRES;
                entradaValores.NumeroDocumento = this.Numero.ToString();
                entradaValores.Projeto = this.Projeto;
                entradaValores.Valor = this.ValorTotal;
                entradaValores.TipoOperacaoFinanceira = TipoOperacaoFinanceira.Saida;
                entradaValores.Save();
            }

        }

	    public virtual void DesiginacaoDivisaoApoio(int id_servidor, int id_naturezaDespesa, int id_ptres, string comentario)
        {
            _naturezaDespesa = NaturezaDespesa.Get(id_naturezaDespesa);
            _ptres = PTRES.Get(id_ptres);
            IrParaProximoStatus(id_servidor, comentario);
        }


        public virtual void GerarNotaEmpenho(int id_servidor, string numeroNotaEmpenho, string comentario)
        {
            _numeroEmpenho = numeroNotaEmpenho;
            IrParaProximoStatus(id_servidor, comentario);
        }

        /// <summary>
        /// Retorna para o status anterior
        /// </summary>
        public virtual void Recusar(int id_servidor, string justificativa)
        {
            if(_status.StatusAutorizacaoCompraEnum == StatusAutorizacaoCompraEnum.AguardandoAprovacaoEncarregadoObtencao)
            {
                Reprovar(id_servidor, justificativa);
                return;
            }

            StatusAutorizacaoCompraEnum proximoStatus = StatusAutorizacaoCompra.GetAnterior(_status.StatusAutorizacaoCompraEnum);

            HistoricoAutorizacaoCompra historico = GetHistorico(id_servidor, proximoStatus);
            historico.Justificativa = justificativa;

            using (TransactionBlock tran = new TransactionBlock())
            {
                historico.FlagReprovado = true;
                historico.Save();

                this.Status = StatusAutorizacaoCompra.Get(proximoStatus);

                base.Save();

                this._historico.Add(historico);
                tran.IsValid = true;
            }
        }

        /// <summary>
        /// Volta para a cotacao
        /// </summary>
        public virtual void Reprovar(int id_servidor, string justificativa)
        {
            using (TransactionBlock tran = new TransactionBlock())
            {
                PedidoCotacao pc;
                //Verifica se a quantidade de itens do PC é a mesma da AC
                bool desmembrar = this._itens[0].PedidoCotacao.Itens.Count != _itens.Count;
                if (desmembrar)
                {
                    //Caso não seja, criamos um novo PC e jogamos os itens desta AC para la
                    pc = new PedidoCotacao();
                    PedidoCotacao pcOriginal = _itens[0].PedidoCotacao;
                    pc.DataEmissao = pcOriginal.DataEmissao;
                    
                    pc.FornecedorCotacao1 = Fornecedor.Get(pcOriginal.FornecedorCotacao1.ID);
                    pc.FornecedorCotacao2 = Fornecedor.Get(pcOriginal.FornecedorCotacao2 == null ? Int32.MinValue : pcOriginal.FornecedorCotacao2.ID);
                    pc.FornecedorCotacao3 = Fornecedor.Get(pcOriginal.FornecedorCotacao3 == null ? Int32.MinValue : pcOriginal.FornecedorCotacao3.ID);
                    pc.FornecedorCotacao4 = Fornecedor.Get(pcOriginal.FornecedorCotacao4 == null ? Int32.MinValue : pcOriginal.FornecedorCotacao4.ID);
                    pc.NaturezaDespesa = NaturezaDespesa.Get(pcOriginal.NaturezaDespesa.ID);
                    pc.Servidor = Servidor.Get(pcOriginal.Servidor.ID);
                    pc.Observacao = pcOriginal.Observacao;
                    pc.TipoCompra = pcOriginal.TipoCompra;
                    pc.Numero = PedidoCotacao.GetNextNumber();
                }
                else
                {
                    pc = _itens[0].PedidoCotacao;
                }

                pc.FlagFinalizado = false;
                pc.FlagRecusado = true;
                pc.JustificativaRecusa = justificativa;
                pc.Save();

                foreach (PedidoCotacaoItem item in _itens)
                {
                    item.PedidoCotacao = pc;
                    item.AutorizacaoCompra = null;
                    item.Save();
                }

                this.Status = StatusAutorizacaoCompra.Get(StatusAutorizacaoCompraEnum.Reprovado);
                base.Save();

                HistoricoAutorizacaoCompra historico = GetHistorico(id_servidor, StatusAutorizacaoCompraEnum.Reprovado);
                historico.Justificativa = justificativa;
                historico.FlagReprovado = true;
                historico.Save();
                
                tran.IsValid = true;
            }
        }

        public virtual void Cancelar(int id_servidor, int id_motivoCancelamento)
        {
            StatusAutorizacaoCompraEnum proximoStatus = StatusAutorizacaoCompraEnum.Cancelado;

            MotivoCancelamento motivo = Business.MotivoCancelamento.Get(id_motivoCancelamento);
            HistoricoAutorizacaoCompra historico = GetHistorico(id_servidor, proximoStatus);
            historico.Justificativa = motivo.Descricao;
            this.MotivoCancelamento = motivo;

            using (TransactionBlock tran = new TransactionBlock())
            {
                historico.FlagReprovado = true;
                historico.Save();

                this.Status = StatusAutorizacaoCompra.Get(proximoStatus);

                base.Save();

                this._historico.Add(historico);

                foreach (PedidoObtencao po in GetPedidosObtencaoRelacionados())
                {
                    po.RegistraCancelamentoAC(id_servidor, this);
                }

                EntradaValores entradaValores = EntradaValores.GetByAC(this.ID);
                if (entradaValores != null) entradaValores.Delete();

                tran.IsValid = true;
            }
        }
        
        private HistoricoAutorizacaoCompra GetHistorico(int id_servidor, StatusAutorizacaoCompraEnum novoStatus)
        {
            HistoricoAutorizacaoCompra historico = new HistoricoAutorizacaoCompra();
            historico.Data = DateTime.Now;
            historico.AutorizacaoCompra = this;
            historico.Descricao = string.Format("{0} -> {1}", this.Status.Descricao, Util.GetDescription(novoStatus));
            historico.Servidor = Servidor.Get(id_servidor);
            return historico;
        }
		#endregion
		
		#region Pagamento
		public  virtual  void RegistrarPagamento(int id_servidor, decimal valor, decimal valorImposto, DateTime data, string nf)
		{
		    if(ValorPago + valor + valorImposto > ValorTotal)
		        throw new Exception("O valor pago não pode ser maior que o valor total da AC.");
		    AutorizacaoCompraPagamento pagamento = new AutorizacaoCompraPagamento();
		    pagamento.AutorizacaoCompra = this;
		    pagamento.Data = data;
		    pagamento.NumeroNotaFiscal = nf;
		    pagamento.Servidor = Business.Servidor.Get(id_servidor);
		    pagamento.Valor = valor;
		    pagamento.ValorImposto = valorImposto;
		    
		    using(TransactionBlock tran = new TransactionBlock())
		    {
		        pagamento.Save();

                if (ValorPago + valor + valorImposto == ValorTotal)
		        {
		            this.FlagPago = true;
		            base.Save();
		        }
		        tran.IsValid = true;
		    }
		    _pagamentos.Add(pagamento);
		}
		#endregion
		
		#region Entrada Itens

        public static List<AutorizacaoCompra> SelectEntradasPendentes()
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"select a from AutorizacaoCompra a inner join fetch a.Fornecedor f                 
			where a.Status.ID = :id_status
			order by a.ID");

            query.SetInt32("id_status", Convert.ToInt32(StatusAutorizacaoCompraEnum.AguardandoEntregaMercadoria));
            return (List<AutorizacaoCompra>)query.List<AutorizacaoCompra>();
        }

        public virtual List<PedidoCotacaoItem> GetItensEntradaPendente()
        {
            List<PedidoCotacaoItem> list = new List<PedidoCotacaoItem>();
            foreach (PedidoCotacaoItem item in _itens)
            {
                if(!item.FlagRecebido)
                    list.Add(item);
            }
            return list;
        }
        
		public virtual void RegistrarEntradaItens(int id_servidor, List<int> list)
		{
		    Business.Servidor servidor = Business.Servidor.Get(id_servidor);
		    using(TransactionBlock tran = new TransactionBlock())
		    {
		        foreach (int id_item in list)
		        {
		            PedidoCotacaoItem item = _itens.Find(id_item);

		            item.FlagRecebido = true;
		            item.Save();

                    //ignorar entrada dos itens q sao PM direto
		            decimal quantidade = 0;
		            foreach (PedidoObtencaoItem itemObtencao in item.ItensObtencao)
		            {
		                if(itemObtencao.PedidoObtencao.TipoPedido == TipoPedido.PedidoMaterial)
                            continue;
		                quantidade += itemObtencao.Quantidade;
		            }

                    if (quantidade > 0)
                    {
                        MovimentoEstoque movimento = new MovimentoEstoque();
                        movimento.Data = DateTime.Today;
                        movimento.Material = item.ServicoMaterial;
                        movimento.OrigemMaterial = OrigemMaterial.PEP;
                        movimento.TipoMovimento = TipoMovimento.Entrada;
                        movimento.TipoOperacaoEstoque = TipoOperacaoEstoque.Normal;
                        movimento.PedidoCotacaoItem = item;
                        movimento.Quantidade = quantidade;
                        movimento.Save();
                    }
		        }

                if(TodosOsItensEntregues())
                    IrParaProximoStatus(id_servidor, null);
                    
		        tran.IsValid = true;
		    }
		}
		
		private bool TodosOsItensEntregues()
		{
		    foreach (PedidoCotacaoItem item in _itens)
		    {
		        if(!item.FlagRecebido)
		            return false;
		    }

		    return true;
		}

		#endregion
        
        #region Limite de Compras por Fornecedor

        public static SaldoFornecedor GetSaldoComprasUtilizado(int id_fornecedor, int id_tipoCompra, int ano)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"select i from PedidoCotacaoItem i 
                inner join fetch i.AutorizacaoCompra a
                inner join fetch a.Status s
			where a.Fornecedor.ID = :id_fornecedor
			and a.TipoCompra.ID = :id_tipoCompra 
			and dbo.GetYear(a.DataEmissao) = :ano
			and s.ID != :statusCancelado		
			order by a.ID");

            query.SetInt32("statusCancelado", Convert.ToInt32(StatusAutorizacaoCompraEnum.Cancelado));
            query.SetInt32("id_fornecedor", id_fornecedor);
            query.SetInt32("id_tipoCompra", id_tipoCompra);
            query.SetInt32("ano", ano);
            
            SaldoFornecedor saldo = new SaldoFornecedor(Business.TipoCompra.Get(id_tipoCompra));
            IList<PedidoCotacaoItem> itens = query.List<PedidoCotacaoItem>();
            foreach (PedidoCotacaoItem item in itens)
            {
                saldo.ValorUtilizadoTotal += item.ValorTotal;

                if (item.AutorizacaoCompra.Status.ID > Convert.ToInt32(StatusAutorizacaoCompraEnum.AguardandoAprovacaoComandanteGeral))
                    saldo.ValorUtilizadoReal += item.ValorTotal;
                else
                    saldo.SaldoARealizar += item.ValorTotal;
            }
            return saldo;
        }

        #endregion

        private List<PedidoObtencao> GetPedidosObtencaoRelacionados()
        {
            List<PedidoObtencao> list = new List<PedidoObtencao>();
            foreach (PedidoCotacaoItem item in _itens)
            {
                foreach (PedidoObtencaoItem obtencaoItem in item.ItensObtencao)
                {
                    if(!list.Contains(obtencaoItem.PedidoObtencao))
                        list.Add(obtencaoItem.PedidoObtencao);
                }
            }
            return list;
        }

	    public static DataSet SelectEquiparacaoPreco(int id_celula, int id_cliente, DateTime dataInicio, DateTime dataFim, int id_material)
	    {
            SQLHelper helper = new SQLHelper();
            object[] param = new object[6];
            param[1] = NullHelper.IsZero(id_celula);
            param[2] = NullHelper.IsZero(id_cliente);
            param[3] = NullHelper.IsNull(dataInicio);
            param[4] = NullHelper.IsNull(dataFim);
            param[5] = NullHelper.IsZero(id_material);

            return helper.ExecuteDataSet("AutorizacaoCompra_EquiparacaoPreco", param);
	    }
	}

    //public class SaldoFornecedor
    //{
        
    //    private decimal _saldoARealizar;
    //    private decimal _valorUtilizadoReal;
    //    private decimal _valorUtilizadoTotal;

    //    /// <summary>
    //    /// Considera todas as ACs, menos as canceladas
    //    /// </summary>
    //    public virtual decimal SaldoTotal
    //    {
    //        get { return _tipoCompra.LimiteAnual - _valorUtilizadoTotal; }
    //    }

    //    /// <summary>
    //    /// Considera apenas ACs que não foram aprovadas pelo comandante
    //    /// </summary>
    //    public virtual decimal SaldoARealizar
    //    {
    //        get { return _saldoARealizar; }
    //        set { _saldoARealizar = value; }
    //    }
    //    /// <summary>
    //    /// Considera apenas ACs que ja foram aprovadas pelo comandante
    //    /// </summary>
    //    public virtual decimal SaldoReal
    //    {
    //        get { return _tipoCompra.LimiteAnual - _valorUtilizadoReal; }
    //    }

    //    /// <summary>
    //    /// Considera apenas ACs que ja foram aprovadas pelo comandante
    //    /// </summary>
    //    public virtual decimal ValorUtilizadoReal
    //    {
    //        get { return _valorUtilizadoReal; }
    //        set { _valorUtilizadoReal = value; }
    //    }

    //    /// <summary>
    //    /// Considera todas as ACs, menos as canceladas
    //    /// </summary>
    //    public virtual decimal ValorUtilizadoTotal
    //    {
    //        get { return _valorUtilizadoTotal; }
    //        set { _valorUtilizadoTotal = value; }
    //    }

    //    private TipoCompra _tipoCompra;
    //    public SaldoFornecedor(TipoCompra tipoCompra)
    //    {
    //        _tipoCompra = tipoCompra;
    //    }
    //}
}

