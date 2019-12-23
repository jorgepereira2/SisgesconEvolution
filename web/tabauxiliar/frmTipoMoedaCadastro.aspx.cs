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

public partial class frmTipoMoedaCadastro : CadastroSimples<TipoMoeda>
{
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        RegistraControlesCadastro(dgCadastro, btnNovo);
        base.BeforeSave += new CadastroSimples<TipoMoeda>.BeforeSaveEventHandler(frmTipoMoedaCadastro_BeforeSave);
    }

    void frmTipoMoedaCadastro_BeforeSave(object sender, BeforeSaveEventArgs<TipoMoeda> e)
    {
        string controleID = "txtSigla" + (e.IsNew ? "Novo" : "");
        TextBox txtSigla = (TextBox)e.DataGridItem.FindControl(controleID);
        e.Object.Sigla = txtSigla.Text;
    }
    #endregion

    #region Bind
    protected override void Bind()
    {
        BindToGrid(TipoMoeda.Select());
    }
    #endregion
}
