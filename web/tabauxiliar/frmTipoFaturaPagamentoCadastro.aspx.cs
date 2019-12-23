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

public partial class frmTipoFaturaPagamentoCadastro : CadastroSimples<TipoFaturaPagamento>
{
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        RegistraControlesCadastro(dgCadastro, btnNovo);
        this.BeforeSave += new BeforeSaveEventHandler(frmTipoFaturaPagamentoCadastro_BeforeSave);
       
    }

    void frmTipoFaturaPagamentoCadastro_BeforeSave(object sender, BeforeSaveEventArgs<TipoFaturaPagamento> e)
    {
        CheckBox chkFlagDiaria = (CheckBox)e.DataGridItem.FindControl(e.IsNew ? "chkFlagDiariaNovo" : "chkFlagDiaria");
        e.Object.FlagDiaria = chkFlagDiaria.Checked;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    #endregion

    #region Bind
    protected override void Bind()
    {
        BindToGrid(TipoFaturaPagamento.Select());
    }
    #endregion

 
}
