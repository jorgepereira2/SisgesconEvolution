using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
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

public partial class frmLicitacaoCadastro : MarinhaPageBase
{
    #region Private Member
    
    protected Licitacao _licitacao
    {
        get { return (Licitacao) Session["frmLicitacaoCadastro._licitacao"]; }
        set { Session["frmLicitacaoCadastro._licitacao"] = value; }
    }

    protected bool IsPedido
    {
        get { return Request["pedido"] != null; }
    }

    [TransientPageState]
    protected LicitacaoItem _item;

    [TransientPageState]
    protected LicitacaoContrato _contrato;

    #endregion 

    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.btnSalvar.Click += new EventHandler(btnSalvar_Click);
        this.btnNovo.Click += new EventHandler(btnNovo_Click);
        this.dgItem.DeleteCommand += new DataGridCommandEventHandler(dgItem_DeleteCommand);
        this.dgItem.EditCommand += new DataGridCommandEventHandler(dgItem_EditCommand);
        this.btnImprimir.Click += new EventHandler(btnImprimir_Click);
        ucBuscaMaterial.SelectedValueChanged += new BuscaServicoMaterial.SelectedValueChangedHandler(ucBuscaMaterial_SelectedValueChanged);
        btnenviar.Click += new EventHandler(btnenviar_Click);
        btnAdicionarPS.Click += new EventHandler(btnAdicionarPS_Click);
        btnRemoverPS.Click += new EventHandler(btnRemoverPS_Click);
        this.dgContrato.DeleteCommand += new DataGridCommandEventHandler(dgContrato_DeleteCommand);
        this.dgContrato.EditCommand += new DataGridCommandEventHandler(dgContrato_EditCommand);
    }

    void btnRemoverPS_Click(object sender, EventArgs e)
    {
        if(lstPS.SelectedIndex > -1)
        {
            lstPS.Items.RemoveAt(lstPS.SelectedIndex);
            lstPS.UpdateAfterCallBack = true;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            FillPage();
            if (Request["ID_Licitacao"] != null)
            {
                _licitacao = Licitacao.Get(Convert.ToInt32(Request["ID_Licitacao"]));
                PopulateFields();
            }
            else
            {
                _licitacao = new Licitacao();
                if(IsPedido)
                    _licitacao.Status = StatusLicitacaoEnum.PedidoLicitacao;
                else
                    _licitacao.Status = StatusLicitacaoEnum.CadastroInicial;
            }

            if(IsPedido)
                Anthem.AnthemClientMethods.Redirect("frmLicitacaoPesquisa.aspx?pedido=true", btnVoltar);
            else
                Anthem.AnthemClientMethods.Redirect("frmLicitacaoPesquisa.aspx", btnVoltar);
            
            Anthem.AnthemClientMethods.Popup(btnCopiar, "../busca/frmLicitacaoBusca.aspx", false, false, false, true, true, true,
                true, 40, 60, 650, 500, false);

            if(IsPedido)
            {
                txtCodigoSiasg.Enabled = false;
                //ddlTipoLicitacao.SelectedIndex = 1;
                //ddlTipoLicitacao.Enabled = false;
                //ddlSistemaLicitatorio.SelectedIndex = 1;
                //ddlSistemaLicitatorio.Enabled = false;
                //ddlModalidadePregao.SelectedIndex = 1;
                //ddlModalidadePregao.Enabled = false;
                //ddlProcessoLicitatorio.SelectedIndex = 1;
                //ddlProcessoLicitatorio.Enabled = false;
                txtNumeroPregao.Enabled = false;
                txtDataPregao.Enabled = false;
                ddlServidorFiscalContrato.Enabled = false;
                //txtNumeroContratoAta.Enabled = false;
                txtObservacao.Enabled = false;
                txtNUP.Enabled = false;
                btnCopiar.Visible = false;
                btnImprimir.Visible = false;
                
            }
        }
    }

    private void FillPage()
    {
        Util.FillDropDownList(ddlTipoCalculo, typeof(TipoCalculoLicitacaoItem), ESCOLHA_OPCAO);
        Util.FillDropDownList(ddlSistemaLicitatorio, typeof(SistemaLicitatorio), ESCOLHA_OPCAO);
        Util.FillDropDownList(ddlProcessoLicitatorio, typeof(ProcessoLicitatorio), ESCOLHA_OPCAO);
        Util.FillDropDownList(ddlTipoLicitacao, TipoLicitacao.List(), ESCOLHA_OPCAO);
        Util.FillDropDownList(ddlModalidadePregao, ModalidadePregao.List(), ESCOLHA_OPCAO);
        Util.FillDropDownList(ddlServidorFiscalContrato, Servidor.List(null), ESCOLHA_OPCAO);
    }

    #endregion

    #region Events
    void btnSalvar_Click(object sender, EventArgs e)
    {
        if(!_licitacao.PodeSerAlterada && TabStrip1.SelectedTab.ID != tabContratos.ID)
        {
            ShowMessage("A licitação não pode mais ser alterada.");
            return;
        }
        if (TabStrip1.SelectedTab.ID == tabDadosBasicos.ID)
            SalvarLicitacao();
        else if (TabStrip1.SelectedTab.ID == tabItem.ID)
            SalvarItemLicitacao();
        else if (TabStrip1.SelectedTab.ID == tabContratos.ID)
            SalvarContratoLicitacao();
    }

    void btnNovo_Click(object sender, EventArgs e)
    {
        if (TabStrip1.SelectedTab.ID == tabDadosBasicos.ID)
        {
            ClearFields();
            _licitacao = new Licitacao();
        }
        else if (TabStrip1.SelectedTab.ID == tabItem.ID)
            ClearFieldsItemLicitacao();
        else if (TabStrip1.SelectedTab.ID == tabContratos.ID)
            ClearFieldsContratoLicitacao();
    }

    void ucBuscaMaterial_SelectedValueChanged(object source, BuscaServicoMaterialEventArgs e)
    {
        txtCodigoSiasg.Text = e.ServicoMaterial.CodigoSiasg;
        txtCodigoSiasg.UpdateAfterCallBack = true;
    }
    #endregion

    #region Licitacao
    private void SalvarLicitacao()
    {
        FillObject();
        _licitacao.Save();

        PedidoServico.RemoveLicitacao(_licitacao.ID);
        foreach (ListItem listItem in lstPS.Items)
        {
            PedidoServico ps = PedidoServico.Get(Convert.ToInt32(listItem.Value));
            ps.Licitacao = _licitacao;
            ps.Save();
        }

        ShowSuccessMessage();
    }

    private void PopulateFields()
    {
        txtDataEmissao.Text = ObjectReader.ReadDate(_licitacao.DataEmissao);
        txtObjetivo.Text = _licitacao.Objetivo;
        txtObservacao.Text = _licitacao.Observacao;
        txtNumeroPregao.Text = _licitacao.NumeroPregao;
        txtDataPregao.Text = ObjectReader.ReadDate(_licitacao.DataPregao);
        lblValorTotalEstimado.Text = ObjectReader.ReadDecimal(_licitacao.ValorTotalEstimado);
        //txtNumeroContratoAta.Text = _licitacao.NumeroContratoAta;
        ddlTipoLicitacao.SelectedValue = ObjectReader.ReadID(_licitacao.TipoLicitacao);
        ddlModalidadePregao.SelectedValue = ObjectReader.ReadID(_licitacao.ModalidadePregao);
        ddlServidorFiscalContrato.SelectedValue = ObjectReader.ReadID(_licitacao.ServidorFiscalContrato);
        ddlSistemaLicitatorio.SelectedValue = Convert.ToInt32(_licitacao.SistemaLicitatorio).ToString();
        ddlProcessoLicitatorio.SelectedValue = Convert.ToInt32(_licitacao.ProcessoLicitatorio).ToString();
        txtNumeroCI.Text = _licitacao.NumeroCI;
        txtNUP.Text = _licitacao.NUP;


        lstPS.Items.Clear();
        foreach (PedidoServico ps in PedidoServico.SelectByLicitacao(_licitacao.ID))
        {
            lstPS.Items.Add(new ListItem(ps.CodigoComAno, ps.ID.ToString()));
        }

        BindItemLicitacao();
        BindContratoLicitacao();
    }

    private void ClearFields()
    {
        txtDataEmissao.Text = "";
        txtObjetivo.Text = "";
        txtObservacao.Text = "";
        txtNumeroPregao.Text = "";
        txtDataPregao.Text = "";
        lblValorTotalEstimado.Text = "";
        //txtNumeroContratoAta.Text = "";
        txtNumeroCI.Text = "";
        txtNUP.Text = "";
        //ddlTipoLicitacao.SelectedValue = "0";
        //ddlModalidadePregao.SelectedValue = "0";
        //ddlServidorFiscalContrato.SelectedValue = "0";

        RefreshFields();
    }

    private void RefreshFields()
    {
        txtDataEmissao.UpdateAfterCallBack = true;
        txtObjetivo.UpdateAfterCallBack = true;
        txtObservacao.UpdateAfterCallBack = true;
        txtNumeroPregao.UpdateAfterCallBack = true;
        txtDataPregao.UpdateAfterCallBack = true;
        txtNumeroCI.UpdateAfterCallBack = true;
        txtNUP.UpdateAfterCallBack = true;
    }

    private void FillObject()
    {
        _licitacao.DataEmissao = PageReader.ReadDate(txtDataEmissao);
        _licitacao.Objetivo = PageReader.ReadString(txtObjetivo);
        _licitacao.Observacao = PageReader.ReadString(txtObservacao);
        _licitacao.NumeroPregao = PageReader.ReadString(txtNumeroPregao);
        _licitacao.DataPregao = PageReader.ReadNullableDate(txtDataPregao);
        _licitacao.ServidorFiscalContrato = Servidor.Get(Convert.ToInt32(ddlServidorFiscalContrato.SelectedValue));
        _licitacao.TipoLicitacao = TipoLicitacao.Get(Convert.ToInt32(ddlTipoLicitacao.SelectedValue));
        _licitacao.ModalidadePregao = ModalidadePregao.Get(Convert.ToInt32(ddlModalidadePregao.SelectedValue));
      //  _licitacao.NumeroContratoAta = PageReader.ReadString(txtNumeroContratoAta);
        _licitacao.SistemaLicitatorio = (SistemaLicitatorio)Convert.ToInt32(ddlSistemaLicitatorio.SelectedValue);
        _licitacao.ProcessoLicitatorio = (ProcessoLicitatorio)Convert.ToInt32(ddlProcessoLicitatorio.SelectedValue);
        _licitacao.NumeroCI = PageReader.ReadString(txtNumeroCI);
        _licitacao.NUP = PageReader.ReadString(txtNUP);

    }
    #endregion

    #region ItemLicitacao
    private void BindItemLicitacao()
    {
        dgItem.DataSource = _licitacao.Itens;
        dgItem.DataKeyField = "ID";
        dgItem.DataBind();
        dgItem.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();
    }

    void dgItem_EditCommand(object source, DataGridCommandEventArgs e)
    {
        int id = Convert.ToInt32(dgItem.DataKeys[e.Item.ItemIndex]);
        _item = _licitacao.Itens.Find(id);
        PopulateFieldsItemLicitacao();
    }

    void dgItem_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        int id = Convert.ToInt32(dgItem.DataKeys[e.Item.ItemIndex]);
        LicitacaoItem item = _licitacao.Itens.Find(id);
        _licitacao.Itens.Remove(item);
        item.Delete();
        BindItemLicitacao();
    }

    private void PopulateFieldsItemLicitacao()
    {
        ucBuscaMaterial.SelectedValue = ObjectReader.ReadID(_item.Material);
        ucBuscaMaterial.Text = _item.Material.DescricaoCompleta;
        txtQuantidade.Text = ObjectReader.ReadDecimal(_item.Quantidade);
        txtValor1.Text = ObjectReader.ReadDecimal(_item.Valor1);
        txtValor2.Text = ObjectReader.ReadDecimal(_item.Valor2);
        txtValor3.Text = ObjectReader.ReadDecimal(_item.Valor3);
        txtValor4.Text = ObjectReader.ReadDecimal(_item.Valor4);
        txtValor5.Text = ObjectReader.ReadDecimal(_item.Valor5);
        ddlTipoCalculo.SelectedValue = Convert.ToInt32(_item.TipoCalculo).ToString();
        txtCodigoSiasg.Text = _item.Material.CodigoSiasg;
        txtObservacaoItem.Text = _item.Observacao;
        
        RefreshFieldsItemLicitacao();
    }

    private void FillObjectItemLicitacao()
    {
        if (_item == null)
        {
            _item = new LicitacaoItem(this._licitacao);
        }
        _item.Material = ServicoMaterial.Get(Convert.ToInt32(ucBuscaMaterial.SelectedValue));
        _item.Quantidade = PageReader.ReadDecimal(txtQuantidade);
        _item.Valor1 = txtValor1.RawValue;
        _item.Valor2 = txtValor2.RawValue;
        _item.Valor3 = txtValor3.RawValue;
        _item.Valor4 = txtValor4.RawValue;
        _item.Valor5 = txtValor5.RawValue;
        _item.TipoCalculo = (TipoCalculoLicitacaoItem)Convert.ToInt32(ddlTipoCalculo.SelectedValue);
        _item.Observacao = PageReader.ReadString(txtObservacaoItem);

        _item.Material.CodigoSiasg = txtCodigoSiasg.Text;
        _item.Material.Save();
    }

    private void ClearFieldsItemLicitacao()
    {
        ucBuscaMaterial.Reset();
        txtQuantidade.Text = "";
        txtValor1.Text = "";
        txtValor2.Text = "";
        txtValor3.Text = "";
        txtValor4.Text = "";
        txtValor5.Text = "";
        txtCodigoSiasg.Text = "";
        txtObservacaoItem.Text = "";
        ddlTipoCalculo.SelectedIndex = -1;
        
        RefreshFieldsItemLicitacao();
        _item = null;
    }

    private void RefreshFieldsItemLicitacao()
    {
        ucBuscaMaterial.UpdateAfterCallBack = true;
        txtQuantidade.UpdateAfterCallBack = true;
        txtValor1.UpdateAfterCallBack = true;
        txtValor2.UpdateAfterCallBack = true;
        txtValor3.UpdateAfterCallBack = true;
        txtValor4.UpdateAfterCallBack = true;
        txtValor5.UpdateAfterCallBack = true;
        ddlTipoCalculo.UpdateAfterCallBack = true;
        txtCodigoSiasg.UpdateAfterCallBack = true;
        txtObservacaoItem.UpdateAfterCallBack = true;
    }

    private void SalvarItemLicitacao()
    {
        FillObjectItemLicitacao();

        bool IsNew = !_item.IsPersisted;
        _item.Save();
        if(IsNew)
            _licitacao.Itens.Add(_item);

        BindItemLicitacao();
        ClearFieldsItemLicitacao();
    }
    #endregion


    #region Contrato
    private void BindContratoLicitacao()
    {
        dgContrato.DataSource = _licitacao.Contratos;
        dgContrato.DataKeyField = "ID";
        dgContrato.DataBind();
        dgContrato.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();
    }

    void dgContrato_EditCommand(object source, DataGridCommandEventArgs e)
    {
        int id = Convert.ToInt32(dgContrato.DataKeys[e.Item.ItemIndex]);
        _contrato = _licitacao.Contratos.Find(id);
        PopulateFieldsContratoLicitacao();
    }

    void dgContrato_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        int id = Convert.ToInt32(dgContrato.DataKeys[e.Item.ItemIndex]);
        LicitacaoContrato contrato = _licitacao.Contratos.Find(id);
        _licitacao.Contratos.Remove(contrato);
        contrato.Delete();
        BindContratoLicitacao();
    }

    private void PopulateFieldsContratoLicitacao()
    {
        ucFornecedorContrato.SelectedValue = ObjectReader.ReadID(_contrato.Fornecedor);
        ucFornecedorContrato.Text = _contrato.Fornecedor.RazaoSocial;
        txtNumeroContrato.Text = _contrato.NumeroContrato;
        txtDataVigencia.Text = ObjectReader.ReadDate(_contrato.DataVigencia);
        txtDataAssinatura.Text = ObjectReader.ReadDate(_contrato.DataAssinatura);

        RefreshFieldsContratoLicitacao();
    }

    private void FillObjectContratoLicitacao()
    {
        if (_contrato == null)
        {
            _contrato = new LicitacaoContrato(this._licitacao);
        }
        _contrato.Fornecedor = Fornecedor.Get(Convert.ToInt32(ucFornecedorContrato.SelectedValue));
        _contrato.DataVigencia = PageReader.ReadDate(txtDataVigencia);
        _contrato.DataAssinatura = PageReader.ReadDate(txtDataAssinatura);
        _contrato.NumeroContrato = txtNumeroContrato.Text;
    }

    private void ClearFieldsContratoLicitacao()
    {
        ucFornecedorContrato.Reset();
        txtDataAssinatura.Text = "";
        txtDataVigencia.Text = "";
        txtNumeroContrato.Text = "";
        RefreshFieldsContratoLicitacao();
        _contrato = null;
    }

    private void RefreshFieldsContratoLicitacao()
    {
        ucFornecedorContrato.UpdateAfterCallBack = true;
        txtDataAssinatura.UpdateAfterCallBack = true;
        txtDataVigencia.UpdateAfterCallBack = true;
        txtNumeroContrato.UpdateAfterCallBack = true;
    }

    private void SalvarContratoLicitacao()
    {
        FillObjectContratoLicitacao();

        bool IsNew = !_contrato.IsPersisted;
        _contrato.Save();
        if (IsNew)
            _licitacao.Contratos.Add(_contrato);

        BindContratoLicitacao();
        ClearFieldsContratoLicitacao();
    }
    #endregion

    #region Copia Licitacao

    [Anthem.Method]
    public void LicitacaoSelecionada(int id_licitacao)
    {
        _licitacao = new Licitacao();
        Licitacao licitacao = Licitacao.Get(id_licitacao);
        _licitacao.DataEmissao = DateTime.Today;
        _licitacao.DataPregao = licitacao.DataPregao;
        _licitacao.NumeroPregao = licitacao.NumeroPregao;
        _licitacao.Objetivo = licitacao.Objetivo;
        _licitacao.Observacao = licitacao.Observacao;
        _licitacao.TipoLicitacao = licitacao.TipoLicitacao;
        _licitacao.ModalidadePregao = licitacao.ModalidadePregao;
        _licitacao.Status = StatusLicitacaoEnum.CadastroInicial;
        _licitacao.SistemaLicitatorio = licitacao.SistemaLicitatorio;
        

        _licitacao.Save();
        foreach (LicitacaoItem item in licitacao.Itens)
        {
            LicitacaoItem newItem = new LicitacaoItem();
            newItem.Licitacao = _licitacao;
            newItem.Material = item.Material;
            newItem.Quantidade = item.Quantidade;
            newItem.TipoCalculo = item.TipoCalculo;
            newItem.Valor1 = item.Valor1;
            newItem.Valor2 = item.Valor2;
            newItem.Valor3 = item.Valor3;
            newItem.Valor4 = item.Valor4;
            newItem.Valor5 = item.Valor5;
            newItem.Save();
            _licitacao.Itens.Add(newItem);
        }

        PopulateFields();
        RefreshFields();
        ShowSuccessMessage();
        
    }
    #endregion

    void btnAdicionarPS_Click(object sender, EventArgs e)
    {
        if(ucPedidoServico.SelectedValue != "0")
        {
            if (lstPS.Items.FindByValue(ucPedidoServico.SelectedValue) == null)
            {
                PedidoServico ps = PedidoServico.Get(Convert.ToInt32(ucPedidoServico.SelectedValue));
                if (ps != null)
                {
                    lstPS.Items.Add(new ListItem(ps.CodigoComAno, ps.ID.ToString()));
                    lstPS.UpdateAfterCallBack = true;
                }
            }
        }
    }

    void btnenviar_Click(object sender, EventArgs e)
    {
        if(IsPedido)
        {
            if (_licitacao.Status == StatusLicitacaoEnum.PedidoLicitacao)
            {
                _licitacao.ProximoStatus(this.ID_Servidor, null);
                ShowMessage("Licitação enviada com sucesso.");
            }
            else
            {
                ShowMessage("Esta Licitação ja foi enviada.");
            }    
        }
        else
        {
            if (_licitacao.Status == StatusLicitacaoEnum.CadastroInicial)
            {
                _licitacao.ProximoStatus(this.ID_Servidor, null);
                ShowMessage("Licitação enviada com sucesso.");
            }
            else
            {
                ShowMessage("Esta Licitação ja foi enviada.");
            }    
        }
        
    }

    void btnImprimir_Click(object sender, EventArgs e)
    {
        Anthem.AnthemClientMethods.Popup(this, "fchLicitacao.aspx?id_licitacao=" + _licitacao.ID.ToString(), false, false, false, true, true, true,
        true, 40, 60, 700, 500);
    }

    protected decimal GetTotal()
    {
        return _licitacao.Itens.Sum(i => i.ValorMedioTotal);
    }
}
