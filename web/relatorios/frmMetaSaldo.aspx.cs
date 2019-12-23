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

public partial class frmMetaSaldo : SortingPageBase
{

    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);       
		this.RegisterSortingControl(gvPesquisa);
        ucColumn.ColumnsChanged += new EventHandler(ucColumn_ColumnsChanged);
        
    }

    void ucColumn_ColumnsChanged(object sender, EventArgs e)
    {
        Bind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
			Bind();
			ucColumn.SetValues();
        }
    }
    #endregion     

    
	protected override void Bind()
    {
		DataTable dt = Meta.SelectSaldoPorMeta(
            Convert.ToInt32(Request["ID_PTRES"]),
            Convert.ToInt32(Request["ID_UGE"]),
            Convert.ToInt32(Request["ID_Fase"]),
            Convert.ToInt32(Request["ID_Projeto"]),
            Convert.ToInt32(Request["ID_NaturezaDespeza"]),
            Convert.ToInt32(Request["ano"])
            ).Tables[0];
        
        gvPesquisa.DataSource = this.Sort(dt);	
        gvPesquisa.DataBind();
		pnGrid.UpdateAfterCallBack = true;

	    
    }

  
}


