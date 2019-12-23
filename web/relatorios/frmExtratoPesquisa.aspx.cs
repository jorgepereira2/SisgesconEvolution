using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Marinha.Business;
using Shared.SessionState;
using ComponentArt.Web.UI;
using Shared.Common;

public partial class frmExtratoPesquisa : MarinhaPageBase
{
   
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.btnImprimir.Click += new EventHandler(btnImprimir_Click);
		
    }
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            Util.FillDropDownList(ddlProjeto, Projeto.List(), "Todos");
            Util.FillDropDownList(ddlNaturezaDespesa, NaturezaDespesa.ListPai(), "Todas");
            Util.FillDropDownList(ddlFonteRecurso, FonteRecurso.List(), "Todas");
            Util.FillDropDownList(ddlPTRES, PTRES.List(), "Todos");

            txtDataInicio.Text = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).ToShortDateString();
            txtDataFim.Text = DateTime.Today.ToShortDateString();
        }
    }
    #endregion  
    
    void btnImprimir_Click(object sender, EventArgs e)
    {
        Dictionary<string, string> list = new Dictionary<string, string>();
        list.Add("id_projeto", ddlProjeto.SelectedValue);
        list.Add("id_naturezaDespesa", ddlNaturezaDespesa.SelectedValue);
        list.Add("id_fonteRecurso", ddlFonteRecurso.SelectedValue);
        list.Add("id_ptres", ddlPTRES.SelectedValue);
        list.Add("dataInicio", HttpUtility.UrlEncode(txtDataInicio.Text));
        list.Add("dataFim", HttpUtility.UrlEncode(txtDataFim.Text));

        string address = "frmExtrato.aspx?" + Util.GetUrlParameterString(list);

        Anthem.AnthemClientMethods.Popup(this, address, false, false, false, true, true, true, true,
            60, 60, 700, 500);
    }

 
}
