using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Shared.NHibernateDAL;
using Marinha.Business;
using Shared.SessionState;
using Shared.Common;

public partial class frmParametroCadastro : MarinhaPageBase
{

    #region Private Member
    [TransientPageState]
    protected Parametro _parametro;

    #endregion 

    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.btnSalvar.Click += new EventHandler(btnSalvar_Click);
    }

	
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
        	FillPage();
           
            _parametro = Parametro.Get();
            PopulateFields();

            dvMergulhoTitulo.Visible = false;
            dvMergulhoConteudo.Visible = false;
        }
    }

	private void FillPage()
	{
		Util.FillDropDownList(ddlComandante, Servidor.List(null), ESCOLHA_OPCAO);
        Util.FillDropDownList(ddlOrdenadorDespesaAC, Servidor.List(null), ESCOLHA_OPCAO);
	}

	private void PopulateFields()
    {
        txtNumeroMinimoCotacoes.Text = ObjectReader.ReadInt(_parametro.NumeroMinimoCotacoesCompra);
        chkEntradaItemManual.Checked = _parametro.EntradaItemCompraManual;
        txtValorMaoObraHora.Text = ObjectReader.ReadDecimal(_parametro.ValorMaoObraHora);
        txtPercentualMaoObraIndireta.Text = ObjectReader.ReadDecimal(_parametro.PercentualMaoObraIndireta);
        txtTaxaOperacionalMaoObra.Text = ObjectReader.ReadDecimal(_parametro.TaxaOperacionalMaoObra);
        txtPercentualMaterialIndireto.Text = ObjectReader.ReadDecimal(_parametro.PercentualMaterialIndireto);
        txtPercentualServicoTerceiroIndireto.Text = ObjectReader.ReadDecimal(_parametro.PercentualServicoTerceiroIndireto);
        txtTaxaOperacionalMaterialServico.Text = ObjectReader.ReadDecimal(_parametro.TaxaOperacionalMaterialServico);
        txtTaxaContribuicaoOperacionalMaoObra.Text = ObjectReader.ReadDecimal(_parametro.TaxaContribuicaoOperacionalMaoObra);
        txtTaxaContribuicaoOperacionalMaterial.Text = ObjectReader.ReadDecimal(_parametro.TaxaContribuicaoOperacionalMaterial);
        txtMaximoHorasFAMOD.Text = ObjectReader.ReadInt(_parametro.MaximoHorasFAMOD);
        txtMaximoHorasMeioExpedienteFAMOD.Text = ObjectReader.ReadInt(_parametro.MaximoHorasMeioExpedienteFAMOD);
        txtPercentualDescontoSubTotalMaoObra.Text = ObjectReader.ReadDecimal(_parametro.PercentualDescontoSubTotalMaoObra);
        txtPercentualDescontoSubTotalMaterialServicoTerceiro.Text = ObjectReader.ReadDecimal(_parametro.PercentualDescontoSubTotalMaterialServicoTerceiro);



        txtValorHomemHoraMergulho.Text = ObjectReader.ReadDecimal(_parametro.ValorHomemHoraMergulho);
        txtValorDeslocamentoMergulho.Text = ObjectReader.ReadDecimal(_parametro.ValorDeslocamentoMergulho);
        txtValorTaxaBoteMergulho.Text = ObjectReader.ReadDecimal(_parametro.ValorTaxaBoteMergulho);
        txtValorTaxaCatamaraMergulho.Text = ObjectReader.ReadDecimal(_parametro.ValorTaxaCatamaraMergulho);
        txtTaxaMaoObraDiretaMergulho.Text = ObjectReader.ReadDecimal(_parametro.TaxaMaoObraDiretaMergulho);
        txtTaxaMaoObraIndiretaMergulho.Text = ObjectReader.ReadDecimal(_parametro.TaxaMaoObraIndiretaMergulho);
        txtTaxaOperacionalMaoObraMergulho.Text = ObjectReader.ReadDecimal(_parametro.TaxaOperacionalMaoObraMergulho);
        txtTaxaContribuicaoOperacionalMergulho.Text = ObjectReader.ReadDecimal(_parametro.TaxaContribuicaoOperacionalMergulho);
        txtDescontoFRE170Mergulho.Text = ObjectReader.ReadDecimal(_parametro.DescontoFRE170Mergulho);
        txtDescontoFRE171172Mergulho.Text = ObjectReader.ReadDecimal(_parametro.DescontoFRE171172Mergulho);
        txtValorTaxaMaterialMergulho.Text = ObjectReader.ReadDecimal(_parametro.ValorTaxaMaterialMergulho);

        txtValorHomemHoraAtividadeSecundaria.Text = ObjectReader.ReadDecimal(_parametro.ValorHomemHoraAtividadeSecundaria);
        txtDescontoFRE170AtividadeSecundaria.Text = ObjectReader.ReadDecimal(_parametro.DescontoFRE170AtividadeSecundaria);
        txtDescontoFRE171172AtividadeSecundaria.Text = ObjectReader.ReadDecimal(_parametro.DescontoFRE171172AtividadeSecundaria);
        txtTaxaOperacionalMaterialServicoAtividadeSecundaria.Text = ObjectReader.ReadDecimal(_parametro.TaxaOperacionalMaterialServicoAtividadeSecundaria);
        txtTaxaOperacionalMaoObraAtividadeSecundaria.Text = ObjectReader.ReadDecimal(_parametro.TaxaOperacionalMaoObraAtividadeSecundaria);
        txtTaxaContribuicaoOperacionalAtividadeSecundaria.Text = ObjectReader.ReadDecimal(_parametro.TaxaContribuicaoOperacionalAtividadeSecundaria);

        txtPercentualBloqueioLicitacao.Text = ObjectReader.ReadDecimal(_parametro.PercentualBloqueioLicitacao);
        txtPercentualLimiteAcimaLicitacao.Text = ObjectReader.ReadDecimal(_parametro.PercentualLimiteAcimaLicitacao);
        txtValorDelineamento.Text = ObjectReader.ReadDecimal(_parametro.ValorDelineamento);

	    txtTextoImpressaoMensagemOrcamento.Text = _parametro.TextoImpressaoMensagemOrcamento;

	    txtForca.Text = _parametro.Forca;
	    txtOrganizacaoMilitar.Text = _parametro.OrganizacaoMilitar;
	    txtCNPJ.Text = _parametro.CNPJ;
	    txtEndereco.Text = _parametro.Endereco;
	    txtTelefone.Text = _parametro.Telefone;
        txtTelefoneContabilidade.Text = _parametro.TelefoneContabilidade;
        txtEmail.Text = _parametro.Email;

	    chkFlagPermiteACComLimiteEstourado.Checked = _parametro.FlagPermiteACComLimiteEstourado;
        chkFlagPermiteEntregaAlemEstoque.Checked = _parametro.FlagPermiteEntregaAbaixoEstoque;
	    txtGarantiaFaturamento.Text = _parametro.GarantiaFaturamento;
	    txtValidadeFaturamento.Text = _parametro.ValidadeFaturamento;

	    txtTextoAC.Text = _parametro.TextoAC;
	    ddlComandante.SelectedValue = ObjectReader.ReadID(_parametro.Comandante);
        ddlOrdenadorDespesaAC.SelectedValue = ObjectReader.ReadID(_parametro.OrdenadorDespesaAC);

	    dgTipoCompra.DataSource = TipoCompra.Select();
	    dgTipoCompra.DataKeyField = "ID";
	    dgTipoCompra.DataBind();
    }

    private void FillObject()
    {
        _parametro.NumeroMinimoCotacoesCompra = PageReader.ReadInt(txtNumeroMinimoCotacoes);
        _parametro.EntradaItemCompraManual = chkEntradaItemManual.Checked;
        _parametro.ValorMaoObraHora = PageReader.ReadDecimal(txtValorMaoObraHora);
        _parametro.PercentualMaoObraIndireta = PageReader.ReadDecimal(txtPercentualMaoObraIndireta);
        _parametro.TaxaOperacionalMaoObra = PageReader.ReadDecimal(txtTaxaOperacionalMaoObra);
        _parametro.PercentualMaterialIndireto = PageReader.ReadDecimal(txtPercentualMaterialIndireto);
        _parametro.PercentualServicoTerceiroIndireto = PageReader.ReadDecimal(txtPercentualServicoTerceiroIndireto);
        _parametro.TaxaOperacionalMaterialServico = PageReader.ReadDecimal(txtTaxaOperacionalMaterialServico);
        _parametro.TaxaContribuicaoOperacionalMaoObra = PageReader.ReadDecimal(txtTaxaContribuicaoOperacionalMaoObra);
        _parametro.TaxaContribuicaoOperacionalMaterial = PageReader.ReadDecimal(txtTaxaContribuicaoOperacionalMaterial);
        _parametro.MaximoHorasFAMOD = PageReader.ReadInt(txtMaximoHorasFAMOD);
        _parametro.MaximoHorasMeioExpedienteFAMOD = PageReader.ReadInt(txtMaximoHorasMeioExpedienteFAMOD);
        _parametro.PercentualDescontoSubTotalMaoObra = PageReader.ReadDecimal(txtPercentualDescontoSubTotalMaoObra);
        _parametro.PercentualDescontoSubTotalMaterialServicoTerceiro = PageReader.ReadDecimal(txtPercentualDescontoSubTotalMaterialServicoTerceiro);
        _parametro.PercentualLimiteAcimaLicitacao = PageReader.ReadDecimal(txtPercentualLimiteAcimaLicitacao);
        _parametro.ValorDelineamento = PageReader.ReadDecimal(txtValorDelineamento);
        _parametro.PercentualBloqueioLicitacao = PageReader.ReadDecimal(txtPercentualBloqueioLicitacao);
        
        _parametro.ValorHomemHoraMergulho = PageReader.ReadDecimal(txtValorHomemHoraMergulho);
        _parametro.ValorDeslocamentoMergulho = PageReader.ReadDecimal(txtValorDeslocamentoMergulho);
        _parametro.ValorTaxaBoteMergulho = PageReader.ReadDecimal(txtValorTaxaBoteMergulho);
        _parametro.ValorTaxaCatamaraMergulho = PageReader.ReadDecimal(txtValorTaxaCatamaraMergulho);
        _parametro.TaxaMaoObraDiretaMergulho = PageReader.ReadDecimal(txtTaxaMaoObraDiretaMergulho);
        _parametro.TaxaMaoObraIndiretaMergulho = PageReader.ReadDecimal(txtTaxaMaoObraIndiretaMergulho);
        _parametro.TaxaOperacionalMaoObraMergulho = PageReader.ReadDecimal(txtTaxaOperacionalMaoObraMergulho);
        _parametro.TaxaContribuicaoOperacionalMergulho = PageReader.ReadDecimal(txtTaxaContribuicaoOperacionalMergulho);
        _parametro.DescontoFRE170Mergulho = PageReader.ReadDecimal(txtDescontoFRE170Mergulho);
        _parametro.DescontoFRE171172Mergulho = PageReader.ReadDecimal(txtDescontoFRE171172Mergulho);
        _parametro.ValorTaxaMaterialMergulho = PageReader.ReadDecimal(txtValorTaxaMaterialMergulho);

        _parametro.ValorHomemHoraAtividadeSecundaria = PageReader.ReadDecimal(txtValorHomemHoraAtividadeSecundaria);
        _parametro.DescontoFRE170AtividadeSecundaria = PageReader.ReadDecimal(txtDescontoFRE170AtividadeSecundaria);
        _parametro.DescontoFRE171172AtividadeSecundaria = PageReader.ReadDecimal(txtDescontoFRE171172AtividadeSecundaria);
        _parametro.TaxaOperacionalMaterialServicoAtividadeSecundaria = PageReader.ReadDecimal(txtTaxaOperacionalMaterialServicoAtividadeSecundaria);
        _parametro.TaxaOperacionalMaoObraAtividadeSecundaria = PageReader.ReadDecimal(txtTaxaOperacionalMaoObraAtividadeSecundaria);
        _parametro.TaxaContribuicaoOperacionalAtividadeSecundaria = PageReader.ReadDecimal(txtTaxaContribuicaoOperacionalAtividadeSecundaria);
        
        _parametro.Forca = PageReader.ReadString(txtForca);
        _parametro.OrganizacaoMilitar = PageReader.ReadString(txtOrganizacaoMilitar);
        _parametro.CNPJ = PageReader.ReadString(txtCNPJ);
        _parametro.Endereco = PageReader.ReadString(txtEndereco);
        _parametro.Telefone = PageReader.ReadString(txtTelefone);
        _parametro.TelefoneContabilidade = PageReader.ReadString(txtTelefoneContabilidade);
        _parametro.Email = PageReader.ReadString(txtEmail);

        _parametro.FlagPermiteACComLimiteEstourado = chkFlagPermiteACComLimiteEstourado.Checked;
        _parametro.FlagPermiteEntregaAbaixoEstoque = chkFlagPermiteEntregaAlemEstoque.Checked;

        _parametro.GarantiaFaturamento = PageReader.ReadString(txtGarantiaFaturamento);
        _parametro.ValidadeFaturamento = PageReader.ReadString(txtValidadeFaturamento);

        _parametro.TextoImpressaoMensagemOrcamento = PageReader.ReadString(txtTextoImpressaoMensagemOrcamento);
        _parametro.TextoAC = PageReader.ReadString(txtTextoAC);
        _parametro.Comandante = Servidor.Get(PageReader.ReadInt(ddlComandante));
        _parametro.OrdenadorDespesaAC = Servidor.Get(PageReader.ReadInt(ddlOrdenadorDespesaAC));
    }
    #endregion

    #region Events 
    void btnSalvar_Click(object sender, EventArgs e)
    {
        FillObject();
        _parametro.Save();

        SalvarTipoCompra();

        if (txtImagemAC.HasFile)
        {
            try
            {
                txtImagemAC.SaveAs(Server.MapPath("../images/imagem_ac.gif"));
            }
            catch(Exception ex)
            {
                ShowMessage("O seguinte erro ocorreu ao enviar a imagem: " + ex.Message);
                return;
            }
        }

		ShowSuccessMessage();
    }

    private void SalvarTipoCompra()
    {
        foreach (DataGridItem item in dgTipoCompra.Items)
        {
            if(item.ItemType == ListItemType.AlternatingItem || item.ItemType == ListItemType.Item)
            {
                TipoCompra tipoCompra = TipoCompra.Get(Convert.ToInt32(dgTipoCompra.DataKeys[item.ItemIndex]));
                TextBox txtLimiteAnual = (TextBox) item.FindControl("txtLimiteAnual");
                tipoCompra.LimiteAnual = PageReader.ReadDecimal(txtLimiteAnual);
                tipoCompra.Save();
            }
        }
    }

    #endregion
   
}
