using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Marinha.Business;
using Shared.Common;
using Shared.NHibernateDAL;
using Shared.SessionState;

public partial class frmEntradaObtencao : MarinhaPageBase
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

            Bind();
            Populate();
            Anthem.AnthemClientMethods.Redirect("frmEntradaObtencaoPesquisa.aspx", btnVoltar);
        }
    }
    
    private void Populate()
    {
        if (_pedido.DelineamentoOrcamento != null)
        {
            lblPS.Text = _pedido.DelineamentoOrcamento.ToString();
            Anthem.AnthemClientMethods.Popup(lblPS, "../servico/fchPedidoServico.aspx?id_pedido=" + _pedido.DelineamentoOrcamento.ID_PedidoServico,
            false, false, false, true, true, true, true, 10, 30, 700, 520, false);
        }
        lblPO.Text = _pedido.CodigoComAno;
        lblDataEmissao.Text = _pedido.DataEmissao.ToShortDateString();

        Anthem.AnthemClientMethods.Popup(lblPO, "fchPedidoObtencao.aspx?id_pedido=" + _pedido.ID,
            false, false, false, true, true, true, true, 10, 30, 700, 520, false);
    }
    #endregion
   
    #region Bind
    private void Bind()
    {
		dgItem.DataSource = _pedido.GetItensEntradaPendente(); 
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
            PedidoObtencaoItem item = (PedidoObtencaoItem)e.Item.DataItem;

            txtQuantidade.Text = (item.Quantidade - item.QuantidadeEntregue).ToString();
        }
    }

    void dgItem_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dgItem.ShowFooter = false;
        dgItem.EditItemIndex = -1;
        Bind();
    }

	#endregion
    
    void btnEnviar_Click(object sender, EventArgs e)
    {
        Dictionary<int, decimal > itens = new Dictionary<int, decimal>();
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

        int id_entrada = _pedido.SalvarEntradaItensObtencao(ID_Servidor, itens);
        
        ShowSuccessMessage();
        btnImprimir.Visible = true;
        btnImprimir.UpdateAfterCallBack = true;
        btnEnviar.Visible = false;
        btnEnviar.UpdateAfterCallBack = true;
        
        Anthem.AnthemClientMethods.Popup(btnImprimir, "fchEntradaMaterial.aspx?id_entrada=" + id_entrada.ToString(), false, false, false, true, true, true, true, 20, 50, 700, 500, false);

        
    }
}
