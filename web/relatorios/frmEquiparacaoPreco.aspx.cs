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

public partial class frmEquiparacaoPreco : SortingPageBase
{

    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);       
		this.RegisterSortingControl(gvPesquisa);
        ucColumn.ColumnsChanged += new EventHandler(ucColumn_ColumnsChanged);
        gvPesquisa.RowDataBound += new GridViewRowEventHandler(gvPesquisa_RowDataBound);
    }

    private decimal totalPO = 0;
    private decimal totalAC = 0;
    private decimal subTotalPO = 0;
    private decimal subTotalAC = 0;
    void gvPesquisa_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if(e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView row = (DataRowView) e.Row.DataItem;
            totalPO += Convert.ToDecimal(row["ValorPO"]);
            totalAC += Convert.ToDecimal(row["ValorAC"]);
            subTotalPO += Convert.ToDecimal(row["SubTotalPO"]);
            subTotalAC += Convert.ToDecimal(row["SubTotalAC"]);

            LinkButton btnDetalhesPO = (LinkButton)e.Row.FindControl("lnkDetalhesPO");
            Anthem.AnthemClientMethods.Popup(btnDetalhesPO, "../pedidoObtencao/fchPedidoObtencaoCompleto.aspx?id_pedido=" + row["ID_PedidoObtencao"], "detalhe_po",
            false, false, false, true, true, true, true, 10, 40, 700, 520, false);

            LinkButton btnDetalhesAC = (LinkButton)e.Row.FindControl("lnkDetalhesAC");
            Anthem.AnthemClientMethods.Popup(btnDetalhesAC, "../pedidoObtencao/fchAutorizacaoCompra.aspx?id_ac=" + row["ID_AutorizacaoCompra"], "detalhe_ac",
            false, false, false, true, true, true, true, 10, 40, 700, 520, false);
        }
        else if(e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblTotalPO = (Label) e.Row.FindControl("lblValorTotalPO");
            Label lblTotalAC = (Label)e.Row.FindControl("lblValorTotalAC");
            Label lblDiferencaTotal = (Label)e.Row.FindControl("lblDiferencaTotal");
            Label lbSublTotalPO = (Label)e.Row.FindControl("lblSubTotalPO");
            Label lblSubTotalAC = (Label)e.Row.FindControl("lblSubTotalAC");
            Label lblDiferencaUnitaria = (Label)e.Row.FindControl("lblDiferencaUnitaria");
            lblTotalPO.Text = totalPO.ToString("N2");
            lblTotalAC.Text = totalAC.ToString("N2");
            lblDiferencaUnitaria.Text = (totalPO - totalAC).ToString("N2");
            lblDiferencaTotal.Text = (subTotalPO - subTotalAC).ToString("N2");
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
	    DataSet ds = AutorizacaoCompra.SelectEquiparacaoPreco(
            Convert.ToInt32(Request["id_celula"]),
            Convert.ToInt32(Request["id_cliente"]),
            IsNull(HttpUtility.UrlDecode(Request["dataInicio"]), DateTime.MinValue),
            IsNull(HttpUtility.UrlDecode(Request["dataFim"]), DateTime.MinValue),
            Convert.ToInt32(Request["id_material"]));


        gvPesquisa.DataSource = this.Sort(ds.Tables[0]);	
        gvPesquisa.DataBind();
		pnGrid.UpdateAfterCallBack = true;
    }
  
}


