using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using NHibernate;
using Shared.Common;
using Shared.DataAccessHelper;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
    public partial class PedidoObtencao : BusinessObject<PedidoObtencao>, IPedidoObtencao	
	{
		#region Private Members

		private Servidor _servidor; 
		private StatusPedidoObtencao _status; 
		private bool _flagpedidoobtencao; 
		private int _numero; 
		private Celula _celula; 
		private DateTime _dataemissao; 
		private DelineamentoOrcamento _delineamentoorcamento;
        private string _aplicacao;
	    private string _observacao;
	    private TipoPedido _tipoPedido;
	    private TipoPedidoObtencao _tipoPedidoObtencao;
	    private MotivoCancelamento _motivoCancelamento;
        private OrigemPO _origemPO;
        private bool _isValidar;

		#endregion
		
		#region Default ( Empty ) Class Constuctor
		
        /// <summary>
		/// default constructor
		/// </summary>
		public PedidoObtencao()
		{
			_servidor =  null; 
			_status =  null; 
			_flagpedidoobtencao = false; 
			_numero = 0; 
			_celula =  null; 
			_dataemissao = DateTime.MinValue; 
			_delineamentoorcamento =  null;
		    _tipoPedido = Business.TipoPedido.PedidoObtencao;
		    _tipoPedidoObtencao = Business.TipoPedidoObtencao.MaterialServico;
            _isValidar = true;

		    _itens = new CustomList<PedidoObtencaoItem>();
		    _historico = new CustomList<HistoricoPedidoObtencao>();
            _itensPEPRodizio = new CustomList<PedidoObtencaoItemPEPRodizio>();
            _itensCancelados = new CustomList<PedidoObtencaoItemCancelado>();
            _pagamentos = new CustomList<PedidoObtencaoPagamento>();
            _empenhos = new CustomList<PedidoObtencaoEmpenho>();
            Documentos = new CustomList<PedidoObtencaoDocumento>();
		}

		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties

        public virtual Fornecedor FornecedorCotacao1 { get; set; }
        public virtual Fornecedor FornecedorCotacao2 { get; set; }
        public virtual Fornecedor FornecedorCotacao3 { get; set; }
        public virtual Fornecedor FornecedorCotacao4 { get; set; }

        public virtual Modalidade Modalidade { get; set; }
        public virtual TipoCompra TipoCompra { get; set; }
        public virtual NaturezaDespesa NaturezaDespesa { get; set; }
        public virtual bool FlagPago { get; set; }
        public virtual Licitacao Licitacao { get; set; }
        public virtual Projeto Projeto { get; set; }
        public virtual string NumeroEmpenho { get; set; }
        public virtual bool FlagACEmitida { get; set; }
        public virtual FonteRecurso FonteRecurso { get; set; }
        public virtual string NumeroLancamento { get; set; }
        public virtual string OrdemBancaria { get; set; }
        public virtual PTRES PTRES { get; set; }
        public virtual string CodigoGestao { get; set; }
        public virtual string Lista { get; set; }
        public virtual Fornecedor Fornecedor { get; set; }
        public virtual ICustomList<PedidoObtencaoDocumento> Documentos { get; set; }
        public virtual string NomeRecebedorEmpenho { get; set; }
        public virtual string TelefoneRecebedorEmpenho { get; set; }
        public virtual string PTRESS { get; set; }

        public virtual MotivoCancelamento MotivoCancelamento
        {
            get { return _motivoCancelamento; }
            set { _motivoCancelamento = value; }
        }
        public virtual TipoPedidoObtencao TipoPedidoObtencao
        {
            get { return _tipoPedidoObtencao; }
            set { _tipoPedidoObtencao = value; }
        }
        
        public virtual TipoPedido TipoPedido
        {
            get { return _tipoPedido; }
            set { _tipoPedido = value; }
        }
        
        public virtual string Observacao
        {
            get { return _observacao; }
            set
            {
                if (value != null)
                    if (value.Length > 600)
                        throw new ArgumentOutOfRangeException("Invalid value for Observacao", value, value.ToString());

                _observacao = value;
            }
        }
        
        public virtual string Aplicacao
        {
            get { return _aplicacao; }
            set
            {
                if (value != null)
                    if (value.Length > 2000)
                        throw new ArgumentOutOfRangeException("Invalid value for Aplicacao", value, value.ToString());

                _aplicacao = value;
            }
        }
        	
		public virtual Servidor Servidor
		{
			get { return _servidor; }
			set { _servidor = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual StatusPedidoObtencao Status
		{
			get { return _status; }
			set { _status = value; }
		}
			
		/// <summary>
		/// Define se o PO já está na fase em que é considerado um PO
		/// </summary>		
		public virtual bool FlagPedidoObtencao
		{
			get { return _flagpedidoobtencao; }
			set { _flagpedidoobtencao = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual int Numero
		{
			get { return _numero; }
			set { _numero = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual Celula Celula
		{
			get { return _celula; }
			set { _celula = value; }
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
		public virtual DelineamentoOrcamento DelineamentoOrcamento
		{
			get { return _delineamentoorcamento; }
			set { _delineamentoorcamento = value; }
		}

        public virtual OrigemPO OrigemPO
        {
            get { return _origemPO; }
            set { _origemPO = value; }
        }

        public virtual bool IsValidar
        {
            get { return _isValidar; }
            set { _isValidar = value; }
        }
        
		#endregion 
			
        #region Collections

        private ICustomList<PedidoObtencaoItem> _itens;
        private ICustomList<HistoricoPedidoObtencao> _historico;
        private ICustomList<PedidoObtencaoItemPEPRodizio> _itensPEPRodizio;
        private ICustomList<PedidoObtencaoItemCancelado> _itensCancelados;
        private ICustomList<PedidoObtencaoPagamento> _pagamentos;
        private ICustomList<PedidoObtencaoEmpenho> _empenhos;


        public virtual ICustomList<PedidoObtencaoItemCancelado> ItensCancelados
        {
            get { return _itensCancelados; }
            set { _itensCancelados = value; }
        }
        public virtual ICustomList<PedidoObtencaoItemPEPRodizio> ItensPEPRodizio
        {
            get { return _itensPEPRodizio; }
            set { _itensPEPRodizio = value; }
        }
        
        public virtual ICustomList<HistoricoPedidoObtencao> Historico
        {
            get { return _historico; }
            set { _historico = value; }
        }

        public virtual ICustomList<PedidoObtencaoItem> Itens
        {
            get { return _itens; }
            set { _itens = value; }
        }

        public virtual ICustomList<PedidoObtencaoPagamento> Pagamentos
        {
            get { return _pagamentos; }
            set { _pagamentos = value; }
        }

        public virtual ICustomList<PedidoObtencaoEmpenho> Empenhos
        {
            get { return _empenhos; }
            set { _empenhos = value; }
        }
        #endregion

        #region Advanced Properties

        public virtual string CodigoComAno
        {
            get { return string.Format("{0}/{1}", _numero, _dataemissao.Year); }
        }

        public virtual int ID_PedidoObtencao
        {
            get { return this.ID; }
        }

        public virtual decimal ValorTotal
        {
            get
            {
                decimal valor = 0;
                foreach (PedidoObtencaoItem item in _itens)
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
                return _status == null ||
                        _status.StatusPedidoObtencaoEnum == StatusPedidoObtencaoEnum.NaoEnviado ||
                        _status.StatusPedidoObtencaoEnum == StatusPedidoObtencaoEnum.Reprovado;
            }
        }

        public virtual HistoricoPedidoObtencao UltimoHistorico
        {
            get
            {
                if (Historico.Count > 0)
                    return Historico[Historico.Count - 1];
                else
                    return null;
            }
        }
       
	    public virtual bool ApenasServicos
	    {
	        get
	        {
	            bool apenasServicos = true;
	            foreach (PedidoObtencaoItem item in _itens)
	            {
	                if(item.ServicoMaterial.TipoServicoMaterial == TipoServicoMaterial.Material)
	                {
	                    apenasServicos = false;
	                    break;
	                }
	            }
	            return apenasServicos;
	        }
	    }

        public virtual string TipoPedidoSigla
        {
            get
            {
                if(_origemPO == OrigemPO.Direto)
                    return this._tipoPedido == TipoPedido.PedidoObtencao ? "ACD" : "PMD";
                else if(_origemPO == OrigemPO.GastoExtraPS)
                    return this._tipoPedido == TipoPedido.PedidoObtencao ? "ACGE" : "PMGE";
                else
                    return "ACPS";
            }
        }

	    public virtual bool IsPOGE
	    {
            get { return TipoPedidoSigla == "ACGE"; }
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

	    public virtual decimal ValorPago
	    {
	        get { return _pagamentos.Sum(p => p.Valor); }
	    }

        public virtual decimal ValorPagoFinalizado
        {
            get { return _pagamentos.Where(p => p.Status.StatusPedidoObtencaoEnum == StatusPedidoObtencaoEnum.Finalizado).Sum(p => p.Valor); }
        }

        public virtual string DescricaoStatus
        {
            get { return this.Status.Descricao; }
        }
	    #endregion
		
		#region Public Methods

        public static List<PedidoObtencao> Select(string texto, DateTime dataInicio, DateTime dataFim, int id_status, int id_celula, string aplicacao, 
            TipoPedido? tipoPedido, OrigemPO? origemPO, int ano)
        {

            string condicaoStatus = "";
            if (id_status == 0)
                condicaoStatus = "";
            else if (id_status == Int32.MinValue || id_status != Convert.ToInt32(StatusPedidoObtencaoEnum.Cancelado) && id_status != Convert.ToInt32(StatusPedidoObtencaoEnum.Reprovado))
                condicaoStatus = string.Format("and s.ID NOT IN ({0},{1})", Convert.ToInt32(StatusPedidoObtencaoEnum.Cancelado), Convert.ToInt32(StatusPedidoObtencaoEnum.Reprovado));

            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
                string.Format(
            @"from PedidoObtencao p
                inner join fetch p.Status s
                inner join fetch p.Celula c
			where p.Numero like :texto
			and dbo.DateIsInBetween(p.DataEmissao, :dataInicio, :dataFim) = 1
			and s.ID = IsNull(:id_status, s.ID)
            and c.ID = IsNull(:id_celula, c.ID)		
            and dbo.GetYear(p.DataEmissao) = IsNull(:ano, dbo.GetYear(p.DataEmissao))	  			
            and p.Aplicacao like :aplicacao
            and p.TipoPedido = IsNull(:tipoPedido, p.TipoPedido)
            and p.OrigemPO = IsNull(:origemPO, p.OrigemPO)
            {0}
			order by p.Numero", condicaoStatus));

            query.SetString("texto", string.Format("%{0}%", texto));
            query.SetString("aplicacao", string.Format("%{0}%", aplicacao));
            query.SetParameter("dataInicio", BusinessHelper.IsNull(dataInicio), NHibernateUtil.DateTime);
            query.SetParameter("dataFim", BusinessHelper.IsNull(dataFim), NHibernateUtil.DateTime);
            query.SetParameter("id_status", BusinessHelper.IsNullOrZero(id_status), NHibernateUtil.Int32);
            query.SetParameter("id_celula", BusinessHelper.IsNullOrZero(id_celula), NHibernateUtil.Int32);
            query.SetParameter("tipoPedido", BusinessHelper.IsNullOrZero( tipoPedido.HasValue ? Convert.ToInt32(tipoPedido) : 0), NHibernateUtil.Int32);
            query.SetParameter("origemPO", BusinessHelper.IsNullOrZero(origemPO.HasValue ? Convert.ToInt32(origemPO) : 0), NHibernateUtil.Int32);
            query.SetParameter("ano", BusinessHelper.IsNullOrZero(ano), NHibernateUtil.Int32);
            return (List<PedidoObtencao>)query.List<PedidoObtencao>();
        }

        public static List<PedidoObtencao> Select(int id_celula, int id_status, DateTime dataInicio, DateTime dataFim, int id_tipoPedido, bool? flagDireto, string numeroPO, int ano,
            int id_servidor, int id_deparamento)
        {
            string condicaoFlagDireto = "";
            if (flagDireto.HasValue && flagDireto.Value)
                condicaoFlagDireto = " and p.DelineamentoOrcamento IS NULL ";
            else if(flagDireto.HasValue)
                condicaoFlagDireto = " and p.DelineamentoOrcamento IS NOT NULL ";

            string condicaoStatus = "";
            if (id_status == 0)
                condicaoStatus = "";
            else if (id_status == Int32.MinValue || id_status != Convert.ToInt32(StatusPedidoObtencaoEnum.Cancelado) && id_status != Convert.ToInt32(StatusPedidoObtencaoEnum.Reprovado))
                condicaoStatus = string.Format("and status.ID NOT IN ({0},{1})", Convert.ToInt32(StatusPedidoObtencaoEnum.Cancelado), Convert.ToInt32(StatusPedidoObtencaoEnum.Reprovado));

            string condicaoDepartamento = "";
            if (id_deparamento > 0)
            {
                Celula departamento = Business.Celula.Get(id_deparamento);
                condicaoDepartamento = string.Format(" AND dbo.celulapertenceadepartamento(p.Celula.Codigo, '{0}') = 1", departamento.Codigo);
            }

            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(string.Format(
            @"from PedidoObtencao p 
                inner join fetch p.Servidor s
                inner join fetch p.Status status
                inner join fetch p.Celula c                
			where status.ID = IsNull(:id_status, status.ID)
			and c.ID = IsNull(:id_celula, c.ID)
            and s.ID = IsNull(:id_servidor, s.ID)
            and dbo.GetYear(p.DataEmissao) = IsNull(:ano, dbo.GetYear(p.DataEmissao))
            and p.TipoPedido = IsNull(:id_tipoPedido, p.TipoPedido)
			and dbo.DateIsInBetween(p.DataEmissao, :dataInicio, :dataFim) = 1  	
            and p.Numero like :numeroPO		
            
            {0}
            {1}
            {2}
			order by p.Numero", condicaoFlagDireto, condicaoStatus, condicaoDepartamento));

            query.SetString("numeroPO", string.Format("%{0}%", numeroPO));
            query.SetParameter("id_status", BusinessHelper.IsNullOrZero(id_status), NHibernateUtil.Int32);
            query.SetParameter("id_celula", BusinessHelper.IsNullOrZero(id_celula), NHibernateUtil.Int32);
            query.SetParameter("id_servidor", BusinessHelper.IsNullOrZero(id_servidor), NHibernateUtil.Int32);
            query.SetParameter("id_tipoPedido", BusinessHelper.IsNullOrZero(id_tipoPedido), NHibernateUtil.Int32);
            query.SetParameter("dataInicio", BusinessHelper.IsNull(dataInicio), NHibernateUtil.DateTime);
            query.SetParameter("dataFim", BusinessHelper.IsNull(dataFim), NHibernateUtil.DateTime);
            query.SetParameter("ano", BusinessHelper.IsNullOrZero(ano), NHibernateUtil.Int32);
            return (List<PedidoObtencao>)query.List<PedidoObtencao>();
        }

        /// <summary>
        /// Seleciona os pedidos que estao pendentes da ação de um servidor
        /// </summary>
        public static List<IPedidoObtencao> Select(int id_servidor, int id_status)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();

            // QUERY 1 - Seleciona os POs por divisão
            IQuery query = session.CreateQuery(
            @"select distinct p from PedidoObtencao p 
                inner join fetch p.Status s 
                inner join s.Responsaveis resp
			where resp.ID = :id_servidor
			and s.FlagVinculoPorDivisao = 0		
			and s.ID = IsNull(:id_status, s.ID)
			order by p.Numero");
            //and s.ID <= :id_statusAguardandoEntregaExecucao
            //and s.ID != :id_statusVerificacaoPaiol            

            // QUERY 2 - Seleciona os POs por divisão
            IQuery query2 = session.CreateQuery(
            @"select distinct p from PedidoObtencao p inner join p.Status s 
                inner join s.ResponsaveisDivisao divisao
			where divisao.Servidor.ID = :id_servidor
			and divisao.Celula.ID = p.Celula.ID
			and s.FlagVinculoPorDivisao = 1
			and s.ID = IsNull(:id_status, s.ID)
			order by p.Numero");

            //and s.ID != :id_statusVerificacaoPaiol
//			and s.ID != :id_statusAprovacaoDepartamento            
//            IQuery query2 = session.CreateQuery(
//            @"select distinct p from PedidoObtencao p inner join p.Status s 
//                inner join s.ResponsaveisDivisao divisao
//			where 
//            ((divisao.Servidor.ID = :id_servidor
//			and divisao.Celula.ID = p.Celula.ID) OR (p.Servidor.ID = :id_servidor and s.ID != :id_statusAguardandoEnvioEmpenho
//            and s.ID != :id_statusAguardandoEntregaExecucao and s.ID != :id_statusAprovacaoDepartamento and s.ID != :id_statusAprovacaoDivisao ))
//			and s.FlagVinculoPorDivisao = 1
//            and s.ID <= :id_statusAguardandoEntregaExecucao
//			and s.ID = IsNull(:id_status, s.ID)		         
//			order by p.Numero");

            // QUERY 3 - pega os pos com status aguardando verificacao paiol, que nao vieram de pedido servico
            IQuery query3 = session.CreateQuery(
                @"select distinct p from PedidoObtencao p 
                inner join fetch p.Status s 
                inner join s.Responsaveis resp
            where resp.ID = :id_servidor
            and s.FlagVinculoPorDivisao = 0									
            and s.ID = IsNull(:id_status, s.ID)
            and p.OrigemPO != :origemPedidoServico
            order by p.Numero");
            //and s.ID = :id_statusAguardandoAprovacaoEncarregadoDivisao

            // QUERY 4 - pega os pos com status nao enviado apenas para o cotador
            IQuery query4 = session.CreateQuery(
                @"select distinct p from PedidoObtencao p 
                inner join fetch p.Status s                 
			where p.Servidor.ID = :id_servidor											
			and s.ID = IsNull(:id_status, s.ID)
            and s.ID = :id_statusNaoEnviado                    
			order by p.Numero");

            // QUERY 5 - pega os pos com ????
            IQuery query5 = session.CreateQuery(
              @"select distinct p from PedidoObtencaoPagamento p 
                 inner join fetch p.PedidoObtencao po              
                 inner join fetch p.Status s
                 inner join s.Responsaveis resp                                    
			where resp.ID = :id_servidor									
			and s.ID = IsNull(:id_status, s.ID)            
            and s.FlagVinculoPorDivisao = 0	
            and s.ID > :id_statusAguardandoEntregaExecucao            
            and s.ID < :id_statusFinalizado
			order by po.Numero");

            // Comentei a query6 conforme email dia 10/11/2011
            //*No PO / etapa de aguardando envio empenho – esta aparecendo tambem para os cotadores, tem de aparecer nos pendentes apenas para a divisão/servidor.
            //            IQuery query6 = session.CreateQuery(
            //             @"select distinct p from PedidoObtencao p 
            //                inner join fetch p.Status s                
            //			where ((p.Servidor.ID = :id_servidor and p.DelineamentoOrcamento IS NULL) OR p.DelineamentoOrcamento.ServidorCotador.ID = :id_servidor)										
            //			and s.ID = IsNull(:id_status, s.ID)
            //            and s.ID = :id_statusAguardandoEnvioEmpenho            
            //			order by p.Numero");
            //            IQuery query7 = session.CreateQuery(
            //            @"select distinct p from PedidoObtencao p 
            //                inner join fetch p.Status s                
            //			where p.DelineamentoOrcamento.ServidorCotador.ID = :id_servidor													
            //            and s.ID = :id_statusImprimirEnviarIntendência
            //			order by p.Numero");
            
            query.SetParameter("id_status", BusinessHelper.IsNullOrZero(id_status), NHibernateUtil.Int32);
            query.SetInt32("id_servidor", id_servidor);
            //query.SetInt32("id_statusVerificacaoPaiol", Convert.ToInt32(StatusPedidoObtencaoEnum.AguardandoVerificacaoPaiol));
            //query.SetInt32("id_statusAguardandoEntregaExecucao", Convert.ToInt32(StatusPedidoObtencaoEnum.AguardandoEntregaMercadoria));
            //query.SetInt32("id_statusImprimirEnviarIntendência", Convert.ToInt32(StatusPedidoObtencaoEnum.ImprimirEnviarIntendência));
            //query.SetInt32("id_statusAguardandoAprovacaoEncarregadoDivisao", Convert.ToInt32(StatusPedidoObtencaoEnum.AguardandoAprovacaoEncarregadoDivisao));
            query2.SetParameter("id_status", BusinessHelper.IsNullOrZero(id_status), NHibernateUtil.Int32);
            query2.SetInt32("id_servidor", id_servidor);
            //query2.SetInt32("id_statusAprovacaoDepartamento", Convert.ToInt32(StatusPedidoObtencaoEnum.AguardandoAprovacaoChefeDepartamento_Departamento));
            //query2.SetInt32("id_statusVerificacaoPaiol", Convert.ToInt32(StatusPedidoObtencaoEnum.AguardandoVerificacaoPaiol));
            //query2.SetParameter("id_status", BusinessHelper.IsNullOrZero(id_status), NHibernateUtil.Int32);
            //query2.SetInt32("id_servidor", id_servidor);
            //query2.SetInt32("id_statusAguardandoEntregaExecucao", Convert.ToInt32(StatusPedidoObtencaoEnum.AguardandoEntregaMercadoria));
            //query2.SetInt32("id_statusAprovacaoDepartamento", Convert.ToInt32(StatusPedidoObtencaoEnum.AguardandoAprovacaoEncarregadoDepartamento));
            //query2.SetInt32("id_statusAprovacaoDivisao", Convert.ToInt32(StatusPedidoObtencaoEnum.AguardandoAprovacaoEncarregadoDivisao));
            //query2.SetInt32("id_statusImprimirEnviarIntendência", Convert.ToInt32(StatusPedidoObtencaoEnum.ImprimirEnviarIntendência));
            //query2.SetInt32("id_statusAguardandoEnvioEmpenho", Convert.ToInt32(StatusPedidoObtencaoEnum.AguardandoEnvioEmpenho));
            query3.SetParameter("id_status", BusinessHelper.IsNullOrZero(id_status), NHibernateUtil.Int32);
            query3.SetInt32("id_servidor", id_servidor);
            //query3.SetInt32("id_statusAguardandoAprovacaoEncarregadoDivisao", Convert.ToInt32(StatusPedidoObtencaoEnum.AguardandoVerificacaoPaiol));
            query3.SetInt32("origemPedidoServico", Convert.ToInt32(Business.OrigemPO.PS));
            query4.SetParameter("id_status", BusinessHelper.IsNullOrZero(id_status), NHibernateUtil.Int32);
            query4.SetInt32("id_servidor", id_servidor);
            query4.SetInt32("id_statusNaoEnviado", Convert.ToInt32(StatusPedidoObtencaoEnum.NaoEnviado));
            query5.SetParameter("id_status", BusinessHelper.IsNullOrZero(id_status), NHibernateUtil.Int32);
            query5.SetInt32("id_servidor", id_servidor);
            query5.SetInt32("id_statusAguardandoEntregaExecucao", Convert.ToInt32(StatusPedidoObtencaoEnum.AguardandoEntregaMercadoria));
            query5.SetInt32("id_statusFinalizado", Convert.ToInt32(StatusPedidoObtencaoEnum.Finalizado));

            // query6.SetParameter("id_status", BusinessHelper.IsNullOrZero(id_status), NHibernateUtil.Int32);
            // query6.SetInt32("id_servidor", id_servidor);
            //// query6.SetInt32("id_statusAguardandoEntregaExecucao", Convert.ToInt32(StatusPedidoObtencaoEnum.AguardandoEntregaMercadoria));
            // query6.SetInt32("id_statusAguardandoEnvioEmpenho", Convert.ToInt32(StatusPedidoObtencaoEnum.AguardandoEnvioEmpenho));
            // query7.SetInt32("id_servidor", id_servidor);
            //query7.SetInt32("id_statusImprimirEnviarIntendência", Convert.ToInt32(StatusPedidoObtencaoEnum.ImprimirEnviarIntendência));

            List<PedidoObtencao> list1 = (List<PedidoObtencao>)query.List<PedidoObtencao>();
            List<PedidoObtencao> list2 = (List<PedidoObtencao>)query2.List<PedidoObtencao>();
            List<PedidoObtencao> list3 = (List<PedidoObtencao>)query3.List<PedidoObtencao>();
            List<PedidoObtencao> list4 = (List<PedidoObtencao>)query4.List<PedidoObtencao>();
            //  List<PedidoObtencao> list6 = (List<PedidoObtencao>)query6.List<PedidoObtencao>();
            // List<PedidoObtencao> list7 = (List<PedidoObtencao>)query7.List<PedidoObtencao>();
            List<PedidoObtencaoPagamento> list5 = (List<PedidoObtencaoPagamento>)query5.List<PedidoObtencaoPagamento>();

            list1.AddRange(list2.Where(p => !(p.Status.StatusPedidoObtencaoEnum == StatusPedidoObtencaoEnum.AguardandoEntregaExecucao && p.ValorTotal == p.ValorPago)
                && !(p.Status.StatusPedidoObtencaoEnum == StatusPedidoObtencaoEnum.AguardandoLiquidacao && p.ValorTotal == p.ValorPago)
                ));
            list1.AddRange(list3);
            list1.AddRange(list4);
            // list1.AddRange(list7);

            //nao mostra os POs aguardando entrega execucao caso todos os pagamentos ja tenham sido criados
            // list1.AddRange(list6.Where(p => p.ValorPago < p.ValorTotal));

            if (id_status <= 0 || id_status == Convert.ToInt32(StatusPedidoObtencaoEnum.AguardandoAprovacaoEncarregadoDepartamento) || id_status == Convert.ToInt32(StatusPedidoObtencaoEnum.AguardandoCreditoEmpenho))
                list1.AddRange(SelectPorDepartamento(id_servidor, id_status));
            //if (id_status <= 0 || id_status == Convert.ToInt32(StatusPedidoObtencaoEnum.AguardandoAprovacaoEncarregadoDivisao))
            //    list1.AddRange(SelectPOGerentePS(id_servidor));

            List<IPedidoObtencao> listaFinal = list1.ConvertAll(delegate(PedidoObtencao p1) { return (IPedidoObtencao)p1; });

            listaFinal.AddRange(list5.ConvertAll(delegate(PedidoObtencaoPagamento p1) { return (IPedidoObtencao)p1; }));

            listaFinal.Sort(new Comparison<IPedidoObtencao>(delegate(IPedidoObtencao p1, IPedidoObtencao p2)
            {
                return p1.Numero.CompareTo(p2.Numero);
            }));

            return listaFinal.Distinct().ToList();
        }

        /// <summary>
        /// Seleciona PO's que estao com o status AguardandoAprovacaoChefeDepartamento_Departamento e
        /// cujas as divisoes dentro do departamento do servidor
        /// </summary>
        /// <param name="id_servidor"></param>
        /// <returns></returns>
        private static List<PedidoObtencao> SelectPorDepartamento(int id_servidor)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"select distinct p from PedidoObtencao p inner join p.Status s 
                inner join s.ResponsaveisDivisao divisao
			where divisao.Servidor.ID = :id_servidor												
			and s.ID = :id_status
			and dbo.CelulaPertenceADepartamento(p.Celula.Codigo, divisao.Celula.Codigo) = 1
			order by p.Numero");

            query.SetInt32("id_status", Convert.ToInt32(StatusPedidoObtencaoEnum.AguardandoAprovacaoChefeDepartamento_Departamento));
            query.SetInt32("id_servidor", id_servidor);

            return (List<PedidoObtencao>)query.List<PedidoObtencao>();
        }

        /// <summary>
        /// Seleciona PO's que estao com o status AguardandoAprovacaoChefeDepartamento_Departamento e
        /// cujas as divisoes dentro do departamento do servidor
        /// </summary>
        /// <param name="id_servidor"></param>
        /// <returns></returns>
        private static List<PedidoObtencao> SelectPorDepartamento(int id_servidor, int id_status)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"select distinct p from PedidoObtencao p inner join p.Status s 
                inner join s.ResponsaveisDivisao divisao
			where divisao.Servidor.ID = :id_servidor												
			and (s.ID = :id_status2 OR s.ID = :id_status3)
            and s.ID = IsNull(:id_status, s.ID)
			and dbo.CelulaPertenceADepartamento(p.Celula.Codigo, divisao.Celula.Codigo) = 1
			order by p.Numero");

            query.SetInt32("id_status2", Convert.ToInt32(StatusPedidoObtencaoEnum.AguardandoAprovacaoEncarregadoDepartamento));
            query.SetInt32("id_status3", Convert.ToInt32(StatusPedidoObtencaoEnum.AguardandoCreditoEmpenho));
            query.SetParameter("id_status", BusinessHelper.IsNullOrZero(id_status), NHibernateUtil.Int32);
            query.SetInt32("id_servidor", id_servidor);

            return (List<PedidoObtencao>) query.List<PedidoObtencao>();
        }

//        /// <summary>
//        /// Seleciona PO's que estao na etapa Aguardando Verificação do Paiol
//        /// e que estão vinculados a PS, pois nesse caso o responsável é o gerente do PS
//        /// 
//        /// </summary>
//        private static List<PedidoObtencao> SelectPOGerentePS(int id_servidor)
//        {
//            ISession session = NHibernateSessionManager.Instance.GetSession();
//            IQuery query = session.CreateQuery(
//            @"select distinct p from PedidoObtencao p 
//                inner join fetch p.Status s 
//                inner join fetch p.DelineamentoOrcamento do
//                inner join fetch do.PedidoServico ps
//			where ps.ServidorGerente.ID = :id_servidor			
//			and s.ID = :id_statusAguardandoAprovacaoEncarregadoDivisao
//            and p.OrigemPO = :origemPedidoServico
//			order by p.Numero");

//            query.SetInt32("id_statusAguardandoAprovacaoEncarregadoDivisao", Convert.ToInt32(StatusPedidoObtencaoEnum.AguardandoAprovacaoEncarregadoDivisao));
//            query.SetInt32("id_servidor", id_servidor);
//            query.SetInt32("origemPedidoServico", Convert.ToInt32(Business.OrigemPO.PS));
//            return (List<PedidoObtencao>)query.List<PedidoObtencao>();
//        }
        
        public static List<IPedidoObtencao> SelectPedidosParaAprovacao(int id_servidor, int id_departamento)
        {
            List<IPedidoObtencao> list1 = Select(id_servidor, Convert.ToInt32(StatusPedidoObtencaoEnum.AguardandoAprovacaoEncarregadoDivisao));
            List<IPedidoObtencao> list4 = Select(id_servidor, Convert.ToInt32(StatusPedidoObtencaoEnum.AguardandoAprovacaoEncarregadoDepartamento));
            
            List<IPedidoObtencao> list2 = Select(id_servidor, Convert.ToInt32(StatusPedidoObtencaoEnum.AguardandoAprovacaoAgenteFiscal));
            List<IPedidoObtencao> list3 = Select(id_servidor, Convert.ToInt32(StatusPedidoObtencaoEnum.AguardandoAprovacaoPAR));
            List<IPedidoObtencao> list5 = Select(id_servidor, Convert.ToInt32(StatusPedidoObtencaoEnum.AguardandoAprovacaoOrdenadorDespesa));
            List<IPedidoObtencao> list6 = Select(id_servidor, Convert.ToInt32(StatusPedidoObtencaoEnum.AguardandoAprovacaoSupervisaoCotacao));

            List<IPedidoObtencao> list7 = Select(id_servidor, Convert.ToInt32(StatusPedidoObtencaoEnum.AguardandoAprovacaoChefeDepartamento_Servidor));
            List<IPedidoObtencao> list8 = Select(id_servidor, Convert.ToInt32(StatusPedidoObtencaoEnum.AguardandoAprovacaoChefeDepartamento_Departamento));
            List<IPedidoObtencao> list9 = Select(id_servidor, Convert.ToInt32(StatusPedidoObtencaoEnum.AguardandoAprovacaoOrdenadorDespesa));
            //List<IPedidoObtencao> list10 = Select(id_servidor, Convert.ToInt32(StatusPedidoObtencaoEnum.AguardandoAprovacaoSupervisaoCotacao));
            //bacs: feito ateh aqui
            
            //	40 - Aguardando Aprovação Chefe Dep. (DPCP)
            //	55 - Aguardando Aprovação Chefe Dep.
            //	70 - Aguardando Aprovação do Agente Fiscal
            //	160 - Aguardando Aprovação Comandante

            //List<PedidoObtencao> list5 = SelectPorDepartamento(id_servidor);
            list1.AddRange(list2);
            list1.AddRange(list3);
            list1.AddRange(list4);
            list1.AddRange(list5);
            list1.AddRange(list6);
            list1.AddRange(list7);
            list1.AddRange(list8);
            list1.AddRange(list9);

            if(id_departamento != 0)
            {
                Business.Celula departamento = Business.Celula.Get(id_departamento);
                List<IPedidoObtencao> posRemovidos = new List<IPedidoObtencao>();
                foreach (IPedidoObtencao po in list1)
                {
                    if (!po.Celula.Codigo.StartsWith(departamento.Codigo.Substring(0,1)))
                        posRemovidos.Add(po);
                }
                foreach (IPedidoObtencao po in posRemovidos)
                {
                    list1.Remove(po);
                }
            }

            return list1.Where(p => p.Status.StatusPedidoObtencaoEnum != StatusPedidoObtencaoEnum.AguardandoEntregaExecucao &&
                    p.Status.StatusPedidoObtencaoEnum != StatusPedidoObtencaoEnum.AguardandoEnvioEmpenho).Distinct().ToList();
        }

        public static Dictionary<int, string> List(int id_orcamento)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"from PedidoObtencao p  
			where p.DelineamentoOrcamento.ID = :id_orcamento
			order by p.Numero");

            query.SetInt32("id_orcamento", id_orcamento);
            IList<PedidoObtencao> pos = query.List<PedidoObtencao>();
            Dictionary<int, string> list = new Dictionary<int, string>(pos.Count);
            foreach (PedidoObtencao po in pos)
            {
                list.Add(po.ID, po.CodigoComAno);
            }
            return list;
        }

		#endregion
		
		#region PEP & Rodizio

        public static List<PedidoObtencao> SelectEntradasPendentes()//OrigemMaterial origem
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"select a from PedidoObtencao a inner join fetch a.Fornecedor f                 
			where a.Status.ID = :id_status
			order by a.ID");

            query.SetInt32("id_status", Convert.ToInt32(StatusPedidoObtencaoEnum.AguardandoEntregaMercadoria));
            return (List<PedidoObtencao>)query.List<PedidoObtencao>();
        }
        
        public virtual List<PedidoObtencaoItemPEPRodizio> GetItensEntradaPEPRodizioPendentes(OrigemMaterial origem, int id_celula)
        {
            List<PedidoObtencaoItemPEPRodizio> list = new List<PedidoObtencaoItemPEPRodizio>();

            foreach (PedidoObtencaoItemPEPRodizio item in _itensPEPRodizio)
            {
                if (item.OrigemMaterial == origem && item.Quantidade > item.QuantidadeEntregue && 
                    !item.FlagCancelado &&
                    (id_celula == 0 || (item.Celula != null && id_celula == item.Celula.ID)) )
                {
                    list.Add(item);
                }
            }

            return list;
        }

        //public virtual int SalvarEntradaItensPEPRodizio(int id_servidor, Dictionary<int, decimal> itens, OrigemMaterial origem)
        public virtual int SalvarEntradaItens(int id_servidor, Dictionary<int, decimal> list)//, OrigemMaterial origem
        {
            if (list.Count == 0)
                throw new Exception("Inclua pelo menos um item na entrada.");

            var servidor = Servidor.Get(id_servidor);
            int id_entrada = 0;

            foreach (int id_item in list.Keys)
            {
                var item = _itens.Find(id_item);

                decimal QtdEntradaTotal = item.QuantidadeEntregue;
                QtdEntradaTotal += list[id_item];

                if (QtdEntradaTotal > item.Quantidade)
                    throw new Exception("Quantidade recebida não pode ser maior que a quantidade.");

                //item.FlagRecebido = item.Quantidade == item.QuantidadeEntregue;
                //item.Save();

                //ignorar entrada dos itens q sao PM direto
                if (TipoPedido == TipoPedido.PedidoMaterial) continue;
            }

            using (TransactionBlock tran = new TransactionBlock())
            {
                id_entrada = CriarEntradaMaterial(list, id_servidor, OrigemMaterial.Obtencao);

                VerificaEntradaFinalizada(OrigemMaterial.Obtencao);

                tran.IsValid = true;

                if (TodosOsItensEntregues())
                    IrParaProximoStatus(id_servidor, null);
            }

            return id_entrada;

            //if (itens.Count == 0)
            //    throw new Exception("Inclua pelo menos um item na entrada.");

            //foreach (KeyValuePair<int, decimal> valuePair in itens)
            //{
            //    if (_itensPEPRodizio.Find(valuePair.Key).QuantidadeEntregue + valuePair.Value > _itensPEPRodizio.Find(valuePair.Key).Quantidade)
            //        throw new Exception("A quantidade entregue não pode ser maior que a quantidade original.");
            //}

            //int id_entrada = 0;

            //using (TransactionBlock tran = new TransactionBlock())
            //{
            //    id_entrada = CriarEntradaMaterial(itens, id_servidor, OrigemMaterial.Obtencao);

            //    VerificaEntradaFinalizada(OrigemMaterial.Obtencao);

            //    if (TodosOsItensEntregues())
            //        IrParaProximoStatus(id_servidor, null);

            //    tran.IsValid = true;
            //}

            //return id_entrada;
        }

        private bool TodosOsItensEntregues()
        {
            return _itens.All(item => item.Quantidade == item.QuantidadeEntregue);
        }

        /// <summary>
        /// Verifica se todos os itens ja foram entregues ou cancelados
        /// </summary>
        /// <param name="origem"></param>
	    protected internal virtual void VerificaEntradaFinalizada(OrigemMaterial origem)
	    {
	        bool finalizado = true;

	        foreach (PedidoObtencaoItemPEPRodizio item in _itensPEPRodizio)
	        {
	            if (item.Quantidade > item.QuantidadeEntregue)
	                finalizado = false;
	        }

	        if (finalizado && (_origemPO == OrigemPO.PS || IsPOGE))
	        {
	            if(origem == OrigemMaterial.PEP)
	                this.DelineamentoOrcamento.StatusEntregaMaterialPEP = StatusEntregaMaterial.MaterialEntregue;

	            else if (origem == OrigemMaterial.Rodizio)
	                this.DelineamentoOrcamento.StatusEntregaMaterialRodizio = StatusEntregaMaterial.MaterialEntregue;

	            _delineamentoorcamento.Save();
	        }
	    }

	    private int CriarEntradaMaterial(Dictionary<int, decimal> itens, int id_servidor, OrigemMaterial origem)
	    {
	        int id_entrada;

	        EntradaMaterial entradaMaterial = new EntradaMaterial();

	        entradaMaterial.Data = DateTime.Now;
	        entradaMaterial.OrigemMaterial = origem;
	        entradaMaterial.DelineamentoOrcamento = this._delineamentoorcamento;
	        entradaMaterial.PedidoObtencao = this;
	        entradaMaterial.Servidor = Business.Servidor.Get(id_servidor);
	        entradaMaterial.Save();

	        id_entrada = entradaMaterial.ID;

	        foreach (KeyValuePair<int, decimal > valuePair in itens)
	        {
                EntradaMaterialItem materialItemEntrada = new EntradaMaterialItem();

                materialItemEntrada.EntradaMaterial = entradaMaterial;
                materialItemEntrada.Quantidade = valuePair.Value;
                
	            if(origem == OrigemMaterial.Obtencao)
	            {
                    PedidoObtencaoItem item = _itens.Find(valuePair.Key);
                    item.QuantidadeEntregue += valuePair.Value;
                    item.Save();
                    materialItemEntrada.Material = item.ServicoMaterial;
	            }
	            else
	            {
	                PedidoObtencaoItemPEPRodizio item = _itensPEPRodizio.Find(valuePair.Key);
	                item.QuantidadeEntregue += valuePair.Value;
	                item.Save();
                    materialItemEntrada.Material = item.ServicoMaterial;
	            }
	            
	            materialItemEntrada.Save();

                if (materialItemEntrada.Material.TipoServicoMaterial == TipoServicoMaterial.Material)
                {   
                    MovimentoEstoque.InsertMovimento(materialItemEntrada);
                }
	        }

	        return id_entrada;
	    }

	    #endregion 
	  
	    #region Entrada Obtencao

        public static List<PedidoObtencao> SelectEntradaObtencaoPendente()
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"select distinct p from PedidoObtencao p inner join p.Itens i
            where i.Quantidade > i.QuantidadeEntregue    
            AND p.OrigemPO != :poDireto
            AND p.TipoPedidoObtencao != :tipoReposicaoEstoque        
            and i.OrigemMaterial = :origem");
            
            //AND i.PedidoCotacaoItem IS NOT NULL 
            //AND i.PedidoCotacaoItem.AutorizacaoCompra IS NOT NULL

            query.SetInt32("origem", Convert.ToInt32(OrigemMaterial.Obtencao));
            query.SetInt32("poDireto", Convert.ToInt32(Business.OrigemPO.Direto));
            query.SetInt32("tipoReposicaoEstoque", Convert.ToInt32(Business.TipoPedidoObtencao.ReposicaoEstoque));
            return (List<PedidoObtencao>)query.List<PedidoObtencao>();
        }
        
        public virtual List<PedidoObtencaoItem> GetItensEntradaPendente()
        {
            List<PedidoObtencaoItem> list = new List<PedidoObtencaoItem>();

            foreach (PedidoObtencaoItem item in _itens)
            {
                // nao mostra os itens q nao tem AC
                if (item.OrigemMaterial == OrigemMaterial.Obtencao && item.Quantidade > item.QuantidadeEntregue 
                    //&& item.PedidoCotacaoItem != null && item.PedidoCotacaoItem.AutorizacaoCompra != null
                    )
                {
                    list.Add(item);
                }
            }
            return list;
        }

        public virtual int SalvarEntradaItensObtencao(int id_servidor, Dictionary<int, decimal > itens)
        {
            if (itens.Count == 0)
                throw new Exception("Inclua pelo menos um item na entrada.");

            foreach (KeyValuePair<int, decimal> valuePair in itens)
            {
                if (_itens.Find(valuePair.Key).QuantidadeEntregue + valuePair.Value > _itens.Find(valuePair.Key).Quantidade)
                    throw new Exception("A quantidade entregue não pode ser maior que a quantidade original.");
            }

            int id_entrada = 0;
            using (TransactionBlock tran = new TransactionBlock())
            {
                //if(_origemPO == OrigemPO.PS)
                id_entrada = CriarEntradaMaterial(itens, id_servidor, OrigemMaterial.Obtencao); 

                bool finalizado = true;
                foreach (PedidoObtencaoItem item in _itens)
                {   
                    if (item.Quantidade > item.QuantidadeEntregue)
                        finalizado = false;
                }

                if (finalizado && _origemPO == OrigemPO.PS)
                {   
                    //TODO: dar continuidade ao status do orçamento
                }

                tran.IsValid = true;
            }

            return id_entrada;
        }

	    #endregion
	  
	    #region Designacao Comprador

	    public virtual List<PedidoObtencaoItem> GetItensComprador(bool comComprador)
	    {
	        List<PedidoObtencaoItem> list = new List<PedidoObtencaoItem>();

            foreach (PedidoObtencaoItem item in _itens)
            {
                if (comComprador && item.ServidorRecebimento != null)
                    list.Add(item);

                else if (!comComprador && item.ServidorRecebimento == null)
                    list.Add(item);
            }

	        return list;
	    }
	    
	    public virtual void DesignaComprador(List<int> list, int id_comprador)
	    {
            Servidor comprador = Servidor.Get(id_comprador);

            using (TransactionBlock tran = new TransactionBlock())
            {
                foreach (int id_item in list)
                {
                    PedidoObtencaoItem item = _itens.Find(id_item);
                    item.ServidorRecebimento = comprador;
                    item.Save();
                }

                tran.IsValid = true;
            }
	    }

        public virtual void CancelaDesignacaoComprador(int id_itemPedidoObtencao)
        {
            PedidoObtencaoItem item = _itens.Find(id_itemPedidoObtencao);

            //if (item.PedidoCotacaoItem != null)
            //    throw new Exception("Este item não pode ser removido pois já está ligado a uma cotação.");

            item.ServidorRecebimento = null;
            item.Save();

            //PedidoObtencaoItem item = _itens.Find(id_itemPedidoObtencao);
            //if(item.PedidoCotacaoItem != null)
            //    throw new Exception("Este item não pode ser removido pois já está ligado a uma cotação.");

            //item.Comprador = null;
            //item.Save();
        }

	    #endregion

        #region Salvar

        public override void Save()
        {
            if (this.IsValidar)
               Validar();

            if (!this.IsPersisted)
                CriarNovo();
            else
                base.Save();
        }

        //public void SavePS()
        //{
        //    if (!this.IsPersisted)
        //        CriarNovo();
        //    else
        //        base.Save();
        //}

        //public void AlterarPS()
        //{
        //    base.Save();
        //}

        public virtual void Alterar()
        {
            base.Save();
        }

        private void Validar()
        {
            if (!this.PodeSerAlterado)
                throw new Exception("Este PO não pode ser alterado neste estágio.");

            if(_origemPO == OrigemPO.GastoExtraPS && _delineamentoorcamento == null)
                throw new Exception("Escolha um Pedido de Serviço.");

            if(_origemPO == OrigemPO.Direto)
                _delineamentoorcamento = null;
        }

        private void CriarNovo()
        {
            this.DataEmissao = DateTime.Today;
            this.Status = this.Status != null ? this.Status : StatusPedidoObtencao.Get(StatusPedidoObtencaoEnum.NaoEnviado);

            if (this.Celula.TipoCelula == TipoCelula.Secao)
                this.Celula = this.Celula.GetDivisao();

            using (TransactionBlock tran = new TransactionBlock())
            {
                ISession session = NHibernateSessionManager.Instance.GetSession();
                IQuery query = session.CreateQuery(
                    @"select MAX(p.Numero) from PedidoObtencao p where dbo.GetYear(p.DataEmissao) = :ano");
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

        public static int CriarNovoPOPS(List<PedidoServicoItemOrcamento> itens)
        {

            //TODO: verificar se todos os itens sao de obtencao?
            
            Business.PedidoObtencao po = new PedidoObtencao();
            po.Aplicacao = "Código Interno: " + itens[0].DelineamentoOrcamento.CodigoComAno + "; PS Cliente: " + itens[0].DelineamentoOrcamento.CodigoPedidoCliente + "; Cliente: " + (itens[0].DelineamentoOrcamento.Cliente.Descricao);
            po.Celula = Celula.Get(itens[0].DelineamentoOrcamento.Celula.ID);
            if (po.Celula.TipoCelula == TipoCelula.Secao)
                po.Celula = po.Celula.GetDivisao();
            po.DataEmissao = DateTime.Now;
            po.DelineamentoOrcamento = DelineamentoOrcamento.Get(itens[0].DelineamentoOrcamento.ID);
            po.FlagPedidoObtencao = false;
            po.Servidor = itens[0].DelineamentoOrcamento.ServidorCotador; // Servidor.Get(this._servidor.ID);
            po.Status = StatusPedidoObtencao.Get(StatusPedidoObtencaoEnum.NaoEnviado);
            po.TipoPedido = TipoPedido.PedidoObtencao;
            po.TipoPedidoObtencao = TipoPedidoObtencao.MaterialServico;
            po.OrigemPO = OrigemPO.PS;
            po.Save();

            InserirItensPS(po, itens);

            return po.ID;
        }

	    public static void InserirItensPS(PedidoObtencao po, List<PedidoServicoItemOrcamento> itens)
	    {
	        foreach (PedidoServicoItemOrcamento itemOrcamento in itens)
	        {
	            Business.PedidoObtencaoItem item = new PedidoObtencaoItem();
	            item.ServicoMaterial = itemOrcamento.ServicoMaterial;
	            item.Quantidade = itemOrcamento.Quantidade;
	            //item.Valor = itemOrcamento.Valor;
	            item.OrigemMaterial = itemOrcamento.OrigemMaterial;
	            item.PedidoObtencao = po;
	            item.Especificacao = itemOrcamento.Observacao;
	            //item.Fornecedor = itemOrcamento.Fornecedor;
	            item.Save();
	            po.Itens.Add(item);

	            itemOrcamento.FlagCotadoPO = true;
	            itemOrcamento.Save();
	        }

            if(itens.Count > 0)
            {
                itens[0].DelineamentoOrcamento.PedidoObtencao = po;
                itens[0].DelineamentoOrcamento.Save();
            }

	    }

	    #endregion
        
        #region Workflow

        private void SalvarNovoStatus(int id_servidor, StatusPedidoObtencaoEnum novoStatus, string justificativa, bool reprovado)
        {
            HistoricoPedidoObtencao historico = GetHistorico(id_servidor, novoStatus);
            historico.Justificativa = justificativa;
            historico.FlagReprovado = reprovado;

            this.Status = StatusPedidoObtencao.Get(novoStatus);

            using (TransactionBlock tran = new TransactionBlock())
            {
                historico.Save();
                base.Save();
                this._historico.Add(historico);
                tran.IsValid = true;
            }
        }
        
        public virtual void Enviar(int id_servidor)
        {
            if (this.Status == null)
                throw new Exception("Status não identificado.");

            if (this.Status.StatusPedidoObtencaoEnum != StatusPedidoObtencaoEnum.NaoEnviado
            && this.Status.StatusPedidoObtencaoEnum != StatusPedidoObtencaoEnum.Reprovado)
                throw new Exception("Este pedido já foi enviado.");

            if (_itens.Count == 0)
                throw new Exception("Insira pelo menos um item.");
            
            if (Fornecedor == null && FornecedorCotacao2 == null && FornecedorCotacao3 == null)
                throw new Exception("Insira pelo menos um Fornecedor.");

            int id_tipoMaterial = (int) _itens[0].ServicoMaterial.TipoServicoMaterial;

            foreach (PedidoObtencaoItem item in _itens)
            {
                if(Convert.ToInt32(item.ServicoMaterial.TipoServicoMaterial) != id_tipoMaterial)
                    throw new Exception("O PO não pode conter Tipo de Materiais diferentes.");

                if(item.ValorCotacao2 < item.ValorCotacao1 || item.ValorCotacao3 < item.ValorCotacao1)
                    throw new Exception("O valor 1 deve ser menor que os outros valores cotados.");

                if(this.TipoCompra.TipoCompraEnum == TipoCompraEnum.Licitacao)
                {   
                    if(this.Licitacao.Itens.Where(i => i.Material.ID == item.ServicoMaterial.ID).Count() == 0)
                        throw new Exception(string.Format("O item {0} não está na licitação selecionada.", item.ServicoMaterial.Descricao));

                   LicitacaoItem licitacaoItem =  this.Licitacao.Itens.Where(i => i.Material.ID == item.ServicoMaterial.ID).First();
                    if(licitacaoItem.QuantidadeDisponivel < item.Quantidade)
                        throw new Exception(string.Format("O item {0} não tem saldo disponível na licitação.", item.ServicoMaterial.Descricao));
                }

                //celulas
                if(item.ServicoMaterial.Celulas.Count > 0 && item.ServicoMaterial.Celulas.Where(c => c.ID == this.Celula.ID).Count() == 0)
                {
                    string celulas = string.Join(", ", item.ServicoMaterial.Celulas);
                    throw new Exception(string.Format("O item \"{0}\" só pode estar vinculados às seguintes células: {1} ", item.ServicoMaterial.Descricao, celulas));
                }
            }

            int FornecedorConta = 0;

            FornecedorConta = FornecedorCotacao1 != null ? FornecedorConta + 1 : FornecedorConta;
            FornecedorConta = FornecedorCotacao2 != null ? FornecedorConta + 1 : FornecedorConta;
            FornecedorConta = FornecedorCotacao3 != null ? FornecedorConta + 1 : FornecedorConta;
            FornecedorConta = FornecedorCotacao4 != null ? FornecedorConta + 1 : FornecedorConta;

            //if (_origemPO == OrigemPO.Direto && TipoCompra.TipoCompraEnum != TipoCompraEnum.Licitacao && (FornecedorCotacao2 == null || FornecedorCotacao3 == null))
            //{
            //    throw new Exception("Insira 3 Fornecedores.");
            //}

            if (FornecedorConta < Parametro.Get().NumeroMinimoCotacoesCompra && this.TipoCompra.TipoCompraEnum != TipoCompraEnum.Licitacao)
            {
                throw new Exception("Insira " + Parametro.Get().NumeroMinimoCotacoesCompra  + " Fornecedores.");
            }

            if (this.TipoCompra.TipoCompraEnum == TipoCompraEnum.Licitacao)
            {
                var limiteLicitacao = Parametro.Get().PercentualBloqueioLicitacao/100;
                foreach (PedidoObtencaoItem item in _itens)
                {
                    LicitacaoItem licitacaoItem = this.Licitacao.Itens.Where(i => i.Material.ID == item.ServicoMaterial.ID).First();
                    if(licitacaoItem.QuantidadeUtilizada + item.Quantidade > licitacaoItem.Quantidade * limiteLicitacao)
                    {
                        throw new Exception(string.Format("Limite de {0:P0} da licitação atingido para o item: {1}.", limiteLicitacao, item.ServicoMaterial.Descricao));        
                    }
                }
            }

            //se veio de um PS
            if(_origemPO == OrigemPO.PS || IsPOGE)
            {
                SalvarNovoStatus(id_servidor, StatusPedidoObtencaoEnum.AguardandoAprovacaoChefeDepartamento_Servidor, null, false);
                //SalvarNovoStatus(id_servidor, StatusPedidoObtencaoEnum.AguardandoVerificacaoPaiol, null, false);
            }
            else
            {
                //Se o veio de um departamento nao precisa pasar pela aprovacao da divisao
                if (_celula.TipoCelula == TipoCelula.Departamento)
                    SalvarNovoStatus(id_servidor, StatusPedidoObtencaoEnum.AprovacaoEncDivisão, null, false);
                else
                    SalvarNovoStatus(id_servidor, StatusPedidoObtencaoEnum.AprovacaoEncDivisão, null, false);
                
                //Se for Pedido de Material, colocar PEP como origem dos itens
                if(_tipoPedido == TipoPedido.PedidoMaterial)
                {
                    foreach (PedidoObtencaoItem item in _itens)
                    {
                        item.OrigemMaterial = OrigemMaterial.PEP;
                        item.Save();
                    }
                }
            }
        }

        public virtual void GerarNotaEmpenho(int id_servidor)
        {
            //NumeroEmpenho = numeroNotaEmpenho;

            if(this.Empenhos.Count == 0)
                throw new Exception("Insira pelo menos um empenho.");

            IrParaProximoStatus(id_servidor, null);
        }

        public virtual void FazerBaixa(int id_servidor, decimal valor, string notaFiscal, int id_empenho)
        {
            if(valor <= 0) throw new Exception("O valor deve ser maior que 0");

            PedidoObtencaoPagamento pagamento = new PedidoObtencaoPagamento();
            pagamento.PedidoObtencao = this;
            pagamento.Valor = valor;
            pagamento.NumeroNF = notaFiscal;
            pagamento.Status = StatusPedidoObtencao.Get(StatusPedidoObtencaoEnum.AguardandoLiquidacao);
            pagamento.Empenho = this.Empenhos.Find(id_empenho);

            HistoricoPedidoObtencao historico = new HistoricoPedidoObtencao();
            historico.Data = DateTime.Now;
            historico.PedidoObtencao = this;
            historico.Descricao = string.Format("Pagamento: {0} -> {1}", this.Status.Descricao, Util.GetDescription(StatusPedidoObtencaoEnum.AguardandoLiquidacao));
            historico.Servidor = Servidor.Get(id_servidor);
            
            this.Pagamentos.Add(pagamento);

           

            using (TransactionBlock tran = new TransactionBlock())
            {
                pagamento.Save();
               
                historico.Save();

                if (this.Pagamentos.Sum(p => p.ValorTotal) >= this.ValorTotal)
                {
                    this.Status = StatusPedidoObtencao.Get(StatusPedidoObtencaoEnum.AguardandoLiquidacao);
                    base.Save();
                }
                
                this._historico.Add(historico);
                tran.IsValid = true;
            }
        }

        public virtual void RegistrarRecebedorEmpenho(int id_servidor, string nomeRecebedorEmpenho, string telefoneRecebedorEmpenho)
        {
            NomeRecebedorEmpenho = nomeRecebedorEmpenho;
            TelefoneRecebedorEmpenho = telefoneRecebedorEmpenho;
            IrParaProximoStatus(id_servidor, null);
        }
        
       //bacs: feito ateh aqui

       

	    private void CriarItensPEPRodizio()
	    {
	        using(TransactionBlock tran = new TransactionBlock())
	        {
	            //Verifica se existem itens que precisam ser comprados
	            foreach (PedidoObtencaoItem item in _itens)
	            {
	                if (item.OrigemMaterial == OrigemMaterial.Obtencao)
	                {
	                    this.FlagPedidoObtencao = true;
	                }
	                else
	                {
	                    PedidoObtencaoItemPEPRodizio itemPEP = new PedidoObtencaoItemPEPRodizio();
	                    itemPEP.OrigemMaterial = item.OrigemMaterial;
	                    itemPEP.PedidoObtencao = this;
	                    itemPEP.Quantidade = item.Quantidade;
	                    itemPEP.Valor = item.Valor;
	                    itemPEP.ServicoMaterial = item.ServicoMaterial;
	                    itemPEP.Especificacao = item.Especificacao;
                        if (item.PedidoObtencao.OrigemPO == OrigemPO.PS || IsPOGE)
                        {
                            PedidoServicoItemOrcamento itemOrcamento = item.PedidoObtencao.DelineamentoOrcamento.FindItemByMaterialId(item.ServicoMaterial.ID);
                            itemPEP.Celula = itemOrcamento.Celula;
                        }
	                    itemPEP.Save();
	                    _itensPEPRodizio.Add(itemPEP);
                        if (_tipoPedido != TipoPedido.PedidoMaterial)
                        {
                            PedidoObtencaoItem itemPO = PedidoObtencaoItem.Get(item.ID);
                            itemPO.Delete();
                        }
	                }
	            }
                
	            tran.IsValid = true;
	        }
	    }
        
        public virtual void IrParaProximoStatus(int id_servidor, string justificativa)
        {
            // TEM QUE SALVAR
            if (!this.IsPersisted)
                throw new Exception("Salve os dados do pedido antes de enviá-lo.");

            // JA FOI FINALIZADO
            if (this.Status.StatusPedidoObtencaoEnum == StatusPedidoObtencaoEnum.Finalizado)
                throw new Exception("Este pedido já foi finalizado.");

            // ETAPAS
            switch(this.Status.StatusPedidoObtencaoEnum)
            {
                //// AguardandoVerificacaoPaiol = 20
                //case StatusPedidoObtencaoEnum.AguardandoVerificacaoPaiol:
                //    EnviarVerificacaoPaiol(id_servidor);
                //    break;

                //// AguardandoAprovacaoEncarregadoDepartamentoMaterial = 30
                //case StatusPedidoObtencaoEnum.AguardandoAprovacaoEncarregadoDepartamentoMaterial:
                //    SalvarNovoStatus(id_servidor, StatusPedidoObtencaoEnum.AguardandoAprovacaoChefeDepartamento_Servidor, justificativa, false);
                //    break;

                // AguardandoAprovacaoChefeDepartamento_Servidor = 40
                case StatusPedidoObtencaoEnum.AguardandoAprovacaoChefeDepartamento_Servidor:
                    if (_origemPO == OrigemPO.PS)
                        SalvarNovoStatus(id_servidor, StatusPedidoObtencaoEnum.AguardandoDesignacaoComprador, justificativa, false);
                    else
                        SalvarNovoStatus(id_servidor, StatusPedidoObtencaoEnum.AguardandoAprovacaoChefeDepartamento_Departamento, justificativa, false);
                    break;

                // AprovacaoEncDivisão = 50
                case StatusPedidoObtencaoEnum.AprovacaoEncDivisão:
                    SalvarNovoStatus(id_servidor, StatusPedidoObtencaoEnum.AguardandoAprovacaoChefeDepartamento_Departamento, justificativa, false);
                    break;

                // AguardandoAprovacaoChefeDepartamento_Departamento = 55
                case StatusPedidoObtencaoEnum.AguardandoAprovacaoChefeDepartamento_Departamento:
                    SalvarNovoStatus(id_servidor, StatusPedidoObtencaoEnum.AguardandoDesignacaoComprador, justificativa, false);
                    break;

                // AguardandoDesignacaoComprador = 58
                case StatusPedidoObtencaoEnum.AguardandoDesignacaoComprador:
                    if (_origemPO == OrigemPO.PS)
                        SalvarNovoStatus(id_servidor, StatusPedidoObtencaoEnum.NaoEnviado, justificativa, false);
                    else
                        EnviarDesignacaoComprador(id_servidor);
                    break;

                // AguardandoAvaliacaoEncarregadoObtencao = 64
                case StatusPedidoObtencaoEnum.AguardandoAvaliacaoEncarregadoObtencao:
                    SalvarNovoStatus(id_servidor, StatusPedidoObtencaoEnum.AguardandoAprovacaoAgenteFiscal, justificativa, false);
                    break;

                // DefiniçãoFinanceiraRelator = 150
                case StatusPedidoObtencaoEnum.DefiniçãoFinanceiraRelator:
                    SalvarNovoStatus(id_servidor, StatusPedidoObtencaoEnum.AguardandoAprovacaoOrdenadorDespesa, justificativa, false);
                    break;

                // AguardandoAprovacaoAgenteFiscal = 155
                case StatusPedidoObtencaoEnum.AguardandoAprovacaoAgenteFiscal:
                    SalvarNovoStatus(id_servidor, StatusPedidoObtencaoEnum.DefiniçãoFinanceiraRelator, justificativa, false);
                    break;

                // AguardandoAprovacaoOrdenadorDespesa = 160
                case StatusPedidoObtencaoEnum.AguardandoAprovacaoOrdenadorDespesa:
                    SalvarNovoStatus(id_servidor, StatusPedidoObtencaoEnum.AguardandoEnvioEmpenho, justificativa, false);
                    break;

                // AguardandoEnvioEmpenho = 185
                case StatusPedidoObtencaoEnum.AguardandoEnvioEmpenho:
                    SalvarNovoStatus(id_servidor, StatusPedidoObtencaoEnum.AguardandoEntregaExecucao, justificativa, false);
                    break;

                // AguardandoEntregaExecucao = 190
                case StatusPedidoObtencaoEnum.AguardandoEntregaExecucao:
                    SalvarNovoStatus(id_servidor, StatusPedidoObtencaoEnum.AguardandoEntregaMercadoria, justificativa, false);
                    break;

                // AguardandoEntregaMercadoria = 200
                case StatusPedidoObtencaoEnum.AguardandoEntregaMercadoria:
                    SalvarNovoStatus(id_servidor, StatusPedidoObtencaoEnum.Finalizado, justificativa, false);
                    break;

                //case StatusPedidoObtencaoEnum.AguardandoAprovacaoSupervisaoCotacao:
                //    SalvarNovoStatus(id_servidor, StatusPedidoObtencaoEnum.AguardandoAprovacaoEncarregadoDivisao, justificativa, false);
                //    break;
                //case StatusPedidoObtencaoEnum.AguardandoAprovacaoEncarregadoDivisao:
                //    SalvarNovoStatus(id_servidor, StatusPedidoObtencaoEnum.AguardandoAprovacaoEncarregadoDepartamento, justificativa, false);
                //    break;

                //case StatusPedidoObtencaoEnum.AguardandoAprovacaoEncarregadoDepartamento:
                //    if(_origemPO == OrigemPO.Direto)
                //    {
                //        SalvarNovoStatus(id_servidor, StatusPedidoObtencaoEnum.AguardandoAprovacaoAgenteFiscal, justificativa, false);
                //    }
                //    else if(_origemPO == OrigemPO.PS || IsPOGE)
                //        SalvarNovoStatus(id_servidor, StatusPedidoObtencaoEnum.AguardandoCreditoEmpenho, justificativa, false);
                //    else
                //        SalvarNovoStatus(id_servidor, StatusPedidoObtencaoEnum.AguardandoAprovacaoAgenteFiscal, justificativa, false);
                //    break;
                //case StatusPedidoObtencaoEnum.AguardandoAprovacaoPAR:
                //    SalvarNovoStatus(id_servidor, StatusPedidoObtencaoEnum.AguardandoAprovacaoAgenteFiscal, justificativa, false);
                //    break;
                //case StatusPedidoObtencaoEnum.AguardandoAprovacaoAgenteFiscal:
                //    SalvarNovoStatus(id_servidor, StatusPedidoObtencaoEnum.AguardandoAprovacaoOrdenadorDespesa, justificativa, false);
                //    break;
                //case StatusPedidoObtencaoEnum.AguardandoAprovacaoOrdenadorDespesa:
                //    SalvarNovoStatus(id_servidor, StatusPedidoObtencaoEnum.AguardandoCreditoEmpenho, justificativa, false);
                //    break;
                ////case StatusPedidoObtencaoEnum.ImprimirEnviarIntendência:
                ////    SalvarNovoStatus(id_servidor, StatusPedidoObtencaoEnum.AguardandoCreditoEmpenho, justificativa, false);
                ////    break;
                //case StatusPedidoObtencaoEnum.AguardandoCreditoEmpenho:
                //    DebitarLicitacao();
                //    SalvarNovoStatus(id_servidor, StatusPedidoObtencaoEnum.AguardandoEnvioEmpenho, justificativa, false);
                //    break;
                //case StatusPedidoObtencaoEnum.AguardandoEnvioEmpenho:
                //    SalvarNovoStatus(id_servidor, StatusPedidoObtencaoEnum.AguardandoEntregaExecucao, justificativa, false);
                //    break;
                ////case StatusPedidoObtencaoEnum.AguardandoEntregaExecucao:
                ////    SalvarNovoStatus(id_servidor, StatusPedidoObtencaoEnum.AguardandoLiquidacao, justificativa, false);
                ////    break;
                ////case StatusPedidoObtencaoEnum.AguardandoLiquidacao:
                ////    SalvarNovoStatus(id_servidor, StatusPedidoObtencaoEnum.AguardandoPagamentoPO, justificativa, false);
                ////    break;
                ////case StatusPedidoObtencaoEnum.AguardandoPagamentoPO:
                ////    SalvarNovoStatus(id_servidor, StatusPedidoObtencaoEnum.Finalizado, justificativa, false);
                ////    break;
                //case StatusPedidoObtencaoEnum.AguardandoEntregaExecucao: //deixar esse por enquanto, pois existem POs que ainda estao no esquema antigo
                //case StatusPedidoObtencaoEnum.AguardandoPagamentoPO:
                //case StatusPedidoObtencaoEnum.AguardandoLiquidacao:
                //    SalvarNovoStatus(id_servidor, StatusPedidoObtencaoEnum.Finalizado, justificativa, false);
                //    break;
                ////bacs: feito ateh aqui
            }
        }

        //private void EnviarVerificacaoPaiol(int id_servidor)
        //{
        //    if (this.Status.StatusPedidoObtencaoEnum != StatusPedidoObtencaoEnum.AguardandoVerificacaoPaiol)
        //        throw new Exception("Status inesperado.");

        //    if (_origemPO == OrigemPO.PS)
        //    {
        //        using (TransactionBlock tran = new TransactionBlock())
        //        {
        //            SalvarNovoStatus(id_servidor, StatusPedidoObtencaoEnum.AguardandoAprovacaoEncarregadoDepartamentoMaterial, null, false);
        //            CriarItensPEPRodizio();

        //            //Se não restou nenhum item no PO, alteramos o status para cancelado
        //            //if (!this.FlagPedidoObtencao)
        //            //    SalvarNovoStatus(id_servidor, StatusPedidoObtencaoEnum.TransformadoEmAC, null, false);
        //            //else
        //            //    SalvarNovoStatus(id_servidor, StatusPedidoObtencaoEnum.AguardandoAprovacaoChefeDepartamento_Servidor, null, false);

        //            //SalvarNovoStatus(id_servidor, StatusPedidoObtencaoEnum.AguardandoAprovacaoChefeDepartamento_Servidor, null, false);

        //            tran.IsValid = true;
        //        }
        //    }
        //    else
        //    {
        //        using (TransactionBlock tran = new TransactionBlock())
        //        {
        //            CriarItensPEPRodizio();

        //            if (!this.FlagPedidoObtencao)
        //                SalvarNovoStatus(id_servidor, StatusPedidoObtencaoEnum.Cancelado, null, false);
        //            else
        //                SalvarNovoStatus(id_servidor, StatusPedidoObtencaoEnum.AguardandoAprovacaoChefeDepartamento_Servidor, null, false);
        //            tran.IsValid = true;
        //        }
        //    }
        //}

	    private void DebitarLicitacao()
	    {
            if (this.TipoCompra.TipoCompraEnum == TipoCompraEnum.Licitacao)
            {
                foreach (PedidoObtencaoItem item in _itens)
                {
                    LicitacaoItem licitacaoItem = this.Licitacao.Itens.Where(i => i.Material.ID == item.ServicoMaterial.ID).First();

                    if (licitacaoItem.QuantidadeUtilizada + item.Quantidade <= licitacaoItem.Quantidade)
                    {
                        licitacaoItem.QuantidadeUtilizada += item.Quantidade;
                        licitacaoItem.Save();    
                    }
                }
            }
	    }

        private void CreditarLicitacao()
        {
            if (this.TipoCompra.TipoCompraEnum == TipoCompraEnum.Licitacao)
            {
                foreach (PedidoObtencaoItem item in _itens)
                {
                    LicitacaoItem licitacaoItem = this.Licitacao.Itens.Where(i => i.Material.ID == item.ServicoMaterial.ID).First();
                    if(licitacaoItem.QuantidadeUtilizada > 0)
                    {
                        licitacaoItem.QuantidadeUtilizada -= item.Quantidade;
                        licitacaoItem.Save();    
                    }
                }
            }
        }

	    private void EnviarDesignacaoComprador(int id_servidor)
        {
            //Verifica se todos os itens foram designados
            foreach (PedidoObtencaoItem item in _itens)
            {
                //if(item.Comprador == null)
                //    throw new Exception("Antes de enviar, defina o comprador de todos os materiais.");
            }

            SalvarNovoStatus(id_servidor, StatusPedidoObtencaoEnum.AguardandoAvaliacaoEncarregadoObtencao, null, false);
        }

	    private void FinalizarPedidoMaterial(int id_servidor)
	    {
	        using(TransactionBlock tran = new TransactionBlock())
	        {
	            CriarItensPEPRodizio();
	            SalvarNovoStatus(id_servidor, StatusPedidoObtencaoEnum.Finalizado,
	                             null, false);
	            tran.IsValid = true;
	        }
	    }

	    /// <summary>
        /// Retorna para o status anterior
        /// </summary>
        public virtual void Recusar(int id_servidor, string justificativa)
        {
            // Enum
            StatusPedidoObtencaoEnum proximoStatus;

            // SE for PO Direto
            if (_status.StatusPedidoObtencaoEnum == StatusPedidoObtencaoEnum.AguardandoAprovacaoChefeDepartamento_Departamento && _origemPO == OrigemPO.Direto)
                proximoStatus = StatusPedidoObtencaoEnum.NaoEnviado;

            // SE for PS
            else if (_status.StatusPedidoObtencaoEnum == StatusPedidoObtencaoEnum.AguardandoDesignacaoComprador && _origemPO == OrigemPO.PS)
                proximoStatus = StatusPedidoObtencaoEnum.AguardandoAprovacaoChefeDepartamento_Servidor;

            // Recusa normal para as etapas anteriores
            else
                proximoStatus = StatusPedidoObtencao.GetAnterior(_status.StatusPedidoObtencaoEnum);
            
            //if (_status.StatusPedidoObtencaoEnum == StatusPedidoObtencaoEnum.AguardandoCreditoEmpenho)
            //{
            //    if (_origemPO != OrigemPO.PS && !IsPOGE)
            //    {
            //        proximoStatus = StatusPedidoObtencaoEnum.AguardandoAprovacaoOrdenadorDespesa;
            //    }
            //    else
            //        proximoStatus = StatusPedidoObtencaoEnum.AguardandoAprovacaoEncarregadoDepartamento;
            //}

            //else if (_status.StatusPedidoObtencaoEnum == StatusPedidoObtencaoEnum.AguardandoAprovacaoEncarregadoDivisao)
            //{
            //    if (_origemPO == OrigemPO.PS || IsPOGE)
            //        proximoStatus = StatusPedidoObtencaoEnum.AguardandoAprovacaoSupervisaoCotacao;
            //    else
            //        proximoStatus = StatusPedidoObtencaoEnum.NaoEnviado;
            //}
            //else if(_status.StatusPedidoObtencaoEnum == StatusPedidoObtencaoEnum.AguardandoAprovacaoEncarregadoDepartamento && _celula.TipoCelula == TipoCelula.Departamento
            //    && _origemPO != OrigemPO.PS)
            //{
            //    proximoStatus = StatusPedidoObtencaoEnum.NaoEnviado;
            //}
            //else if(_status.StatusPedidoObtencaoEnum == StatusPedidoObtencaoEnum.AguardandoEnvioEmpenho)
            //{
            //    CreditarLicitacao();
            //    proximoStatus = StatusPedidoObtencao.GetAnterior(_status.StatusPedidoObtencaoEnum);
            //}
            //else if (_status.StatusPedidoObtencaoEnum == StatusPedidoObtencaoEnum.AguardandoAprovacaoAgenteFiscal)
            //{
            //    proximoStatus = StatusPedidoObtencaoEnum.AguardandoAprovacaoEncarregadoDepartamento;
            //}
            //else
            //{
            //    proximoStatus = StatusPedidoObtencao.GetAnterior(_status.StatusPedidoObtencaoEnum);
            //}
            
            //if(proximoStatus == StatusPedidoObtencaoEnum.NaoEnviado)
            //{
            //    LimparItensLicitacao();
            //}

            SalvarNovoStatus(id_servidor, proximoStatus, justificativa, true);
        }

        public virtual void RegistraCancelamentoAC(int id_servidor, AutorizacaoCompra ac)
        {
            HistoricoPedidoObtencao historico = new HistoricoPedidoObtencao();
            historico.Data = DateTime.Now;
            historico.PedidoObtencao = this;
            historico.Descricao = string.Format("AC número {0} cancelada.", ac.CodigoComAno);
            historico.Servidor = Servidor.Get(id_servidor);
            historico.Save();
        }

        private HistoricoPedidoObtencao GetHistorico(int id_servidor, StatusPedidoObtencaoEnum novoStatus)
        {
            HistoricoPedidoObtencao historico = new HistoricoPedidoObtencao();
            historico.Data = DateTime.Now;
            historico.PedidoObtencao = this;
            historico.Descricao = string.Format("{0} -> {1}", this.Status.Descricao, Util.GetDescription(novoStatus));
            historico.Servidor = Servidor.Get(id_servidor);
            return historico;
        }
        
        public virtual void SepararItem(int id_item, int id_origemMaterial, decimal quantidade)
        {
            PedidoObtencaoItem item = _itens.Find(id_item);
            if (item.Quantidade <= quantidade)
                throw new Exception("A quantidade separada deve ser inferior a quantidade original.");
                
            using(TransactionBlock tran = new TransactionBlock())
            {
                item.Quantidade -= quantidade;
                item.Save();
                
                PedidoObtencaoItem novoItem = new PedidoObtencaoItem();
                novoItem.ServicoMaterial = item.ServicoMaterial;
                novoItem.PedidoObtencao = this;
                novoItem.OrigemMaterial = (OrigemMaterial) id_origemMaterial;
                novoItem.Quantidade = quantidade;
                novoItem.Valor = item.Valor;
                novoItem.Save();
                
                _itens.Add(novoItem);
                
                tran.IsValid = true;
            }
        }
        #endregion

        #region Outros

        public virtual void Cancelar(int id_servidor, int id_motivoCancelamento)
        {
            if(this._status.StatusPedidoObtencaoEnum == StatusPedidoObtencaoEnum.Finalizado ||
                this._status.StatusPedidoObtencaoEnum == StatusPedidoObtencaoEnum.Cancelado)
                throw new Exception("Este pedido não pode mais ser cancelado.");

            HistoricoPedidoObtencao historico = GetHistorico(id_servidor, StatusPedidoObtencaoEnum.Cancelado);
            historico.Justificativa = "PS cancelado";

            using (TransactionBlock tran = new TransactionBlock())
            {
                //Verifica se pode cancelar

                // Exclui todas as entradas feitas no estoque
                PedidoObtencao.ExcluirMovimentosEstoque(this.ID);

                //Licitacao
                if(_status.StatusPedidoObtencaoEnum >= StatusPedidoObtencaoEnum.AguardandoEnvioEmpenho)
                    LimparItensLicitacao();
                
                historico.Save();

                this.Status = StatusPedidoObtencao.Get(StatusPedidoObtencaoEnum.Cancelado);
                this.MotivoCancelamento = MotivoCancelamento.Get(id_motivoCancelamento);

                base.Save();

                this._historico.Add(historico);
                tran.IsValid = true;
            }
        }

        private void LimparItensLicitacao()
        {
            if (this.Licitacao == null) return;
            //Licitacao
            if (this.TipoCompra.TipoCompraEnum == TipoCompraEnum.Licitacao)
            {
                foreach (PedidoObtencaoItem item in _itens)
                {
                    LicitacaoItem licitacaoItem = this.Licitacao.Itens.Where(i => i.Material.ID == item.ServicoMaterial.ID).FirstOrDefault();
                    if (licitacaoItem != null && licitacaoItem.QuantidadeUtilizada > 0)
                    {
                        licitacaoItem.QuantidadeUtilizada -= item.Quantidade;
                        licitacaoItem.Save();
                    }
                }
            }
        }

        public static void ExcluirMovimentosEstoque(int id_pedidoObtencao)
        {
            SQLHelper helper = new SQLHelper();
            object[] param = new object[2];
            param[1] = NullHelper.IsZero(id_pedidoObtencao);

            helper.ExecuteNonQuery("PedidoObtencao_ExcluirMovimentosEstoque", CommandType.StoredProcedure, param);
        }

        public virtual List<IItemServicoMaterial> GetItens(OrigemMaterial origemMaterial)
        {
            List<IItemServicoMaterial> list = new List<IItemServicoMaterial>();
            foreach (IItemServicoMaterial item in _itens)
            {
                if (item.OrigemMaterial == origemMaterial)
                    list.Add(item);
            }

            if (_tipoPedido != TipoPedido.PedidoMaterial)
            {
                foreach (IItemServicoMaterial item in _itensPEPRodizio)
                {
                    if (item.OrigemMaterial == origemMaterial)
                        list.Add(item);
                }
            }
            return list;
        }

	    public virtual void InserirItens(List<PedidoServicoItemOrcamento> itens)
	    {
	        
	    }

	    public virtual void EnviarParaPAR(int id_servidor, string justificativa)
	    {
            if (_origemPO == OrigemPO.Direto && this._status.StatusPedidoObtencaoEnum == StatusPedidoObtencaoEnum.AguardandoAprovacaoEncarregadoDepartamento)
                SalvarNovoStatus(id_servidor, StatusPedidoObtencaoEnum.AguardandoAprovacaoPAR, justificativa, false);
        }

        #endregion

        #region Atualiza Cotação

        public virtual void AtualizaCotacao()
        {
            base.Save();
        }

        #endregion

        #region Limite de Compras por Fornecedor

        public static SaldoSubNatureza GetSaldoComprasUtilizado(int id_subNatureza, int ano)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();

            IQuery query = session.CreateQuery(
            @"select Sum(i.Valor * i.Quantidade) from PedidoObtencaoItem i 
                inner join i.PedidoObtencao po                 
			where po.TipoCompra.ID != :id_tipoCompraLicitacao
            and i.SubNaturezaDespesa.ID = :id_subNatureza
			and dbo.GetYear(po.DataEmissao) = :ano
			and po.Status.ID != :statusCancelado		
            and po.Status.ID >= :statusAguardandoAprovacaoComandante");

            query.SetInt32("statusCancelado", Convert.ToInt32(StatusPedidoObtencaoEnum.Cancelado));
            query.SetInt32("statusAguardandoAprovacaoComandante", Convert.ToInt32(StatusPedidoObtencaoEnum.AguardandoAprovacaoOrdenadorDespesa));
            query.SetInt32("id_subNatureza", id_subNatureza);
            query.SetInt32("id_tipoCompraLicitacao", Convert.ToInt32(TipoCompraEnum.Licitacao));
            query.SetInt32("ano", ano);

            var subNatureza = SubNaturezaDespesa.Get(id_subNatureza);

            //var tipoCompraEnum = subNatureza.Industrial ? TipoCompraEnum.Industrial : TipoCompraEnum.NaoIndustrial;
            var tipoCompraEnum = TipoCompraEnum.Dispensa;
            
            var saldo = new SaldoSubNatureza(TipoCompra.Get(Convert.ToInt32(tipoCompraEnum)));

            saldo.ValorUtilizadoTotal = Convert.ToDecimal(query.UniqueResult());
            saldo.ValorComprometido = GetSaldoComprasComprometido(id_subNatureza, ano);

            return saldo;
        }

        public static decimal GetSaldoComprasComprometido(int id_subNatureza, int ano)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();

            IQuery query = session.CreateQuery(
            @"select Sum(i.Valor * i.Quantidade) from PedidoObtencaoItem i 
                inner join i.PedidoObtencao po                 
			where po.TipoCompra.ID != :id_tipoCompraLicitacao
            and i.SubNaturezaDespesa.ID = :id_subNatureza
			and dbo.GetYear(po.DataEmissao) = :ano
			and po.Status.ID != :statusCancelado		
            and po.Status.ID > :statusNaoEnviado
            and po.Status.ID < :statusAC");

            query.SetInt32("statusCancelado", Convert.ToInt32(StatusPedidoObtencaoEnum.Cancelado));
            query.SetInt32("statusNaoEnviado", Convert.ToInt32(StatusPedidoObtencaoEnum.NaoEnviado));
            query.SetInt32("statusAC", Convert.ToInt32(StatusPedidoObtencaoEnum.AguardandoEntregaMercadoria));
            query.SetInt32("id_subNatureza", id_subNatureza);
            query.SetInt32("id_tipoCompraLicitacao", Convert.ToInt32(TipoCompraEnum.Licitacao));
            query.SetInt32("ano", ano);

            var subNatureza = SubNaturezaDespesa.Get(id_subNatureza);

            //var tipoCompraEnum = subNatureza.Industrial ? TipoCompraEnum.Industrial : TipoCompraEnum.NaoIndustrial;
            var tipoCompraEnum = TipoCompraEnum.Dispensa;

            var saldoPo = Convert.ToDecimal(query.UniqueResult());

//            query = session.CreateQuery(
//            @"select Sum(i.Valor * i.Quantidade) from AutorizacaoCompraItem i 
//                inner join i.AutorizacaoCompra a                 
//			where a.TipoCompra.ID != :id_tipoCompraLicitacao
//            and i.SubNaturezaDespesa.ID = :id_subNatureza
//			and dbo.GetYear(a.DataEmissao) = :ano
//			and a.Status.ID != :statusCancelado		
//            and a.Status.ID < :statusAguardandoAprovacaoComandante");

//            query.SetInt32("statusCancelado", Convert.ToInt32(StatusAutorizacaoCompraEnum.Cancelado));
//            query.SetInt32("statusAguardandoAprovacaoComandante", Convert.ToInt32(StatusAutorizacaoCompraEnum.AguardandoAprovacaoComandanteGeral));
//            query.SetInt32("id_subNatureza", id_subNatureza);
//            query.SetInt32("id_tipoCompraLicitacao", Convert.ToInt32(TipoCompraEnum.Licitacao));
//            query.SetInt32("ano", ano);

            return saldoPo; // +Convert.ToDecimal(query.UniqueResult());
        }

        #endregion
    }

    public class SaldoSubNatureza
    {
        private decimal _valorUtilizadoTotal;

        /// <summary>
        /// Considera todas as ACs, menos as canceladas
        /// </summary>
        public virtual decimal SaldoDisponivel
        {
            get { return _tipoCompra.LimiteAnual - _valorUtilizadoTotal; }
        }

        /// <summary>
        /// Considera todas as ACs, menos as canceladas
        /// </summary>
        public virtual decimal ValorUtilizadoTotal
        {
            get { return _valorUtilizadoTotal; }
            set { _valorUtilizadoTotal = value; }
        }

        public decimal ValorComprometido { get; set; }

        private TipoCompra _tipoCompra;

        public SaldoSubNatureza(TipoCompra tipoCompra)
        {
            _tipoCompra = tipoCompra;
        }
    }

    public class SaldoFornecedor
    {

        private decimal _saldoARealizar;
        private decimal _valorUtilizadoReal;
        private decimal _valorUtilizadoTotal;

        /// <summary>
        /// Considera todas as ACs, menos as canceladas
        /// </summary>
        public virtual decimal SaldoTotal
        {
            get { return _tipoCompra.LimiteAnual - _valorUtilizadoTotal; }
        }

        /// <summary>
        /// Considera apenas ACs que não foram aprovadas pelo comandante
        /// </summary>
        public virtual decimal SaldoARealizar
        {
            get { return _saldoARealizar; }
            set { _saldoARealizar = value; }
        }
        /// <summary>
        /// Considera apenas ACs que ja foram aprovadas pelo comandante
        /// </summary>
        public virtual decimal SaldoReal
        {
            get { return _tipoCompra.LimiteAnual - _valorUtilizadoReal; }
        }

        /// <summary>
        /// Considera apenas ACs que ja foram aprovadas pelo comandante
        /// </summary>
        public virtual decimal ValorUtilizadoReal
        {
            get { return _valorUtilizadoReal; }
            set { _valorUtilizadoReal = value; }
        }

        /// <summary>
        /// Considera todas as ACs, menos as canceladas
        /// </summary>
        public virtual decimal ValorUtilizadoTotal
        {
            get { return _valorUtilizadoTotal; }
            set { _valorUtilizadoTotal = value; }
        }

        private TipoCompra _tipoCompra;
        public SaldoFornecedor(TipoCompra tipoCompra)
        {
            _tipoCompra = tipoCompra;
        }
    }
}

