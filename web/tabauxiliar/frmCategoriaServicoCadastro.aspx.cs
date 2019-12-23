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

public partial class frmCategoriaServicoCadastro : CadastroSimples<CategoriaServico>
{
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        RegistraControlesCadastro(dgCadastro, btnNovo);
        base.BeforeSave += new CadastroSimples<CategoriaServico>.BeforeSaveEventHandler(frmCategoriaServicoCadastro_BeforeSave);
    }

    void frmCategoriaServicoCadastro_BeforeSave(object sender, BeforeSaveEventArgs<CategoriaServico> e)
    {
        
        TextBox txtIdentificador = (TextBox)e.DataGridItem.FindControl("txtIdentificador");
        e.Object.Identificador = txtIdentificador.Text;

        CheckBox chkRequerFaturamento = (CheckBox)e.DataGridItem.FindControl("chkRequerFaturamento");
        e.Object.FlagRequerFaturamento = chkRequerFaturamento.Checked;

        CheckBox chkMergulho = (CheckBox)e.DataGridItem.FindControl("chkMergulho");
        e.Object.FlagMergulho = chkMergulho.Checked;
    }
    #endregion

    #region Bind
    protected override void Bind()
    {
        BindToGrid(CategoriaServico.Select());
    }
    #endregion

 
}
