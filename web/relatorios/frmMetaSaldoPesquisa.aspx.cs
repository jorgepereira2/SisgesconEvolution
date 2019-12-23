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

public partial class frmMetaSaldoPesquisa : MarinhaPageBase
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
            Util.FillDropDownList(ddlPTRES, PTRES.List(), "Todos");
            Util.FillDropDownList(ddlUGE, UGE.List(), "Todas");
            Util.FillDropDownList(ddlFase, Fase.List(), "Todas");
            Util.FillDropDownList(ddlNaturezaDespesa, NaturezaDespesa.List(), "Todas");
            Util.FillDropDownList(ddlProjeto, Projeto.List(), "Todos");
            Util.FillDropDownList(ddlAno, DateTimeManager.Anos(2009, DateTime.Today.Year + 1));
            ddlAno.SelectedValue = DateTime.Today.Year.ToString();
            //ddlStatus.Items.Insert(1, new ListItem("Todos (Exceto Cancelados)", Int32.MinValue.ToString()));
        }
    }
    #endregion  
    
    void btnImprimir_Click(object sender, EventArgs e)
    {
        Dictionary<string, string> list = new Dictionary<string, string>();
        list.Add("id_ptres", ddlPTRES.SelectedValue);
        list.Add("id_uge", ddlUGE.SelectedValue);
        list.Add("id_fase", ddlFase.SelectedValue);
        list.Add("id_naturezaDespesa", ddlNaturezaDespesa.SelectedValue);
        list.Add("id_projeto", ddlProjeto.SelectedValue);
        list.Add("ano", ddlAno.SelectedValue);
        //list.Add("dataInicio", HttpUtility.UrlEncode(txtDataInicio.Text));
        //list.Add("dataFim", HttpUtility.UrlEncode(txtDataInicio.Text));
        
        string address = "frmMetaSaldo.aspx?" + Util.GetUrlParameterString(list);

        Anthem.AnthemClientMethods.Popup(this, address, false, false, false, true, true, true, true,
            60, 60, 700, 500);
    }

 
}
