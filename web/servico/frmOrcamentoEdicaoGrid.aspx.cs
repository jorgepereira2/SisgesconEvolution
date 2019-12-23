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

public partial class frmOrcamentoEdicaoGrid : SortingPageBase
{
   
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.btnPesquisar.Click += new EventHandler(btnPesquisar_Click);
		this.RegisterSortingControl(this.gvPesquisa);
        gvPesquisa.RowEditing += new GridViewEditEventHandler(gvPesquisa_RowEditing);
        gvPesquisa.RowCancelingEdit += new GridViewCancelEditEventHandler(gvPesquisa_RowCancelingEdit);
        gvPesquisa.RowUpdating += new GridViewUpdateEventHandler(gvPesquisa_RowUpdating);
    }

    void gvPesquisa_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        DelineamentoOrcamento orcamento = DelineamentoOrcamento.Get(Convert.ToInt32(gvPesquisa.DataKeys[e.RowIndex]["ID"]));

        TextBox txtComprometimento = (TextBox) gvPesquisa.Rows[e.RowIndex].FindControl("txtComprometimento");
        TextBox txtNumeroNL = (TextBox)gvPesquisa.Rows[e.RowIndex].FindControl("txtNumeroNL");
        TextBox txtMensagemProntificacao = (TextBox)gvPesquisa.Rows[e.RowIndex].FindControl("txtMensagemProntificacao");
        TextBox txtMensagemEnvioCliente = (TextBox)gvPesquisa.Rows[e.RowIndex].FindControl("txtMensagemEnvioCliente");
        TextBox txtPrevisaoEntrega = (TextBox)gvPesquisa.Rows[e.RowIndex].FindControl("txtPrevisaoEntrega");

        orcamento.MensagemEnvioCliente = txtMensagemEnvioCliente.Text;
        orcamento.MensagemProntificacao = txtMensagemProntificacao.Text;
        orcamento.NumeroNL = txtNumeroNL.Text;
        orcamento.ComprometimentoCliente = txtComprometimento.Text;
        orcamento.Save();

        orcamento.PedidoServico.PrevisaoEntrega = PageReader.ReadNullableDate(txtPrevisaoEntrega);
        orcamento.PedidoServico.Save();

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
        List<DelineamentoOrcamento> list = DelineamentoOrcamento.Select(
            txtTexto.Text,
            IsNull(txtDataInicio.Text, DateTime.MinValue),
            IsNull(txtDataFim.Text, DateTime.MinValue),
            Convert.ToInt32(ddlStatus.SelectedValue),
            Int32.MinValue,
            Int32.MinValue,
            txtNumeroPedidoCliente.Text,
            Int32.MinValue);
        
		gvPesquisa.DataSource = list;
        gvPesquisa.DataKeyNames = new string[]{"ID"};
        gvPesquisa.DataBind();
        gvPesquisa.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();

        gvPesquisa.Visible = list.Count > 0;
        pnMensagem.Visible = list.Count == 0;
        pnMensagem.UpdateAfterCallBack = true;
    }
}