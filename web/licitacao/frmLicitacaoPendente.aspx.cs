using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Drawing;
using System.Text;
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

public partial class frmLicitacaoPendente : SortingPageBase
{
    protected Licitacao _licitacao
    {
        get { return Session["frmLicitacaoPendente._licitacao"] == null ? null : (Licitacao)Session["frmLicitacaoPendente._licitacao"]; }
        set { Session["frmLicitacaoPendente._licitacao"] = value; }
    }
    
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
		this.RegisterSortingControl(this.gvPesquisa);
        this.gvPesquisa.RowDataBound += new GridViewRowEventHandler(gvPesquisa_RowDataBound);
        this.gvPesquisa.RowEditing += new GridViewEditEventHandler(gvPesquisa_RowEditing);
        ucMessageBox.MessageBoxClose += new UserControls_MessageBox.MessageBoxEventHandler(ucMessageBox_MessageBoxClose);
        ucInputBox.TextoInformado += new EventHandler(ucInputBox_TextoInformado);
        gvPesquisa.RowDeleting += new GridViewDeleteEventHandler(gvPesquisa_RowDeleting);
        ucJustificativa.JustificativaInformada += new EventHandler(ucJustificativa_JustificativaInformada);
        btnFiltrar.Click += btnFiltrar_Click;
        
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            Anthem.Manager.Register(this);

            Util.FillDropDownList(ddlStatus, StatusLicitacao.List(), "Todos");

            Bind();
        }
    }
    #endregion  
  
    protected override void Bind()
    {
        List<Licitacao> list = Licitacao.Select(this.ID_Servidor, Convert.ToInt32(ddlStatus.SelectedValue));
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

    void btnFiltrar_Click(object sender, EventArgs e)
    {
        Bind();
    }

    void ucJustificativa_JustificativaInformada(object sender, EventArgs e)
    {
        Licitacao Licitacao = Licitacao.Get(ucJustificativa.ID_Item);
        Licitacao.Recusar(ID_Servidor, ucJustificativa.Justificativa);
        ucJustificativa.Close();
        Bind();
    }

    void gvPesquisa_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = Convert.ToInt32(gvPesquisa.DataKeys[e.RowIndex]["ID"]);
        ucJustificativa.Show(id);
    }

    void gvPesquisa_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int id = Convert.ToInt32(gvPesquisa.DataKeys[e.NewEditIndex]["ID"]);
        StatusLicitacaoEnum status = (StatusLicitacaoEnum)Convert.ToInt32(((Label)gvPesquisa.Rows[e.NewEditIndex].FindControl("lblID_Status")).Text);

        if(status == StatusLicitacaoEnum.CadastroInicial)
        {
            Redirect("frmLicitacaoCadastro.aspx?id_licitacao=" + id);
        }
        else if (status != StatusLicitacaoEnum.Cancelado)
        {
            _licitacao = Licitacao.Get(id);
            //ucMessageBox.Show("Confirma operação?", id);
            ucInputBox.Show(id, "Confirma operação?");
        }
       
    }

    void ucMessageBox_MessageBoxClose(object sender, MessageBoxEventArgs e)
    {
        if (e.Result == MessageBoxResult.Sim)
        {
            _licitacao.ProximoStatus(this.ID_Servidor, null);
            _licitacao = null;
            Bind();
        }
        _licitacao = null;
    }

    void ucInputBox_TextoInformado(object sender, EventArgs e)
    {
       
        _licitacao.ProximoStatus(this.ID_Servidor, ucInputBox.Texto);
       
        _licitacao = null;
        ucInputBox.Close();
        Bind();
    }

    void gvPesquisa_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Licitacao Licitacao = (Licitacao)e.Row.DataItem;
            
            if (Licitacao.UltimoHistorico != null && Licitacao.UltimoHistorico.FlagReprovado)
            {
                e.Row.ForeColor = Color.Red;
                e.Row.ToolTip = Licitacao.UltimoHistorico.Justificativa;

                e.Row.Attributes.Add("onmouseover", string.Format("Tip('<b>Justificativa:</b><br><br>{0}', SHADOW, true, PADDING, 7, FOLLOWMOUSE, false);",
                    Licitacao.UltimoHistorico.Justificativa));
            }

            if(Licitacao.Status == StatusLicitacaoEnum.CadastroInicial)
            {
                LinkButton btnEditar = (LinkButton)e.Row.FindControl("btnEditar");
                btnEditar.Text = "Editar";
            }

            LinkButton btnDetalhes = (LinkButton)e.Row.FindControl("lnkDetalhes");
            Anthem.AnthemClientMethods.Popup(btnDetalhes, "fchLicitacao.aspx?id_licitacao=" + Licitacao.ID, false, false, false, true, true, true, true, 10, 40, 700, 520, false);
     
        }
    }
    
}
