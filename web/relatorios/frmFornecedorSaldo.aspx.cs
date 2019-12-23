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

public partial class frmFornecedorSaldo : SortingPageBase
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
		DataTable dt = Fornecedor.SelectSaldo(
            Convert.ToInt32(Request["ID_TipoCompra"]),
            Convert.ToInt32(Request["Ano"]),
			HttpUtility.UrlDecode(Request["texto"]),
            Convert.ToInt32(Request["id_tipoFornecedor"]),
            HttpUtility.UrlDecode(Request["materialFornecido"])
            );
        
        gvPesquisa.DataSource = this.Sort(dt);	
        gvPesquisa.DataBind();
		pnGrid.UpdateAfterCallBack = true;

	    
    }

  
}


