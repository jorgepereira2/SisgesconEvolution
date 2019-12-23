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

public partial class frmFamodPorServico : MarinhaPageBase
{
    private DataSet _ds;
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        dlCategoria.ItemDataBound +=new DataListItemEventHandler(dlCategoria_ItemDataBound);
        btnExportar.Click +=new EventHandler(btnExportar_Click);
    }
  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
			Bind();
        }
    }
    #endregion     

    void btnExportar_Click(object sender, EventArgs e)
    {
        App_Code.WebUtil.ExportToExcel(dlCategoria, this, false);
    }
    
	protected void Bind()
    {
        _ds = Famod.SelectFamodPorServico(
             IsNull(Request["id_cliente"], Int32.MinValue),
             Convert.ToInt32(Request["id_categoriaServico"]),
             Convert.ToInt32(Request["id_equipamento"]),
             IsNull(HttpUtility.UrlDecode(Request["dataInicio"]), DateTime.MinValue),
             IsNull(HttpUtility.UrlDecode(Request["dataFim"]), DateTime.MinValue),
             Int32.MinValue);
       
        dlCategoria.DataSource = _ds.Tables[0];
        dlCategoria.DataBind();
    }

    void dlCategoria_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataGrid dgPedidoServico = (DataGrid)e.Item.FindControl("dgPedidoServico");
            DataRowView row = (DataRowView)e.Item.DataItem;

            DataView dvPedidoServico = new DataView(_ds.Tables[1], "id_CategoriaServico=" + row["ID_CategoriaServico"].ToString(), "", DataViewRowState.CurrentRows);
            dgPedidoServico.DataSource = dvPedidoServico;
            dgPedidoServico.DataBind();

            object sum =
                    _ds.Tables[1].Compute("SUM(TotalHorasDelineadas)", "id_CategoriaServico=" + row["ID_CategoriaServico"].ToString());

            int totalHorasDelineadas = 0;
            if (sum != DBNull.Value)
                totalHorasDelineadas = Convert.ToInt32(sum);

            Label lblTotalHorasDelineadas = (Label) e.Item.FindControl("lblTotalHorasDelineadas");
            Label lblDiferenca = (Label)e.Item.FindControl("lblDiferenca");

            lblTotalHorasDelineadas.Text = totalHorasDelineadas.ToString();
            lblDiferenca.Text = (Convert.ToInt32(row["TotalHorasApropriadas"]) - totalHorasDelineadas).ToString();
        }
    }
}


