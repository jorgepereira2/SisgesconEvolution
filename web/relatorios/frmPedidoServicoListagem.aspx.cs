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

public partial class frmPedidoServicoListagem : SortingPageBase
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
        List<DelineamentoOrcamento> list = PedidoServico.Select(
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
            Convert.ToInt32(Request["ano"]),
            HttpUtility.UrlDecode(Request["numeroPS"]),
            IsNull(HttpUtility.UrlDecode(Request["dataPrevisaoEntregaInicio"]), DateTime.MinValue),
            IsNull(HttpUtility.UrlDecode(Request["dataPrevisaoEntregaFim"]), DateTime.MinValue),
            IsNull(HttpUtility.UrlDecode(Request["dataRealizadoInicio"]), DateTime.MinValue),
            IsNull(HttpUtility.UrlDecode(Request["dataRealizadoFim"]), DateTime.MinValue),
             Convert.ToInt32(Request["id_cotador"]),
             Convert.ToInt32(Request["id_oficina"]),
             HttpUtility.UrlDecode(Request["codigoPedidoCliente"]),
            Convert.ToInt32(Request["id_statusHistorico"]), 
             IsNull(HttpUtility.UrlDecode(Request["dataHistoricoInicio"]), DateTime.MinValue),
            IsNull(HttpUtility.UrlDecode(Request["dataHistoricoFim"]), DateTime.MinValue),
            Convert.ToBoolean(Request["recusado"]) 
             );

        //SortIPedido.Sort(list, this.SortExpression);
	    this.Sort(list);
        gvPesquisa.DataSource = list;		
        gvPesquisa.DataBind();
		pnGrid.UpdateAfterCallBack = true;

	    lblValorTotal.Text = totalValor.ToString("C2");
        lblQuantidade.Text = list.Count.ToString("N0");
    }

    private decimal totalValor = 0;
   // private int totalQuantidade = 0;
    void gvPesquisa_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DelineamentoOrcamento orcamento = (DelineamentoOrcamento)e.Row.DataItem;
            LinkButton btnDetalhes = (LinkButton)e.Row.FindControl("lnkDetalhes");
            Anthem.AnthemClientMethods.Popup(btnDetalhes, "../servico/fchPedidoServico.aspx?id_pedido=" + orcamento.PedidoServico.ID.ToString(),"detalhe_ps",
            false, false, false, true, true, true, true, 10, 40, 700, 520, false);

            e.Row.Cells[0].Attributes.Add("class", "text");
            totalValor += orcamento.ValorTotalOrcamento;
            //totalQuantidade += orcamento.Quantidade;

            if(orcamento.PedidoObtencao != null)
            {
                LinkButton lnkPO = (LinkButton)e.Row.FindControl("lnkPO");
                Anthem.AnthemClientMethods.Popup(lnkPO, "../pedidoObtencao/fchPedidoObtencaoCompleto.aspx?id_pedido=" + orcamento.PedidoObtencao.ID, "detalhe_po",
                     false, false, false, true, true, true, true, 10, 40, 700, 520, false);

                lnkPO.Text = string.Format("{0} ({1})", orcamento.PedidoObtencao.CodigoComAno, orcamento.PedidoObtencao.DescricaoStatus);
            }

        }
    }
}


