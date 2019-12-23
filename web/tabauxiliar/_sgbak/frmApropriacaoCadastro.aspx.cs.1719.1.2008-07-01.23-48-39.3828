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

public partial class frmApropriacaoCadastro : CadastroSimples<Apropriacao>
{
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        RegistraControlesCadastro(dgCadastro, btnNovo);
       
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    #endregion

    #region Bind
    protected override void Bind()
    {
        BindToGrid(Apropriacao.Select());
    }
    #endregion

 
}
