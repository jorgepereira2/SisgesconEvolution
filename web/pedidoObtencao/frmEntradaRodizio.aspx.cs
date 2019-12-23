using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Marinha.Business;
using Shared.Common;
using Shared.NHibernateDAL;
using Shared.SessionState;

public partial class frmEntradaRodizio : MarinhaPageBase
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
            Anthem.AnthemClientMethods.Redirect("frmEntradaRodizioPesquisa.aspx", btnVoltar);
        }
    }
    
    private void Populate()
    {
        lnkPS.Text = _pedido.DelineamentoOrcamento == null ? "" : _pedido.DelineamentoOrcamento.ToString();
        lnkPO.Text = _pedido.CodigoComAno;
        lblDataEmissao.Text = _pedido.DataEmissao.ToShortDateString();

        Anthem.AnthemClientMethods.Popup(lnkPO, "fchPedidoObtencao.aspx?id_pedido=" + _pedido.ID,
                    "po", false, false, false, true, true, true, true, 20, 40, 700, 500, false);

        if (_pedido.DelineamentoOrcamento != null)
            Anthem.AnthemClientMethods.Popup(lnkPS, "../servico/fchPedidoServico.aspx?id_pedido=" + _pedido.DelineamentoOrcamento.PedidoServico.ID,
                    "po", false, false, false, true, true, true, true, 20, 40, 700, 500, false);
    }
    #endregion
   
    #region Bind
    private void Bind()
    {
        dgItem.DataSource = _pedido.GetItensEntradaPEPRodizioPendentes(OrigemMaterial.Rodizio, Convert.ToInt32(ddlOficina.SelectedValue)); 
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
                                                 Convert.ToInt32(OrigemMaterial.Rodizio)).QuantidadeDisponivel.ToString();
        }
    }

    void dgItem_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        int id = Convert.ToInt32(dgItem.DataKeys[e.Item.ItemIndex]);
        _pedido.ItensPEPRodizio.Find(id).Cancelar(ID_Servidor, OrigemMaterial.Rodizio);
        Bind();
        if (dgItem.Items.Count == 0)
            Anthem.AnthemClientMethods.Redirect("frmEntradaRodizioPesquisa.aspx");
    }

	#endregion
    
    void btnEnviar_Click(object sender, EventArgs e)
    {
        Dictionary<int, decimal> itens = new Dictionary<int, decimal>();
        foreach (DataGridItem item in dgItem.Items)
        {
            if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
            {
                Anthem.NumericTextBox txtQuantidade = (Anthem.NumericTextBox)item.FindControl("txtQuantidade");
                if(Convert.ToDecimal(txtQuantidade.Text) > 0)
                {
                    int id = Convert.ToInt32(dgItem.DataKeys[item.ItemIndex]);
                    itens.Add(id, Convert.ToDecimal(txtQuantidade.Text));
                }
            }
        }

        //int id_entrada = _pedido.SalvarEntradaItensPEPRodizio(ID_Servidor, itens, OrigemMaterial.Rodizio);
        
        ShowSuccessMessage();
        btnImprimir.Visible = true;
        btnImprimir.UpdateAfterCallBack = true;
        btnEnviar.Visible = false;
        btnEnviar.UpdateAfterCallBack = true;

        //Anthem.AnthemClientMethods.Popup(btnImprimir, "fchEntradaMaterial.aspx?id_entrada=" + id_entrada.ToString(), false, false, false, true, true, true, true, 20, 50, 700, 500, false);
    }
}
