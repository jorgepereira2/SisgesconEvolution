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

public partial class frmPedidoObtencaoCadastro : MarinhaPageBase
{
    #region Private Member

    [TransientPageState]
    protected PedidoObtencao _pedido;

    [TransientPageState]
    protected PedidoObtencaoItem _item;

    private Parametro _parametro = Parametro.Get();

    private OrigemPO _origemPO
    {
        get { return (OrigemPO) Convert.ToInt32(Request["OrigemPO"]); }
    }

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
        btnImprimirItens.Click += new EventHandler(btnImprimirItens_Click);
        btnDocumentos.Click += new EventHandler(btnDocumentos_Click);
        btnEnviar.Click += BtnEnviar_OnClick;
        ucBuscaMaterial.SelectedValueChanged += new BuscaServicoMaterial.SelectedValueChangedHandler(ucBuscaMaterial_SelectedValueChanged);
        ddlOrigemMaterial.SelectedIndexChanged += new EventHandler(ddlOrigemMaterial_SelectedIndexChanged);
        ucOrcamento.SelectedValueChanged += new BuscaOrcamento.SelectedValueChangedHandler(ucOrcamento_SelectedValueChanged);
        btnAtualizarCotacao.Click += new EventHandler(btnAtualizarCotacao_Click);
        dgItem.ItemDataBound += new DataGridItemEventHandler(dgItem_ItemDataBound);
        btnAtualizarValoresLicitacao.Click += new EventHandler(btnAtualizarValoresLicitacao_Click);
        
