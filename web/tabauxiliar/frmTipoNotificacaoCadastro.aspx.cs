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

public partial class frmTipoNotificacaoCadastro : CadastroSimples<TipoNotificacao>
{
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        RegistraControlesCadastro(dgCadastro, btnNovo);
        this.BeforeSave += new CadastroSimples<TipoNotificacao>.BeforeSaveEventHandler(frmTipoNotificacaoCadastro_BeforeSave);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    #endregion


    void frmTipoNotificacaoCadastro_BeforeSave(object sender, BeforeSaveEventArgs<TipoNotificacao> e)
    {
        CheckBox chkFlagAprovacao = (CheckBox)e.DataGridItem.FindControl("chkFlagAprovacao");

        e.Object.FlagAprovacao = chkFlagAprovacao.Checked;
    }

    #region Bind
    protected override void Bind()
    {
        BindToGrid(TipoNotificacao.Select());
    }
    #endregion

 
}
