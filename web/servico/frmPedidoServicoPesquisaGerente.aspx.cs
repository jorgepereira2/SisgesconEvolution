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

public partial class frmPedidoServicoPesquisaGerente : SortingPageBase
{
   
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.btnPesquisar.Click += new EventHandler(btnPesquisar_Click);
		this.RegisterSortingControl(this.gvPesquisa);
        gvPesquisa.RowDataBound += new GridViewRowEventHandler(gvPesquisa_RowDataBound);
        gvPesquisa.RowCommand += new GridViewCommandEventHandler(gvPesquisa_RowCommand);
    }

    void gvPesquisa_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if(e.CommandName == "Garantia")
        {
            DelineamentoOrcamento orcamento = DelineamentoOrcamento.Get(Convert.ToInt32(e.CommandArgument));
            orcamento.ColocarEmGarantia(ID_Servidor);
            Bind();
        }
        else if(e.CommandName == "Finalizar")
        {
            DelineamentoOrcamento orcamento = DelineamentoOrcamento.Get(Convert.ToInt32(e.CommandArgument));
            orcamento.TirarDaGarantia(ID_Servidor);
            Bind();
        }
    }

    void gvPesquisa_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if(e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btnDetalhes = (LinkButton) e.Row.FindControl("lnkDetalhes");
            LinkButton btnOrcamento = (LinkButton)e.Row.FindControl("lnkOrcamento");
            LinkButton btnNovoOrcamento = (LinkButton)e.Row.FindControl("lnkNovoOrcamento");
            LinkButton btnEditar = (LinkButton)e.Row.FindControl("lnkEditar");
            LinkButton lnkGarantia = (LinkButton)e.Row.FindControl("lnkGarantia");
            LinkButton lnkFinalizar = (LinkButton)e.Row.FindControl("lnkFinalizar");
            HtmlGenericControl br = (HtmlGenericControl)e.Row.FindControl("br");
            IPedido ps = (IPedido)e.Row.DataItem;
            Anthem.AnthemClientMethods.Popup(btnDetalhes, "fchPedidoServico.aspx?id_pedido=" + ps.ID_PedidoServico.ToString(),
            false, false, false, true, true, true, true, 10, 40, 700, 520, false);

            lnkGarantia.Visible = false;
            lnkFinalizar.Visible = false;
            br.Visible = false;
            if (ps is DelineamentoOrcamento)
            {
                Anthem.AnthemClientMethods.Popup(btnOrcamento, "fchOrcamento.aspx?id_orcamento=" + ps.ID.ToString(),
                    false, false, false, true, true, true, true, 10, 40, 700, 520, false);

                Anthem.AnthemClientMethods.Redirect("frmPedidoServicoOrcamento.aspx?id_delineamentoOrcamento=" + ps.ID, btnNovoOrcamento);

                Anthem.AnthemClientMethods.Popup(btnEditar, "frmOrcamentoEdicao.aspx?id_orcamento=" + ps.ID.ToString(),
                    false, false, false, true, true, true, true, 10, 40, 600, 300, false);

               
                if (ps.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.Finalizado)
                {
                    btnNovoOrcamento.Visible = false;
                    lnkGarantia.Visible = true;
                    br.Visible = true;
                }
                else if (ps.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.EmGarantia)
                {
                    lnkFinalizar.Visible = true;
                    br.Visible = true;
                }
            }
            else
                btnEditar.Visible = btnOrcamento.Visible = btnNovoOrcamento.Visible = false;
            
        }
    }

    void btnPesquisar_Click(object sender, EventArgs e)
    {
        Bind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            Anthem.Manager.Register(this);
            Util.FillDropDownList(ddlStatus, StatusPedidoServico.List(), "Todos");
        }
    }
    #endregion  
    
    protected override void Bind()
    {
        List<IPedido> list = DelineamentoOrcamento.SelectPorGerente(
            txtTexto.Text,
            IsNull(txtDataInicio.Text, DateTime.MinValue),
            IsNull(txtDataFim.Text, DateTime.MinValue),
            Convert.ToInt32(ddlStatus.SelectedValue),
            this.ID_Servidor,
            Int32.MinValue,
            txtNumeroPedidoCliente.Text);
        SortIPedido.Sort(list, this.SortExpression);
		gvPesquisa.DataSource = list;
        gvPesquisa.DataBind();
        gvPesquisa.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();

        gvPesquisa.Visible = list.Count > 0;
        pnMensagem.Visible = list.Count == 0;
        pnMensagem.UpdateAfterCallBack = true;
    }
}