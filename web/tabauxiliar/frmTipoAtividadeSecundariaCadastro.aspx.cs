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

public partial class frmTipoAtividadeSecundariaCadastro : CadastroSimples<TipoAtividadeSecundaria>
{
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        RegistraControlesCadastro(dgCadastro, btnNovo);
        this.BeforeSave += new BeforeSaveEventHandler(frmTipoAtividadeSecundariaCadastro_BeforeSave);
       
    }

    void frmTipoAtividadeSecundariaCadastro_BeforeSave(object sender, BeforeSaveEventArgs<TipoAtividadeSecundaria> e)
    {
        CheckBox chkSemTaxas = (CheckBox) e.DataGridItem.FindControl(e.IsNew ? "chkSemTaxasNovo" : "chkSemTaxas");
        e.Object.SemTaxas = chkSemTaxas.Checked;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    #endregion

    #region Bind
    protected override void Bind()
    {
        BindToGrid(TipoAtividadeSecundaria.Select());
    }
    #endregion

 
}
