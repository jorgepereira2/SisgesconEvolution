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

public partial class frmPedidoServicoProntificacaoEquipamento : MarinhaPageBase
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
        _ds = PedidoServico.SelectProntificacaoEquipamento(
             Convert.ToInt32(Request["Ano"]),
             progem,
             Convert.ToInt32(Request["id_tipoCliente"]),
             Convert.ToInt32(Request["id_cliente"]),
             Convert.ToInt32(Request["id_celula"])
             );
       
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
            DataList dlTipoEquipamento = (DataList)e.Item.FindControl("dlTipoEquipamento");
            DataRowView row = (DataRowView)e.Item.DataItem;

            DataView dvTipoEquipamento = new DataView(_ds.Tables[2], string.Format("ID_Celula = {0} AND FlagProgem = {1}", row["ID_Celula"], row["FlagProgem"]), "SubTipoEquipamento ASC", DataViewRowState.CurrentRows);
            dlTipoEquipamento.DataSource = dvTipoEquipamento;
            dlTipoEquipamento.DataBind();
        }
    }

    protected void dlTipoEquipamento_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataGrid dgEquipamento = (DataGrid)e.Item.FindControl("dgEquipamento");
            DataRowView row = (DataRowView)e.Item.DataItem;

            DataView dvEquipamento = new DataView(_ds.Tables[3], string.Format("ID_Celula = {0} AND FlagProgem = {1} AND ID_SubTipoEquipamento = {2}", row["ID_Celula"], row["FlagProgem"], row["ID_SubTipoEquipamento"]), "Equipamento ASC", DataViewRowState.CurrentRows);
            dgEquipamento.DataSource = dvEquipamento;
            dgEquipamento.DataBind();
        }
    }
}


