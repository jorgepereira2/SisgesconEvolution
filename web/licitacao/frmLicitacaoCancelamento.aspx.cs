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

public partial class frmLicitacaoCancelamento : SortingPageBase
{
   
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.btnPesquisar.Click += new EventHandler(btnPesquisar_Click);
		this.RegisterSortingControl(this.gvPesquisa);
        this.gvPesquisa.RowDeleting +=new GridViewDeleteEventHandler(gvPesquisa_RowDeleting);
        this.ucMotivoCancelamento.OkClick +=new EventHandler(ucMotivoCancelamento_OkClick);
        gvPesquisa.RowEditing += new GridViewEditEventHandler(gvPesquisa_RowEditing);
        
    }


    void gvPesquisa_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int id = Convert.ToInt32(gvPesquisa.DataKeys[e.NewEditIndex]["ID"]);
        Licitacao licitacao = Licitacao.Get(id);
        licitacao.Desativar();
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
			Anthem.AnthemClientMethods.Redirect("frmLicitacaoCadastro.aspx", btnNovo);
        }
    }
    #endregion  
    
    protected override void Bind()
    {
		List<Licitacao> list = Licitacao.Select(
		    IsNull(txtDataInicio.Text, DateTime.MinValue), 
		    IsNull(txtDataFim.Text, DateTime.MinValue),
		    null);
		this.Sort(list);
		gvPesquisa.DataSource = list;
        gvPesquisa.DataKeyNames = new string[] {"ID"};
        gvPesquisa.DataBind();
        gvPesquisa.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();

        gvPesquisa.Visible = list.Count > 0;
        pnMensagem.Visible = list.Count == 0;
        pnMensagem.UpdateAfterCallBack = true;
    }

    void gvPesquisa_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = Convert.ToInt32(gvPesquisa.DataKeys[e.RowIndex]["ID"]);
        ucMotivoCancelamento.Show(id);
    }

    void ucMotivoCancelamento_OkClick(object sender, EventArgs e)
    {
        Licitacao licitacao = Licitacao.Get(ucMotivoCancelamento.ID_Objeto);
        licitacao.Cancelar(ID_Servidor, ucMotivoCancelamento.ID_MotivoCancelamento);
        ucMotivoCancelamento.Close();
        Bind();
    }
}
