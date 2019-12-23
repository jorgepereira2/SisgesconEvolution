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

public partial class frmPedidoAquisicaoCadastro : MarinhaPageBase
{
    #region Private Member
    [TransientPageState]
    protected PedidoAquisicao _pedido;

    [TransientPageState]
    protected PedidoAquisicaoItem _item;

    private Parametro _parametro = Parametro.Get();
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
        ddlMeta.SelectedIndexChanged += new EventHandler(ddlMeta_SelectedIndexChanged);
        btnEnviar.Click += BtnEnviar_OnClick;
    }

   

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            SetLayout();
            FillPage();
            if (Request["ID_Pedido"] != null)
            {
                _pedido = PedidoAquisicao.Get(Convert.ToInt32(Request["ID_Pedido"]));
                PopulateFields();
            }
            else
            {
                _pedido = new PedidoAquisicao();
            }
            Anthem.AnthemClientMethods.Redirect("frmPedidoAquisicaoPesquisa.aspx", btnVoltar);
            
        }
    }

    private void ShowSaldoMeta()
    {
        DataSet ds = Meta.SelectSaldo(Convert.ToInt32(ddlMeta.SelectedValue), DateTime.Today.Year);
        if(ds.Tables[0].Rows.Count > 0)
            lblSaldoMeta.Text = string.Format("Total Planejado: {0:C2}<br>Total em Execução: {1:C2}<br>Total Realizado: {2:C2}<br>Saldo: {3:C2}",
                ds.Tables[0].Rows[0]["ValorPlanejado"], ds.Tables[0].Rows[0]["ValorEmExecucao"], ds.Tables[0].Rows[0]["ValorRealizado"],
                Convert.ToDecimal(ds.Tables[0].Rows[0]["ValorPlanejado"]) - Convert.ToDecimal(ds.Tables[0].Rows[0]["ValorEmExecucao"]) - Convert.ToDecimal(ds.Tables[0].Rows[0]["ValorRealizado"]));
        lblSaldoMeta.UpdateAfterCallBack = true;
    }

    private void ShowSaldoConta()
    {
        DataSet ds = Conta.SelectSaldo(Convert.ToInt32(ddlConta.SelectedValue), DateTime.Today.Year);
        if (ds.Tables[0].Rows.Count > 0)
            lblSaldoConta.Text = string.Format("Saldo Geral: {0:C2}<br>Valor Comprometido: {1:C2}<br>Saldo Disponível: {2:C2}",
                ds.Tables[0].Rows[0]["SaldoGeral"], ds.Tables[0].Rows[0]["ValorComprometido"], ds.Tables[0].Rows[0]["SaldoDisponivel"]);
        lblSaldoConta.UpdateAfterCallBack = true;
    }

    private void FillPage()
    {
        Util.FillDropDownList(ddlTipoPedidoAquisicao, TipoPedidoAquisicao.List(), ESCOLHA_OPCAO);
        Util.FillDropDownList(ddlConta, Conta.List(), ESCOLHA_OPCAO);
        Util.InsertDefaultItem(ddlMeta, "Escolha a Conta");
    }

    #endregion
    
    #region Layout
    private  void SetLayout()
    {
        if(_parametro.EntradaItemCompraManual)
        {
            trDescricao.Visible = true;
            trTipoServicoMaterial.Visible = true;
            trMaterial.Visible = false;
        }
        else
        {
            trDescricao.Visible = false;
            trTipoServicoMaterial.Visible = false;
            trMaterial.Visible = true;
        }
    }
    #endregion

    #region Events
    void btnSalvar_Click(object sender, EventArgs e)
    {
        if(!_pedido.PodeSerAlterado)
        {
            ShowMessage("O PA não pode mais ser alterado.");
            return;
        }
        if (TabStrip1.SelectedTab.ID == tabDadosBasicos.ID)
            SalvarPedidoAquisicao();
        else if (TabStrip1.SelectedTab.ID == tabItem.ID)
            SalvarItemPedidoAquisicao();
    }

    void btnNovo_Click(object sender, EventArgs e)
    {
        if (TabStrip1.SelectedTab.ID == tabDadosBasicos.ID)
        {
            ClearFields();
            _pedido = new PedidoAquisicao();
        }
        else if (TabStrip1.SelectedTab.ID == tabItem.ID)
            ClearFieldsItemPedidoAquisicao();
    }

    protected void ddlConta_Changed(object sender, EventArgs e)
    {
        Util.FillDropDownList(ddlMeta, Meta.List(Convert.ToInt32(ddlConta.SelectedValue)), ESCOLHA_OPCAO);
        ddlMeta.UpdateAfterCallBack = true;
        ShowSaldoConta();
    }
    

    void ddlMeta_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowSaldoMeta();
    }

    void btnImprimir_Click(object sender, EventArgs e)
    {
        Anthem.AnthemClientMethods.Popup(this, "fchPedidoAquisicao.aspx?id_pedido=" + _pedido.ID, false, false, false, true, true, true,
        true, 40, 60, 700, 500);
    }
    #endregion

    #region PedidoAquisicao
    private void SalvarPedidoAquisicao()
    {
        FillObject();
        _pedido.Save();
        UpdateLabels();
        ShowSuccessMessage();
    }

    private void UpdateLabels()
    {
        lblData.Text = ObjectReader.ReadDate(_pedido.DataEmissao);
        lblNumero.Text = _pedido.Numero.ToString();
        lblStatus.Text = _pedido.Status.Descricao;
        lblDivisao.Text = _pedido.Celula.Descricao;
        
        lblData.UpdateAfterCallBack = true;
        lblNumero.UpdateAfterCallBack = true;
        lblStatus.UpdateAfterCallBack = true;
        lblDivisao.UpdateAfterCallBack = true;
    }

    private void PopulateFields()
    {
        UpdateLabels();
        
        txtAplicacao.Text = _pedido.Aplicacao;
        ddlTipoPedidoAquisicao.SelectedValue = _pedido.TipoPedidoAquisicao.ID.ToString();

        ddlConta.SelectedValue = ObjectReader.ReadID(_pedido.Conta);
        ddlConta_Changed(null, null);
        ddlMeta.SelectedValue = _pedido.Meta.ID.ToString();
        ShowSaldoMeta();

        ucFornecedor.SelectedValue = ObjectReader.ReadID(_pedido.Fornecedor);
        ucFornecedor.Text = _pedido.Fornecedor.RazaoSocial;
        ucFornecedor2.SelectedValue = ObjectReader.ReadID(_pedido.Fornecedor2);
        ucFornecedor2.Text = _pedido.Fornecedor2 == null ? "" : _pedido.Fornecedor2.RazaoSocial;
        ucFornecedor3.SelectedValue = ObjectReader.ReadID(_pedido.Fornecedor3);
        ucFornecedor3.Text = _pedido.Fornecedor3 == null ? "" : _pedido.Fornecedor3.RazaoSocial;
        ucFornecedor4.SelectedValue = ObjectReader.ReadID(_pedido.Fornecedor4);
        ucFornecedor4.Text = _pedido.Fornecedor4 == null ? "" : _pedido.Fornecedor4.RazaoSocial;
        txtObservacao.Text = _pedido.Observacao;
        
        BindItemPedidoAquisicao();
        BindHistorico();
    }

    private void BindHistorico()
    {
        ucHistorico.DataSource = _pedido.Historico;
        ucHistorico.DataBind();
        Anthem.AnthemClientMethods.ResizeIFrame();
    }

    private void ClearFields()
    {
        lblData.Text = "";
        lblNumero.Text = "";
        lblStatus.Text = "";
        txtAplicacao.Text = "";
        txtObservacao.Text = "";
        ddlTipoPedidoAquisicao.SelectedValue = "0";
        ddlMeta.SelectedValue = "0";
        ddlConta.SelectedValue = "0";
        ucFornecedor.Reset();
        ucFornecedor2.Reset();
        ucFornecedor3.Reset();
        ucFornecedor4.Reset();

        lblData.UpdateAfterCallBack = true;
        lblNumero.UpdateAfterCallBack = true;
        ddlMeta.UpdateAfterCallBack = true;
        ddlConta.UpdateAfterCallBack = true;
        txtAplicacao.UpdateAfterCallBack = true;
        txtObservacao.UpdateAfterCallBack = true;
        ddlTipoPedidoAquisicao.UpdateAfterCallBack = true;
    }

    private void FillObject()
    {
        _pedido.Aplicacao = txtAplicacao.Text;
        _pedido.TipoPedidoAquisicao = TipoPedidoAquisicao.Get(Convert.ToInt32(ddlTipoPedidoAquisicao.SelectedValue));
        _pedido.Meta = Meta.Get(Convert.ToInt32(ddlMeta.SelectedValue));
        _pedido.Conta = Conta.Get(Convert.ToInt32(ddlConta.SelectedValue));
        _pedido.Fornecedor = Fornecedor.Get(Convert.ToInt32(ucFornecedor.SelectedValue));
        _pedido.Fornecedor2 = Fornecedor.Get(Convert.ToInt32(ucFornecedor2.SelectedValue));
        _pedido.Fornecedor3 = Fornecedor.Get(Convert.ToInt32(ucFornecedor3.SelectedValue));
        _pedido.Fornecedor4 = Fornecedor.Get(Convert.ToInt32(ucFornecedor4.SelectedValue));
        _pedido.Observacao = PageReader.ReadString(txtObservacao);
        _pedido.ServidorCadastro = Servidor.Get(ID_Servidor);
    }
    #endregion

    #region ItemPedidoAquisicao
    private void BindItemPedidoAquisicao()
    {
        dgItem.DataSource = _pedido.Itens;
        dgItem.DataKeyField = "ID";
        dgItem.DataBind();
        dgItem.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();
    }

    void dgItem_EditCommand(object source, DataGridCommandEventArgs e)
    {
        int id = Convert.ToInt32(dgItem.DataKeys[e.Item.ItemIndex]);
        _item = _pedido.Itens.Find(id);
        PopulateFieldsItemPedidoAquisicao();
    }

    void dgItem_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        if(!_pedido.PodeSerAlterado)
        {
            ShowMessage("Este PA não pode ser alterado neste estágio.");
            return;
        }

        int id = Convert.ToInt32(dgItem.DataKeys[e.Item.ItemIndex]);
        PedidoAquisicaoItem item = _pedido.Itens.Find(id);
        _pedido.Itens.Remove(item);
        item.Delete();
        BindItemPedidoAquisicao();
    }

    private void PopulateFieldsItemPedidoAquisicao()
    {
        if(_parametro.EntradaItemCompraManual)
        {
            txtDescricaoItem.Text = _item.Descricao;
        }
        else
        {
            ucBuscaMaterial.SelectedValue = ObjectReader.ReadID(_item.ServicoMaterial);
            ucBuscaMaterial.Text = _item.ServicoMaterial.DescricaoCompleta;
        }
        txtQuantidade.Text = ObjectReader.ReadInt(_item.Quantidade);
        txtValor1.Text = ObjectReader.ReadDecimal(_item.Valor);
        txtValor2.Text = ObjectReader.ReadDecimal(_item.Valor2);
        txtValor3.Text = ObjectReader.ReadDecimal(_item.Valor3);
        txtValor4.Text = ObjectReader.ReadDecimal(_item.Valor4);
        
        RefreshFieldsItemPedidoAquisicao();
    }

    private void FillObjectItemPedidoAquisicao()
    {
        if (_item == null)
        {
            _item = new PedidoAquisicaoItem();
            _item.PedidoAquisicao = _pedido;
        }
        
        
        _item.Quantidade = PageReader.ReadInt(txtQuantidade);
        _item.Valor = PageReader.ReadDecimal(txtValor1);
        _item.Valor2 = PageReader.ReadNullableDecimal(txtValor2);
        _item.Valor3 = PageReader.ReadNullableDecimal(txtValor3);
        _item.Valor4 = PageReader.ReadNullableDecimal(txtValor4);
        _item.Descricao = PageReader.ReadString(txtDescricaoItem);
        if(!_parametro.EntradaItemCompraManual)
        {
            _item.ServicoMaterial = ServicoMaterial.Get(Convert.ToInt32(ucBuscaMaterial.SelectedValue));
        }
    }

    private void ClearFieldsItemPedidoAquisicao()
    {
        ucBuscaMaterial.Reset();
        txtQuantidade.Text = "";
        txtValor1.Text = "";
        txtValor2.Text = "";
        txtValor3.Text = "";
        txtValor4.Text = "";
        txtDescricaoItem.Text = "";
        
        RefreshFieldsItemPedidoAquisicao();
        _item = null;
    }

    private void RefreshFieldsItemPedidoAquisicao()
    {
        ucBuscaMaterial.UpdateAfterCallBack = true;
        txtQuantidade.UpdateAfterCallBack = true;
        txtValor1.UpdateAfterCallBack = true;
        txtValor2.UpdateAfterCallBack = true;
        txtValor3.UpdateAfterCallBack = true;
        txtValor4.UpdateAfterCallBack = true;
        txtDescricaoItem.UpdateAfterCallBack = true;
    }

    private void SalvarItemPedidoAquisicao()
    {
        FillObjectItemPedidoAquisicao();

        bool IsNew = !_item.IsPersisted;
        _item.Save();
        if(IsNew)
            _pedido.Itens.Add(_item);

        BindItemPedidoAquisicao();
        ClearFieldsItemPedidoAquisicao();
    }
    #endregion

    [Anthem.Method]
    public void AbaAlterada()
    {
        if (TabStrip1.SelectedTab.ID == tabDadosBasicos.ID)
            btnSalvar.ValidationGroup = "DadosBasicos";
        if (TabStrip1.SelectedTab.ID == tabItem.ID)
            btnSalvar.ValidationGroup = "Item";

        btnSalvar.UpdateAfterCallBack = true;
    }

    private void BtnEnviar_OnClick(object sender, EventArgs e)
    {
        _pedido.Manager.Enviar(ID_Servidor);
        ShowMessage("PA encaminhado com sucesso.");
        //if(_pedido.ValorTotal > _parametro.ValorMaximoSemOrcamentoPA)
        //    ShowMessage("Favor anexar os orçamentos em papeleta amarela e enviar para a Divisão de Finanças.");
        ClearFields();
        
    }

}
