using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


using Marinha.Business;
using Shared.NHibernateDAL;


public partial class fchOrcamento : MarinhaPageBase
{
    protected DelineamentoOrcamento _orcamento;
    protected Parametro _parametro;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
    }
    
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!this.IsPostBack)
		{
		    if (Request["id_orcamento"] != null)
            {
                _orcamento = DelineamentoOrcamento.Get(Convert.ToInt32(Request["id_orcamento"]));
                ucOrcamento.Orcamento = _orcamento;
            }
		    _parametro = Parametro.Get();

		    repServico.DataSource = _orcamento.ItensDelineamento;
			Page.DataBind();
			
			if(Request["flagFaturamento"] != null && Request["flagFaturamento"] == "true")
			    lblTitulo.Text = "Faturamento";
		}
	}
}
