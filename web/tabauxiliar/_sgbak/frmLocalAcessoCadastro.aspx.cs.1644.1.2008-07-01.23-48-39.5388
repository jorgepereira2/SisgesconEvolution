using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Marinha.Business;
using Shared.SessionState;
using Shared.Common;

public partial class frmLocalAcessoCadastro : SortingPageBase
{
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.btnNovo.Click += btnNovo_Click;
        this.dgCadastro.EditCommand += dgCadastro_EditCommand;
        this.dgCadastro.CancelCommand += dgCadastro_CancelCommand;
        this.dgCadastro.UpdateCommand += dgCadastro_UpdateCommand;
        this.dgCadastro.ItemCommand += dgCadastro_ItemCommand;
        this.RegisterSortingControl(dgCadastro);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            Bind();
            RegisterDeleteScript();
        }
    }

    #endregion

    #region Bind
    protected override void Bind()
    {
        List<LocalAcesso> list = LocalAcesso.Select();
        this.Sort(list);

        dgCadastro.DataSource = list;
        dgCadastro.DataKeyField = "ID";
        dgCadastro.DataBind();
        dgCadastro.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();
    }

    #endregion

    #region DataGrid

    void dgCadastro_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Insert")
        {
            try
            {
                Anthem.TextBox txtDescricao = (Anthem.TextBox)e.Item.FindControl("txtDescricaoNovo");
                Anthem.CheckBox chkAtivo = (Anthem.CheckBox)e.Item.FindControl("chkAtivoNovo");
                Anthem.TextBox txtIPInicial = (Anthem.TextBox)e.Item.FindControl("txtIPInicialNovo");
                Anthem.TextBox txtIPFinal = (Anthem.TextBox)e.Item.FindControl("txtIPFinalNovo");

                LocalAcesso local = new LocalAcesso();
                local.Descricao = txtDescricao.Text;
                local.IPInicial = txtIPInicial.Text;
                local.IPFinal = txtIPFinal.Text;
                local.FlagAtivo = chkAtivo.Checked;
                local.Save();

                Bind();
                dgCadastro.ShowFooter = false;
            }
            catch (Exception ex)
            {
                Anthem.AnthemClientMethods.Alert(ex.Message, this);
            }
        }
    }

    void dgCadastro_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            Anthem.TextBox txtDescricao = (Anthem.TextBox)e.Item.FindControl("txtDescricao");
            Anthem.CheckBox chkAtivo = (Anthem.CheckBox)e.Item.FindControl("chkAtivo");
            Anthem.TextBox txtIPInicial = (Anthem.TextBox)e.Item.FindControl("txtIPInicial");
            Anthem.TextBox txtIPFinal = (Anthem.TextBox)e.Item.FindControl("txtIPFinal");


            int id = Convert.ToInt32(dgCadastro.DataKeys[e.Item.ItemIndex]);

            LocalAcesso local = LocalAcesso.Get(id);
            local.Descricao = txtDescricao.Text;
            local.FlagAtivo = chkAtivo.Checked;
            local.IPInicial = txtIPInicial.Text;
            local.IPFinal = txtIPFinal.Text;
            local.Save();
            dgCadastro.EditItemIndex = -1;
            Bind();
        }
        catch (Exception ex)
        {
            Anthem.AnthemClientMethods.Alert(ex.Message, this);
        }
    }

    void dgCadastro_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dgCadastro.EditItemIndex = -1;
        dgCadastro.ShowFooter = false;
        Bind();
    }

    void dgCadastro_EditCommand(object source, DataGridCommandEventArgs e)
    {
        dgCadastro.EditItemIndex = e.Item.ItemIndex;
        Bind();
    }

    [Anthem.Method]
    public void Excluir(int id)
    {
        LocalAcesso local = LocalAcesso.Get(id);
        local.Delete();
        Bind();
    }


    void btnNovo_Click(object sender, EventArgs e)
    {
        dgCadastro.ShowFooter = true;
        Bind();
        dgCadastro.UpdateAfterCallBack = true;
    }
    #endregion
}
