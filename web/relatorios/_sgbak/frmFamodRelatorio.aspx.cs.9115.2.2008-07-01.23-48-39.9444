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

public partial class frmFamodRelatorio : MarinhaPageBase
{
    private DataSet _ds;
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        dlOficina.ItemDataBound += new DataListItemEventHandler(dlOficina_ItemDataBound);
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
	    bool? flagAtividadeDireta = null;
	    if(Request["tipoAtividade"] != "0")
	        flagAtividadeDireta = Boolean.Parse(Request["tipoAtividade"]);
		_ds = Famod.SelectPorOficina(
            IsNull(Request["id_atividade"], Int32.MinValue),
            flagAtividadeDireta,
            Convert.ToInt32(Request["id_oficina"]),
            Convert.ToInt32(Request["id_servidor"]),
			IsNull(HttpUtility.UrlDecode(Request["dataInicio"]), DateTime.MinValue),
            IsNull(HttpUtility.UrlDecode(Request["dataFim"]), DateTime.MinValue),
            Convert.ToInt32(Request["id_situacao"])
            );
       
        dlOficina.DataSource = _ds.Tables[0];
        dlOficina.DataBind();
    }

    void dlOficina_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataList dlServidor = (DataList)e.Item.FindControl("dlServidor");
            DataRowView row = (DataRowView)e.Item.DataItem;

            DataView dvServidor = new DataView(_ds.Tables[1], "id_oficina=" + row["ID_Oficina"].ToString(), "", DataViewRowState.CurrentRows);
            dlServidor.DataSource = dvServidor;
            dlServidor.DataBind();
        }
    }

    protected void dlServidor_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataGrid dgAtividadeDireta = (DataGrid)e.Item.FindControl("dgAtividadeDireta");
            DataRowView row = (DataRowView)e.Item.DataItem;

            DataView dvAtividadeDireta = new DataView(_ds.Tables[2], 
                "id_oficina=" + row["ID_Oficina"].ToString() + " and id_servidor=" + row["id_servidor"] 
                , "", DataViewRowState.CurrentRows);
            dgAtividadeDireta.DataSource = dvAtividadeDireta;
            dgAtividadeDireta.DataBind();

            DataGrid dgAtividadeIndireta = (DataGrid)e.Item.FindControl("dgAtividadeIndireta");
            DataRowView row2 = (DataRowView)e.Item.DataItem;

            DataView dvAtividadeIndireta = new DataView(_ds.Tables[3],
                "id_oficina=" + row2["ID_Oficina"].ToString() + " and id_servidor=" + row2["id_servidor"]
                , "", DataViewRowState.CurrentRows);
            dgAtividadeIndireta.DataSource = dvAtividadeIndireta;
            dgAtividadeIndireta.DataBind();

            Label lblAtividadeDireta = (Label) e.Item.FindControl("lblTotalHorasAtividadeDireta");
            Label lblAtividadeIndireta = (Label)e.Item.FindControl("lblTotalHorasAtividadeIndireta");

            object totalAtividadeDireta = _ds.Tables[2].Compute("SUM(HorasApropriadas)", "id_oficina=" + row["ID_Oficina"].ToString() + " and id_servidor=" + row["id_servidor"]);
            object totalAtividadeIndireta = _ds.Tables[3].Compute("SUM(HorasApropriadas)", "id_oficina=" + row["ID_Oficina"].ToString() + " and id_servidor=" + row["id_servidor"]);

            lblAtividadeDireta.Text = totalAtividadeDireta == DBNull.Value ? "0" : Convert.ToInt32(totalAtividadeDireta).ToString();
            lblAtividadeIndireta.Text = totalAtividadeIndireta == DBNull.Value ? "0" : Convert.ToInt32(totalAtividadeIndireta).ToString();
        }
    }
}


