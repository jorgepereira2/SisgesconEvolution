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


public partial class fchPedidoObtencaoAssinatura : MarinhaPageBase
{
    protected PedidoObtencao _po;
    protected Parametro _parametro;
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!this.IsPostBack)
		{
		    _parametro = Parametro.Get();
            _po = PedidoObtencao.Get(Convert.ToInt32(Request["id_pedido"]));
           
            dgItem.DataSource = _po.Itens;
		    dgEmpenho.DataSource = _po.Empenhos;

		    Page.DataBind();

		    img.Visible =  _po.Status.StatusPedidoObtencaoEnum >=StatusPedidoObtencaoEnum.AguardandoCreditoEmpenho 
                && _po.Status.StatusPedidoObtencaoEnum != StatusPedidoObtencaoEnum.Cancelado && _po.Status.StatusPedidoObtencaoEnum != StatusPedidoObtencaoEnum.AguardandoCreditoEmpenho;		        
		}
	}
}