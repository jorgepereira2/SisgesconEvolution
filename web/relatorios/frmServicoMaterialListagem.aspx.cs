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

public partial class frmServicoMaterialListagem : SortingPageBase
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
        List<ServicoMaterial> list = ServicoMaterial.Select(
            Int32.MinValue,
            Convert.ToInt32(Request["id_classeServicoMaterial"]),
            Convert.ToInt32(Request["id_subClasseServicoMaterial"]),
			HttpUtility.UrlDecode(Request["texto"]),
            Int32.MinValue,
            null,
            null,
            Convert.ToInt32(Request["id_sjb"]),
            Convert.ToInt32(Request["id_naturezaDespesa"]),
            5000, 0, null, new List<int>(), null);
		this.Sort(list);

        gvPesquisa.DataSource = list;		
        gvPesquisa.DataBind();
		pnGrid.UpdateAfterCallBack = true;
    }
}