        ddlTipoCompra.SelectedIndexChanged += new EventHandler(ddlTipoCompra_SelectedIndexChanged);
        //ddlTipoCompra.SelectedIndexChanged += new EventHandler(ddlTipoCompra_SelectedIndexChanged);
    }

    void dgItem_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if(e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            pedidoObtencao_SaldoServicoMaterial uc = (pedidoObtencao_SaldoServicoMaterial) e.Item.FindControl("ucSaldo");
            PedidoObtencaoItem item = (PedidoObtencaoItem) e.Item.DataItem;
            uc.AtualizarSaldo(item.ServicoMaterial.ID, _pedido.DataEmissao.Year);

            LicitacaoItem itemLicitacao = LicitacaoItem.GetItemAberto(item.ServicoMaterial.ID, Convert.ToInt32(ddlLicitacao.SelectedValue));
            if(itemLicitacao != null)
            {
                Label lblSaldoLicitacao = (Label)e.Item.FindControl("lblSaldoLicitacao");
                lblSaldoLicitacao.Text = itemLicitacao.QuantidadeDisponivel.ToString("N2");
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!this.IsPostBack)
        {
            tr1.Visible = false;

            lblSubNaturezaDespesa.Visible = false;
            ddlSubNaturezaDespesa.Visible = false;

            lblSubNaturezaDespesa.UpdateAfterCallBack = false;
            ddlSubNaturezaDespesa.UpdateAfterCallBack = false;

            FillPage();

            if (Request["ID_Pedido"] != null)
            {
                _pedido = PedidoObtencao.Get(Convert.ToInt32(Request["ID_Pedido"]));
                PopulateFields();
            }
            else
            {
                _pedido = new PedidoObtencao();
            }
            
            //se for pedido de material
            if (Request["pm"] != null)
            {
                ddlTipoPedido.SelectedValue = Convert.ToInt32(TipoPedido.PedidoMaterial).ToString();
                txtValor1.Text = "0,00";
                txtValor1.Enabled = false;
                trTipoPedidoObtencao.Visible = false;
            }
            else
            {
                ddlTipoPedido.SelectedValue = Convert.ToInt32(TipoPedido.PedidoObtencao).ToString();
                //if(_origemPO != OrigemPO.Direto)
                //{
                //    trTipoPedidoObtencao.Visible = false;
                //}
            }

            //btnAtualizarCotacao.Visible = _pedido.OrigemPO == OrigemPO.PS;

            if (Request["origemPO"] == OrigemPO.GastoExtraPS.GetHashCode().ToString())
                ddlOrigemPO.SelectedValue = OrigemPO.GastoExtraPS.GetHashCode().ToString();
            else if(_pedido.DelineamentoOrcamento == null)
                ddlOrigemPO.SelectedValue = Convert.ToInt32(OrigemPO.Direto).ToString();
            
            OrigemPOChanged();

           // ddlOrigemPO.SelectedValue = Convert.ToInt32(_origemPO).ToString();
          //  OrigemPOChanged();

            string address = "frmPedidoObtencaoPesquisa.aspx?origemPO=" + Convert.ToInt32(_origemPO).ToString();
            if (Request["pm"] != null)
                address += "&pm=true";
            Anthem.AnthemClientMethods.Redirect(address, btnVoltar);

            string addressPopup = "../busca/frmPedidoObtencaoBusca.aspx?origemPO=" + Convert.ToInt32(_origemPO).ToString();
            if (Request["pm"] != null)
                addressPopup += "&pm=true";
            Anthem.AnthemClientMethods.Popup(btnCopiar, addressPopup, false, false, false, true, true, true, true, 20, 20, 700, 600, false);

            SetTitulo();

            // TIPO PO PM
            if (Request["pm"] != null)
            {
                tr_forn_001.Visible = false;
                tr_forn_002.Visible = false;
                tr_forn_003.Visible = false;
                tr_forn_004.Visible = false;

                tr_item_001.Visible = false;
                tr_item_002.Visible = false;
                tr_item_003.Visible = false;
            }
        }
    }

    private void SetTitulo()
    {
        if (Request["pm"] == null && _origemPO == OrigemPO.Direto)
            lblTitulo.Text = "Cadastro de Autorização de Compra Direto";

        else if (Request["pm"] == null && _origemPO == OrigemPO.GastoExtraPS)
            lblTitulo.Text = "Cadastro de Autorização de Compra de Gasto Extra PS";

        else if (Request["pm"] != null && _origemPO == OrigemPO.Direto)
            lblTitulo.Text = "Cadastro de Pedido de Material Direto";

        else if (Request["pm"] != null && _origemPO == OrigemPO.GastoExtraPS)
            lblTitulo.Text = "Cadastro de Pedido de Material de Gasto Extra PS";
    }

    private void FillPage()
    {
        Servidor servidor = Servidor.Get(this.ID_Servidor);

        if(servidor.GetFlagPodeFazerPOOutraCelula())
            Util.FillDropDownList(ddlCelula, Celula.List(), ESCOLHA_OPCAO);
        else
            Util.FillDropDownList(ddlCelula, Celula.ListCelulasSubordinadas(this.ID_Servidor, null));
                
        Util.FillDropDownList(ddlTipoPedido, typeof(TipoPedido), ESCOLHA_OPCAO);
        Util.FillDropDownList(ddlOrigemPO, typeof(OrigemPO), ESCOLHA_OPCAO);
       // ddlOrigemPO.Items.Remove(ddlOrigemPO.Items.FindByValue(Convert.ToInt32(OrigemPO.PS).ToString()));

        Util.FillDropDownList(ddlOrigemMaterial, typeof(OrigemMaterial), ESCOLHA_OPCAO);
        ddlOrigemMaterial.Items.Remove(ddlOrigemMaterial.Items.FindByValue(Convert.ToInt32(OrigemMaterial.Obtencao).ToString()));
        ddlOrigemMaterial.SelectedValue = Convert.ToInt32(OrigemMaterial.PEP).ToString();

        Util.FillDropDownList(ddlTipoPedidoObtencao, typeof(TipoPedidoObtencao));

        Util.FillDropDownList(ddlTipoCompra, TipoCompra.List(), ESCOLHA_OPCAO);
        Util.FillDropDownList(ddlModalidade, Modalidade.List(), ESCOLHA_OPCAO);

        //Util.FillDropDownList(ddlLicitacao, Licitacao.ListParaPO(), ESCOLHA_OPCAO);
        FillLicitacaoDropDownList();
    }

    private void FillLicitacaoDropDownList()
    {
        ddlLicitacao.Items.Add(new ListItem(ESCOLHA_OPCAO, "0"));
        List<Licitacao> licitacoes = Licitacao.Select(DateTime.MinValue, DateTime.MinValue, StatusLicitacaoEnum.Finalizado);

        foreach (Licitacao licitacao in licitacoes)
        {
            if(licitacao.Contratos.Where(c => c.DataVigencia > DateTime.Today).Count() > 0)
            {
                foreach (LicitacaoContrato contrato in licitacao.Contratos)
                {
                    ddlLicitacao.Items.Add(new ListItem(string.Format("{0} - {1} - {2}", licitacao.NumeroCI, contrato.NumeroContrato, contrato.Fornecedor.RazaoSocial), licitacao.ID.ToString()));    
                }
            }
        }
    }

    #endregion

    #region Events

    void btnSalvar_Click(object sender, EventArgs e)
    {
        if(!_pedido.PodeSerAlterado)
        {
            ShowMessage("O pedido não pode mais ser alterado.");
            return;
        }

        if (TabStrip1.SelectedTab.ID == tabDadosBasicos.ID)
        {
            if (Request["pm"] == null && ucFornecedor1.Text == "")
            {
                ShowMessage("Obrigatório pelo menos 1 fornecedor.");
                return;
            }

            SalvarPedidoObtencao();
        }
        else if (TabStrip1.SelectedTab.ID == tabItem.ID)
            SalvarItemPedidoObtencao();
    }

    void btnNovo_Click(object sender, EventArgs e)
    {
        if (TabStrip1.SelectedTab.ID == tabDadosBasicos.ID)
        {
            ClearFields();
            _pedido = new PedidoObtencao();
        }
        else if (TabStrip1.SelectedTab.ID == tabItem.ID)
            ClearFieldsItemPedidoObtencao();
    }
       
    private void OrigemPOChanged()
    {
        ddlCelula.Enabled = true;

        if (Convert.ToInt32(ddlOrigemPO.SelectedValue) == Convert.ToInt32(OrigemPO.Direto))
        {
            trPedidoServico.Visible = false;
            //trRMC.Visible = false;

        }
        else if (Convert.ToInt32(ddlOrigemPO.SelectedValue) == Convert.ToInt32(OrigemPO.GastoExtraPS))
        {
            trPedidoServico.Visible = true;
            if (Request["pm"] == null)
            {
               // trRMC.Visible = false;
            }

            ddlCelula.Enabled = false;
        }

        ddlCelula.UpdateAfterCallBack = true;

        trOrigemMaterial.Visible = (Request["pm"] != null && _origemPO == OrigemPO.GastoExtraPS);
    }

    void ucBuscaMaterial_SelectedValueChanged(object source, BuscaServicoMaterialEventArgs e)
    {
        if(_pedido.TipoCompra.TipoCompraEnum == TipoCompraEnum.Licitacao)
        {
            LicitacaoItem itemLicitacao = LicitacaoItem.GetItemAberto(e.ServicoMaterial.ID, Convert.ToInt32(ddlLicitacao.SelectedValue));

            if(itemLicitacao == null)
            {
                ShowMessage("O material selecionado deve estar disponível em uma licitação");
                ucBuscaMaterial.Reset();
                return;
            }
            else
            {
                txtValor1.Text = itemLicitacao.ValorFinalPregao.ToString("N2");
                txtValor2.Text = "";
                txtValor3.Text = "";
                txtValor4.Text = "";
                txtValor1.UpdateAfterCallBack = txtValor2.UpdateAfterCallBack = txtValor3.UpdateAfterCallBack = txtValor4.UpdateAfterCallBack = true;
            }
        }

        if (_pedido.TipoCompra.TipoCompraEnum == TipoCompraEnum.Dispensa)
        {
            LicitacaoItem itemLicitacao = LicitacaoItem.GetItemAberto(e.ServicoMaterial.ID, Convert.ToInt32(ddlLicitacao.SelectedValue));

            if (itemLicitacao != null)
            {
                ShowMessage("Esse material selecionado já possui licitação: " + itemLicitacao.Licitacao.NumeroCI + ".");
            }
        }

        CarregaDadosLicitacao(e.ServicoMaterial);

        ServicoMaterial Material = ServicoMaterial.Get(e.ServicoMaterial.ID);
        
        SubNaturezaDespesa SubNaturezaDespesa = SubNaturezaDespesa.Get(Material.SubNaturezaDespesa.ID);

        lblNaturezaDespesa.Text = Material.NaturezaDespesa.Codigo + " - " + Material.NaturezaDespesa.Descricao;
        lblNaturezaDespesa.UpdateAfterCallBack = true;

        if (Request["cadastroespecial"] != null)
        {
            Util.FillDropDownList(ddlSubNaturezaDespesa, SubNaturezaDespesa.List(Material.ID), ESCOLHA_OPCAO);
            ddlSubNaturezaDespesa.UpdateAfterCallBack = true;
            
            ddlSubNaturezaDespesa.SelectedValue = Material.SubNaturezaDespesa.ID.ToString();
            ddlSubNaturezaDespesa.Visible = true;
            ddlSubNaturezaDespesa.UpdateAfterCallBack = true;

            lblSubNaturezaDespesa.Visible = false;
            lblSubNaturezaDespesa.UpdateAfterCallBack = true;
        }
        else
        {
            lblSubNaturezaDespesa.Text = SubNaturezaDespesa.NaturezaDespesa.Codigo + "." + SubNaturezaDespesa.NaturezaDespesa.Descricao + SubNaturezaDespesa.Descricao;
            lblSubNaturezaDespesa.Visible = true;
            lblSubNaturezaDespesa.UpdateAfterCallBack = true;

            ddlSubNaturezaDespesa.Visible = false;
            ddlSubNaturezaDespesa.UpdateAfterCallBack = true;
        }

        AtualizaQuantidade(e.ServicoMaterial.ID);
        ucSaldoServicoMaterial.AtualizarSaldo(e.ServicoMaterial.ID, _pedido.DataEmissao.Year);
    }

    private void AtualizaQuantidade(int id_servicoMaterial)
    {
        QuantidadeEstoque qtd = MovimentoEstoque.GetQuantidadeEstoque(id_servicoMaterial, Convert.ToInt32(ddlOrigemMaterial.SelectedValue));
        lblEstoquePEP.Text = qtd.QuantidadeAtual.ToString();
        lblEstoquePEP.UpdateAfterCallBack = true;
    }

    void ddlOrigemMaterial_SelectedIndexChanged(object sender, EventArgs e)
    {
        OrigemMaterialChanged();
    }

    private void OrigemMaterialChanged()
    {
        //Anthem.AnthemClientMethods.ShowHide(trRMC, Convert.ToInt32(ddlOrigemMaterial.SelectedValue) == Convert.ToInt32(OrigemMaterial.Singra));
        AtualizaQuantidade(Convert.ToInt32(ddlOrigemMaterial.SelectedValue));
    }

    void ucOrcamento_SelectedValueChanged(object source, BuscaOrcamentoEventArgs e)
    {
        ddlCelula.SelectedValue = e.DelineamentoOrcamento.Celula.ID.ToString();
        ddlCelula.UpdateAfterCallBack = true;

        lblEquipamento.Text = e.DelineamentoOrcamento.DescricaoEquipamentos;
        lblEquipamento.UpdateAfterCallBack = true;
    }
    #endregion

    #region PedidoObtencao

    private void SalvarPedidoObtencao()
    {
        if (ValidaFornecedores() == false)
        {
            ShowMessage("O fornecedor não pode se repetir.");
            return;
        }

        if(Convert.ToInt32(ddlTipoCompra.SelectedValue) == Convert.ToInt32(TipoCompraEnum.Licitacao) && ddlLicitacao.SelectedValue == "0")
        {
            ShowMessage("Selecione a Licitacão");
            return;
        }

        if (Convert.ToInt32(ddlTipoCompra.SelectedValue) == Convert.ToInt32(TipoCompraEnum.Licitacao))
        {
            Licitacao licitacao = Licitacao.Get(Convert.ToInt32(ddlLicitacao.SelectedValue));
            if (licitacao.Itens.Where(i => i.Fornecedor.ID == Convert.ToInt32(ucFornecedor1.SelectedValue)).Count() == 0)
            {
                ShowMessage("Selecione um fornecedor contido na Licitacão");
                return;
            }
        }

        FillObject();
        _pedido.Save();
        UpdateLabels();
        ShowSuccessMessage();
    }

    private void UpdateLabels()
    {
        lblData.Text = ObjectReader.ReadDate(_pedido.DataEmissao);
        lblNumero.Text = _pedido.CodigoComAno;
        lblStatus.Text = _pedido.Status.Descricao;
        
        lblData.UpdateAfterCallBack = true;
        lblNumero.UpdateAfterCallBack = true;
        lblStatus.UpdateAfterCallBack = true;
    }

    private void PopulateFields()
    {
        UpdateLabels();
        
        txtAplicacao.Text = _pedido.Aplicacao;
        txtObservacao.Text = _pedido.Observacao;
        ddlCelula.SelectedValue = _pedido.Celula.ID.ToString();
        ddlTipoPedido.SelectedValue = Convert.ToInt32(_pedido.TipoPedido).ToString();
        ddlOrigemPO.SelectedValue = Convert.ToInt32(_pedido.OrigemPO).ToString();
        ucOrcamento.SelectedValue = ObjectReader.ReadID(_pedido.DelineamentoOrcamento);
        ucOrcamento.Text = _pedido.DelineamentoOrcamento != null ? _pedido.DelineamentoOrcamento.CodigoComAno : "";
        lblEquipamento.Text = _pedido.DelineamentoOrcamento != null ? _pedido.DelineamentoOrcamento.DescricaoEquipamentos : "";
        ddlTipoPedidoObtencao.SelectedValue = Convert.ToInt32(_pedido.TipoPedidoObtencao).ToString();
        ddlTipoCompra.SelectedValue = ObjectReader.ReadID(_pedido.TipoCompra);
        ddlModalidade.SelectedValue = ObjectReader.ReadID(_pedido.Modalidade);
        ddlLicitacao.SelectedValue = ObjectReader.ReadID(_pedido.Licitacao);
      
        ucFornecedor1.SelectedValue = ObjectReader.ReadID(_pedido.Fornecedor);
        ucFornecedor1.Text = _pedido.Fornecedor != null ? _pedido.Fornecedor.RazaoSocial : "";
        
        ucFornecedor2.SelectedValue = ObjectReader.ReadID(_pedido.FornecedorCotacao2);
        ucFornecedor2.Text = _pedido.FornecedorCotacao2 != null ? _pedido.FornecedorCotacao2.RazaoSocial : "";

        ucFornecedor3.SelectedValue = ObjectReader.ReadID(_pedido.FornecedorCotacao3);
        ucFornecedor3.Text = _pedido.FornecedorCotacao3 != null ? _pedido.FornecedorCotacao3.RazaoSocial : "";

        ucFornecedor4.SelectedValue = ObjectReader.ReadID(_pedido.FornecedorCotacao4);
        ucFornecedor4.Text = _pedido.FornecedorCotacao4 != null ? _pedido.FornecedorCotacao4.RazaoSocial : "";

        BindItemPedidoObtencao();
        BindHistorico();

        ValidaTipoLicitacao();
    }

    //protected void ddlTipoCompra_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    ucFornecedor1.ID_TipoCompra = Convert.ToInt32(ddlTipoCompra.SelectedValue);
    //    ucFornecedor2.ID_TipoCompra = Convert.ToInt32(ddlTipoCompra.SelectedValue);
    //    ucFornecedor3.ID_TipoCompra = Convert.ToInt32(ddlTipoCompra.SelectedValue);
    //    ucFornecedor4.ID_TipoCompra = Convert.ToInt32(ddlTipoCompra.SelectedValue);

    //    ucFornecedor1.AtualizarSaldo();
    //    ucFornecedor2.AtualizarSaldo();
    //    ucFornecedor3.AtualizarSaldo();
    //    ucFornecedor4.AtualizarSaldo();
    //}

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
        ucFornecedor1.Text = "";
        ucFornecedor1.SelectedValue = "0";
        ucFornecedor2.Text = "";
        ucFornecedor2.SelectedValue = "0";
        ucFornecedor3.Text = "";
        ucFornecedor3.SelectedValue = "0";
        ucFornecedor4.Text = "";
        ucFornecedor4.SelectedValue = "0";
        ucOrcamento.Reset();
        ddlCelula.Enabled = true;
        lblEquipamento.Text = "";
    
        ddlTipoPedido.SelectedIndex = -1;
        ddlTipoCompra.SelectedIndex = -1;
        ddlModalidade.SelectedIndex = -1;
        ddlLicitacao.SelectedIndex = -1;
        ddlCelula.Items.Clear();
        Util.InsertDefaultItem(ddlCelula, ESCOLHA_OPCAO);

        dgItem.DataSource = null;
        dgItem.DataBind();
        
        RefreshFields();

        ClearCarregaDadosLicitacao();
    }

    private void RefreshFields()
    {

        lblData.UpdateAfterCallBack = true;
        lblNumero.UpdateAfterCallBack = true;
        txtAplicacao.UpdateAfterCallBack = true;
        txtObservacao.UpdateAfterCallBack = true;
        ddlCelula.UpdateAfterCallBack = true;
        ddlTipoPedido.UpdateAfterCallBack = true;
        
        ucFornecedor1.UpdateAfterCallBack = true;
        ucFornecedor2.UpdateAfterCallBack = true;
        ucFornecedor3.UpdateAfterCallBack = true;
        ucFornecedor4.UpdateAfterCallBack = true;

        lblEquipamento.UpdateAfterCallBack = true;

        ddlTipoCompra.UpdateAfterCallBack = true;
        ddlModalidade.UpdateAfterCallBack = true;
        ddlLicitacao.UpdateAfterCallBack = true;

        dgItem.UpdateAfterCallBack = true;
    }

    private void FillObject()
    {
        _pedido.Aplicacao = txtAplicacao.Text;
        _pedido.Observacao = PageReader.ReadString(txtObservacao);
        _pedido.Servidor = Servidor.Get(ID_Servidor);
        _pedido.Celula = Celula.Get(Convert.ToInt32(ddlCelula.SelectedValue));
        _pedido.TipoPedido = (TipoPedido)Convert.ToInt32(ddlTipoPedido.SelectedValue);
        _pedido.TipoCompra = TipoCompra.Get(Convert.ToInt32(ddlTipoCompra.SelectedValue));
        _pedido.Modalidade = Modalidade.Get(Convert.ToInt32(ddlModalidade.SelectedValue));
        _pedido.Licitacao = Licitacao.Get(Convert.ToInt32(ddlLicitacao.SelectedValue));

        _pedido.Fornecedor = Fornecedor.Get(Convert.ToInt32(ucFornecedor1.SelectedValue));
        _pedido.FornecedorCotacao1 = Fornecedor.Get(Convert.ToInt32(ucFornecedor1.SelectedValue));
        _pedido.FornecedorCotacao2 = Fornecedor.Get(Convert.ToInt32(ucFornecedor2.SelectedValue));
        _pedido.FornecedorCotacao3 = Fornecedor.Get(Convert.ToInt32(ucFornecedor3.SelectedValue));
        _pedido.FornecedorCotacao4 = Fornecedor.Get(Convert.ToInt32(ucFornecedor4.SelectedValue));
       
        _pedido.OrigemPO = (OrigemPO) Convert.ToInt32(ddlOrigemPO.SelectedValue);
        _pedido.DelineamentoOrcamento = DelineamentoOrcamento.Get(Convert.ToInt32(ucOrcamento.SelectedValue));
        _pedido.TipoPedidoObtencao = (TipoPedidoObtencao)Convert.ToInt32(ddlTipoPedidoObtencao.SelectedValue);
    }

    #endregion

    #region ItemPedidoObtencao

    private void BindItemPedidoObtencao()
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
        PopulateFieldsItemPedidoObtencao();
    }

    void dgItem_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        if(_pedido.Status.StatusPedidoObtencaoEnum != StatusPedidoObtencaoEnum.NaoEnviado)
        {
            ShowMessage("Este pedido não pode mais sFer alterado.");
            return;
        }

        int id = Convert.ToInt32(dgItem.DataKeys[e.Item.ItemIndex]);
        PedidoObtencaoItem item = _pedido.Itens.Find(id);
        _pedido.Itens.Remove(item);
        item.Delete();
        BindItemPedidoObtencao();
    }

    private void PopulateFieldsItemPedidoObtencao()
    {
        CarregaDadosLicitacao(_item.ServicoMaterial);

        ucBuscaMaterial.SelectedValue = ObjectReader.ReadID(_item.ServicoMaterial);
        ucBuscaMaterial.Text = _item.ServicoMaterial.DescricaoCompleta;

        lblNaturezaDespesa.Text = _item.ServicoMaterial.NaturezaDespesa.Codigo + " - " + _item.ServicoMaterial.NaturezaDespesa.Descricao;

        if (Request["cadastroespecial"] != null)
        {
            Util.FillDropDownList(ddlSubNaturezaDespesa, SubNaturezaDespesa.List(_item.ServicoMaterial.NaturezaDespesa.ID), ESCOLHA_OPCAO);
            ddlSubNaturezaDespesa.UpdateAfterCallBack = true;

            ddlSubNaturezaDespesa.SelectedValue = _item.SubNaturezaDespesa.ID.ToString();
            ddlSubNaturezaDespesa.Visible = true;
        }
        else
        {
            lblSubNaturezaDespesa.Text = _item.SubNaturezaDespesa.NaturezaDespesa.Codigo + "." + _item.SubNaturezaDespesa.Descricao;
            lblSubNaturezaDespesa.Visible = true;
        }
        
        txtEspecificacao.Text = _item.Especificacao;

        txtQuantidade.Text = ObjectReader.ReadDecimal(_item.Quantidade);
        txtValor1.Text = ObjectReader.ReadDecimal(_item.Valor);
        txtValor2.Text = ObjectReader.ReadDecimal(_item.ValorCotacao2);
        txtValor3.Text = ObjectReader.ReadDecimal(_item.ValorCotacao3);
        txtValor4.Text = ObjectReader.ReadDecimal(_item.ValorCotacao4);

        chkFlagExec.Checked = _item.FlagExec;

        if (_item.OrigemMaterial != OrigemMaterial.Obtencao)
        {
            ddlOrigemMaterial.SelectedValue = Convert.ToInt32(_item.OrigemMaterial).ToString();
        }

        // txtRMC.Text = _item.RMC;
        OrigemMaterialChanged();
        RefreshFieldsItemPedidoObtencao();
    }

    private void FillObjectItemPedidoObtencao()
    {
        if (_item == null)
        {
            _item = new PedidoObtencaoItem();
            _item.PedidoObtencao = _pedido;
        }

        _item.Especificacao = PageReader.ReadString(txtEspecificacao);
        _item.Quantidade = PageReader.ReadDecimal(txtQuantidade);
        _item.Valor = PageReader.ReadDecimal(txtValor1);
        _item.ValorCotacao2 = PageReader.ReadDecimal(txtValor2);
        _item.ValorCotacao3 = PageReader.ReadDecimal(txtValor3);
        _item.ValorCotacao4 = PageReader.ReadDecimal(txtValor4);
        _item.ServicoMaterial = ServicoMaterial.Get(Convert.ToInt32(ucBuscaMaterial.SelectedValue));
        _item.OrigemMaterial = OrigemMaterial.Obtencao;
        _item.FlagExec = chkFlagExec.Checked;

        if (Request["cadastroespecial"] != null)
            _item.SubNaturezaDespesa = SubNaturezaDespesa.Get(Convert.ToInt32(ddlSubNaturezaDespesa.SelectedValue));
        else
            _item.SubNaturezaDespesa = SubNaturezaDespesa.Get(ServicoMaterial.Get(Convert.ToInt32(ucBuscaMaterial.SelectedValue)).SubNaturezaDespesa.ID);
        
        if (Request["pm"] != null && _pedido.OrigemPO == OrigemPO.GastoExtraPS)
        {
            _item.OrigemMaterial = (OrigemMaterial)Convert.ToInt32(ddlOrigemMaterial.SelectedValue);
            // _item.RMC = null;
        }

        //Qtd nao pode ser zero
        if (_item.Quantidade == 0)
            throw new Exception("Quantidade deve ser cadastrada.");

        //SubNatureza nao pode ser null
        if (_item.ServicoMaterial.SubNaturezaDespesa == null)
            throw new Exception("Sub-Natureza deste serviço/material deve ser cadastrada.");

        //SubNatureza tem que ser da mesma natureza do PO
        if (_pedido.NaturezaDespesa != null && _pedido.NaturezaDespesa.ID != _item.ServicoMaterial.SubNaturezaDespesa.NaturezaDespesa.ID && _item.PedidoObtencao.TipoPedido == TipoPedido.PedidoObtencao)
            throw new Exception("Este serviço/material pertence a outra Natureza de Despesa. O PO só pode conter itens da mesma natureza.");
    }

    private void ClearFieldsItemPedidoObtencao()
    {
        ucBuscaMaterial.Reset();
        txtEspecificacao.Text = "";
        txtQuantidade.Text = "";
        chkFlagExec.Checked = false;
        if(Request["pm"] == null)
            txtValor1.Text = "";
        //txtRMC.Text = "";
        txtValor2.Text = "";
        txtValor3.Text = "";
        txtValor4.Text = "";
        lblNaturezaDespesa.Text = "";
        ddlTipoPedido.SelectedIndex = -1;

        lblSubNaturezaDespesa.Text = "";
        lblSubNaturezaDespesa.Visible = false;

        ddlSubNaturezaDespesa.SelectedIndex = -1;
        ddlSubNaturezaDespesa.Visible = false;

        ddlOrigemMaterial.SelectedValue = Convert.ToInt32(OrigemMaterial.PEP).ToString();

        ClearCarregaDadosLicitacao();
       
        RefreshFieldsItemPedidoObtencao();
        _item = null;
    }

    private void RefreshFieldsItemPedidoObtencao()
    {
        ucBuscaMaterial.UpdateAfterCallBack = true;
        txtEspecificacao.UpdateAfterCallBack = true;
        txtQuantidade.UpdateAfterCallBack = true;
        txtValor1.UpdateAfterCallBack = true;
        txtValor2.UpdateAfterCallBack = true;
        txtValor3.UpdateAfterCallBack = true;
        txtValor4.UpdateAfterCallBack = true;
        ddlOrigemMaterial.UpdateAfterCallBack = true;
        // txtRMC.UpdateAfterCallBack = true;
        chkFlagExec.UpdateAfterCallBack = true;
        lblNaturezaDespesa.UpdateAfterCallBack = true;
        lblSubNaturezaDespesa.UpdateAfterCallBack = true;
        ddlSubNaturezaDespesa.UpdateAfterCallBack = true;
    }

    private void SalvarItemPedidoObtencao()
    {
        FillObjectItemPedidoObtencao();

        decimal Valor2 = (txtValor2.Text == "") ? Convert.ToDecimal(0) : Convert.ToDecimal(txtValor2.Text);
        decimal Valor3 = (txtValor2.Text == "") ? Convert.ToDecimal(0) : Convert.ToDecimal(txtValor2.Text);
        decimal Valor4 = (txtValor2.Text == "") ? Convert.ToDecimal(0) : Convert.ToDecimal(txtValor2.Text);

        if ((Convert.ToDecimal(txtValor1.Text) > Valor2 && Valor2 > 0) || (Convert.ToDecimal(txtValor1.Text) > Valor3 && Valor3 > 0) || (Convert.ToDecimal(txtValor1.Text) > Valor4 && Valor4 > 0))
        {
            ShowMessage("Valor do ganhador não pode ser maior que o valor 2, 3 e 4.");
            return;
        }

        bool IsNew = !_item.IsPersisted;
        _item.Save();
        if(IsNew)
            _pedido.Itens.Add(_item);

        BindItemPedidoObtencao();
        ClearFieldsItemPedidoObtencao();
    }

    void btnAtualizarCotacao_Click(object sender, EventArgs e)
    {
        foreach (DataGridItem gridItem in dgItem.Items)
        {
            int id = Convert.ToInt32(dgItem.DataKeys[gridItem.ItemIndex]);
            PedidoObtencaoItem item = _pedido.Itens.Find(id);

            TextBox txtValorGrid1 = (TextBox) gridItem.FindControl("txtValorGrid1");
            TextBox txtValorGrid2 = (TextBox)gridItem.FindControl("txtValorGrid2");
            TextBox txtValorGrid3 = (TextBox)gridItem.FindControl("txtValorGrid3");
            TextBox txtValorGrid4 = (TextBox)gridItem.FindControl("txtValorGrid4");

            item.Valor = PageReader.ReadDecimal(txtValorGrid1);
            item.ValorCotacao2 = PageReader.ReadDecimal(txtValorGrid2);
            item.ValorCotacao3 = PageReader.ReadDecimal(txtValorGrid3);
            item.ValorCotacao4 = PageReader.ReadDecimal(txtValorGrid4);

            item.Save();
        }

        ShowSuccessMessage();
    }

    #endregion

    #region Outros

    void ddlTipoCompra_SelectedIndexChanged(object sender, EventArgs e)
    {
        int id = Convert.ToInt32(ddlTipoCompra.SelectedValue);

        _pedido.TipoCompra = (TipoCompra.Get(id));

        if (_pedido.TipoCompra.TipoCompraEnum == TipoCompraEnum.Licitacao)
        {
            ucFornecedor2.Text = "";
            ucFornecedor2.SelectedValue = "0";
            ucFornecedor3.Text = "";
            ucFornecedor3.SelectedValue = "0";
            ucFornecedor4.Text = "";
            ucFornecedor4.SelectedValue = "0";

            ucFornecedor2.Enabled = ucFornecedor3.Enabled = ucFornecedor4.Enabled = false;
            
            ddlLicitacao.Enabled = true;

            txtValor1.Enabled = txtValor2.Enabled = txtValor3.Enabled = txtValor4.Enabled = false;
        }
        else
        {
            ucFornecedor2.Enabled = ucFornecedor3.Enabled = true;

            ddlLicitacao.Enabled = false;

            ddlLicitacao.SelectedValue = "0";

            txtValor1.Enabled = txtValor2.Enabled = txtValor3.Enabled = txtValor4.Enabled = true;
        }

        ddlLicitacao.UpdateAfterCallBack = ucFornecedor2.UpdateAfterCallBack = ucFornecedor3.UpdateAfterCallBack = ucFornecedor4.UpdateAfterCallBack =
        txtValor1.UpdateAfterCallBack = txtValor2.UpdateAfterCallBack = txtValor3.UpdateAfterCallBack = txtValor4.UpdateAfterCallBack = true;
    }

    private void BtnEnviar_OnClick(object sender, EventArgs e)
    {
        //if ( (_pedido.Celula.TipoCelula == TipoCelula.Departamento && StatusPedidoObtencao.Get(StatusPedidoObtencaoEnum.AguardandoAprovacaoChefeDepartamento_Departamento).Responsaveis.Count == 0)
        //    || (_pedido.Celula.TipoCelula == TipoCelula.Divisao && StatusPedidoObtencao.Get(StatusPedidoObtencaoEnum.AguardandoAprovacaoChefeDepartamento_Divisao).GetResponsaveisDivisao(_pedido.Celula.ID).Count == 0))
        //{
        //    Anthem.Manager.AddScriptForClientSideEval("if(confirm('Não há nenhum responsável para aprovação deste PO, deseja continuar assim mesmo?') == true) Anthem_InvokePageMethod('Enviar');");   
        //}
        //else
            Enviar();
    }

    [Anthem.Method]
    public void Enviar()
    {
        _pedido.Enviar(ID_Servidor);
        UpdateLabels();
        ShowSuccessMessage();
    }

    void btnImprimir_Click(object sender, EventArgs e)
    {
        Anthem.AnthemClientMethods.Popup(this, "fchPedidoObtencaoCompleto.aspx?id_pedido=" + _pedido.ID.ToString(), false, false, false, true, true, true,
        true, 40, 60, 700, 500);
    }

    void btnImprimirItens_Click(object sender, EventArgs e)
    {
        Anthem.AnthemClientMethods.Popup(this, "fchImpressaoParaCotacao.aspx?id_pedido=" + _pedido.ID.ToString(), false, false, false, true, true, true,
       true, 40, 60, 700, 500);
    }

    void btnDocumentos_Click(object sender, EventArgs e)
    {
        Anthem.AnthemClientMethods.Popup(this, "frmPedidoObtencaoDocumento.aspx?id_pedido=" + _pedido.ID.ToString(), false, false, false, true, true, true,
       true, 40, 60, 700, 500);
    }

    #endregion

    #region Copia PO

    [Anthem.Method]
    public void CopiarPO(int id_po)
    {
        try
        {
            PedidoObtencao poOrigem = PedidoObtencao.Get(id_po);
            _pedido.Aplicacao = poOrigem.Aplicacao;
            _pedido.Celula = poOrigem.Celula;
            _pedido.Observacao = poOrigem.Observacao;
            _pedido.OrigemPO = poOrigem.OrigemPO;
            _pedido.TipoPedido = poOrigem.TipoPedido;
            _pedido.TipoPedidoObtencao = poOrigem.TipoPedidoObtencao;
            _pedido.Servidor = Servidor.Get(this.ID_Servidor);
            _pedido.Save();
            foreach (PedidoObtencaoItem itemOrigem in poOrigem.Itens)
            {
                PedidoObtencaoItem item = new PedidoObtencaoItem();
                item.PedidoObtencao = _pedido;
                item.Especificacao = itemOrigem.Especificacao;
                item.OrigemMaterial = itemOrigem.OrigemMaterial;
                item.Quantidade = itemOrigem.Quantidade;
                item.ServicoMaterial = itemOrigem.ServicoMaterial;
                item.Valor = itemOrigem.Valor;
                item.Fornecedor = itemOrigem.Fornecedor;
                item.Save();
                _pedido.Itens.Add(item);

                PopulateFields();
                RefreshFields();
            }
        }
        catch(Exception ex)
        {
            ShowMessage(ex.Message);
        }
        

      
    }  
 
    #endregion

    #region Atualizar Valores Licitacao / Carrega dados da licitacao

    void btnAtualizarValoresLicitacao_Click(object sender, EventArgs e)
    {
        if(_pedido.Licitacao == null) return;

        foreach (PedidoObtencaoItem item in _pedido.Itens)
        {
            LicitacaoItem itemLicitacao = _pedido.Licitacao.Itens.Where(i => i.Material.ID == item.ServicoMaterial.ID).FirstOrDefault();

            if(itemLicitacao != null)
            {
                item.Valor = itemLicitacao.Valor1;
                if (_pedido.TipoCompra.TipoCompraEnum != TipoCompraEnum.Licitacao)
                {
                    item.ValorCotacao2 = itemLicitacao.Valor2;
                    item.ValorCotacao3 = itemLicitacao.Valor3;
                }
            }
        }

        BindItemPedidoObtencao();
    }

    void CarregaDadosLicitacao(ServicoMaterial ServicoMaterial)
    {
        if (_pedido.Licitacao == null)
        {
            trDadosLicitacao.Visible = false;
        }
        else
        {
            LicitacaoItem itemLicitacao = _pedido.Licitacao.Itens.Where(i => i.Material.ID == ServicoMaterial.ID).FirstOrDefault();

            if (itemLicitacao != null)
            {
                lblLicitacaoNumeroProcesso.Text = "Número Processo: " + itemLicitacao.NumeroContratoAta;
                lblLicitacaoDataEmissao.Text = "Data Emissão: " + itemLicitacao.Licitacao.DataEmissao.ToShortDateString();
                lblLicitacaoTipoLicitacao.Text = "Tipo Licitação: " + itemLicitacao.Licitacao.TipoLicitacao.Descricao;
                lblLicitacaoSistemaLicitatorio.Text = "Sistema Licitatório: " + Util.GetDescription(itemLicitacao.Licitacao.ProcessoLicitatorio);
                lblLicitacaoDataPregao.Text = itemLicitacao.Licitacao.DataPregao.HasValue ? "Data Pregão: " + itemLicitacao.Licitacao.DataPregao.Value.ToShortDateString() : "";
                lblLicitacaoStatus.Text = "Status: " + Util.GetDescription(itemLicitacao.Licitacao.Status);

                ReflashCarregaDadosLicitacao();
            }
        }
    }

    void ClearCarregaDadosLicitacao()
    {
        lblLicitacaoNumeroProcesso.Text = "";
        lblLicitacaoDataEmissao.Text = "";
        lblLicitacaoTipoLicitacao.Text = "";
        lblLicitacaoSistemaLicitatorio.Text = "";
        lblLicitacaoDataPregao.Text = "";
        lblLicitacaoStatus.Text = "";

        ReflashCarregaDadosLicitacao();
    }

    void ReflashCarregaDadosLicitacao()
    {
        lblLicitacaoNumeroProcesso.UpdateAfterCallBack = true;
        lblLicitacaoDataEmissao.UpdateAfterCallBack = true;
        lblLicitacaoTipoLicitacao.UpdateAfterCallBack = true;
        lblLicitacaoSistemaLicitatorio.UpdateAfterCallBack = true;
        lblLicitacaoDataPregao.UpdateAfterCallBack = true;
        lblLicitacaoStatus.UpdateAfterCallBack = true;
    }

    #endregion

    #region Valida Fornecedores

    bool ValidaFornecedores()
    {
        if ((ucFornecedor1.SelectedValue == ucFornecedor2.SelectedValue && ucFornecedor1.SelectedValue != "0") ||
            (ucFornecedor1.SelectedValue == ucFornecedor3.SelectedValue && ucFornecedor1.SelectedValue != "0") ||
            (ucFornecedor1.SelectedValue == ucFornecedor4.SelectedValue && ucFornecedor1.SelectedValue != "0")
            )
        {
            return false;
        }

        else if ((ucFornecedor2.SelectedValue == ucFornecedor1.SelectedValue && ucFornecedor2.SelectedValue != "0") ||
                 (ucFornecedor2.SelectedValue == ucFornecedor3.SelectedValue && ucFornecedor2.SelectedValue != "0") ||
                 (ucFornecedor2.SelectedValue == ucFornecedor4.SelectedValue && ucFornecedor2.SelectedValue != "0")
           )
        {
            return false;
        }

        else if ((ucFornecedor3.SelectedValue == ucFornecedor1.SelectedValue && ucFornecedor3.SelectedValue != "0") ||
                 (ucFornecedor3.SelectedValue == ucFornecedor2.SelectedValue && ucFornecedor3.SelectedValue != "0") ||
                 (ucFornecedor3.SelectedValue == ucFornecedor4.SelectedValue && ucFornecedor3.SelectedValue != "0")
           )
        {
            return false;
        }

        else if ((ucFornecedor4.SelectedValue == ucFornecedor1.SelectedValue && ucFornecedor4.SelectedValue != "0") ||
                 (ucFornecedor4.SelectedValue == ucFornecedor2.SelectedValue && ucFornecedor4.SelectedValue != "0") ||
                 (ucFornecedor4.SelectedValue == ucFornecedor3.SelectedValue && ucFornecedor4.SelectedValue != "0")
        )
        {
            return false;
        }

        return true;
    }

    #endregion

    #region Valida Tipo Licitação

    void ValidaTipoLicitacao()
    {
        if (_pedido.TipoCompra.TipoCompraEnum == TipoCompraEnum.Licitacao)
        {
            ucFornecedor2.Text = "";
            ucFornecedor2.SelectedValue = "0";
            ucFornecedor3.Text = "";
            ucFornecedor3.SelectedValue = "0";
            ucFornecedor4.Text = "";
            ucFornecedor4.SelectedValue = "0";

            ucFornecedor2.Enabled = ucFornecedor3.Enabled = ucFornecedor4.Enabled = false;

            ddlLicitacao.Enabled = true;

            txtValor1.Enabled = txtValor2.Enabled = txtValor3.Enabled = txtValor4.Enabled = false;
        }
        else
        {
            ucFornecedor2.Enabled = ucFornecedor3.Enabled = true;

            ddlLicitacao.Enabled = false;

            ddlLicitacao.SelectedValue = "0";

            txtValor1.Enabled = txtValor2.Enabled = txtValor3.Enabled = txtValor4.Enabled = true;
        }
    }

    #endregion
}



