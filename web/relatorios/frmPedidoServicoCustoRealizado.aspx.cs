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

public partial class frmPedidoServicoCustoRealizado : SortingPageBase
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
	    bool? progem = null;
        if(Request["progem"] != "0")
            progem = Convert.ToBoolean(Request["progem"]);
        DataSet ds = PedidoServico.SelectCustoRealizado(
            Convert.ToInt32(Request["id_gerente"]),
            Convert.ToInt32(Request["id_equipamento"]),
            Convert.ToInt32(Request["id_status"]),
            Convert.ToInt32(Request["id_celula"]),
            progem,
			IsNull(HttpUtility.UrlDecode(Request["dataInicio"]), DateTime.MinValue),
            IsNull(HttpUtility.UrlDecode(Request["dataFim"]), DateTime.MinValue),
            HttpUtility.UrlDecode(Request["numeroRegistro"]),
            HttpUtility.UrlDecode(Request["observacao"]),
            Convert.ToInt32(Request["id_cliente"]),
            HttpUtility.UrlDecode(Request["numeroPS"]),
            Convert.ToInt32(Request["mes"]),
            Convert.ToInt32(Request["ano"])
            );

        //SortIPedido.Sort(list, this.SortExpression);
	    this.Sort(ds.Tables[0]);
        gvPesquisa.DataSource = ds.Tables[0];		
        gvPesquisa.DataBind();
		pnGrid.UpdateAfterCallBack = true;
    }

    void gvPesquisa_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView row = (DataRowView)e.Row.DataItem;
            LinkButton btnDetalhes = (LinkButton)e.Row.FindControl("lnkDetalhes");
            Anthem.AnthemClientMethods.Popup(btnDetalhes, "../servico/fchPedidoServico.aspx?id_pedido=" + row["ID_PedidoServico"],"detalhe_ps",
            false, false, false, true, true, true, true, 10, 40, 700, 520, false);
        }
    }
}


