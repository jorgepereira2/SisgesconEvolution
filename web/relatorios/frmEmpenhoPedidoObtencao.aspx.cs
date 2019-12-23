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

public partial class frmEmpenhoPedidoObtencao : MarinhaPageBase
{
    private DataSet _ds;
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        dlPO.ItemDataBound += new DataListItemEventHandler(dlPO_ItemDataBound);
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
        bool? flagDireto = null;
        if (Request["flagDireto"] != "0")
            flagDireto = Boolean.Parse(Request["flagDireto"]);
        List<PedidoObtencao> list = PedidoObtencao.Select(
            Convert.ToInt32(Request["id_celula"]),
            Convert.ToInt32(Request["id_status"]),
            IsNull(HttpUtility.UrlDecode(Request["dataInicio"]), DateTime.MinValue),
            IsNull(HttpUtility.UrlDecode(Request["dataFim"]), DateTime.MinValue),
            Convert.ToInt32(Request["id_tipoPedido"]),
            flagDireto,
            HttpUtility.UrlDecode(Request["numeroPO"]),
            Convert.ToInt32(Request["ano"]),
            Convert.ToInt32(Request["id_servidor"]),
            0
            );

       
        dlPO.DataSource = list;
        dlPO.DataBind();
        
    }

    void dlPO_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataGrid dgEmpenho = (DataGrid)e.Item.FindControl("dgEmpenho");
            PedidoObtencao po = (PedidoObtencao)e.Item.DataItem;

            LinkButton btnDetalhes = (LinkButton)e.Item.FindControl("lnkDetalhes");
            btnDetalhes.Text = po.CodigoComAno;
            Anthem.AnthemClientMethods.Popup(btnDetalhes, "../pedidoObtencao/fchPedidoObtencaoCompleto.aspx?id_pedido=" + po.ID.ToString(), "detalhe_po",
            false, false, false, true, true, true, true, 10, 40, 700, 520, false);

            dgEmpenho.DataSource = po.Empenhos;
            dgEmpenho.DataBind();
        }
    }
}


