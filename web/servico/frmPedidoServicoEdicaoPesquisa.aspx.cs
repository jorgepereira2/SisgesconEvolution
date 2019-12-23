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

public partial class frmPedidoServicoEdicaoPesquisa : SortingPageBase
{
   
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.btnPesquisar.Click += new EventHandler(btnPesquisar_Click);
		this.RegisterSortingControl(this.gvPesquisa);
        gvPesquisa.RowDataBound += new GridViewRowEventHandler(gvPesquisa_RowDataBound);
        gvPesquisa.RowEditing += new GridViewEditEventHandler(gvPesquisa_RowEditing);
        gvPesquisa.RowCancelingEdit += new GridViewCancelEditEventHandler(gvPesquisa_RowCancelingEdit);
        gvPesquisa.RowUpdating += new GridViewUpdateEventHandler(gvPesquisa_RowUpdating);
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
            Util.FillDropDownList(ddlDivisao, Celula.List(TipoCelula.Divisao, true), "Todas");
            Util.FillDropDownList(ddlGerente, Servidor.List(FuncaoServidor.GerenteDPCP), "Todos");
            Util.FillDropDownList(ddlAno, DateTimeManager.Anos(DateTime.Today.AddYears(-6).Year, DateTime.Today.Year), "Todos");
            ddlAno.SelectedValue = DateTime.Today.Year.ToString();
        }
    }
    #endregion  
    
    protected override void Bind()
    {

        List<PedidoServico> list = PedidoServico.SelectEditaveis(
                txtTexto.Text,
                IsNull(txtDataInicio.Text, DateTime.MinValue),
                IsNull(txtDataFim.Text, DateTime.MinValue),
                Convert.ToInt32(ddlStatus.SelectedValue),
                Convert.ToInt32(ddlGerente.SelectedValue),
                Convert.ToInt32(ddlDivisao.SelectedValue),
                Convert.ToInt32(ddlAno.SelectedValue));

		this.Sort(list);
		gvPesquisa.DataSource = list;
		gvPesquisa.DataKeyNames = new string[]{"ID"};
        gvPesquisa.DataBind();
        gvPesquisa.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();

        gvPesquisa.Visible = list.Count > 0;
        pnMensagem.Visible = list.Count == 0;
        pnMensagem.UpdateAfterCallBack = true;
    }

    void gvPesquisa_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow row = gvPesquisa.Rows[e.RowIndex];
        TextBox txtDiversos = (TextBox) row.FindControl("txtDiversos");
        TextBox txtDataPlanejamento = (TextBox)row.FindControl("txtDataPlanejamento");
        TextBox txtDataPronto = (TextBox)row.FindControl("txtDataPronto");

        PedidoServico ps = PedidoServico.Get(Convert.ToInt32(gvPesquisa.DataKeys[e.RowIndex]["ID"]));
        ps.DataPronto = PageReader.ReadNullableDate(txtDataPronto);
        ps.DataPlanejamentoPS = PageReader.ReadNullableDate(txtDataPlanejamento);
        ps.Diversos = PageReader.ReadString(txtDiversos);
        ps.Save();
        gvPesquisa.EditIndex = -1;
        Bind();
    }

    void gvPesquisa_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvPesquisa.EditIndex = -1;
        Bind();
    }

    void gvPesquisa_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvPesquisa.EditIndex = e.NewEditIndex;
        Bind();
    }

    void gvPesquisa_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btnDetalhes = (LinkButton)e.Row.FindControl("lnkDetalhes");
            LinkButton btnEditar = (LinkButton)e.Row.FindControl("lnkEditar");
            PedidoServico pedido = (PedidoServico)e.Row.DataItem;
            Anthem.AnthemClientMethods.Popup(btnDetalhes, "fchPedidoServico.aspx?id_pedido=" + pedido.ID.ToString(),
            false, false, false, true, true, true, true, 10, 40, 700, 520, false);

            string address = "frmPedidoServicoEdicao.aspx?id_pedido=" + pedido.ID.ToString();
            Anthem.AnthemClientMethods.Redirect(address, btnEditar);
        }
    }
}
