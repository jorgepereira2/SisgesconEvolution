using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Linq;
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

public partial class frmEmpenhoPedidoServico : MarinhaPageBase
{
    private DataSet _ds;
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        dlPS.ItemDataBound += new DataListItemEventHandler(dlPO_ItemDataBound);
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
             0, DateTime.MinValue, DateTime.MinValue, false);

       
        dlPS.DataSource = list.Where(p => p.PedidoObtencao != null);
        dlPS.DataBind();
        
    }

    void dlPO_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataGrid dgEmpenho = (DataGrid)e.Item.FindControl("dgEmpenho");
            DelineamentoOrcamento orcamento = (DelineamentoOrcamento)e.Item.DataItem;

            LinkButton btnDetalhes = (LinkButton)e.Item.FindControl("lnkDetalhes");
            LinkButton btnDetalhesPO = (LinkButton)e.Item.FindControl("lnkDetalhesPO");
            btnDetalhes.Text = orcamento.CodigoComAno;
            btnDetalhesPO.Text = orcamento.PedidoObtencao.CodigoComAno;
            Anthem.AnthemClientMethods.Popup(btnDetalhes, "../servico/fchPedidoServico.aspx?id_pedido=" + orcamento.ID_PedidoServico, "detalhe_orcamento",
            false, false, false, true, true, true, true, 10, 40, 700, 520, false);

            Anthem.AnthemClientMethods.Popup(btnDetalhesPO, "../pedidoObtencao/fchPedidoObtencaoCompleto.aspx?id_pedido=" + orcamento.PedidoObtencao.ID, "detalhe_po",
             false, false, false, true, true, true, true, 10, 40, 700, 520, false);

            dgEmpenho.DataSource = orcamento.PedidoObtencao.Empenhos;
            dgEmpenho.DataBind();
        }
    }

    protected void dgEmpenhoItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataGrid dgPagamento = (DataGrid)e.Item.FindControl("dgPagamento");
            PedidoObtencaoEmpenho empenho = (PedidoObtencaoEmpenho) e.Item.DataItem;

            Literal litBreak = (Literal)e.Item.FindControl("litBreak");

            DataGrid dgEmpenho = (DataGrid) sender;

            litBreak.Text = "</td></tr><tr><td colspan=" + dgEmpenho.Columns.Count + " align=right>" ;
            
            dgPagamento.DataSource = empenho.PedidoObtencao.Pagamentos.Where(p => p.Empenho != null && p.Empenho.ID == empenho.ID);
            dgPagamento.DataBind();
        }
    }
}


