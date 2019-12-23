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

public partial class frmFaturamentoListagem : SortingPageBase
{

    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);       
		this.RegisterSortingControl(gvPesquisa);
        ucColumn.ColumnsChanged += new EventHandler(ucColumn_ColumnsChanged);
        gvPesquisa.RowDataBound += new GridViewRowEventHandler(gvPesquisa_RowDataBound);
    }

    private decimal total = 0;
    void gvPesquisa_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if(e.Row.RowType == DataControlRowType.DataRow)
        {
            DelineamentoOrcamentoFaturamento faturamento = (DelineamentoOrcamentoFaturamento) e.Row.DataItem;
            total += faturamento.Valor;
        }
        else if(e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblTotal = (Label) e.Row.FindControl("lblValorTotal");
            lblTotal.Text = total.ToString("N2");
        }
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
	    List<DelineamentoOrcamentoFaturamento> list = DelineamentoOrcamentoFaturamento.Select(
			HttpUtility.UrlDecode(Request["texto"]),
            Convert.ToInt32(Request["id_status"]),
            IsNull(HttpUtility.UrlDecode(Request["dataInicio"]), DateTime.MinValue),
            IsNull(HttpUtility.UrlDecode(Request["dataFim"]), DateTime.MinValue),
            Convert.ToInt32(Request["id_celula"]));
		this.Sort(list);

        gvPesquisa.DataSource = list;		
        gvPesquisa.DataBind();
		pnGrid.UpdateAfterCallBack = true;
    }
  
}


