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
using Shared.NHibernateDAL;


public partial class fchAutorizacaoCompra : MarinhaPageBase
{
    protected AutorizacaoCompra _ac;
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!this.IsPostBack)
		{
            _ac = AutorizacaoCompra.Get(Convert.ToInt32(Request["id_ac"]));
           
            dgItem.DataSource = _ac.Itens;
		    dgPagamento.DataSource = _ac.Pagamentos;
			Page.DataBind();

            dlHistorico.DataSource = _ac.Historico;
            dlHistorico.DataKeyField = "ID";
            dlHistorico.DataBind();

		    pnPagamento.Visible = _ac.Pagamentos.Count > 0;

            repPO.DataSource = _ac.POs;
            repPO.DataBind();


            repPS.DataSource = _ac.PSs;
            repPS.DataBind();
		}
	}

    protected void repPO_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            KeyValuePair<int, string> pair = (KeyValuePair<int, string>)e.Item.DataItem;
            LinkButton lnkPO = (LinkButton)e.Item.FindControl("lnkPO");
            Anthem.AnthemClientMethods.Popup(lnkPO, "fchPedidoObtencao.aspx?id_pedido=" + pair.Key.ToString(), "po", false, false, false, true, true, true, true, 20, 40, 700, 500, false);
        }
    }

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
