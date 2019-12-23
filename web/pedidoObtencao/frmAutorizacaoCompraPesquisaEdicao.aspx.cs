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

public partial class frmAutorizacaoCompraPesquisaEdicao : SortingPageBase
{
    protected AutorizacaoCompra _ac
    {
        get { return Session["frmAutorizacaoCompraPesquisaEdicao._ac"] == null ? null : (AutorizacaoCompra)Session["frmAutorizacaoCompraPesquisaEdicao._ac"]; }
        set { Session["frmAutorizacaoCompraPesquisaEdicao._ac"] = value; }
    }

    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.btnPesquisar.Click += new EventHandler(btnPesquisar_Click);
		this.RegisterSortingControl(this.gvPesquisa);
		gvPesquisa.RowDataBound +=new GridViewRowEventHandler(gvPesquisa_RowDataBound);
        gvPesquisa.RowEditing += new GridViewEditEventHandler(gvPesquisa_RowEditing);
        ucEdicaoAC.OkClicked += new EventHandler(ucEdicaoAC_OkClicked);
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

            Util.FillDropDownList(ddlComprador, Servidor.List(FuncaoServidor.Comprador), "Todos");
            Util.FillDropDownList(ddlStatus, StatusAutorizacaoCompra.List(), "Todos");
        }
    }
    #endregion  
    
    protected override void Bind()
    {
        List<AutorizacaoCompra> list = AutorizacaoCompra.Select(
            txtTexto.Text,
		    IsNull(txtDataInicio.Text, DateTime.MinValue), 
		    IsNull(txtDataFim.Text, DateTime.MinValue),
		    Convert.ToInt32(ddlStatus.SelectedValue),
            Convert.ToInt32(ddlComprador.SelectedValue));
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

    void gvPesquisa_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btnDetalhes = (LinkButton)e.Row.FindControl("lnkDetalhes");
            AutorizacaoCompra ac = (AutorizacaoCompra) e.Row.DataItem;
            Anthem.AnthemClientMethods.Popup(btnDetalhes, "fchAutorizacaoCompraAssinatura.aspx?id_ac=" + ac.ID.ToString(),
            false, false, false, true, true, true, true, 10, 40, 700, 520, false);

        }
    }

    void gvPesquisa_RowEditing(object sender, GridViewEditEventArgs e)
    {
        _ac = AutorizacaoCompra.Get(Convert.ToInt32(gvPesquisa.DataKeys[e.NewEditIndex]["ID"]));
        ucEdicaoAC.Show(_ac);

    }

    void ucEdicaoAC_OkClicked(object sender, EventArgs e)
    {
        if (_ac == null) return;
        
        _ac.NumeroLancamento = ucEdicaoAC.NumeroLancamento;
        _ac.Observacao = ucEdicaoAC.Observacao;
        _ac.Lista = ucEdicaoAC.Lista;
        _ac.CodigoGestao = ucEdicaoAC.CodigoGestao;
        _ac.NumeroEmpenho = ucEdicaoAC.NumeroEmpenho;
        _ac.NaturezaDespesa = NaturezaDespesa.Get(ucEdicaoAC.ID_NaturezaDespesa);

        _ac.Save();

        ucEdicaoAC.Close();
    }
}
