using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


using Marinha.Business;
using Shared.NHibernateDAL;


public partial class fchFaturamento : MarinhaPageBase
{
    protected DelineamentoOrcamentoFaturamento _faturamento;
    protected Parametro _parametro;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
    }
    
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!this.IsPostBack)
		{
	        _faturamento = DelineamentoOrcamentoFaturamento.Get(Convert.ToInt32(Request["id_faturamento"]));
            ucOrcamento.Orcamento = _faturamento.DelineamentoOrcamento;
                     
            
		    _parametro = Parametro.Get();

		    repServico.DataSource = _faturamento.DelineamentoOrcamento.ItensDelineamento;
			Page.DataBind();

		    lnkExportar.HRef = "fchFaturamento.aspx?id_faturamento=" + Request["id_faturamento"] + "&word=true";
		}

        if(Request["word"] != null)
        {
            divExportar.Visible = false;

            Response.Clear(); //this clears the Response of any headers or previous output
            Response.Buffer = true; //make sure that the entire output is rendered simultaneously

            Response.ContentEncoding = System.Text.Encoding.UTF7;
            ///
            ///Set content type to MS Excel sheet
            ///Use "application/msword" for MS Word doc files
            ///"application/pdf" for PDF files
            ///

            Response.ContentType = "application/msword";
            StringWriter stringWriter = new StringWriter(); //System.IO namespace should be used

            HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);

            ///
            ///Render the entire Page control in the HtmlTextWriter object
            ///We can render individual controls also, like a DataGrid to be
            ///exported in custom format (excel, word etc)
            ///
            this.RenderControl(htmlTextWriter);
            Response.Write(stringWriter.ToString());
            Response.End();
        }
	}
}
