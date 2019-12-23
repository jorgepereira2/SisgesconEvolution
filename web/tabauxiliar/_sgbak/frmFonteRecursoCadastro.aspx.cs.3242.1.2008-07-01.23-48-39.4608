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

public partial class frmFonteRecursoCadastro : CadastroSimples<FonteRecurso>
{
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        RegistraControlesCadastro(dgCadastro, btnNovo);
        base.BeforeSave += new CadastroSimples<FonteRecurso>.BeforeSaveEventHandler(frmFonteRecursoCadastro_BeforeSave);
    }

    void frmFonteRecursoCadastro_BeforeSave(object sender, BeforeSaveEventArgs<FonteRecurso> e)
    {
        string controleID = "txtCodigo" + (e.IsNew ? "Novo" : "");
        TextBox txtCodigo = (TextBox)e.DataGridItem.FindControl(controleID);
        e.Object.Codigo = txtCodigo.Text;
    }
    #endregion

    #region Bind
    protected override void Bind()
    {
        BindToGrid(FonteRecurso.Select());
    }
    #endregion
}
