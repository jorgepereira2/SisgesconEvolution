using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Marinha.Business;
using Shared.Common;
using Shared.NHibernateDAL;

public partial class fchPedidoObtencaoCompleto : MarinhaPageBase
{
    protected PedidoObtencao _pedido;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        dgItem.ItemDataBound += new DataGridItemEventHandler(dgItem_ItemDataBound);
    }

    void dgItem_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if(e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            PedidoObtencaoItem item = (PedidoObtencaoItem) e.Item.DataItem;
            PedidoCotacaoItemDescartado itemDescartado =
                PedidoCotacaoItemDescartado.GetByPO(item.PedidoObtencao.ID, item.ServicoMaterial.ID);
            
            
            if(itemDescartado != null)
            {
                Label lblDescartado = (Label) e.Item.FindControl("lblDescartado");
                lblDescartado.Text = "(Descartado)";
                e.Item.ToolTip = itemDescartado.Justificativa;
            }

            if(!string.IsNullOrEmpty(item.Especificacao))
            {
                Literal lit = new Literal();
                lit.Text = "<br>Obs.: " + item.Especificacao;
                e.Item.Cells[0].Controls.Add(lit);
            }
        }
    }

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!this.IsPostBack)
		{
            _pedido = PedidoObtencao.Get(Convert.ToInt32(Request["id_pedido"]));


            dgItem.DataSource = _pedido.GetItens(OrigemMaterial.Obtencao);
            dgItemPEP.DataSource = _pedido.GetItens(OrigemMaterial.PEP);
            dgItemRodizio.DataSource = _pedido.GetItens(OrigemMaterial.Rodizio);

		    dgEmpenho.DataSource = _pedido.Empenhos;
            dgPagamento.DataSource = _pedido.Pagamentos;

            ucHistorico.DataSource = _pedido.Historico; //.OrderBy(h => h.Data);

            Page.DataBind();

            pnItem.Visible = dgItem.Items.Count > 0;
            pnItemPEP.Visible = dgItemPEP.Items.Count > 0;
            pnItemRodizio.Visible = dgItemRodizio.Items.Count > 0;
		    pnEmpenho.Visible = _pedido.Empenhos.Count > 0;
            pnPagamentos.Visible = _pedido.Pagamentos.Count > 0;


            if (_pedido.TipoPedido == TipoPedido.PedidoMaterial) { }
                //lblTitulo.Text = "Pedido Material";

            if (_pedido.DelineamentoOrcamento != null)
            {
                lnkPS.Text = "(detalhes)";
                Anthem.AnthemClientMethods.Popup(lnkPS, "../servico/fchPedidoServico.aspx?id_pedido=" + _pedido.DelineamentoOrcamento.ID_PedidoServico.ToString(),
                    "ps", false, false, false, true, true, true, true, 20, 40, 700, 500, false);
            }

            if (_pedido.TipoCompra.TipoCompraEnum == TipoCompraEnum.Licitacao && _pedido.Licitacao != null)
            {
                lnkLicitacao.Text = _pedido.Licitacao.NumeroPregao;
                Anthem.AnthemClientMethods.Popup(lnkLicitacao, "../licitacao/fchLicitacao.aspx?id_licitacao=" + _pedido.Licitacao.ID.ToString(),
                    "licitacao", false, false, false, true, true, true, true, 20, 40, 700, 500, false);
            }
		}
	}

    //protected void repPO_ItemDataBound(object sender, RepeaterItemEventArgs e)
    //{
    //    if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
    //    {
    //        KeyValuePair<int, string> pair = (KeyValuePair<int, string>)e.Item.DataItem;
    //        LinkButton lnkPO = (LinkButton)e.Item.FindControl("lnkPO");
    //        Anthem.AnthemClientMethods.Popup(lnkPO, "fchPedidoObtencao.aspx?id_pedido=" + pair.Key.ToString(), "po", false, false, false, true, true, true, true, 20, 40, 700, 500, false);
    //    }
    //}

    protected void repPS_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            KeyValuePair<int, string> pair = (KeyValuePair<int, string>)e.Item.DataItem;
            LinkButton lnkPS = (LinkButton)e.Item.FindControl("lnkPS");
            Anthem.AnthemClientMethods.Popup(lnkPS, "../servico/fchPedidoServico.aspx?id_pedido=" + pair.Key.ToString(), "ps", false, false, false, true, true, true, true, 20, 40, 700, 500, false);
        }
    }
}
