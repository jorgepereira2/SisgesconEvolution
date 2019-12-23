using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using Marinha.Business;
using Shared.Common;
using Shared.NHibernateDAL;
using Shared.SessionState;

public partial class frmEntradaPEP : MarinhaPageBase
{
    #region private variables

    [TransientPageState] protected PedidoObtencao _pedido;
    
    #endregion
    
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.dgItem.ItemDataBound += dgItem_OnItemDataBound;
        this.btnEnviar.Click += new EventHandler(btnEnviar_Click);
        this.btnFiltrar.Click += delegate { Bind(); };
        dgItem.CancelCommand +=new DataGridCommandEventHandler(dgItem_CancelCommand);
        ucMessageBox.MessageBoxClose += new UserControls_MessageBox.MessageBoxEventHandler(ucMessageBox_MessageBoxClose);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Anthem.Manager.Register(this);
        if (!this.IsPostBack)
        {

            if (Request["id_pedido"] != null)
            {
                _pedido = PedidoObtencao.Get(Convert.ToInt32(Request["id_pedido"]));
            }
            Util.FillDropDownList(ddlOficina, Celula.List(null, true), "Todas");
            Bind();
            Populate();
            Anthem.AnthemClientMethods.Redirect("frmEntradaPEPPesquisa.aspx", btnVoltar);
        }
    }
    
    private void Populate()
    {
        lnkPS.Text = _pedido.DelineamentoOrcamento == null ? "" : _pedido.DelineamentoOrcamento.ToString();
        lnkPO.Text = _pedido.CodigoComAno;
        lblDataEmissao.Text = _pedido.DataEmissao.ToShortDateString();

        Anthem.AnthemClientMethods.Popup(lnkPO, "fchPedidoObtencao.aspx?id_pedido=" + _pedido.ID,
                    "po", false, false, false, true, true, true, true, 20, 40, 700, 500, false);

        if(_pedido.DelineamentoOrcamento != null)
            Anthem.AnthemClientMethods.Popup(lnkPS, "../servico/fchPedidoServico.aspx?id_pedido=" + _pedido.DelineamentoOrcamento.PedidoServico.ID,
                    "po", false, false, false, true, true, true, true, 20, 40, 700, 500, false);
    }
    #endregion
   
    #region Bind
    private void Bind()
    {
        dgItem.DataSource = _pedido.GetItensEntradaPEPRodizioPendentes(OrigemMaterial.PEP, Convert.ToInt32(ddlOficina.SelectedValue)); 
        dgItem.DataKeyField = "ID";
        dgItem.DataBind();
        dgItem.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();
    }
    #endregion

    #region Orcamento

    private void dgItem_OnItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Anthem.NumericTextBox txtQuantidade = (Anthem.NumericTextBox) e.Item.FindControl("txtQuantidade");
            PedidoObtencaoItemPEPRodizio item = (PedidoObtencaoItemPEPRodizio)e.Item.DataItem;

            txtQuantidade.Text = (item.Quantidade - item.QuantidadeEntregue).ToString();

            Anthem.Label lblQuantidadeEstoque = (Anthem.Label)e.Item.FindControl("lblQuantidadeEstoque");
            lblQuantidadeEstoque.Text = MovimentoEstoque.GetQuantidadeEstoque(item.ServicoMaterial.ID,
                                                 Convert.ToInt32(OrigemMaterial.PEP)).QuantidadeDisponivel.ToString();
        }
    }

    void dgItem_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        int id = Convert.ToInt32(dgItem.DataKeys[e.Item.ItemIndex]);
        _pedido.ItensPEPRodizio.Find(id).Cancelar(ID_Servidor, OrigemMaterial.PEP);
        Bind();
        if(dgItem.Items.Count == 0)
            Anthem.AnthemClientMethods.Redirect("frmEntradaPEPPesquisa.aspx");
    }

	#endregion
    
    void btnEnviar_Click(object sender, EventArgs e)
    {
        if(PrecisaConfirmarSaidaEstoque())
        {
            ucMessageBox.Show("A quantidade digitada para um ou mais itens é maior que a quantidade disponível no estoque. <br>Clique em SIM para efetuar a saída apenas da contidade disponível ou clique em NÃO para cancelar.", null);       
        }
        else 
            Enviar(false);
        
    }

    [Anthem.Method]
    public void ucMessageBox_MessageBoxClose(object sender, MessageBoxEventArgs e)
    {
        if(e.Result == MessageBoxResult.Sim)
            Enviar(false);
        else
            Enviar(true);

    }

    void Enviar(bool cancelarItensAlemEstoque)
    {   
       
        Dictionary<int, decimal> itens = new Dictionary<int, decimal>();
        foreach (DataGridItem item in dgItem.Items)
        {
            if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
            {
                Anthem.NumericTextBox txtQuantidade = (Anthem.NumericTextBox)item.FindControl("txtQuantidade");
                Anthem.Label lblQuantidadeEstoque = (Anthem.Label)item.FindControl("lblQuantidadeEstoque");
                decimal quantidadeEstoque = Convert.ToDecimal(lblQuantidadeEstoque.Text);
                decimal quantidadeSaida = Convert.ToDecimal(txtQuantidade.Text);
                if (quantidadeSaida > 0)
                {
                    int id = Convert.ToInt32(dgItem.DataKeys[item.ItemIndex]);
                    if (!Parametro.Get().FlagPermiteEntregaAbaixoEstoque && quantidadeSaida > quantidadeEstoque && !cancelarItensAlemEstoque)
                    {
                        if(quantidadeEstoque > 0)
                            itens.Add(id, quantidadeEstoque);
                    }
                    else if(!Parametro.Get().FlagPermiteEntregaAbaixoEstoque && quantidadeSaida > quantidadeEstoque && cancelarItensAlemEstoque)
                    {
                        //do nothing
                    }
                    else
                    {
                        itens.Add(id, quantidadeSaida);
                    }
                }
            }
        }

        //int id_entrada = _pedido.SalvarEntradaItensPEPRodizio(ID_Servidor, itens, OrigemMaterial.PEP);

        ShowSuccessMessage();
        btnImprimir.Visible = true;
        btnImprimir.UpdateAfterCallBack = true;
        btnEnviar.Visible = false;
        btnEnviar.UpdateAfterCallBack = true;
        Bind();

        //Anthem.AnthemClientMethods.Popup(btnImprimir, "fchEntradaMaterial.aspx?id_entrada=" + id_entrada.ToString(), false, false, false, true, true, true, true, 20, 50, 700, 500, false);
    }

    bool PrecisaConfirmarSaidaEstoque()
    {
        if(Parametro.Get().FlagPermiteEntregaAbaixoEstoque) return false;

        StringBuilder str = new StringBuilder();
        foreach (DataGridItem item in dgItem.Items)
        {
            if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
            {
                Anthem.NumericTextBox txtQuantidade = (Anthem.NumericTextBox)item.FindControl("txtQuantidade");
                if (Convert.ToDecimal(txtQuantidade.Text) > 0)
                {
                    Label lblQuantidadeEstoque = (Label)item.FindControl("lblQuantidadeEstoque");
                    if(Convert.ToDecimal(txtQuantidade.Text) > Convert.ToDecimal(lblQuantidadeEstoque.Text))
                        return true;
                }
            }
        }
        return false;

    }
}
