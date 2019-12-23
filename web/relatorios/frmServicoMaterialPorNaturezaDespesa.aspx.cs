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

public partial class frmServicoMaterialPorNaturezaDespesa : SortingPageBase
{

    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);       
		//this.RegisterSortingControl(gvPesquisa);
       // ucColumn.ColumnsChanged += new EventHandler(ucColumn_ColumnsChanged);
        dlNatureza.ItemDataBound += new DataListItemEventHandler(dlNatureza_ItemDataBound);
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
			//ucColumn.SetValues();
        }
    }
    #endregion

    private List<ServicoMaterial> list = null;
	protected override void Bind()
    {
        list = ServicoMaterial.Select(
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

	    List<NaturezaDespesa> naturezas = (from item in list group item by item.NaturezaDespesa into g select g.Key).ToList();

        dlNatureza.DataSource = naturezas.OrderBy(n => n == null ? "" : n.Descricao);		
        dlNatureza.DataBind();
		pnGrid.UpdateAfterCallBack = true;
    }

    void dlNatureza_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            GridView gv = (GridView)e.Item.FindControl("gvPesquisa");
            NaturezaDespesa natureza = (NaturezaDespesa) e.Item.DataItem;

            
            gv.DataSource = list.Where(i => (i.NaturezaDespesa != null && natureza != null && i.NaturezaDespesa.ID == natureza.ID) || (i.NaturezaDespesa == null && natureza == null));
            gv.DataBind();
            
        }
    }
}


