using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Marinha.Business;
using Shared.SessionState;
using ComponentArt.Web.UI;
using Shared.Common;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;

public partial class Acesso_frmProcessoCadastro : SortingPageBase
{
    #region Private Member


    private ProcessoCollection _processos
    {
        get { return (ProcessoCollection)Session["_processos"]; }
        set { Session["_processos"] = value; }
    }

    private Processo _processoAtual
    {
        get { return (Processo)Session["_processoAtual"]; }
        set { Session["_processoAtual"] = value; }
    }

    #endregion 


    #region Initialization

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.btnImprimir.Click += new EventHandler(btnImprimir_Click);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            Bind();
        }
    }
    
    protected override void Bind()
    {
        List<PerfilAcesso> list = PerfilAcesso.Select(null);

        gvPesquisa.DataSource = list;
        gvPesquisa.DataKeyNames = new string[] { "ID" };
        gvPesquisa.DataBind();
        gvPesquisa.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();
    }

    #endregion


    #region Imprimir

    void btnImprimir_Click(object sender, EventArgs e)
    {
        string ListaPerfilAcesso = GetListaPerfilAcesso();

        if (ListaPerfilAcesso == null || ListaPerfilAcesso == "")
        {
            ShowMessage("Selecione um Perfil de Acesso!");
            return;
        }

        string address = "";

        object[] param = new object[42];

        param[0] = HttpUtility.UrlEncode(txtNome.Text);
        param[1] = ListaPerfilAcesso;

        address = string.Format(@"relPerfilAcessoPesquisaDetalhadoListagem.aspx?nome={0}&perfilAcesso={1}", param);

        Anthem.AnthemClientMethods.Popup(this, address, false, false, false, true, true, true, true,
            60, 60, 700, 500);
    }

    private string GetListaPerfilAcesso()
    {
        StringBuilder sb = new StringBuilder();

        foreach (GridViewRow row in gvPesquisa.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                int id = Convert.ToInt32(gvPesquisa.DataKeys[row.RowIndex].Value.ToString());

                Anthem.CheckBox chk = (Anthem.CheckBox)row.FindControl("chkPerfilAcesso");

                if (chk.Checked)
                {
                    sb.AppendFormat("{0},", id);
                }
            }
        }

        if (sb.Length > 0)
            return Util.RemoveLastChar(sb.ToString());
        else
            return "";
    }

    #endregion


    #region MarcaDesmarcaTodos Checkbox

    protected void chkMarcarTodos2_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gvPesquisa.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                int id = Convert.ToInt32(gvPesquisa.DataKeys[row.RowIndex].Value.ToString());

                Anthem.CheckBox chk = (Anthem.CheckBox)row.FindControl("chkPerfilAcesso");

                if (chk.Checked)
                    chk.Checked = false;

                else
                    chk.Checked = true;
            }
        }

        gvPesquisa.UpdateAfterCallBack = true;
    }

    #endregion
}