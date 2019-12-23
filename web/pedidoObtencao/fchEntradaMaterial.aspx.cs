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


public partial class fchEntradaMaterial : MarinhaPageBase
{
    protected EntradaMaterial _entrada;
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!this.IsPostBack)
		{
            _entrada = EntradaMaterial.Get(Convert.ToInt32(Request["id_entrada"]));
           
            dgItem.DataSource = _entrada.Itens;
			Page.DataBind();
		}
	}
}
