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


public partial class fchPedidoServicoAtividadeSecundaria : MarinhaPageBase
{
    protected PedidoServicoAtividadeSecundaria _pedido;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
       
    }
    
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!this.IsPostBack)
		{
		    if(Request["id_pedido"] != null)
                _pedido = PedidoServicoAtividadeSecundaria.Get(Convert.ToInt32(Request["id_pedido"]));
            
            dgHistorico.DataSource = _pedido.Historico;
		    dgItem.DataSource = _pedido.Itens;
            
			Page.DataBind();
		}
	}

   
}
