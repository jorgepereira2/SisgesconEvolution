using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class DelineamentoOrcamento : BusinessObject<DelineamentoOrcamento>, IPedido
	{
		#region Private Members

		private PedidoServico _pedidoservico; 
		private DateTime _data; 
		private Servidor _servidor;
	    private StatusPedidoServico _status;
	    private CategoriaServico _categoriaServico;
	    private bool _flagRecusado;
        private string _mensagemEnvioCliente;
        private string _mensagemAprovacaoCliente;
        private string _comprometimentoCliente;
	    private string _mensagemChamadaMeio;
        private int _numero;
	   
        private decimal _valorMaoObraHora;
        private decimal _percentualMaoObraIndireta;
        private decimal _taxaOperacionalMaoObra;
        private decimal _percentualMaterialIndireto;
        private decimal _percentualServicoTerceiroIndireto;
        private decimal _taxaOperacionalMaterialServico;
        private decimal _taxaContribuicaoOperacionalMaoObra;
        private decimal _taxaContribuicaoOperacionalMaterial;
        private decimal _percentualDescontoSubTotalMaoObra;
        private decimal _percentualDescontoSubTotalMaterialServicoTerceiro;
	    private string _comentario;

	    private StatusEntregaMaterial _statusEntregaMaterialSingra;
	    private StatusEntregaMaterial _statusEntregaMaterialRodizio;
        private StatusEntregaMaterial _statusEntregaMaterialPEP;

	    private PedidoObtencao _pedidoObtencao;
	    private DateTime? _dataValidade;
	    private string _mensagemProntificacao;
	    private DateTime? _dataDevolucao;
	    private string _comentarioDevolucao;
	    private string _numeroNL;

		#endregion
		
		#region Default ( Empty ) Class Constuctor

		/// <summary>
		/// default constructor
		/// </summary>
		public DelineamentoOrcamento()
		{
			_pedidoservico =  null; 
			_data = DateTime.MinValue; 
			_servidor =  null;
		    _status = null;
		    _categoriaServico = null;
            _numero = 0;
		    _mensagemEnvioCliente = null;
		    _mensagemAprovacaoCliente = null;
		    _comprometimentoCliente = null;
		    
            _percentualMaoObraIndireta = 0;
            _percentualMaterialIndireto = 0;
            _percentualServicoTerceiroIndireto = 0;
            _taxaContribuicaoOperacionalMaoObra = 0;
            _taxaContribuicaoOperacionalMaterial = 0;
            _taxaOperacionalMaoObra = 0;
            _taxaOperacionalMaterialServico = 0;
            _valorMaoObraHora = 0;
            
            _statusEntregaMaterialRodizio = StatusEntregaMaterial.NaoAplicavel;
            _statusEntregaMaterialPEP = StatusEntregaMaterial.NaoAplicavel;
            _statusEntregaMaterialSingra = StatusEntregaMaterial.NaoAplicavel;

		    _pedidoObtencao = null;
            _itensOrcamento = new CustomList<PedidoServicoItemOrcamento>();
            _itensDelineamento = new CustomList<PedidoServicoDelineamento>();
		    _entradas = new CustomList<EntradaMaterial>();
		    _ocorrencias = new CustomList<DelineamentoOrcamentoOcorrencia>();
		    _faturamentos = new CustomList<DelineamentoOrcamentoFaturamento>();
            Delineamentos = new CustomList<DelineamentoOficina>();
            Rotinas = new CustomList<Rotina>();
		}

		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties

	    //public virtual Rotina Rotina { get; set; }

        public virtual string NumeroNL
        {
            get { return _numeroNL; }
            set { _numeroNL = value; }
        }
        public virtual string ComentarioDevolucao
        {
            get { return _comentarioDevolucao; }
            set { _comentarioDevolucao = value; }
        }
        public virtual DateTime? DataDevolucao
        {
            get { return _dataDevolucao; }
            set { _dataDevolucao = value; }
        }
        public virtual string MensagemProntificacao
        {
            get { return _mensagemProntificacao; }
            set { _mensagemProntificacao = value; }
        }
        public virtual DateTime? DataValidade
        {
            get { return _dataValidade; }
            set { _dataValidade = value; }
        }
        
        public virtual string MensagemChamadaMeio
        {
            get { return _mensagemChamadaMeio; }
            set { _mensagemChamadaMeio = value; }
        }
        
        public virtual PedidoObtencao PedidoObtencao
        {
            get { return _pedidoObtencao; }
            set { _pedidoObtencao = value; }
        }
        
        public virtual StatusEntregaMaterial StatusEntregaMaterialRodizio
        {
            get { return _statusEntregaMaterialRodizio; }
            set { _statusEntregaMaterialRodizio = value; }
        }

        public virtual StatusEntregaMaterial StatusEntregaMaterialPEP
        {
            get { return _statusEntregaMaterialPEP; }
            set { _statusEntregaMaterialPEP = value; }
        }
        
        public virtual StatusEntregaMaterial StatusEntregaMaterialSingra
        {
            get { return _statusEntregaMaterialSingra; }
            set { _statusEntregaMaterialSingra = value; }
        }

        public virtual decimal TaxaContribuicaoOperacionalMaterial
        {
            get { return _taxaContribuicaoOperacionalMaterial; }
            set { _taxaContribuicaoOperacionalMaterial = value; }
        }
        public virtual decimal TaxaContribuicaoOperacionalMaoObra
        {
            get { return _taxaContribuicaoOperacionalMaoObra; }
            set { _taxaContribuicaoOperacionalMaoObra = value; }
        }
        public virtual decimal TaxaOperacionalMaterialServico
        {
            get { return _taxaOperacionalMaterialServico; }
            set { _taxaOperacionalMaterialServico = value; }
        }
        public virtual decimal PercentualServicoTerceiroIndireto
        {
            get { return _percentualServicoTerceiroIndireto; }
            set { _percentualServicoTerceiroIndireto = value; }
        }
        public virtual decimal PercentualMaterialIndireto
        {
            get { return _percentualMaterialIndireto; }
            set { _percentualMaterialIndireto = value; }
        }
        public virtual decimal TaxaOperacionalMaoObra
        {
            get { return _taxaOperacionalMaoObra; }
            set { _taxaOperacionalMaoObra = value; }
        }
        public virtual decimal PercentualMaoObraIndireta
        {
            get { return _percentualMaoObraIndireta; }
            set { _percentualMaoObraIndireta = value; }
        }
        public virtual decimal ValorMaoObraHora
        {
            get { return _valorMaoObraHora; }
            set { _valorMaoObraHora = value; }
        }

        public virtual decimal PercentualDescontoSubTotalMaterialServicoTerceiro
        {
            get { return _percentualDescontoSubTotalMaterialServicoTerceiro; }
            set { _percentualDescontoSubTotalMaterialServicoTerceiro = value; }
        }

        public virtual decimal PercentualDescontoSubTotalMaoObra
        {
            get { return _percentualDescontoSubTotalMaoObra; }
            set { _percentualDescontoSubTotalMaoObra = value; }
        }
        
        /// <summary>
        /// Número sequencial por pedido
        /// </summary>
        public virtual int Numero
        {
            get { return _numero; }
            set { _numero = value; }
        }

        public virtual string ComprometimentoCliente
        {
            get { return _comprometimentoCliente; }
            set { _comprometimentoCliente = value; }
        }
        public virtual string MensagemAprovacaoCliente
        {
            get { return _mensagemAprovacaoCliente; }
            set { _mensagemAprovacaoCliente = value; }
        }
        public virtual string MensagemEnvioCliente
        {
            get { return _mensagemEnvioCliente; }
            set { _mensagemEnvioCliente = value; }
        }	
        
        public virtual CategoriaServico CategoriaServico
        {
            get { return _categoriaServico; }
            set { _categoriaServico = value; }
        }
        
        public virtual StatusPedidoServico Status
        {
            get { return _status; }
            set { _status = value; }
        }
      
        public virtual bool FlagRecusado
        {
            get { return _flagRecusado; }
            set { _flagRecusado = value; }
        }

	    public virtual HistoricoPedidoServico UltimoHistorico
	    {
	        get { return _pedidoservico.GetUltimoHistorico(this.ID);}
	    }

	    public virtual PedidoServico PedidoServico
		{
			get { return _pedidoservico; }
			set { _pedidoservico = value; }
		}

        public virtual string Comentario
        {
            get { return _comentario; }
            set { _comentario = value; }
        }
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual DateTime Data
		{
			get { return _data; }
			set { _data = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual Servidor Servidor
		{
			get { return _servidor; }
			set { _servidor = value; }
		}

	    public virtual Servidor ServidorCotador { get; set; }

		#endregion 
		
		#region IPedido Members
        //public virtual Equipamento Equipamento
        //{
        //    get { return PedidoServico.Equipamento; }
        //}

        public virtual string DescricaoEquipamentos
        {
            get { return PedidoServico.DescricaoEquipamentos; }
        }

        public virtual string CodigoPedidoCliente
        {
            get { return PedidoServico.CodigoPedidoCliente; }
        }

        public virtual string DescricaoCliente
        {
            get { return Cliente.Descricao; }
        }

        public virtual string DescricaoStatus
        {
            get { return this.Status.Descricao; }
        }
      

        public virtual string CodigoComAno
        {
            get
            {
                if (_numero > 1)
                    return PedidoServico.CodigoComAno + " C" + _numero.ToString();
                else
                    return PedidoServico.CodigoComAno;
            }
        }

        public virtual int CodigoInterno
        {
            get { return PedidoServico.CodigoInterno; }
        }

        public virtual Cliente Cliente
        {
            get { return PedidoServico.Cliente; }
        }

        public virtual DateTime DataEmissao
        {
            get { return _pedidoservico.DataEmissao; }
        }

        public virtual Cliente ClientePagador
        {
            get { return PedidoServico.ClientePagador; }
        }

      

        public virtual Servidor ServidorGerente
        {
            get { return PedidoServico.ServidorGerente; }
        }

        public virtual bool FlagProgem
        {
            get { return PedidoServico.FlagProgem; }
        }

        public virtual Celula Celula
        {
            get { return PedidoServico.Celula; }
        }

        public virtual int ID_PedidoServico
	    {
            get { return PedidoServico.ID; }
	    }

        public virtual string NumeroRegistro
        {
            get { return PedidoServico.NumeroRegistro; }
        }
	    #endregion
			
        #region Collections
        public virtual ICustomList<Rotina> Rotinas { get; set; }

        private ICustomList<PedidoServicoItemOrcamento> _itensOrcamento;
        private ICustomList<PedidoServicoDelineamento> _itensDelineamento;
	    private ICustomList<EntradaMaterial> _entradas;
	    private ICustomList<DelineamentoOrcamentoOcorrencia> _ocorrencias;
        private ICustomList<DelineamentoOrcamentoFaturamento> _faturamentos;

        public virtual ICustomList<DelineamentoOrcamentoFaturamento> Faturamentos
        {
            get { return _faturamentos; }
            set { _faturamentos = value; }
        }

	    public virtual ICustomList<DelineamentoOrcamentoOcorrencia> Ocorrencias
	    {
	        get { return _ocorrencias; }
	        set { _ocorrencias = value; }
	    }

	    public virtual ICustomList<EntradaMaterial> Entradas
	    {
	        get { return _entradas; }
	        set { _entradas = value; }
	    }
        public virtual ICustomList<PedidoServicoDelineamento> ItensDelineamento
        {
            get { return _itensDelineamento; }
            set { _itensDelineamento = value; }
        }

        public virtual ICustomList<PedidoServicoItemOrcamento> ItensOrcamento
        {
            get { return _itensOrcamento; }
            set { _itensOrcamento = value; }
        }

	    public virtual ICustomList<DelineamentoOficina> Delineamentos { get; set; }
        #endregion

        #region Advanced properties
        public virtual int HomemHoraTotal
        {
            get
            {
                int valor = 0;
                foreach (PedidoServicoDelineamento delineamento in _itensDelineamento)
                {
                    valor += delineamento.HomemHora;
                }
                return valor;
            }
        }

        public virtual decimal ValorTotalItens
        {
            get
            {
                decimal valor = 0;
                foreach (PedidoServicoItemOrcamento itemOrcamento in _itensOrcamento)
                {
                    valor += itemOrcamento.Valor * itemOrcamento.Quantidade;
                }
                return valor;
            }
        }

        public virtual decimal ValorTotalOrcamento
        {
            get
            {
                return ValorTotalMaoObra + SubTotalMaterial + ValorTaxaContribuicaoOperacionalMaoObra + 
                    ValorTaxaContribuicaoOperacionalMaterial - ValorDescontoMaterial;
            }
        }

	    public virtual decimal ValorMaoObraDireta
	    {
	        get
	        {
	            return HomemHoraTotal*this.ValorMaoObraHora;
	        }
	    }

        public virtual decimal ValorMaoObraIndireta
        {
            get
            {
                return this.ValorMaoObraDireta * this.PercentualMaoObraIndireta/100;
            }
        }

        public virtual decimal ValorTaxaOperacionalMaoObra
        {
            get
            {
                return (ValorMaoObraDireta + ValorMaoObraIndireta)* this.TaxaOperacionalMaoObra / 100;
            }
        }

        public virtual decimal DescontoMaoObra
        {
            get
            {
                return ValorMaoObraDireta + ValorMaoObraIndireta; // +ValorTaxaOperacionalMaoObra;
            }
        }

        public virtual decimal SubTotalMaoObra
        {
            get
            {
                return ValorMaoObraDireta + ValorMaoObraIndireta + ValorTaxaOperacionalMaoObra;
            }
        }

        public virtual decimal ValorTotalMaoObra
        {
            get
            {
                return SubTotalMaoObra;// - ValorDescontoMaoObra;
            }
        }

	    public virtual decimal ValorMateriaPrima
	    {
	        get
	        {
                decimal valor = 0;
                foreach (PedidoServicoItemOrcamento itemOrcamento in _itensOrcamento)
                {
                    if (itemOrcamento.ServicoMaterial.TipoServicoMaterial == TipoServicoMaterial.Material)
                        valor += itemOrcamento.ValorTotal;
                }
                return valor;
	        }
	    }

        public virtual decimal ValorServicoTerceiros
        {
            get
            {
                decimal valor = 0;
                foreach (PedidoServicoItemOrcamento itemOrcamento in _itensOrcamento)
                {
                    if (itemOrcamento.ServicoMaterial.TipoServicoMaterial == TipoServicoMaterial.Servico)
                        valor += itemOrcamento.ValorTotal;
                }
                return valor;
            }
        }

	    public virtual decimal ValorMaterialIndireto
	    {
	        get { return (ValorMateriaPrima + ValorMaoObraDireta)*PercentualMaterialIndireto/100;}
	    }

	    public virtual decimal ValorServicoTerceirosIndireto
	    {
	        get { return (ValorMateriaPrima + ValorServicoTerceiros + ValorMaoObraDireta)*PercentualServicoTerceiroIndireto/100;}
	    }

	    public virtual decimal ValorTaxaOperacionalMaterialServicos
	    {
            get { return (ValorServicoTerceirosIndireto + ValorMaterialIndireto + this.ValorMateriaPrima + this.ValorServicoTerceiros) * this.TaxaOperacionalMaterialServico / 100; }
	    }

	    public virtual decimal SubTotalMaterial
	    {
	        get{
	            return
	                ValorMateriaPrima + ValorServicoTerceiros + ValorMaterialIndireto + ValorServicoTerceirosIndireto +
	                ValorTaxaOperacionalMaterialServicos;}
	    }

	    public virtual decimal ValorDescontoMaoObra
	    {
            get { return DescontoMaoObra * _percentualDescontoSubTotalMaoObra / 100; }
	    }

        public virtual decimal ValorDescontoMaterial
        {
            get { return (SubTotalMaterial + ValorTaxaContribuicaoOperacionalMaoObra + ValorTaxaContribuicaoOperacionalMaterial) * _percentualDescontoSubTotalMaterialServicoTerceiro / 100; }
        }

	    public virtual decimal ValorTaxaContribuicaoOperacionalMaoObra
	    {
	        get { return SubTotalMaoObra*_taxaContribuicaoOperacionalMaoObra/100; }
	    }

        public virtual decimal ValorTaxaContribuicaoOperacionalMaterial
        {
            get { return SubTotalMaterial * _taxaContribuicaoOperacionalMaterial / 100; }
        }

	    public virtual bool ApenasServico
	    {
	        get
	        {
	            foreach (PedidoServicoItemOrcamento itemOrcamento in _itensOrcamento)
	            {
	                if(itemOrcamento.ServicoMaterial.TipoServicoMaterial == TipoServicoMaterial.Material)
	                    return false;
	            }
	            return true;
	        }
	    }

	    public virtual bool ExisteItensDisponiveisPO
	    {
	        get
	        {
                return this.ItensOrcamento.Where(i => i.OrigemMaterial == OrigemMaterial.Obtencao && !i.FlagCotadoPO).Count() > 0;
	        }
	    }

	    #endregion

        #region Delineamento
        public virtual void AddDelineamento(PedidoServicoDelineamento delineamento)
        {
            delineamento.DelineamentoOrcamento = this;
            bool isNew = !delineamento.IsPersisted;

            delineamento.Data = DateTime.Now;
            delineamento.Save();

            if (isNew)
                _itensDelineamento.Add(delineamento);
        }

        public virtual void RemoveDelineamento(PedidoServicoDelineamento delineamento)
        {
            delineamento.Delete();
            _itensDelineamento.Remove(delineamento);
        }
        #endregion

        #region Orçamento
        public virtual void AddItemOrcamento(PedidoServicoItemOrcamento item)
        {
            item.DelineamentoOrcamento = this;
            item.Save();
            _itensOrcamento.Add(item);
        }

        public virtual void RemoveItemOrcamento(PedidoServicoItemOrcamento item)
        {
            item.Delete();
            _itensOrcamento.Remove(item);
        }
        #endregion

        #region Aprovacao

        public static List<DelineamentoOrcamento> SelectOrcamentosParaAprovacao(int id_servidor)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"select distinct d from DelineamentoOrcamento d 
                    inner join d.Status s 
                    inner join s.Responsaveis resp
                    inner join fetch d.PedidoServico p
                    inner join fetch p.Cliente c                     
			where resp.ID = :id_servidor
			and (
                    d.Status.ID = :status OR
                    d.Status.ID = :status2
                )
			order by p.CodigoInterno");

            query.SetInt32("status", Convert.ToInt32(StatusPedidoServicoEnum.OrcamentoFinalizado));
            query.SetInt32("status2", Convert.ToInt32(StatusPedidoServicoEnum.AguardandoAprovacaoComandanteGeral));
            query.SetInt32("id_servidor", id_servidor);

            return (List<DelineamentoOrcamento>)query.List<DelineamentoOrcamento>();
        }

        #endregion

		#region Workflow


        #region Metodos Okay para o BACS

        public virtual void EnviarParaDelineamento(int id_servidor)
        {
            if (this.Status.StatusPedidoServicoEnum != StatusPedidoServicoEnum.AguardandoEnvioParaDelineamentoAprovar)
                throw new Exception("Este pedido não pode ser encaminhado.");

            if (this.Delineamentos.Count == 0)
                throw new Exception("Insira pelo menos um delineador.");

            using (TransactionBlock tran = new TransactionBlock())
            {
                HistoricoPedidoServico historico = GetHistorico(id_servidor, StatusPedidoServicoEnum.EmDelineamento);

                historico.Save();

                this.Status = historico.StatusPosterior;
                this.PedidoServico.Status = this.Status;

                _flagRecusado = false;
                base.Save();

                this._pedidoservico.Historico.Add(historico);
                this._pedidoservico.Save();


                tran.IsValid = true;
            }
        }


        /// <summary>
        /// Acusa a finalizacao do delineamento e passa para a etapa de orçamento
        /// </summary>
        public virtual void FinalizarDelineamento(int id_servidor, string comentario)
        {
            if (this.Status.StatusPedidoServicoEnum != StatusPedidoServicoEnum.EmDelineamento)
                throw new Exception("Este delineamento não pode ser finalizado.");

            if (this.ItensDelineamento.Count == 0)
                throw new Exception("Adicione pelo menos um item de delineamento.");

            DelineamentoOficina delineamentoOficina = this.Delineamentos.Where(d => d.Servidor.ID == id_servidor).First();

            if (delineamentoOficina.Enviado)
                throw new Exception("Este delineamento já foi enviado.");

            using (TransactionBlock tran = new TransactionBlock())
            {
                delineamentoOficina.Comentario = comentario;
                delineamentoOficina.Enviado = true;
                delineamentoOficina.FlagRecusado = false;
                delineamentoOficina.Save();

                //vai para a proxima fase apenas se todos os delineamentos foram enviados
                if (this.Delineamentos.Where(d => !d.Enviado).Count() == 0)
                {
                    HistoricoPedidoServico historico = GetHistorico(id_servidor, StatusPedidoServicoEnum.AguardandoOrcamento);//AguardandoAprovacaoDelineamento

                    //historico.JustificativaRecusa = comentario;
                    historico.Save();
                    this._pedidoservico.Historico.Add(historico);

                    //this._pedidoservico.Status = StatusPedidoServico.Get(StatusPedidoServicoEnum.OrcamentoFinalizado);
                    //_pedidoservico.CategoriaServico = this.CategoriaServico;
                    //this._pedidoservico.Save();

                    this._servidor = historico.Servidor;
                    this._status = historico.StatusPosterior;
                    this._flagRecusado = false;
                    this._data = DateTime.Now;

                    base.Save();

                    this._pedidoservico.Status = this.Status;
                    this._pedidoservico.Save();
                }
                tran.IsValid = true;
            }
        }

        public virtual void AprovarDelineamento(int id_servidor)
        {
            HistoricoPedidoServico historico;

            //if (this.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoAprovacaoComandanteDCPC)
            //{
            //    historico = GetHistorico(id_servidor, StatusPedidoServicoEnum.AguardandoAprovacaoComandanteGeral);
            //    _flagRecusado = false;
            //}

            //else 
            if (this.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoAprovacaoComandanteGeral)
            {
                historico = GetHistorico(id_servidor, StatusPedidoServicoEnum.AguardandoEnvioMensagemCliente);
                _flagRecusado = false;
            }

            else if (this.Delineamentos.Where(d => d.FlagRecusado).Count() == 0)
            {
                historico = GetHistorico(id_servidor, StatusPedidoServicoEnum.AguardandoOrcamento);// AguardandoDesignacaoCotador
                _flagRecusado = false;
            }

            else
            {
                historico = GetHistorico(id_servidor, StatusPedidoServicoEnum.EmDelineamento);
                this._flagRecusado = true;
                
                foreach (DelineamentoOficina delineamentoOficina in this.Delineamentos.Where(d => d.FlagRecusado))
                {
                    historico.JustificativaRecusa += string.Format("Delineamento de {0} recusado. Justificativa: {1}<br>", delineamentoOficina.Servidor.NomeCompleto, delineamentoOficina.Justificativa);
                    delineamentoOficina.Enviado = false;
                }
            }
            
            using (TransactionBlock tran = new TransactionBlock())
            {

                foreach (DelineamentoOficina delineamentoOficina in this.Delineamentos)
                    delineamentoOficina.Save();

                historico.Save();

                this.Status = historico.StatusPosterior;
                base.Save();

                this.PedidoServico.Historico.Add(historico);

                this._pedidoservico.Status = this.Status;
                this._pedidoservico.Save();

                tran.IsValid = true;
            }
        }

        public virtual void DesignarCotador(int id_servidor, int id_servidorCotador, string comentario)
        {
            if (this.Status.StatusPedidoServicoEnum != StatusPedidoServicoEnum.Nenhum)//AguardandoDesignacaoCotador
                throw new Exception("Status inválido.");

            if (id_servidorCotador == 0)
                throw new Exception("Selecione o cotador.");

            using (TransactionBlock tran = new TransactionBlock())
            {
                HistoricoPedidoServico historico = GetHistorico(id_servidor, StatusPedidoServicoEnum.AguardandoOrcamento);

                historico.JustificativaRecusa = comentario;
                historico.Save();

                this.ServidorCotador = Business.Servidor.Get(id_servidorCotador);

                this.Status = historico.StatusPosterior;
                this.PedidoServico.Status = this.Status;

                _flagRecusado = false;
                base.Save();

                this._pedidoservico.Historico.Add(historico);
                this._pedidoservico.Save();


                tran.IsValid = true;
            }
        }

        public virtual void FinalizarCotacao(int id_servidor, string comentario)
        {
            if (this.Status.StatusPedidoServicoEnum != StatusPedidoServicoEnum.AguardandoOrcamento)
                throw new Exception("Status inválido.");

            foreach (PedidoServicoItemOrcamento pedidoServicoItemOrcamento in this.ItensOrcamento)
            {
                if(pedidoServicoItemOrcamento.Fornecedor == null && pedidoServicoItemOrcamento.OrigemMaterial == OrigemMaterial.Obtencao)
                    throw new Exception("Selecione o fornecedor para todos os itens de obtenção.");
            }
            
            using (TransactionBlock tran = new TransactionBlock())
            {
                HistoricoPedidoServico historico = GetHistorico(id_servidor, StatusPedidoServicoEnum.OrcamentoFinalizado);

                historico.JustificativaRecusa = comentario;
                historico.Save();

                //this.Comentario = comentario;
                
                this.Status = historico.StatusPosterior;
                this.PedidoServico.Status = this.Status;

                _flagRecusado = false;
                base.Save();

                this._pedidoservico.Historico.Add(historico);
                this._pedidoservico.Save();


                tran.IsValid = true;
            }
        }

        public virtual void RegistrarMensagemCliente(int id_servidor, string mensagemCliente)
        {
            HistoricoPedidoServico historico;
            
            if (this.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoEnvioParaDelineamento)
            {
                historico = GetHistorico(id_servidor, StatusPedidoServicoEnum.AguardandoEnvioParaDelineamentoAprovar);
            }
            
            else if (this.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoEnvioMensagemCliente)
            {
                historico = GetHistorico(id_servidor, StatusPedidoServicoEnum.AguardandoAprovacaoCliente);
                this._mensagemEnvioCliente = mensagemCliente;
            }

            else if (this.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoAprovacaoCliente)
            {
                historico = GetHistorico(id_servidor, StatusPedidoServicoEnum.AguardandoVerificacaoEstoque);
                this._mensagemEnvioCliente = mensagemCliente;
            }

            else if (this.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoVerificacaoEstoque)
            {
                historico = GetHistorico(id_servidor, StatusPedidoServicoEnum.AguardandoComprometimentoCliente);
                this._mensagemEnvioCliente = mensagemCliente;
            }
           
            else if (this.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoChamadaMeio)
            {
                historico = GetHistorico(id_servidor, StatusPedidoServicoEnum.AguardandoMeio);
                this._mensagemChamadaMeio = mensagemCliente;
            }

            else if (this.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoMensagemProntificacao)
            {
                historico = GetHistorico(id_servidor, StatusPedidoServicoEnum.AguardandoEmissaoFaturamentoFinal);
                this._mensagemProntificacao = mensagemCliente;
            }

            //else if (this.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoMensagemProntificacao)
            //{
            //    historico = GetHistorico(id_servidor, StatusPedidoServicoEnum.AguardandoEmissaoFaturamentoFinal);
            //    this._mensagemProntificacao = mensagemCliente;
            //}
            else if (this.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoSatisfeito)
            {
                historico = GetHistorico(id_servidor, StatusPedidoServicoEnum.AguardandoEmissaoFaturamentoFinal);
                this._mensagemProntificacao = mensagemCliente;
            }
            else if (this.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoEmissaoFaturamentoFinal)
            {
                historico = GetHistorico(id_servidor, StatusPedidoServicoEnum.Finalizado);
                //TODO: verificar se é o unico orcamento aberto do PS, se for finalizar o PS tb

            }

            else
                throw new Exception("Status inexperado.");

            using (TransactionBlock tran = new TransactionBlock())
            {
                historico.Save();

                this.Status = historico.StatusPosterior;
                _flagRecusado = false;
              
                base.Save();

                this.PedidoServico.Historico.Add(historico);
                tran.IsValid = true;
            }
        }

        public virtual void RegistrarIndicacaoRecurso(int id_servidor, string mensagemCliente)
        {
            if(this.Status.StatusPedidoServicoEnum != StatusPedidoServicoEnum.AguardandoComprometimentoCliente)
                throw new Exception("Status inesperado.");

            HistoricoPedidoServico historico = GetHistorico(id_servidor, StatusPedidoServicoEnum.AguardandoPlanejamento);

            if (_status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoComprometimentoCliente)
                CotacaoComprometido();

            this._comprometimentoCliente = mensagemCliente;

            using (TransactionBlock tran = new TransactionBlock())
            {
                historico.Save();

                this.Status = historico.StatusPosterior;
                _flagRecusado = false;

                base.Save();

                this.PedidoServico.Historico.Add(historico);
                tran.IsValid = true;
            }
        }

        private void CotacaoComprometido()
        {
            //Verifica se existem itens do singra, PEP e Rodizio
            bool precisaPO = false;

            foreach (PedidoServicoItemOrcamento itemOrcamento in _itensOrcamento)
            {
                if (itemOrcamento.OrigemMaterial == OrigemMaterial.Rodizio)
                {
                    _statusEntregaMaterialRodizio = StatusEntregaMaterial.AguardandoEntrega;
                    precisaPO = true;
                }
                else if (itemOrcamento.OrigemMaterial == OrigemMaterial.PEP)
                {
                    _statusEntregaMaterialPEP = StatusEntregaMaterial.AguardandoEntrega;
                    precisaPO = true;
                }
                else if (itemOrcamento.OrigemMaterial == OrigemMaterial.Obtencao)
                    precisaPO = true;
            }

            if (precisaPO)
            {
                if (_itensOrcamento.Where(i => i.ServicoMaterial.TipoServicoMaterial == TipoServicoMaterial.Servico).Count() > 0)
                    CriarPOs(true);
                if (_itensOrcamento.Where(i => i.ServicoMaterial.TipoServicoMaterial != TipoServicoMaterial.Servico).Count() > 0)
                    CriarPOs(false);
            }
        }

        private void CriarPOs(bool flagServico)
        {
            //Cria PO de Serviço

            Business.PedidoObtencao po = new PedidoObtencao();
            po.Aplicacao = "PS " + CodigoComAno + "<br>Indicativo Naval: " + _pedidoservico.Cliente.IndicativoNaval + "<br>PROGEM: " + (_pedidoservico.FlagProgem ? "Sim" : "Não") + "<br>Nº Equipamentos:" + _pedidoservico.Equipamentos.Count;
            po.Celula = Celula.Get(this.PedidoServico.Celula.ID);
            po.DataEmissao = DateTime.Now;
            po.DelineamentoOrcamento = DelineamentoOrcamento.Get(this.ID);
            po.FlagPedidoObtencao = false;
            po.Servidor = Servidor.Get(this._servidor.ID);
            po.Status = StatusPedidoObtencao.Get(StatusPedidoObtencaoEnum.AguardandoAprovacaoChefeDepartamento_Servidor);
            po.TipoPedido = TipoPedido.PedidoObtencao;
            po.TipoPedidoObtencao = TipoPedidoObtencao.MaterialServico;
            po.TipoCompra = TipoCompra.Get(2);
            po.Modalidade = Modalidade.Get(2);
            po.OrigemPO = OrigemPO.PS;
            po.IsValidar = false;
            po.Save();

            //po.Status = StatusPedidoObtencao.Get(StatusPedidoObtencaoEnum.AguardandoAprovacaoChefeDepartamento_Servidor);
            //po.AlterarPS();

            foreach (PedidoServicoItemOrcamento itemOrcamento in _itensOrcamento.Where(i => (i.ServicoMaterial.TipoServicoMaterial == TipoServicoMaterial.Servico) == flagServico))
            {
                Business.PedidoObtencaoItem item = new PedidoObtencaoItem();
                item.ServicoMaterial = itemOrcamento.ServicoMaterial;
                item.Quantidade = itemOrcamento.Quantidade;
                item.Valor = itemOrcamento.Valor;
                item.OrigemMaterial = itemOrcamento.OrigemMaterial;
                item.PedidoObtencao = po;
                item.Especificacao = itemOrcamento.Observacao;
                item.Fornecedor = itemOrcamento.Fornecedor;
                item.Save();
                po.Itens.Add(item);
            }
            //po.Enviar(this.Servidor.ID);

            this.PedidoObtencao = po;
        }

	    public virtual void Aprovar(int id_servidor, string comentario)
        {
            StatusPedidoServicoEnum novoStatus;
            switch (this._status.StatusPedidoServicoEnum)
            {
                //case StatusPedidoServicoEnum.AguardandoAprovaçãoDivProgControle:
                //    novoStatus = StatusPedidoServicoEnum.AguardandoMensagemProntificacao;
                //    break;
                case StatusPedidoServicoEnum.OrcamentoFinalizado:
                    novoStatus = StatusPedidoServicoEnum.AguardandoAprovacaoComandanteGeral;
                    break;

                case StatusPedidoServicoEnum.AguardandoAprovacaoComandanteGeral:
                    novoStatus = StatusPedidoServicoEnum.AguardandoEnvioMensagemCliente;
                    break;

                default:
                    throw new Exception("Status inesperado.");
            }


            HistoricoPedidoServico historico = GetHistorico(id_servidor, novoStatus);

            historico.JustificativaRecusa = comentario;

            using (TransactionBlock tran = new TransactionBlock())
            {
                historico.Save();

                this.Status = historico.StatusPosterior;
                _flagRecusado = false;
                base.Save();

                this.PedidoServico.Historico.Add(historico);
                tran.IsValid = true;
            }
        }

        #endregion

    


      
        
      
        
        public virtual void EfetuarPlanejamento(int id_servidor, Dictionary<int, DateTime> listDatas)
        {
            if (this.Status.StatusPedidoServicoEnum != StatusPedidoServicoEnum.AguardandoPlanejamento)
                throw new Exception("Este orçamento não pode ser planejado. Status inesperado");

            using (TransactionBlock tran = new TransactionBlock())
            {
                HistoricoPedidoServico historico = GetHistorico(id_servidor, StatusPedidoServicoEnum.AguardandoChamadaMeio);
                
                historico.Save();
                this._pedidoservico.Historico.Add(historico);
                this._pedidoservico.Save();
               
                this._status = historico.StatusPosterior;
                this._flagRecusado = false;
                base.Save();

                foreach (KeyValuePair<int, DateTime> keyValuePair in listDatas)
                {
                    PedidoServicoDelineamento delineamento = _itensDelineamento.Find(keyValuePair.Key);
                    delineamento.DataPrevisaoInicio = keyValuePair.Value;
                    delineamento.Save();
                }

                tran.IsValid = true;
            }
        }
       
       
        public virtual void RegistrarChegadaMeio(int id_servidor)
        {
            if (this.Status.StatusPedidoServicoEnum != StatusPedidoServicoEnum.AguardandoMeio)
                throw new Exception("Status inesperado");

            StatusPedidoServicoEnum proximoStatus = StatusPedidoServicoEnum.AguardandoInicioExecucao;
            AlterarStatus(id_servidor, proximoStatus);
        }

        public virtual void RegistrarInicioExecucao(int id_servidor, DateTime dataPrevisaoEntrega, string comentario)
        {
            if (this.Status.StatusPedidoServicoEnum != StatusPedidoServicoEnum.AguardandoInicioExecucao)
                throw new Exception("Status inesperado");

            this.PedidoServico.PrevisaoEntrega = dataPrevisaoEntrega;
            this.PedidoServico.Diversos = comentario;
            
            StatusPedidoServicoEnum proximoStatus = StatusPedidoServicoEnum.EmExecucao;
            AlterarStatus(id_servidor, proximoStatus);
        }

        public virtual void RegistrarFimExecucao(int id_servidor, string numeroNL)
        {
            if (this.Status.StatusPedidoServicoEnum != StatusPedidoServicoEnum.EmExecucao)
                throw new Exception("Status inesperado");

            _numeroNL = numeroNL;

            StatusPedidoServicoEnum proximoStatus = StatusPedidoServicoEnum.AguardandoMensagemProntificacao;
            AlterarStatus(id_servidor, proximoStatus);
        }

        //public virtual void RegistrarEmissaoFaturamento(int id_servidor)
        //{
        //    if (this.Status.StatusPedidoServicoEnum != StatusPedidoServicoEnum.AguardandoEmissaoFaturamentoFinal)
        //        throw new Exception("Status inesperado");

        //    if(Math.Round(ValorAFaturar, 2) > 0 && _categoriaServico.FlagRequerFaturamento)
        //        throw new Exception("O valor faturado deve ser igual ao valor do orçamento");

        //    StatusPedidoServicoEnum proximoStatus = StatusPedidoServicoEnum.AguardandoSatisfeito;
        //    AlterarStatus(id_servidor, proximoStatus);
        //}

        //public virtual void RegistrarDevolucaoMeio(int id_servidor, string comentario)
        //{
        //    if (this.Status.StatusPedidoServicoEnum != StatusPedidoServicoEnum.AguardandoDevolucaoMeio)
        //        throw new Exception("Status inesperado");

        //    this.ComentarioDevolucao = comentario;
        //    StatusPedidoServicoEnum proximoStatus = StatusPedidoServicoEnum.AguardandoSatisfeito;
        //    AlterarStatus(id_servidor, proximoStatus);
        //}

        //public virtual void RegistrarSatisfeito(int id_servidor)
        //{
        //    if (this.Status.StatusPedidoServicoEnum != StatusPedidoServicoEnum.AguardandoSatisfeito)
        //        throw new Exception("Status inesperado");
            
        //    StatusPedidoServicoEnum proximoStatus = StatusPedidoServicoEnum.AguardandoLiquidacao;
        //    AlterarStatus(id_servidor, proximoStatus);
        //}

        public virtual void ColocarEmGarantia(int id_servidor)
        {
            if (this.Status.StatusPedidoServicoEnum != StatusPedidoServicoEnum.Finalizado)
                throw new Exception("Status inesperado");

            StatusPedidoServicoEnum proximoStatus = StatusPedidoServicoEnum.EmGarantia;
            AlterarStatus(id_servidor, proximoStatus);
        }

        public virtual void TirarDaGarantia(int id_servidor)
        {
            if (this.Status.StatusPedidoServicoEnum != StatusPedidoServicoEnum.EmGarantia)
                throw new Exception("Status inesperado");

            StatusPedidoServicoEnum proximoStatus = StatusPedidoServicoEnum.Finalizado;
            AlterarStatus(id_servidor, proximoStatus);
        }
               

	    private void AlterarStatus(int id_servidor, StatusPedidoServicoEnum proximoStatus)
	    {
	       AlterarStatus(id_servidor, proximoStatus, null);
	    }

        private void AlterarStatus(int id_servidor, StatusPedidoServicoEnum proximoStatus, string comentario)
        {
            using (TransactionBlock tran = new TransactionBlock())
            {
                HistoricoPedidoServico historico = GetHistorico(id_servidor, proximoStatus);
                historico.JustificativaRecusa = comentario;

                historico.Save();
                this._pedidoservico.Historico.Add(historico);
                this._pedidoservico.Save();

                this._status = historico.StatusPosterior;
                this._flagRecusado = false;

                base.Save();

                tran.IsValid = true;
            }
        }

	    private HistoricoPedidoServico GetHistorico(int id_servidor, StatusPedidoServicoEnum novoStatus)
        {
            HistoricoPedidoServico historico = new HistoricoPedidoServico();
            historico.Data = DateTime.Now;
            historico.PedidoServico = this.PedidoServico;
            historico.StatusAnterior = this.Status;
            historico.Servidor = Servidor.Get(id_servidor);
            historico.StatusPosterior = Business.StatusPedidoServico.Get(novoStatus);
            historico.ID_DelineamentoOrcamento = this.ID;
            return historico;
        }

        /// <summary>
        /// Retorna para o status anterior (REVER ESSE METODO PARA O BACS)
        /// </summary>
        public virtual void Recusar(int id_servidor, string justificativa)
        {
            if(String.IsNullOrEmpty(justificativa))
                throw new Exception("Quando o pedido é recusado, o comentário é obrigatório.");
            StatusPedidoServicoEnum novoStatus;
            // se é o primeiro status do orcamento temos q alterar o status do PS tambem e alterar o status do orcamento para NAO ENVIADO
            if (this.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.EmDelineamento)
            {

                _pedidoservico.Status = StatusPedidoServico.Get(StatusPedidoServicoEnum.AguardandoEnvioParaDelineamentoAprovar);
                _pedidoservico.FlagRecusado = true;
                _pedidoservico.Save();
                novoStatus = StatusPedidoServicoEnum.AguardandoEnvioParaDelineamentoAprovar;
            }
            else if(this.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.OrcamentoFinalizado && this.PedidoServico.UsaRotina)
            {
                using(TransactionBlock tran = new TransactionBlock())
                {
                    _pedidoservico.Status = StatusPedidoServico.Get(StatusPedidoServicoEnum.NaoEnviado);
                    _pedidoservico.FlagRecusado = true;
                    _pedidoservico.Orcamentos.Remove(this);
                    _pedidoservico.Save();
                    novoStatus = StatusPedidoServicoEnum.NaoEnviado;
                    DeleteOrcamento();
                    tran.IsValid = true;
                }
                
            }
            else 
                novoStatus = StatusPedidoServico.GetAnterior(_status.StatusPedidoServicoEnum);

            HistoricoPedidoServico historico = GetHistorico(id_servidor, novoStatus);
            historico.JustificativaRecusa = justificativa;

            if (novoStatus == StatusPedidoServicoEnum.NaoEnviado)
                historico.ID_DelineamentoOrcamento = null;

            using (TransactionBlock tran = new TransactionBlock())
            {
                historico.Save();

                this.Status = historico.StatusPosterior;
                _flagRecusado = true;

                base.Save();

                this.PedidoServico.Historico.Add(historico);
                tran.IsValid = true;
            }
        }

        public virtual void DeleteOrcamento()
        {
            using(TransactionBlock tran = new TransactionBlock())
            {
                foreach (var historico in this.PedidoServico.Historico.Where(x => x.ID_DelineamentoOrcamento == this.ID))
                {
                    historico.ID_DelineamentoOrcamento = null;
                    historico.Save();
                }

                foreach (DelineamentoOrcamentoOcorrencia ocorrencia in Ocorrencias)
                {
                    ocorrencia.Delete();
                }

                foreach (PedidoServicoItemOrcamento itemOrcamento in ItensOrcamento)
                {
                    itemOrcamento.Delete();
                }

                foreach (PedidoServicoDelineamento item in ItensDelineamento)
                {
                    item.Delete();
                }

                foreach (var item in Delineamentos)
                {
                    item.Delete();
                }
                
                this.Delete();
                tran.IsValid = true;
            }
        }

	    #endregion

        #region ToString / CriarNovoOrcamento / PreencherTaxas

        public override string ToString()
        {
            return CodigoComAno;
        }

	    public virtual void CriarNovoOrcamento(PedidoServico pedidoServico, Servidor servidor, IList<Rotina> rotinas)
	    {
	        this.PedidoServico = pedidoServico;
	        this.CategoriaServico = pedidoServico.CategoriaServico;
	        this._servidor = servidor;
	        this._numero = _pedidoservico.Orcamentos.Count + 1;
	        this._flagRecusado = false;
	        this._data = DateTime.Now;

            this.Rotinas.Clear();

            if(rotinas.Count > 0)
            {
                this._status = StatusPedidoServico.Get(StatusPedidoServicoEnum.OrcamentoFinalizado);
                foreach (Rotina rotina in rotinas)
                {
                    this.Rotinas.Add(rotina);
                }
            }
            else
            {
                this._status = StatusPedidoServico.Get(StatusPedidoServicoEnum.AguardandoEnvioParaDelineamentoAprovar);    
            }
            
	        PreencherTaxas();

	        this.Save();
	        _pedidoservico.Orcamentos.Add(this);

            if(rotinas.Count > 0)
            {
                foreach (Rotina rotina in rotinas)
                {
                    AdicionarRotina(rotina, pedidoServico.CategoriaServico.ID);    
                }
            }
            
	    }
       
	    private void PreencherTaxas()
	    {
	        Parametro parametro = Parametro.Get();
	        _percentualMaoObraIndireta = parametro.PercentualMaoObraIndireta;
	        _percentualMaterialIndireto = parametro.PercentualMaterialIndireto;
	        _percentualServicoTerceiroIndireto = parametro.PercentualServicoTerceiroIndireto;
	        _taxaContribuicaoOperacionalMaoObra = parametro.TaxaContribuicaoOperacionalMaoObra;
	        _taxaContribuicaoOperacionalMaterial = parametro.TaxaContribuicaoOperacionalMaterial;
	        _taxaOperacionalMaoObra = parametro.TaxaOperacionalMaoObra;
	        _taxaOperacionalMaterialServico = parametro.TaxaOperacionalMaterialServico;
	        _valorMaoObraHora = parametro.ValorMaoObraHora;
	        _percentualDescontoSubTotalMaoObra = parametro.PercentualDescontoSubTotalMaoObra;
	        _percentualDescontoSubTotalMaterialServicoTerceiro = parametro.PercentualDescontoSubTotalMaterialServicoTerceiro;
	    }

        #endregion

	    #region Selects

        public static List<DelineamentoOrcamento> Select(string texto, DateTime dataInicio, DateTime dataFim, int id_status, int id_gerente, int id_equipamento,
            string numeroPedidoCliente, int id_cliente)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"from DelineamentoOrcamento o inner join fetch o.PedidoServico p inner join fetch p.Cliente c 
			where dbo.DateIsInBetween(p.DataEmissao, :dataInicio, :dataFim) = 1
            and (c.ID = IsNull(:id_cliente, c.ID) OR p.ClientePagador.ID = IsNull(:id_cliente, p.ClientePagador.ID))
			and (dbo.BuscaCodigoPS(p.CodigoInterno, p.DataEmissao, :texto) = 1            
			or c.Descricao like :texto)
			and IsNull(p.CodigoPedidoCliente, 'xxx') like :codigoPedidoCliente
			and o.Status.ID = IsNull(:id_status, o.Status.ID)
			and IsNull(p.ServidorGerente.ID, -1) = IsNull(:id_gerente, IsNull(p.ServidorGerente.ID, -1))			
			order by p.CodigoInterno");

            query.SetParameter("texto", string.Format("%{0}%", texto));
            query.SetParameter("codigoPedidoCliente", string.Format("%{0}%", numeroPedidoCliente));
            query.SetParameter("dataInicio", BusinessHelper.IsNull(dataInicio), NHibernateUtil.DateTime);
            query.SetParameter("dataFim", BusinessHelper.IsNull(dataFim), NHibernateUtil.DateTime);
            query.SetParameter("id_status", BusinessHelper.IsNullOrZero(id_status), NHibernateUtil.Int32);
            query.SetParameter("id_cliente", BusinessHelper.IsNullOrZero(id_cliente), NHibernateUtil.Int32);
            query.SetParameter("id_gerente", BusinessHelper.IsNullOrZero(id_gerente), NHibernateUtil.Int32);
            //query.SetParameter("id_equipamento", BusinessHelper.IsNullOrZero(id_equipamento), NHibernateUtil.Int32);
            return (List<DelineamentoOrcamento>)query.List<DelineamentoOrcamento>();
        }

        public static List<IPedido> SelectPorGerente(string texto, DateTime dataInicio, DateTime dataFim, int id_status, int id_gerente, int id_equipamento, string numeroPedidoCliente)
        {
            List<DelineamentoOrcamento> orcamentos = Select(texto, dataInicio, dataFim, id_status, id_gerente, id_equipamento, numeroPedidoCliente, Int32.MinValue);
            List<PedidoServico> pedidos = PedidoServico.Select(texto, dataInicio, dataFim, id_status, id_gerente, id_equipamento, numeroPedidoCliente, "", Int32.MinValue);

            List<IPedido> list = new List<IPedido>();
            foreach (PedidoServico pedido in pedidos)
            {
                if(pedido.Status.ID <= Convert.ToInt32(StatusPedidoServicoEnum.EmDelineamento))
                    list.Add(pedido);
            }

            foreach (DelineamentoOrcamento orcamento in orcamentos)
            {
                if(orcamento.Status.ID >= Convert.ToInt32(StatusPedidoServicoEnum.OrcamentoFinalizado))
                    list.Add(orcamento);
            }
            return list;
        }

        public static IEnumerable FastSearch(string texto, StatusPedidoServicoEnum? status)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"select new DelineamentoOrcamentoUI(d.ID, p.CodigoInterno, p.DataEmissao, d.Numero)
            from DelineamentoOrcamento d inner join d.PedidoServico p
			where dbo.BuscaCodigoPS(p.CodigoInterno, p.DataEmissao, :texto) = 1
			and  d.Status = IsNull(:status, d.Status)
            and  p.Status != :id_statusCancelado
			order by d.ID DESC");

            query.SetMaxResults(20);
            query.SetString("texto", "%" + texto + "%");
            query.SetInt32("id_statusCancelado", Convert.ToInt32(StatusPedidoServicoEnum.Cancelado));
            query.SetParameter("status", BusinessHelper.IsNullOrZero(status.HasValue ? Convert.ToInt32(status) : Int32.MinValue), NHibernateUtil.Int32);

            return query.List<DelineamentoOrcamentoUI>();
        }

        public static List<DelineamentoOrcamento> SelectParaFaturamento(string texto, DateTime dataInicio, DateTime dataFim, int id_status, 
            bool? flagApenasFaturados, int ano)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"from DelineamentoOrcamento o 
                        inner join fetch o.PedidoServico p 
                        inner join fetch p.Cliente c                         	
                        inner join fetch o.Status s
                        inner join fetch o.CategoriaServico cs
                        
			where dbo.DateIsInBetween(p.DataEmissao, :dataInicio, :dataFim) = 1
			and (dbo.BuscaCodigoPS(p.CodigoInterno, p.DataEmissao, :texto) = 1
			or c.Descricao like :texto)
			and s.ID = IsNull(:id_status, s.ID)
            and s.ID > :statusComprometimentoCliente
            and s.ID != :statusCancelado
            and cs.FlagRequerFaturamento = 1
            and dbo.GetYear(p.DataEmissao) = IsNull(:ano, dbo.GetYear(p.DataEmissao))
			order by p.CodigoInterno");

            query.SetParameter("texto", string.Format("%{0}%", texto));
            query.SetParameter("dataInicio", BusinessHelper.IsNull(dataInicio), NHibernateUtil.DateTime);
            query.SetParameter("dataFim", BusinessHelper.IsNull(dataFim), NHibernateUtil.DateTime);
            query.SetParameter("id_status", BusinessHelper.IsNullOrZero(id_status), NHibernateUtil.Int32);
            query.SetParameter("ano", BusinessHelper.IsNullOrZero(ano), NHibernateUtil.Int32);
            query.SetInt32("statusComprometimentoCliente", Convert.ToInt32(StatusPedidoServicoEnum.AguardandoComprometimentoCliente));
            query.SetInt32("statusCancelado", Convert.ToInt32(StatusPedidoServicoEnum.Cancelado));

            if(!flagApenasFaturados.HasValue)
                return (List<DelineamentoOrcamento>)query.List<DelineamentoOrcamento>();
            else
            {
                List<DelineamentoOrcamento> list = new List<DelineamentoOrcamento>();
                foreach (DelineamentoOrcamento orcamento in query.List<DelineamentoOrcamento>())
                {
                    if( (orcamento.Faturamentos.Count > 0) == flagApenasFaturados.Value)
                        list.Add(orcamento);
                }
                return list;
            }
        }


        public static List<DelineamentoOrcamento> SelectParaPO(string texto, DateTime dataInicio, DateTime dataFim,
           int ano, int id_servidor)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();
            IQuery query = session.CreateQuery(
            @"from DelineamentoOrcamento o 
                        inner join fetch o.PedidoServico p 
                        inner join fetch p.Cliente c                         
                        inner join fetch o.Status s
                        inner join fetch o.CategoriaServico cs
                        
			where dbo.DateIsInBetween(p.DataEmissao, :dataInicio, :dataFim) = 1
			and (dbo.BuscaCodigoPS(p.CodigoInterno, p.DataEmissao, :texto) = 1
			or c.Descricao like :texto)			
            and s.ID >= :statusAguardandoInicioExecucao
            and s.ID != :statusCancelado
            and cs.FlagRequerFaturamento = 1
            and dbo.GetYear(p.DataEmissao) = IsNull(:ano, dbo.GetYear(p.DataEmissao))
            and o.ServidorCotador.ID = :id_servidor
			order by p.CodigoInterno");

            query.SetParameter("texto", string.Format("%{0}%", texto));
            query.SetParameter("dataInicio", BusinessHelper.IsNull(dataInicio), NHibernateUtil.DateTime);
            query.SetParameter("dataFim", BusinessHelper.IsNull(dataFim), NHibernateUtil.DateTime);
            query.SetParameter("ano", BusinessHelper.IsNullOrZero(ano), NHibernateUtil.Int32);
            query.SetInt32("statusAguardandoInicioExecucao", Convert.ToInt32(StatusPedidoServicoEnum.AguardandoInicioExecucao));
            query.SetInt32("statusCancelado", Convert.ToInt32(StatusPedidoServicoEnum.Cancelado));
            query.SetInt32("id_servidor", id_servidor);

            IList<DelineamentoOrcamento> orcamentos = query.List<DelineamentoOrcamento>();

            return orcamentos.Where(o => o.ExisteItensDisponiveisPO).ToList();

        }

        #endregion

        #region Faturamento

        public virtual void InsereFaturamento(DelineamentoOrcamentoFaturamento faturamento, int ID_Servidor)
        {
            if( Math.Round(ValorFaturado + faturamento.Valor, 2) > Math.Round(ValorTotalOrcamento, 2))
                throw new Exception("O valor faturado não pode ser maior que o valor total do orçamento");

            faturamento.DelineamentoOrcamento = this;
            faturamento.Save();
            this.Faturamentos.Add(faturamento);
            this.Aprovar(ID_Servidor, "");
        }

	    public virtual decimal ValorFaturado
	    {
	        get
	        {
	            decimal valor = 0;
	            foreach (DelineamentoOrcamentoFaturamento faturamento in Faturamentos)
	            {
	                valor += faturamento.Valor;
	            }
	            return valor;
	        }
	    }

        public virtual decimal ValorAFaturar
        {
            get
            {
                return ValorTotalOrcamento - ValorFaturado;
            }
        }

	    public virtual bool Desconto100Porcento
	    {
            get { return ValorTotalOrcamento == 0 && (ValorDescontoMaoObra > 0 || ValorDescontoMaterial > 0) ; }
	    }

	    #endregion

        #region Demais

        public virtual void CancelarItem(int id_item)
        {
            using (TransactionBlock tran = new TransactionBlock())
            {
                PedidoServicoItemOrcamento item = _itensOrcamento.Find(id_item);
                item.Delete();
                _itensOrcamento.Remove(item);
                bool finalizado = true;
                foreach (PedidoServicoItemOrcamento itemOrcamento in _itensOrcamento)
                {
                    if (itemOrcamento.Quantidade > itemOrcamento.QuantidadeEntregue)
                        finalizado = false;
                }

                if (finalizado)
                {
                    this.StatusEntregaMaterialSingra = StatusEntregaMaterial.MaterialEntregue;
                    base.Save();
                }
                tran.IsValid = true;
            }
        }

        public virtual void RecalcularTaxas()
        {
            PreencherTaxas();
            this.Save();
        }

        public virtual List<Etapa> GetEtapas()
        {
            return _pedidoservico.GetEtapas(this.ID);
        }

        public virtual List<PedidoServicoItemOrcamento> GetItensOrcamento(TipoServicoMaterial tipoServicoMaterial)
        {
            List<PedidoServicoItemOrcamento> list = new List<PedidoServicoItemOrcamento>();
            foreach (PedidoServicoItemOrcamento itemOrcamento in _itensOrcamento)
            {
                if (itemOrcamento.ServicoMaterial.TipoServicoMaterial == tipoServicoMaterial)
                    list.Add(itemOrcamento);
            }
            return list;
        }

        public virtual PedidoServicoItemOrcamento FindItemByMaterialId(int id_servicoMaterial)
        {
            foreach (PedidoServicoItemOrcamento itemOrcamento in _itensOrcamento)
            {
                if (itemOrcamento.ServicoMaterial.ID == id_servicoMaterial)
                    return itemOrcamento;
            }
            return null;
        }

        public virtual int TotalHHDelienado
        {
            get
            {
                int total = 0;
                foreach (PedidoServicoDelineamento delineamento in _itensDelineamento)
                {
                    total += delineamento.HomemHora;
                }
                return total;
            }
        }

        public virtual int TotalHHFamod
        {
            get
            {
                int total = 0;
                foreach (PedidoServicoDelineamento delineamento in _itensDelineamento)
                {
                    total += delineamento.HomemHora;
                }
                return total;
            }
        }

	    public virtual List<PedidoServicoItemOrcamento> ItensDisponiveisPO
	    {
            get
            {
                return this.ItensOrcamento.Where(i => i.OrigemMaterial == OrigemMaterial.Obtencao && !i.FlagCotadoPO).ToList();
            }
	    }

        /// <summary>
        /// Chamado ao inviar um PS com rotina
        /// </summary>
	    public virtual void AdicionarRotina(Rotina rotina, int categoriaId)
	    {
            RotinaCategoriaServico rotinaCategoriaServico = rotina.CategoriasServico.Where(c => c.CategoriaServico.ID == categoriaId).FirstOrDefault();

            if (rotinaCategoriaServico == null) return;

            foreach (RotinaCategoriaServicoDelineamento delineamento in rotinaCategoriaServico.ItensDelineamento)
            {
                PedidoServicoDelineamento novoDelineamento = new PedidoServicoDelineamento();
                novoDelineamento.Celula = delineamento.Celula;
                novoDelineamento.DescricaoServicoOficina = delineamento.DescricaoServicoOficina;
                novoDelineamento.HomemHora = delineamento.HomemHora;
                novoDelineamento.ServidorDelineamento = this.Servidor;
                novoDelineamento.DelineamentoOrcamento = this;
                novoDelineamento.Data = DateTime.Today;
                this.ItensDelineamento.Add(novoDelineamento);
                novoDelineamento.Save();
            }
            
            //List<PedidoServicoItemOrcamento> itemRemover = this.ItensOrcamento.Where(d => d.ServidorDelineamento.ID == this.Servidor.ID).ToList();
            //foreach (PedidoServicoItemOrcamento d in itemRemover)
            //{
            //    this.RemoveItemOrcamento(d);
            //}


            foreach (RotinaCategoriaServicoItemOrcamento item in rotinaCategoriaServico.ItensOrcamento)
            {
                PedidoServicoItemOrcamento novoItem = new PedidoServicoItemOrcamento();
                novoItem.Celula = item.Celula;
                novoItem.OrigemMaterial = item.OrigemMaterial;
                novoItem.ServicoMaterial = item.ServicoMaterial;
                novoItem.Valor = item.ServicoMaterial.PrecoEstimadoVenda;
                novoItem.Quantidade = item.Quantidade;
                novoItem.ServidorDelineamento = this.Servidor;
                novoItem.DelineamentoOrcamento = this;
                this.ItensOrcamento.Add(novoItem);
                novoItem.Save();
            }

            //StatusPedidoServicoEnum proximoStatus = StatusPedidoServicoEnum.OrcamentoFinalizado;
            //AlterarStatus(this.Servidor.ID, proximoStatus);
        }

        public virtual void RecalcularRotinas()
        {
            if (!PedidoServico.UsaRotina || this.Rotinas.Count == 0) return;

            using(TransactionBlock tran = new TransactionBlock())
            {
                var itemRemover = this.ItensOrcamento.ToList();
                foreach (PedidoServicoItemOrcamento d in itemRemover)
                {
                    this.RemoveItemOrcamento(d);
                }

                var delineamentoRemover = this.ItensDelineamento.ToList();
                foreach (var d in delineamentoRemover)
                {
                    this.RemoveDelineamento(d);
                }

                foreach (Rotina rotina in Rotinas)
                {
                    AdicionarRotina(rotina, this.CategoriaServico.ID);
                }

                tran.IsValid = true;
            }

        }

        #endregion
    }
}