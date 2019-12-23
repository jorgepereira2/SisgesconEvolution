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

public partial class frmPedidoServicoStatusPorCelula : MarinhaPageBase
{
    private DataSet _ds;
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        dlCelula.ItemDataBound += new DataListItemEventHandler(dlCelula_ItemDataBound);
    }
  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
			Bind();
            
        }
    }
    #endregion     

    
	protected void Bind()
    {
	    bool? progem = null;
        if (Request["progem"] != "0")
            progem = Convert.ToBoolean(Request["progem"]);
        _ds = PedidoServico.SelectStatusPorCelula(
             Convert.ToInt32(Request["Ano"]),
             progem
             );
       
        dlCelula.DataSource = _ds.Tables[0];
        dlCelula.DataBind();
    }

    void dlCelula_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataGrid dgStatus = (DataGrid)e.Item.FindControl("dgStatus");
            DataRowView row = (DataRowView)e.Item.DataItem;

            DataView dvStatus = new DataView(_ds.Tables[1], "Status like '" + row["ID_Celula"].ToString() + "|%'","" , DataViewRowState.CurrentRows);
            dgStatus.DataSource = dvStatus;
            dgStatus.DataBind();
        }
    }

    protected void dgStatus_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Header)
        {
            
            DataRowView row = (DataRowView)e.Item.DataItem;
            e.Item.Cells[1].Visible = false;

            if (e.Item.ItemType == ListItemType.Header)
            {
                for (int i = 2; i < e.Item.Cells.Count - 1; i++)
                {
                    e.Item.Cells[i].Text =
                        System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat.AbbreviatedMonthNames[
                            Convert.ToInt32(e.Item.Cells[i].Text) - 1];
                }
            }
            
        }
    }
}


