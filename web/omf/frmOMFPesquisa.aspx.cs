using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Drawing;
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

public partial class frmOMFPesquisa : SortingPageBase
{

    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.btnPesquisar.Click += new EventHandler(btnPesquisar_Click);
		this.RegisterSortingControl(this.gvPesquisa);
		this.gvPesquisa.RowDataBound += GvPesquisa_OnRowDataBound;
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            Anthem.Manager.Register(this);

            Anthem.AnthemClientMethods.Redirect("frmOMFCadastro.aspx", btnNovo);
			
			Util.FillDropDownList(ddlStatus, StatusOMF.List(), "Todos");
            Util.FillDropDownList(ddlTipoEmprego, TipoEmprego.List(), "Todos");
            Util.FillDropDownList(ddlRecebedor, Servidor.List(null), "Todos");
            
        }
    }
    #endregion  
    
    protected override void Bind()
    {
        List<NotaEntregaMaterialOMF> list = NotaEntregaMaterialOMF.Select(
            txtTexto.Text,
            Convert.ToInt32(ddlRecebedor.SelectedValue),
            Convert.ToInt32(ddlTipoEmprego.SelectedValue),
            Convert.ToInt32(ddlStatus.SelectedValue),
		    IsNull(txtDataInicio.Text, DateTime.MinValue), 
		    IsNull(txtDataFim.Text, DateTime.MinValue)
            );
		this.Sort(list);
		gvPesquisa.DataSource = list;
        gvPesquisa.DataBind();
        gvPesquisa.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();

        gvPesquisa.Visible = list.Count > 0;
        pnMensagem.Visible = list.Count == 0;
        pnMensagem.UpdateAfterCallBack = true;
    }

    private void GvPesquisa_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //PedidoObtencao pedido = (PedidoObtencao)e.Row.DataItem;

            //if (pedido.Status.StatusPedidoObtencaoEnum == StatusPedidoObtencaoEnum.Reprovado)
            //{
            //    e.Row.ForeColor = Color.Red;
            //    e.Row.ToolTip = pedido.UltimoHistorico.Justificativa;
            //    e.Row.Attributes.Add("onmouseover",
            //                         string.Format(
            //                             "Tip('<b>Justificativa:</b><br><br>{0}', SHADOW, true, PADDING, 7, FOLLOWMOUSE, false);",
            //                             pedido.UltimoHistorico.Justificativa));
            //}

            //LinkButton lnkEditar = (LinkButton)e.Row.FindControl("lnkEditar");
            //string address = "frmPedidoObtencaoCadastro.aspx?id_pedido=" + pedido.ID.ToString();
            //if (Request["pm"] != null)
            //    address += "&pm=true";

            //address += "&origemPO=" + Convert.ToInt32(_origemPO).ToString();
            //Anthem.AnthemClientMethods.Redirect(address, lnkEditar);
        }
    }

    void btnPesquisar_Click(object sender, EventArgs e)
    {
        Bind();
    }
}
