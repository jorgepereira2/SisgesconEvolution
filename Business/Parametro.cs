using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class Parametro : BusinessObject<Parametro>	
	{
		#region Private Members
		private int _numerominimocotacoescompra; 
		private bool _entradaitemcompramanual;
	    private decimal _valorMaoObraHora;
	    private decimal _percentualMaoObraIndireta;
	    private decimal _taxaOperacionalMaoObra;
	    private decimal _percentualMaterialIndireto;
	    private decimal _percentualServicoTerceiroIndireto;
	    private decimal _taxaOperacionalMaterialServico;
	    private decimal _taxaContribuicaoOperacionalMaoObra;
	    private decimal _taxaContribuicaoOperacionalMaterial;
	    private int _maximoHorasFAMOD;
	    private int _maximoHorasMeioExpedienteFAMOD;
        private decimal _percentualDescontoSubTotalMaoObra;
        private decimal _percentualDescontoSubTotalMaterialServicoTerceiro;

	    private string _forca;
	    private string _organizacaoMilitar;
	    private string _cnpj;
	    private string _endereco;
	    private string _telefone;
	    private string _inscricaoEstadual;

	    private bool _flagPermiteACComLimiteEstourado;
	    private string _nomeSistema;
	    private decimal _valorMaximoSemOrcamentoPA;

	    private bool _flagUsaPA;
	    private string _validadeFaturamento;
	    private string _garantiaFaturamento;
        private bool _flagPermiteEntregaAbaixoEstoque;

	    private string _textoAC;
	    private Servidor _comandante;
	    private Servidor _ordenadorDespesaAC;
        private decimal _percentualLimiteAcimaLicitacao;

		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public Parametro()
		{
			_numerominimocotacoescompra = 0; 
			_entradaitemcompramanual = false;
		    _percentualMaoObraIndireta = 0;
		    _percentualMaterialIndireto = 0;
		    _percentualServicoTerceiroIndireto = 0;
		    _taxaContribuicaoOperacionalMaoObra = 0;
		    _taxaContribuicaoOperacionalMaterial = 0;
		    _taxaOperacionalMaoObra = 0;
		    _taxaOperacionalMaterialServico = 0;
		    _valorMaoObraHora = 0;
		    _maximoHorasFAMOD = 0;
		    _maximoHorasMeioExpedienteFAMOD = 0;
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Public Properties


        public virtual decimal ValorHomemHoraMergulho { get; set; }
        public virtual decimal ValorDeslocamentoMergulho { get; set; }
        public virtual decimal ValorTaxaBoteMergulho { get; set; }
        public virtual decimal ValorTaxaCatamaraMergulho { get; set; }
        public virtual decimal TaxaMaoObraDiretaMergulho { get; set; }
        public virtual decimal TaxaMaoObraIndiretaMergulho { get; set; }
        public virtual decimal TaxaOperacionalMaoObraMergulho { get; set; }
        public virtual decimal TaxaContribuicaoOperacionalMergulho { get; set; }
        public virtual decimal DescontoFRE170Mergulho { get; set; }
        public virtual decimal DescontoFRE171172Mergulho { get; set; }
        public virtual decimal ValorTaxaMaterialMergulho { get; set; }

        public virtual decimal DescontoFRE170AtividadeSecundaria { get; set; }
        public virtual decimal DescontoFRE171172AtividadeSecundaria { get; set; }
        public virtual decimal ValorHomemHoraAtividadeSecundaria { get; set; }
        public virtual decimal TaxaOperacionalMaterialServicoAtividadeSecundaria { get; set; }
        public virtual decimal TaxaOperacionalMaoObraAtividadeSecundaria { get; set; }
        public virtual decimal TaxaContribuicaoOperacionalAtividadeSecundaria { get; set; }

        public virtual string TextoImpressaoMensagemOrcamento { get; set; }
        public virtual string Email { get; set; }
        public virtual string TelefoneContabilidade { get; set; }
        public virtual decimal PercentualBloqueioLicitacao { get; set; }

        public virtual Servidor OrdenadorDespesaAC
        {
            get { return _ordenadorDespesaAC; }
            set { _ordenadorDespesaAC = value; }
        }
        public virtual Servidor Comandante
        {
            get { return _comandante; }
            set { _comandante = value; }
        }
        public virtual string TextoAC
        {
            get { return _textoAC; }
            set { _textoAC = value; }
        }

        public virtual string GarantiaFaturamento
        {
            get { return _garantiaFaturamento; }
            set { _garantiaFaturamento = value; }
        }
        public virtual string ValidadeFaturamento
        {
            get { return _validadeFaturamento; }
            set { _validadeFaturamento = value; }
        }
        public virtual bool FlagUsaPA
        {
            get { return _flagUsaPA; }
            set { _flagUsaPA = value; }
        }

        public virtual decimal ValorMaximoSemOrcamentoPA
        {
            get { return _valorMaximoSemOrcamentoPA; }
            set { _valorMaximoSemOrcamentoPA = value; }
        }

        public virtual string NomeSistema
        {
            get { return _nomeSistema; }
            set { _nomeSistema = value; }
        }
        
        public virtual bool FlagPermiteACComLimiteEstourado
        {
            get { return _flagPermiteACComLimiteEstourado; }
            set { _flagPermiteACComLimiteEstourado = value; }
        }
        
        public virtual string InscricaoEstadual
        {
            get { return _inscricaoEstadual; }
            set { _inscricaoEstadual = value; }
        }

        public virtual string Telefone
        {
            get { return _telefone; }
            set { _telefone = value; }
        }
        public virtual string Endereco
        {
            get { return _endereco; }
            set { _endereco = value; }
        }
        public virtual string CNPJ
        {
            get { return _cnpj; }
            set { _cnpj = value; }
        }
        public virtual string OrganizacaoMilitar
        {
            get { return _organizacaoMilitar; }
            set { _organizacaoMilitar = value; }
        }
        public virtual string Forca
        {
            get { return _forca; }
            set { _forca = value; }
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
        
        public virtual int MaximoHorasMeioExpedienteFAMOD
        {
            get { return _maximoHorasMeioExpedienteFAMOD; }
            set { _maximoHorasMeioExpedienteFAMOD = value; }
        }
        public virtual int MaximoHorasFAMOD
        {
            get { return _maximoHorasFAMOD; }
            set { _maximoHorasFAMOD = value; }
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
		public virtual int NumeroMinimoCotacoesCompra
		{
			get { return _numerominimocotacoescompra; }
			set { _numerominimocotacoescompra = value; }
		}
			
		/// <summary>
		/// Determina se na tela de compras a entrada é em forma de texto (Manual) ou se escolhe da lista de materiais
		/// </summary>		
		public virtual bool EntradaItemCompraManual
		{
			get { return _entradaitemcompramanual; }
			set { _entradaitemcompramanual = value; }
		}

	    public virtual bool FlagPermiteEntregaAbaixoEstoque
	    {
	        get { return _flagPermiteEntregaAbaixoEstoque; }
	        set { _flagPermiteEntregaAbaixoEstoque = value; }
	    }

	    public virtual decimal PercentualLimiteAcimaLicitacao
	    {
	        get { return _percentualLimiteAcimaLicitacao; }
	        set { _percentualLimiteAcimaLicitacao = value; }
	    }

	    public virtual decimal ValorDelineamento { get; set;} 

	    #endregion

	    private static Parametro _parametro;
		public static Parametro Get()
		{
		    if(_parametro == null)
		        _parametro = Parametro.Get(1);
		    return _parametro;
		}

        public override void Save()
        {
            base.Save();
            _parametro = Parametro.Get(1);
        }
	}
}
