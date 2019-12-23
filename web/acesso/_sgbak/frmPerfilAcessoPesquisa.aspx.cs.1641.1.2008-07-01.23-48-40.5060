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

public partial class frmPerfilAcessoPesquisa : SortingPageBase
{

    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.btnPesquisar.Click += new EventHandler(btnPesquisar_Click);
		this.RegisterSortingControl(gvPesquisa);
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
            Anthem.AnthemClientMethods.Redirect("frmPerfilAcessoCadastro.aspx", btnNovo);

			RegisterDeleteScript();
        }
    }
    #endregion     

    
    protected override void Bind()
    {
        List<PerfilAcesso> list = PerfilAcesso.Select(txtNome.Text);
		this.Sort(list);
        gvPesquisa.DataSource = list;
        gvPesquisa.DataBind();
        gvPesquisa.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();


        gvPesquisa.Visible = list.Count > 0;
        pnMensagem.Visible = list.Count == 0;
        pnMensagem.UpdateAfterCallBack = true;
    }

    [Anthem.Method]
    public void Excluir(int id)
    {
        PerfilAcesso p = PerfilAcesso.Get(id);
        p.Delete();
        Bind();
    }
   
}
