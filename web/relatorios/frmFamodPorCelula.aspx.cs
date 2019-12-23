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

public partial class frmFamodPorCelula : MarinhaPageBase
{
    private DataSet _ds;
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        dlDivisao.ItemDataBound += new DataListItemEventHandler(dlDivisao_ItemDataBound);
        btnExportar.Click +=new EventHandler(btnExportar_Click);
    }

    void btnExportar_Click(object sender, EventArgs e)
    {
        App_Code.WebUtil.ExportToExcel(dlDivisao, this, false);
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
        _ds = Famod.SelectFamodPorCelula(
             Convert.ToInt32(Request["ID_Servidor"]),
             Convert.ToInt32(Request["ID_Atividade"]),
             Convert.ToInt32(Request["ID_Apropriacao"]),
             Convert.ToInt32(Request["ID_Divisao"]),
             IsNull(HttpUtility.UrlDecode(Request["dataInicio"]), DateTime.MinValue),
             IsNull(HttpUtility.UrlDecode(Request["dataFim"]), DateTime.MinValue)
             );
       
        dlDivisao.DataSource = _ds.Tables[0];
        dlDivisao.DataBind();
    }

    void dlDivisao_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataGrid dgSecao = (DataGrid)e.Item.FindControl("dgSecao");
            DataRowView row = (DataRowView)e.Item.DataItem;

            DataView dvSecao = new DataView(_ds.Tables[1], "id_divisao=" + row["ID_Divisao"].ToString(), "", DataViewRowState.CurrentRows);
            dgSecao.DataSource = dvSecao;
            dgSecao.DataBind();

            Label lblHorasDisponiveis = (Label) e.Item.FindControl("lblHorasDisponiveis");
            Label lblDiferenca = (Label)e.Item.FindControl("lblDiferenca");

            int horasDisponiveis =
                Convert.ToInt32(
                    _ds.Tables[1].Compute("SUM(HOrasDisponiveis)", "id_divisao=" + row["ID_Divisao"].ToString()));
            int diferenca = horasDisponiveis - Convert.ToInt32(row["HorasApropriadas"]);
            lblHorasDisponiveis.Text = horasDisponiveis.ToString();
            lblDiferenca.Text = diferenca.ToString();
        }
    }
}


