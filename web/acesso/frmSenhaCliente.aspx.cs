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



public partial class frmSenhaCliente : MarinhaPageBase
{  

    #region Private Member
    [TransientPageState]
    protected Cliente _cliente;

    #endregion 

    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.btnSalvar.Click += new EventHandler(btnSalvar_Click);
        ddlCliente.SelectedIndexChanged += DdlServidor_OnSelectedIndexChanged;            
    }

    private void DdlServidor_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCliente.SelectedValue != "0")
        {
            _cliente = Cliente.Get(Convert.ToInt32(ddlCliente.SelectedValue));
            lblLogin.Text = _cliente.IndicativoNaval;
            txtSenha.Attributes["value"] = _cliente.Senha;
            txtConfirmacaoSenha.Attributes["value"] = _cliente.Senha;

            lblLogin.UpdateAfterCallBack = true;
            txtSenha.UpdateAfterCallBack = true;
            txtConfirmacaoSenha.UpdateAfterCallBack = true;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            Util.FillDropDownList(ddlCliente, Cliente.List(), ESCOLHA_OPCAO);
        }
    }
    #endregion

    #region Events
    void btnSalvar_Click(object sender, EventArgs e)
    {
        if(this.IsValid)
        {
            _cliente.Senha = txtSenha.Text;
            _cliente.Save();
            ShowSuccessMessage();
        }
    }
    #endregion

   

   
}
