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


public partial class fchOcorrencia : MarinhaPageBase
{
    protected DelineamentoOrcamentoOcorrencia _ocorrencia;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
    }
    
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!this.IsPostBack)
		{

            _ocorrencia = DelineamentoOrcamentoOcorrencia.Get(Convert.ToInt32(Request["id_ocorrencia"]));
			Page.DataBind();
		}
	}
}
