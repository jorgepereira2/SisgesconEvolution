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

public partial class frmOMFListagem : SortingPageBase
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
        List<NotaEntregaMaterialOMF> list = NotaEntregaMaterialOMF.Select(
            Convert.ToInt32(Request["id_status"]),
            Convert.ToInt32(Request["id_tipoEmprego"]),
            Convert.ToInt32(Request["id_fornecedor"]),
            Convert.ToInt32(Request["id_recebedor"]),
            HttpUtility.UrlDecode(Request["NumeroNota"]),
            HttpUtility.UrlDecode(Request["NumeroEmpenho"]),
			IsNull(HttpUtility.UrlDecode(Request["dataInicio"]), DateTime.MinValue),
            IsNull(HttpUtility.UrlDecode(Request["dataFim"]), DateTime.MinValue)
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
            NotaEntregaMaterialOMF omf = (NotaEntregaMaterialOMF)e.Row.DataItem;
            LinkButton btnDetalhes = (LinkButton)e.Row.FindControl("lnkDetalhes");
            Anthem.AnthemClientMethods.Popup(btnDetalhes, "../omf/fchOMF.aspx?id_omf=" + omf.ID.ToString(),"detalhe_omf",
            false, false, false, true, true, true, true, 10, 40, 700, 520, false);
        }
    }
}


