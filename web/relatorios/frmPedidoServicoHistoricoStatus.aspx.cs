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

public partial class frmPedidoServicoHistoricoStatus : MarinhaPageBase
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
        _ds = PedidoServico.SelectHistoricoStatus(
             Convert.ToInt32(Request["Ano"]),
             progem,
             Convert.ToInt32(Request["id_tipoCliente"]),
             Convert.ToInt32(Request["id_cliente"]),
             Convert.ToInt32(Request["id_celula"])
             );

        
	    _ds.Tables[3].Columns.Add("ID_Status", typeof (int));
	    foreach (DataRow row in _ds.Tables[3].Rows)
	    {
            if (row["Status"].ToString() != "Total")
                row["ID_Status"] = Convert.ToInt32(row["Status"].ToString().Substring(0, row["Status"].ToString().IndexOf('_')));
            else
                row["ID_Status"] = 50000; 
	    }
	    for (int i = 1; i <= 12; i++)
	    {
            if (!_ds.Tables[3].Columns.Contains(i.ToString()))
                _ds.Tables[3].Columns.Add(i.ToString(), typeof (int));
	    }

        
       
        dlCelula.DataSource = _ds.Tables[0];
        dlCelula.DataBind();
    }

    void dlCelula_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataList dlProgem = (DataList)e.Item.FindControl("dlProgem");
            DataRowView row = (DataRowView)e.Item.DataItem;

            DataView dvStatus = new DataView(_ds.Tables[1], "ID_Celula = " + row["ID_Celula"],"" , DataViewRowState.CurrentRows);
            dlProgem.DataSource = dvStatus;
            dlProgem.DataBind();
        }
    }

    protected void dlProgem_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataList dlTipoCliente = (DataList)e.Item.FindControl("dlTipoCliente");
            DataRowView row = (DataRowView)e.Item.DataItem;

            DataView dvTipoCliente = new DataView(_ds.Tables[2], string.Format("FlagProgem = {0} and ID_Celula = {1}", row["FlagProgem"], row["ID_Celula"]), "", DataViewRowState.CurrentRows);

            dlTipoCliente.DataSource = dvTipoCliente;
            dlTipoCliente.DataBind();
        }
    }

    protected void dlTipoCliente_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataGrid dgStatus = (DataGrid)e.Item.FindControl("dgStatus");
            DataRowView row = (DataRowView)e.Item.DataItem;

            DataView dvStatus = new DataView(_ds.Tables[3], string.Format("Status like '%@{0}|%' and Status like '%_{1}$%' and Status like '%${2}@%' ", row["ID_Celula"], row["FlagProgem"], row["ID_TipoCliente"]), "ID_Status ASC", DataViewRowState.CurrentRows);

            dgStatus.DataSource = dvStatus;
            dgStatus.DataBind();
        }
    }

    //protected void dgStatus_ItemDataBound(object sender, DataGridItemEventArgs e)
    //{
    //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Header)
    //    {
            
    //        DataRowView row = (DataRowView)e.Item.DataItem;
    //        e.Item.Cells[1].Visible = false;

    //        if (e.Item.ItemType == ListItemType.Header)
    //        {
    //            for (int i = 2; i < e.Item.Cells.Count - 1; i++)
    //            {
    //                e.Item.Cells[i].Text =
    //                    System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat.AbbreviatedMonthNames[
    //                        Convert.ToInt32(e.Item.Cells[i].Text) - 1];
    //            }
    //        }
            
    //    }
    //}
}


