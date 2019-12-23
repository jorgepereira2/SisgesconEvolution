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


public partial class fchPedidoCotacao : MarinhaPageBase
{
    protected PedidoCotacao _pedido;
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!this.IsPostBack)
		{
            _pedido = PedidoCotacao.Get(Convert.ToInt32(Request["id_pedido"]));
           
            dgItem.DataSource = _pedido.Itens;
			Page.DataBind();
		}
	}
}
