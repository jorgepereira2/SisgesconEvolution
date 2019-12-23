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

public partial class frmSaldoAnualPesquisa : MarinhaPageBase
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
            Util.FillDropDownList(ddlAno, DateTimeManager.Anos(2010, DateTime.Today.Year));
        }
    }
    #endregion  
    
    void btnImprimir_Click(object sender, EventArgs e)
    {
        Dictionary<string, string> list = new Dictionary<string, string>();
        list.Add("ano", ddlAno.SelectedValue);
        
        string address = "frmSaldoAnual.aspx?" + Util.GetUrlParameterString(list);

        Anthem.AnthemClientMethods.Popup(this, address, false, false, false, true, true, true, true,
            60, 60, 700, 500);
    }

 
}
