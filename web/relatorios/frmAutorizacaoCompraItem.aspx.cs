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

public partial class frmAutorizacaoCompraItem : SortingPageBase
{

    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);       
		this.RegisterSortingControl(gvPesquisa);
        ucColumn.ColumnsChanged += new EventHandler(ucColumn_ColumnsChanged);
        gvPesquisa.RowDataBound += new GridViewRowEventHandler(gvPesquisa_RowDataBound);
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
        List<PedidoCotacaoItem> list = AutorizacaoCompra.SelectItens(
            Convert.ToInt32(Request["id_classeServicoMaterial"]),
            Convert.ToInt32(Request["id_subClasseServicoMaterial"]),
            Convert.ToInt32(Request["id_servicoMaterial"]),
            Convert.ToInt32(Request["id_fornecedor"]),
            Convert.ToInt32(Request["id_sjb"]),
            Convert.ToInt32(Request["id_fabricante"]),
            HttpUtility.UrlDecode(Request["texto"]),
            HttpUtility.UrlDecode(Request["numeroPO"])
            );

	    this.Sort(list);
        gvPesquisa.DataSource = list;		
        gvPesquisa.DataBind();
		pnGrid.UpdateAfterCallBack = true;
    }

    void gvPesquisa_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //PedidoCotacaoItem item = (PedidoCotacaoItem)e.Row.DataItem;
            //LinkButton btnDetalhes = (LinkButton)e.Row.FindControl("lnkDetalhes");
            //Anthem.AnthemClientMethods.Popup(btnDetalhes, "../pedidoObtencao/fchAutorizacaoCompra.aspx?id_ac=" + ac.ID.ToString(),"detalhe_ac",
            //false, false, false, true, true, true, true, 10, 40, 700, 520, false);
        }
    }
}


