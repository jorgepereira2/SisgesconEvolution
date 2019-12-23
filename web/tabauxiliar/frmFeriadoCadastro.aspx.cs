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

public partial class frmFeriadoCadastro : CadastroSimples<Feriado>
{
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        RegistraControlesCadastro(dgCadastro, btnNovo);
        base.BeforeSave += new CadastroSimples<Feriado>.BeforeSaveEventHandler(frmFeriadoCadastro_BeforeSave);
    }

    void frmFeriadoCadastro_BeforeSave(object sender, BeforeSaveEventArgs<Feriado> e)
    {
        string controleID = "chkMeioExpediente" + (e.IsNew ? "Novo" : "");
        CheckBox chkMeioExpediente = (CheckBox)e.DataGridItem.FindControl(controleID);
        Anthem.DateTextBox txtData = (Anthem.DateTextBox) e.DataGridItem.FindControl("txtData" + (e.IsNew ? "Novo" : ""));
        e.Object.FlagMeioExpediente = chkMeioExpediente.Checked;
        e.Object.Data = Convert.ToDateTime(txtData.Text);
    }
    #endregion

    #region Bind
    protected override void Bind()
    {
        BindToGrid(Feriado.Select());
    }
    #endregion
}
