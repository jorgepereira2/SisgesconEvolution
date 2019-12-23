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



public partial class frmAlterarSenha : MarinhaPageBase
{  

    #region Private Member
    [TransientPageState]
    protected Servidor _servidor;

    #endregion 

    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.btnSalvar.Click += new EventHandler(btnSalvar_Click);        
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
			_servidor = Servidor.Get(this.ID_Servidor);
			lblLogin.Text = _servidor.Login;
			lblNome.Text = _servidor.NomeCompleto;
        }        
    }
    #endregion

    #region Events
    void btnSalvar_Click(object sender, EventArgs e)
    {
		if (_servidor.FlagPrimeiroAcesso)
		{
			_servidor.FlagPrimeiroAcesso = false;
			_servidor.AlterarSenha(txtSenha.Text);
			Anthem.AnthemClientMethods.Redirect("../default.aspx");
		}
		else
		{
			_servidor.AlterarSenha(txtSenha.Text);
			ShowSuccessMessage();
		}
    }
    #endregion

   

   
}
