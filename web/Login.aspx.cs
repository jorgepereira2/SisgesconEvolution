using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

using Marinha.Business;


public partial class Login : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

#if DEBUG 
		if(!this.IsPostBack)
		{
			txtLogin.Text = "teste";
			txtSenha.Text = "zap0";			
		}
#endif

        if(!IsPostBack)
        {
            Parametro parametro = Parametro.Get();
            this.Title = parametro.NomeSistema;
        }
	}

	protected override void OnInit(EventArgs e)
	{
		base.OnInit(e);
		btnEntrar.Click += new EventHandler(btnEntrar_Click);
	}

	void btnEntrar_Click(object sender, EventArgs e)
	{
		try
		{
			lblMensagem.Text = "";
			Servidor servidor = Servidor.Get(txtLogin.Text, txtSenha.Text);
			if (servidor != null)
			{
				InsertCookie(servidor);

			    SaveLog(servidor);

				if (servidor.FlagPrimeiroAcesso)
				{
					FormsAuthentication.SetAuthCookie(txtLogin.Text, false);
					Response.Redirect("acesso/frmAlterarSenha.aspx");
				}
				else
					FormsAuthentication.RedirectFromLoginPage(txtLogin.Text, false);
				//Response.Redirect("default.aspx");
			}
			else
			{
				lblMensagem.Text = "Credenciais inválidas";
			}
		}
		catch (Exception ex)
		{
		    lblMensagem.Text = MarinhaPageBase.GetCompleteErrorMessage(ex);
		}
	}

    private void InsertCookie(Servidor servidor)
    {
        //Cria o cookie
        HttpCookie cookie = new HttpCookie("Marinha");
		cookie.Values.Add("ID_Servidor", servidor.ID.ToString());
        cookie.Values.Add("FlagAcessaTodosMateriais", servidor.FlagAcessaTodosMateriais.ToString());

        StringBuilder str = new StringBuilder();

        foreach (SJB sjb in servidor.SJBLiberados)
            str.Append(sjb.ID.ToString()).Append(",");

        cookie.Values.Add("SJBLiberados", Shared.Common.Util.RemoveLastChar(str.ToString()));
        
        Response.Cookies.Add(cookie);
    }
    
    private  void SaveLog(Servidor servidor)
    {
        LogAcesso log = new LogAcesso();
        log.Servidor = servidor;
        log.IP = Request.UserHostAddress;
        log.Browser = Request.Browser.Browser + " " + Request.Browser.MajorVersion + "." + Request.Browser.MinorVersion;
        log.Data = DateTime.Now;
        log.Save();
    }
}