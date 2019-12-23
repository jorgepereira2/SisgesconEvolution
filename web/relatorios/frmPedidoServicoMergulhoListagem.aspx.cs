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

public partial class frmPedidoServicoMergulhoListagem : SortingPageBase
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
        List<PedidoServicoMergulho> list = PedidoServicoMergulho.Select(
            Convert.ToInt32(Request["id_status"]),
            Convert.ToInt32(Request["id_celula"]),
			IsNull(HttpUtility.UrlDecode(Request["dataInicio"]), DateTime.MinValue),
            IsNull(HttpUtility.UrlDecode(Request["dataFim"]), DateTime.MinValue),
            HttpUtility.UrlDecode(Request["numeroRegistro"]),
            HttpUtility.UrlDecode(Request["observacao"]),
            Convert.ToInt32(Request["id_cliente"]),
            Convert.ToInt32(Request["ano"]),
            HttpUtility.UrlDecode(Request["numeroPS"]),
            IsNull(HttpUtility.UrlDecode(Request["dataPrevisaoEntregaInicio"]), DateTime.MinValue),
            IsNull(HttpUtility.UrlDecode(Request["dataPrevisaoEntregaFim"]), DateTime.MinValue),
            IsNull(HttpUtility.UrlDecode(Request["dataRealizadoInicio"]), DateTime.MinValue),
            IsNull(HttpUtility.UrlDecode(Request["dataRealizadoFim"]), DateTime.MinValue),
             HttpUtility.UrlDecode(Request["codigoPedidoCliente"]));
        
        //SortIPedido.Sort(list, this.SortExpression);
	    this.Sort(list);
        gvPesquisa.DataSource = list;		
        gvPesquisa.DataBind();
		pnGrid.UpdateAfterCallBack = true;

	    lblValorTotal.Text = totalValor.ToString("C2");
       // lblQuantidade.Text = totalQuantidade.ToString();
    }

    private decimal totalValor = 0;
   // private int totalQuantidade = 0;
    void gvPesquisa_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            PedidoServicoMergulho psm = (PedidoServicoMergulho)e.Row.DataItem;
            LinkButton btnDetalhes = (LinkButton)e.Row.FindControl("lnkDetalhes");
            Anthem.AnthemClientMethods.Popup(btnDetalhes, "../mergulho/fchPedidoServicoMergulho.aspx?id_pedido=" + psm.ID.ToString(),"detalhe_psm",
            false, false, false, true, true, true, true, 10, 40, 700, 520, false);

            e.Row.Cells[0].Attributes.Add("class", "text");
            totalValor += psm.ValorTotalItens;
            //totalQuantidade += orcamento.Quantidade;

        }
    }
}


