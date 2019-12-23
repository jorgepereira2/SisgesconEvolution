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

public partial class frmBaixaACListagem : SortingPageBase
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
	   
        List<AutorizacaoCompraPagamento> list = AutorizacaoCompraPagamento.Select(
            HttpUtility.UrlDecode(Request["texto"]),
			IsNull(HttpUtility.UrlDecode(Request["dataInicio"]), DateTime.MinValue),
            IsNull(HttpUtility.UrlDecode(Request["dataFim"]), DateTime.MinValue),
            Convert.ToInt32(Request["ano"]));

	    this.Sort(list);
        gvPesquisa.DataSource = list;		
        gvPesquisa.DataBind();
		pnGrid.UpdateAfterCallBack = true;

	    //lblValorTotal.Text = valorTotal.ToString("C2");
    }

    private decimal valorTotal = 0;
    void gvPesquisa_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            AutorizacaoCompraPagamento pagamento = (AutorizacaoCompraPagamento)e.Row.DataItem;
            LinkButton lnkAC = (LinkButton)e.Row.FindControl("lnkAC");
            Anthem.AnthemClientMethods.Popup(lnkAC, "../pedidoObtencao/fchAutorizacaoCompra.aspx?id_ac=" + pagamento.AutorizacaoCompra.ID.ToString(),"detalhe_ac",
            false, false, false, true, true, true, true, 10, 40, 700, 520, false);

            //valorTotal += pagamento.ValorPago;
        }
    }
}


