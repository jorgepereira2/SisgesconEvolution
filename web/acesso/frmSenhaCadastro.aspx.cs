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



public partial class frmSenhaCadastro : MarinhaPageBase
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
        ddlServidor.SelectedValueChanged += DdlServidor_OnSelectedIndexChanged;            
    }

    private void DdlServidor_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if(ddlServidor.SelectedValue != "0")
        {
            _servidor = Servidor.Get(Convert.ToInt32(ddlServidor.SelectedValue));
            txtLogin.Text = _servidor.Login;
            txtSenha.Attributes["value"] = _servidor.Senha;
            txtConfirmacaoSenha.Attributes["value"] = _servidor.Senha;

            txtLogin.UpdateAfterCallBack = true;
            txtSenha.UpdateAfterCallBack = true;
            txtConfirmacaoSenha.UpdateAfterCallBack = true;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            //Util.FillDropDownList(ddlServidor, Servidor.List(null), ESCOLHA_OPCAO);
        }
    }
    #endregion

    #region Events
    void btnSalvar_Click(object sender, EventArgs e)
    {
        if(this.IsValid)
        {
            _servidor = Servidor.Get(Convert.ToInt32(ddlServidor.SelectedValue));
            _servidor.SalvarSenha(txtLogin.Text, txtSenha.Text, chkFlagPrimeiroAcesso.Checked);
            ShowSuccessMessage();
        }
    }
    #endregion

   

   
}
