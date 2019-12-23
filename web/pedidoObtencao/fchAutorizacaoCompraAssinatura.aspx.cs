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


public partial class fchAutorizacaoCompraAssinatura : MarinhaPageBase
{
    protected AutorizacaoCompra _ac;
    protected Parametro _parametro;
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!this.IsPostBack)
		{
		    _parametro = Parametro.Get();
            _ac = AutorizacaoCompra.Get(Convert.ToInt32(Request["id_ac"]));
           
            dgItem.DataSource = _ac.Itens;
		    Page.DataBind();

		    img.Visible = _ac.Status.StatusAutorizacaoCompraEnum >
		                  StatusAutorizacaoCompraEnum.AguardandoAprovacaoComandanteGeral && _ac.Status.StatusAutorizacaoCompraEnum != StatusAutorizacaoCompraEnum.Cancelado
                          && _ac.Status.StatusAutorizacaoCompraEnum != StatusAutorizacaoCompraEnum.AguardandoNotaEmpenho;
	
	        
		}
	}
}
