using System;
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


public partial class fchPedidoServicoMergulho : MarinhaPageBase
{
    protected PedidoServicoMergulho _pedido;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
       
    }
    
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!this.IsPostBack)
		{
		    if(Request["id_pedido"] != null)
                _pedido = PedidoServicoMergulho.Get(Convert.ToInt32(Request["id_pedido"]));
            
            dgHistorico.DataSource = _pedido.Historico;
		    dgOrcamentoItem.DataSource = _pedido.ItensOrcamento;
            dgDelineamento.DataSource = _pedido.ItensDelineamento;
            
			Page.DataBind();
		}
	}

   
    
    protected void dgDelineamento_ItemCreated(object sender, DataGridItemEventArgs e)
    {
        if(e.Item.ItemType == ListItemType.Footer)
        {
            Label lblTotalHH = (Label) e.Item.FindControl("lblTotalHH");
            lblTotalHH.Text = _pedido.HomemHoraTotal.ToString();
        }
    }
}
