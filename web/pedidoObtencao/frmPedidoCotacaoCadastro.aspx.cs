using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Drawing;
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

public partial class frmPedidoCotacaoCadastro : MarinhaPageBase
{
    #region Private Member
    [TransientPageState]
    protected PedidoCotacao _cotacao;

    #endregion 

    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.btnSalvar.Click += new EventHandler(btnSalvar_Click);
        btnEnviar.Click += BtnEnviar_OnClick;
        dgItem.DeleteCommand += new DataGridCommandEventHandler(dgItem_DeleteCommand);
        btnMarcarMenorPreco.Click += new EventHandler(btnMarcarMenorPreco_Click);
        btnAtualizarSaldo.Click += delegate { AtualizarSaldos();};
        dgItem.ItemDataBound += new DataGridItemEventHandler(dgItem_ItemDataBound);
        dgItem.ItemCommand += new DataGridCommandEventHandler(dgItem_ItemCommand);
        ucComentario.JustificativaInformada += new EventHandler(ucComentario_JustificativaInformada);
    }

  

    void dgItem_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if(e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            LinkButton lnkUltimasCotacoes = (LinkButton) e.Item.FindControl("lnkUltimasCotacoes");
            PedidoCotacaoItem item = (PedidoCotacaoItem) e.Item.DataItem;
            Anthem.AnthemClientMethods.Popup(lnkUltimasCotacoes, "frmPedidoCotacaoUltimasCotacoes.aspx?id_servicoMaterial=" + item.ServicoMaterial.ID.ToString(),
            false, false, false, true, true, true, true, 100, 200, 550, 230, false);
            
            //if(item.ServicoMaterial.TipoServicoMaterial == TipoServicoMaterial.Material)
            //{
            //    LinkButton lnkDadosComplementares = (LinkButton)e.Item.FindControl("lnkDadosComplementares");
            //    lnkDadosComplementares.Visible = false;
            //}

            
        }
    }
  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            FillPage();

            _cotacao = PedidoCotacao.Get(Convert.ToInt32(Request["ID_Pedido"]));
            PopulateFields();
            MarcarMenorPreco();
            DesabilitaSeLicitacao();
            AtualizarSaldos();
            if (_cotacao.FlagRecusado && !_cotacao.FlagFinalizado)
            {
                trRecusado.Visible = true;
                trJustificativa.Visible = true;
                lblJustificativa.Text = _cotacao.JustificativaRecusa;
            }


            Anthem.AnthemClientMethods.Redirect("frmPedidoCotacaoPesquisa.aspx", btnVoltar);
            RegisterDeleteScript();

            Anthem.AnthemClientMethods.Popup(btnImprimir, "fchPedidoCotacao.aspx?id_pedido=" + _cotacao.ID.ToString(), false, false, false, true, true, true,
                true, 40, 60, 700, 500, false);
                
            Anthem.AnthemClientMethods.Popup(btnImprimirParaFornecedor, "fchPedidoCotacaoParaFornecedor.aspx?id_pedido=" + _cotacao.ID.ToString(), false, false, false, true, true, true,
                true, 40, 60, 700, 500, false);
        }
    }
    
    private void DesabilitaSeLicitacao()
    {
        //Caso seja referente a uma licitação, permitimos apenas um fornecedor
        if(_cotacao.FlagLicitacao)
        {
            ucFornecedor1.Enabled = false;
            ucFornecedor1.Enabled = false;
            ucFornecedor1.Enabled = false;
            ucFornecedor1.Enabled = false;
            ddlTipoCompra.Enabled = false;
            ddlTipoCompra.SelectedValue = Convert.ToInt32(TipoCompraEnum.Licitacao).ToString();
            
            foreach (DataGridItem gridItem in dgItem.Items)
            {
                if (gridItem.ItemType == ListItemType.AlternatingItem || gridItem.ItemType == ListItemType.Item)
                {
                   
                    RadioButton rbValor2 = (RadioButton)gridItem.FindControl("rbValor2");
                    RadioButton rbValor3 = (RadioButton)gridItem.FindControl("rbValor3");
                    RadioButton rbValor4 = (RadioButton)gridItem.FindControl("rbValor4");
                    TextBox txtValor2 = (TextBox)gridItem.FindControl("txtValor2");
                    TextBox txtValor3 = (TextBox)gridItem.FindControl("txtValor3");
                    TextBox txtValor4 = (TextBox)gridItem.FindControl("txtValor4");

                    rbValor2.Enabled = rbValor3.Enabled = rbValor4.Enabled = txtValor2.Enabled = txtValor3.Enabled = txtValor4.Enabled = false;
                }
            }
        }
        else
        {
            ddlTipoCompra.Items.Remove(ddlTipoCompra.Items.FindByValue(Convert.ToInt32(TipoCompraEnum.Licitacao).ToString()));
        }
        
    }

    private void FillPage()
    {
        Util.FillDropDownList(ddlTipoCompra, TipoCompra.List(), ESCOLHA_OPCAO);
        Util.FillDropDownList(ddlNaturezaDespesa, NaturezaDespesa.List(), ESCOLHA_OPCAO);
    }

    #endregion
    
    #region Events
    void btnSalvar_Click(object sender, EventArgs e)
    {
        if(!_cotacao.PodeSerAlterado)
        {
            ShowMessage("O pedido não pode mais ser alterado.");
            return;
        }
        if (TabStrip1.SelectedTab.ID == tabDadosBasicos.ID)
            SalvarAC();
        else if (TabStrip1.SelectedTab.ID == tabItem.ID)
            SalvarItem();
    }
    #endregion

    #region Pedido Cotacao
    private void SalvarAC()
    {
        FillObject();
        _cotacao.Save();
        
        BindItem();
        AtualizarSaldos();
        ShowSuccessMessage();
    }

    private void PopulateFields()
    {
        
        lblComprador.Text = _cotacao.Servidor.Identificacao;
        lblData.Text = _cotacao.DataEmissao.ToShortDateString();
        lblNumero.Text = _cotacao.Numero.ToString();
        ddlTipoCompra.SelectedValue = ObjectReader.ReadID(_cotacao.TipoCompra);
        ddlNaturezaDespesa.SelectedValue = ObjectReader.ReadID(_cotacao.NaturezaDespesa);

        ucFornecedor1.ID_TipoCompra = _cotacao.TipoCompra.ID;
        ucFornecedor1.SelectedValue = ObjectReader.ReadID(_cotacao.FornecedorCotacao1);
        ucFornecedor1.Text = _cotacao.FornecedorCotacao1 != null ? _cotacao.FornecedorCotacao1.RazaoSocial : "";

        ucFornecedor2.ID_TipoCompra = _cotacao.TipoCompra.ID;
        ucFornecedor2.SelectedValue = ObjectReader.ReadID(_cotacao.FornecedorCotacao2);
        ucFornecedor2.Text = _cotacao.FornecedorCotacao2 != null ? _cotacao.FornecedorCotacao2.RazaoSocial : "";

        ucFornecedor3.ID_TipoCompra = _cotacao.TipoCompra.ID;
        ucFornecedor3.SelectedValue = ObjectReader.ReadID(_cotacao.FornecedorCotacao3);
        ucFornecedor3.Text = _cotacao.FornecedorCotacao3 != null ? _cotacao.FornecedorCotacao3.RazaoSocial : "";

        ucFornecedor4.ID_TipoCompra = _cotacao.TipoCompra.ID;
        ucFornecedor4.SelectedValue = ObjectReader.ReadID(_cotacao.FornecedorCotacao4);
        ucFornecedor4.Text = _cotacao.FornecedorCotacao4 != null ? _cotacao.FornecedorCotacao4.RazaoSocial : "";
        
        txtObservacao.Text = _cotacao.Observacao;
        
        BindItem();
    }

    
    private void FillObject()
    {
        _cotacao.TipoCompra = TipoCompra.Get(Convert.ToInt32(ddlTipoCompra.SelectedValue));
        _cotacao.NaturezaDespesa = NaturezaDespesa.Get(Convert.ToInt32(ddlNaturezaDespesa.SelectedValue));
        _cotacao.FornecedorCotacao1 = Fornecedor.Get(Convert.ToInt32(ucFornecedor1.SelectedValue));
        _cotacao.FornecedorCotacao2 = Fornecedor.Get(Convert.ToInt32(ucFornecedor2.SelectedValue));
        _cotacao.FornecedorCotacao3 = Fornecedor.Get(Convert.ToInt32(ucFornecedor3.SelectedValue));
        _cotacao.FornecedorCotacao4 = Fornecedor.Get(Convert.ToInt32(ucFornecedor4.SelectedValue));
        _cotacao.Observacao = PageReader.ReadString(txtObservacao);
        
    }
    #endregion

    #region ItemPedidoObtencao
    private void BindItem()
    {
        dgItem.DataSource = _cotacao.Itens;
        dgItem.DataKeyField = "ID";
        dgItem.DataBind();
        dgItem.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();
    }

   

    private void SalvarItem()
    {
        foreach (DataGridItem gridItem in dgItem.Items)
        {
            if(gridItem.ItemType == ListItemType.AlternatingItem || gridItem.ItemType == ListItemType.Item)
            {
                TextBox txtValor1 = (TextBox) gridItem.FindControl("txtValor1");
                TextBox txtValor2 = (TextBox)gridItem.FindControl("txtValor2");
                TextBox txtValor3 = (TextBox)gridItem.FindControl("txtValor3");
                TextBox txtValor4 = (TextBox)gridItem.FindControl("txtValor4");

                int id = Convert.ToInt32(dgItem.DataKeys[gridItem.ItemIndex]);
                PedidoCotacaoItem item = _cotacao.Itens.Find(id);
                item.ValorCotacao1 = PageReader.ReadNullableDecimal(txtValor1);
                item.ValorCotacao2 = PageReader.ReadNullableDecimal(txtValor2);
                item.ValorCotacao3 = PageReader.ReadNullableDecimal(txtValor3);
                item.ValorCotacao4 = PageReader.ReadNullableDecimal(txtValor4);
                item.Save();
            }
        }
        ShowSuccessMessage();
    }

    void dgItem_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        int id = Convert.ToInt32(dgItem.DataKeys[e.Item.ItemIndex]);
        _cotacao.RetirarItemCotacao(id);
        BindItem();
    }

    void dgItem_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "DadosComplementares")
        {
            int id = Convert.ToInt32(dgItem.DataKeys[e.Item.ItemIndex]);
            PedidoCotacaoItem item = _cotacao.Itens.Find(id);
            ucDadosComplementares.Show(item);
        }
        else if(e.CommandName == "AbrirPO")
        {
            int id = Convert.ToInt32(dgItem.DataKeys[e.Item.ItemIndex]);
            PedidoCotacaoItem item = _cotacao.Itens.Find(id);

            if (item.ItensObtencao.Count > 0)
            {
                LinkButton lnkDetalhesPO = (LinkButton)e.Item.FindControl("lnkDetalhesPO");

                int left = 40;
                foreach (PedidoObtencaoItem obtencaoItem in item.ItensObtencao)
                {
                    Anthem.AnthemClientMethods.Popup(this,
                                                     "fchPedidoObtencaoCompleto.aspx?id_pedido=" +
                                                     obtencaoItem.PedidoObtencao.ID, obtencaoItem.PedidoObtencao.ID.ToString(),
                                                     false, false, false, true, true, true, true, 50, left, 700, 570);
                    left += 20;
                }

            }
        }
    }
    #endregion

    private void BtnEnviar_OnClick(object sender, EventArgs e)
    {
        ucComentario.Show(0);
    }

    void ucComentario_JustificativaInformada(object sender, EventArgs e)
    {
        Dictionary<int, int> list = new Dictionary<int, int>(dgItem.Items.Count);

        foreach (DataGridItem gridItem in dgItem.Items)
        {
            if (gridItem.ItemType == ListItemType.AlternatingItem || gridItem.ItemType == ListItemType.Item)
            {
                RadioButton rbValor1 = (RadioButton)gridItem.FindControl("rbValor1");
                RadioButton rbValor2 = (RadioButton)gridItem.FindControl("rbValor2");
                RadioButton rbValor3 = (RadioButton)gridItem.FindControl("rbValor3");
                RadioButton rbValor4 = (RadioButton)gridItem.FindControl("rbValor4");

                int id = Convert.ToInt32(dgItem.DataKeys[gridItem.ItemIndex]);
                if (rbValor1.Checked)
                    list.Add(id, 1);
                else if (rbValor2.Checked)
                    list.Add(id, 2);
                else if (rbValor3.Checked)
                    list.Add(id, 3);
                else if (rbValor4.Checked)
                    list.Add(id, 4);

            }
        }

        _cotacao.CriarAutorizacaoCompra(list,ucComentario.Justificativa);
        ShowSuccessMessage();
        ucComentario.Close();
    }

    void btnMarcarMenorPreco_Click(object sender, EventArgs e)
    {
        MarcarMenorPreco();
    }

    void MarcarMenorPreco()
    {
        foreach (DataGridItem gridItem in dgItem.Items)
        {
            if (gridItem.ItemType == ListItemType.AlternatingItem || gridItem.ItemType == ListItemType.Item)
            {

                TextBox txtValor1 = (TextBox)gridItem.FindControl("txtValor1");
                TextBox txtValor2 = (TextBox)gridItem.FindControl("txtValor2");
                TextBox txtValor3 = (TextBox)gridItem.FindControl("txtValor3");
                TextBox txtValor4 = (TextBox)gridItem.FindControl("txtValor4");

                List<decimal?> valores = new List<decimal?>(4);
                valores.Add(PageReader.ReadNullableDecimal(txtValor1));
                valores.Add(PageReader.ReadNullableDecimal(txtValor2));
                valores.Add(PageReader.ReadNullableDecimal(txtValor3));
                valores.Add(PageReader.ReadNullableDecimal(txtValor4));

                int menor = 0;
                for (int i = 1; i < 4; i++)
                {
                    if (valores[menor].HasValue && valores[i].HasValue)
                    {
                        if (valores[menor] > valores[i])
                            menor = i;
                    }
                    else if (!valores[menor].HasValue && valores[i].HasValue)
                        menor = i;
                }

                Anthem.RadioButton rb = (Anthem.RadioButton)gridItem.FindControl("rbValor" + (menor + 1).ToString());
                rb.Checked = true;
                rb.UpdateAfterCallBack = true;

            }
        }
    }

    protected void ddlTipoCompra_SelectedIndexChanged(object sender, EventArgs e)
    {
        ucFornecedor1.ID_TipoCompra = Convert.ToInt32(ddlTipoCompra.SelectedValue);
        ucFornecedor2.ID_TipoCompra = Convert.ToInt32(ddlTipoCompra.SelectedValue);
        ucFornecedor3.ID_TipoCompra = Convert.ToInt32(ddlTipoCompra.SelectedValue);
        ucFornecedor4.ID_TipoCompra = Convert.ToInt32(ddlTipoCompra.SelectedValue);
        
        ucFornecedor1.AtualizarSaldo();
        ucFornecedor2.AtualizarSaldo();
        ucFornecedor3.AtualizarSaldo();
        ucFornecedor4.AtualizarSaldo();
        
    }
    
    private void AtualizarSaldos()
    {
        lblFornecedorLimite1.Text = _cotacao.FornecedorCotacao1 != null ? _cotacao.FornecedorCotacao1.ToString() : "";
        lblFornecedorLimite2.Text = _cotacao.FornecedorCotacao2 != null ? _cotacao.FornecedorCotacao2.ToString() : "";
        lblFornecedorLimite3.Text = _cotacao.FornecedorCotacao3 != null ? _cotacao.FornecedorCotacao3.ToString() : "";
        lblFornecedorLimite4.Text = _cotacao.FornecedorCotacao4 != null ? _cotacao.FornecedorCotacao4.ToString() : "";

        decimal totalFornecedor1 = 0;
        decimal totalFornecedor2 = 0;
        decimal totalFornecedor3 = 0;
        decimal totalFornecedor4 = 0;
        foreach (DataGridItem gridItem in dgItem.Items)
        {
            if (gridItem.ItemType == ListItemType.AlternatingItem || gridItem.ItemType == ListItemType.Item)
            {
                TextBox txtValor1 = (TextBox)gridItem.FindControl("txtValor1");
                TextBox txtValor2 = (TextBox)gridItem.FindControl("txtValor2");
                TextBox txtValor3 = (TextBox)gridItem.FindControl("txtValor3");
                TextBox txtValor4 = (TextBox)gridItem.FindControl("txtValor4");

                RadioButton rbValor1 = (RadioButton)gridItem.FindControl("rbValor1");
                RadioButton rbValor2 = (RadioButton)gridItem.FindControl("rbValor2");
                RadioButton rbValor3 = (RadioButton)gridItem.FindControl("rbValor3");
                RadioButton rbValor4 = (RadioButton)gridItem.FindControl("rbValor4");

                int id = Convert.ToInt32(dgItem.DataKeys[gridItem.ItemIndex]);
                PedidoCotacaoItem item = _cotacao.Itens.Find(id);
                
                if(rbValor1.Checked)
                    totalFornecedor1 += item.Quantidade * (PageReader.ReadNullableDecimal(txtValor1).HasValue ? PageReader.ReadNullableDecimal(txtValor1).Value : 0);
                if (rbValor2.Checked)
                    totalFornecedor2 += item.Quantidade * (PageReader.ReadNullableDecimal(txtValor2).HasValue ? PageReader.ReadNullableDecimal(txtValor2).Value : 0);
                if (rbValor3.Checked)
                    totalFornecedor3 += item.Quantidade * (PageReader.ReadNullableDecimal(txtValor3).HasValue ? PageReader.ReadNullableDecimal(txtValor3).Value : 0);
                if (rbValor4.Checked)
                    totalFornecedor4 += item.Quantidade * (PageReader.ReadNullableDecimal(txtValor4).HasValue ? PageReader.ReadNullableDecimal(txtValor4).Value : 0);
            }
        }

        SaldoFornecedor saldo =
           AutorizacaoCompra.GetSaldoComprasUtilizado(Convert.ToInt32(ucFornecedor1.SelectedValue), Convert.ToInt32(ddlTipoCompra.SelectedValue),
                                               DateTime.Today.Year);
        lblSaldoReal1.Text = saldo.SaldoReal.ToString("N2");
        lblARealizar1.Text = saldo.SaldoARealizar.ToString("N2");
        lblSaldoTotal1.Text = (saldo.SaldoTotal - totalFornecedor1).ToString("N2");

        saldo =
          AutorizacaoCompra.GetSaldoComprasUtilizado(Convert.ToInt32(ucFornecedor2.SelectedValue), Convert.ToInt32(ddlTipoCompra.SelectedValue),
                                              DateTime.Today.Year);
        lblSaldoReal2.Text = saldo.SaldoReal.ToString("N2");
        lblARealizar2.Text = saldo.SaldoARealizar.ToString("N2");
        lblSaldoTotal2.Text = (saldo.SaldoTotal - totalFornecedor2).ToString("N2");

        saldo =
          AutorizacaoCompra.GetSaldoComprasUtilizado(Convert.ToInt32(ucFornecedor3.SelectedValue), Convert.ToInt32(ddlTipoCompra.SelectedValue),
                                              DateTime.Today.Year);
        lblSaldoReal3.Text = saldo.SaldoReal.ToString("N2");
        lblARealizar3.Text = saldo.SaldoARealizar.ToString("N2");
        lblSaldoTotal3.Text = (saldo.SaldoTotal - totalFornecedor3).ToString("N2");

        saldo =
          AutorizacaoCompra.GetSaldoComprasUtilizado(Convert.ToInt32(ucFornecedor4.SelectedValue), Convert.ToInt32(ddlTipoCompra.SelectedValue),
                                              DateTime.Today.Year);
        lblSaldoReal4.Text = saldo.SaldoReal.ToString("N2");
        lblARealizar4.Text = saldo.SaldoARealizar.ToString("N2");
        lblSaldoTotal4.Text = (saldo.SaldoTotal - totalFornecedor4).ToString("N2");


        //lblSaldo1.ForeColor = lblSaldo1.Text.Contains("-") ? Color.Red : Color.Black;
        //lblSaldo2.ForeColor = lblSaldo2.Text.Contains("-") ? Color.Red : Color.Black;
        //lblSaldo3.ForeColor = lblSaldo3.Text.Contains("-") ? Color.Red : Color.Black;
        //lblSaldo4.ForeColor = lblSaldo4.Text.Contains("-") ? Color.Red : Color.Black;
        
        pnLimites.UpdateAfterCallBack = true;
    }


    protected void btnEspecificacao_Click(object sender, EventArgs e)
    {
        if(!_cotacao.PodeSerAlterado)
        {
            ShowMessage("Este PC não pode mais ser alterado.");
            return;
        }
        LinkButton btnEspecificacao = (LinkButton) sender;
        DataGridItem item = (DataGridItem)btnEspecificacao.Parent.Parent;
        Panel pnEspecificacao = (Panel) item.FindControl("pnEspecificacao");
        Anthem.AnthemClientMethods.ShowHide(pnEspecificacao, true);
        Anthem.AnthemClientMethods.ShowHide(btnEspecificacao, false);
    }

    protected void btnSalvarEspecificacao_Click(object sender, EventArgs e)
    {
        if (!_cotacao.PodeSerAlterado)
        {
            ShowMessage("Este PC não pode mais ser alterado.");
            return;
        }

        LinkButton btnSalvarEspecificacao = (LinkButton)sender;
        DataGridItem item = (DataGridItem)btnSalvarEspecificacao.Parent.Parent.Parent;
        Anthem.LinkButton btnEspecificacao = (Anthem.LinkButton) item.FindControl("btnEspecificacao");
        Panel pnEspecificacao = (Panel)item.FindControl("pnEspecificacao");
        TextBox txtEspecificacao = (TextBox) pnEspecificacao.FindControl("txtEspecificacao");
        Anthem.AnthemClientMethods.ShowHide(pnEspecificacao, false);
        Anthem.AnthemClientMethods.ShowHide(btnEspecificacao, true);

        PedidoCotacaoItem pcItem = _cotacao.Itens.Find(Convert.ToInt32(dgItem.DataKeys[item.ItemIndex]));
        pcItem.Especificacao = txtEspecificacao.Text;
        pcItem.Save();

        btnEspecificacao.Text = pcItem.Especificacao;
        btnEspecificacao.UpdateAfterCallBack = true;
        
    }
}
