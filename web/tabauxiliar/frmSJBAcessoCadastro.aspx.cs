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

public partial class frmSJBAcessoCadastro : CadastroSimples<SJB>
{
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        RegistraControlesCadastro(dgCadastro, btnNovo);
        base.BeforeSave += new CadastroSimples<SJB>.BeforeSaveEventHandler(frmSJBCadastro_BeforeSave);
    }

    void frmSJBCadastro_BeforeSave(object sender, BeforeSaveEventArgs<SJB> e)
    {
        string controleID = "txtCodigo" + (e.IsNew ? "Novo" : "");
        TextBox txtCodigo = (TextBox)e.DataGridItem.FindControl(controleID);
        
        e.Object.Codigo = txtCodigo.Text;
        CheckBox chkAcessoRestrito = (CheckBox)e.DataGridItem.FindControl("chkAcessoRestrito");
        e.Object.FlagAcessoRestrito = chkAcessoRestrito.Checked;
    }
    #endregion

    #region Bind
    protected override void Bind()
    {
        BindToGrid(SJB.Select());
    }
    #endregion
}
