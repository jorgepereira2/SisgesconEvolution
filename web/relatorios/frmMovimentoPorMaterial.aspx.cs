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

public partial class frmMovimentoPorMaterial : MarinhaPageBase
{
    private DataSet _ds;
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        dlMaterial.ItemDataBound += new DataListItemEventHandler(dlMaterial_ItemDataBound);
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
        _ds = MovimentoEstoque.SelectMovimentoPorMaterial(
             IsNull(Request["id_material"], Int32.MinValue),
             Convert.ToInt32(Request["id_origemMaterial"]),
             IsNull(HttpUtility.UrlDecode(Request["dataInicio"]), DateTime.MinValue),
             IsNull(HttpUtility.UrlDecode(Request["dataFim"]), DateTime.MinValue)
             );
       
        dlMaterial.DataSource = _ds.Tables[0];
        dlMaterial.DataBind();
    }

    void dlMaterial_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataGrid dgMovimento = (DataGrid)e.Item.FindControl("dgMovimento");
            DataRowView row = (DataRowView)e.Item.DataItem;

            DataView dvMaterial = new DataView(_ds.Tables[1], "id_material=" + row["ID_ServicoMaterial"].ToString(), "", DataViewRowState.CurrentRows);
            dgMovimento.DataSource = dvMaterial;
            dgMovimento.DataBind();
        }
    }

    protected void dgMovimento_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            LinkButton lnkCodigo = (LinkButton)e.Item.FindControl("lnkCodigo");
            DataRowView row = (DataRowView)e.Item.DataItem;

            string address = "";
            if(row["Codigo"].ToString().StartsWith("PS"))
                address = "../servico/fchPedidoServico.aspx?id_pedido=" + row["id_pedidoServico"].ToString();
            else if (row["Codigo"].ToString().StartsWith("PO") || row["Codigo"].ToString().StartsWith("PM"))
                address = "../pedidoObtencao/fchPedidoObtencao.aspx?id_pedido=" + row["id_pedidoObtencao"].ToString();
            else if (row["Codigo"].ToString().StartsWith("RE"))
                address = "../estoque/fchRequisicaoEstoque.aspx?id_requisicao=" + row["id_requisicaoEstoque"].ToString();
            else if (row["Codigo"].ToString().StartsWith("AC"))
                address = "../pedidoObtencao/fchAutorizacaoCompra.aspx?id_ac=" + row["id_autorizacaocompra"].ToString();
                
            Anthem.AnthemClientMethods.Popup(lnkCodigo, address, "novajanela", false, false, false, true, true, true, true, 20, 40, 720, 500, false);
        }
    }
}


