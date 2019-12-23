using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Linq;
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

public partial class frmServidorPermissao : SortingPageBase
{

    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);       
		//this.RegisterSortingControl(gvPesquisa);
       // ucColumn.ColumnsChanged += new EventHandler(ucColumn_ColumnsChanged);
        dlProcesso.ItemDataBound += new DataListItemEventHandler(dlProcesso_ItemDataBound);
    }

   

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
			Bind();
			//ucColumn.SetValues();
        }
    }
    #endregion

    
	protected override void Bind()
	{
        List<Processo> list = Processo.SelectPaginas();

        if (Request["nome"] != "")
            list = list.Where(p => p.TextoCaminho.Contains(Request["nome"])).ToList();

        dlProcesso.DataSource = list.OrderBy(m => m.TextoCaminho);
        dlProcesso.DataBind();
		pnGrid.UpdateAfterCallBack = true;
    }

    void dlProcesso_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            GridView gv = (GridView)e.Item.FindControl("gvPesquisa");
            Processo processo = (Processo)e.Item.DataItem;

            
            gv.DataSource = processo.Servidores.OrderBy(s => s.NomeCompleto);
            gv.DataBind();
            
        }
    }
}


