using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Security.Permissions;

using Marinha.Business;

public partial class _Default : MarinhaPageBase
{


	protected override void OnInit(EventArgs e)
	{
        base.DisableAnthemProgressIcon = true;
		base.OnInit(e);
        timerPopup.Tick += new EventHandler(timerPopup_Tick);
        timerPopup.Interval = 10000;
	   
	}

    protected void Page_Load(object sender, System.EventArgs e)
    {
		Anthem.Manager.Register(this);
        if (!IsPostBack)
        {
            InicializaVariaveisChat();
			string script = @"<script language=javascript>
				function Logoff()
				{
					if(confirm('Deseja sair do sistema?') == true)
					{
						Anthem_InvokePageMethod('Logoff', [], function(result){});						
					}        
				}</script>";
			this.ClientScript.RegisterClientScriptBlock(this.GetType(), "logoff", script);

			Servidor funcionario = Servidor.Get(this.ID_Servidor);
			frmCabecalho.Servidor = funcionario.NomeCompleto;

            Parametro parametro = Parametro.Get();
            this.Title = parametro.NomeSistema;
        }
    }
		
	private void Logoff()
	{			
		System.Web.Security.FormsAuthentication.SignOut();
		Anthem.AnthemClientMethods.Redirect(System.Web.Security.FormsAuthentication.LoginUrl);		
	}

    #region Popup Chat Control
    /// <summary>
    /// Retorna o array com os IDs dos funcionários com 
    /// janela de chat ativo
    /// </summary>
    private List<int> ChatsAtivos
    {
        get
        {
            if (Session["ChatsAtivos"] == null)
            {
                return new List<int>();
            }
            else
            {
                return (List<int>)Session["ChatsAtivos"];
            }
        }
    }

    private void InicializaVariaveisChat()
    {
        //Armazenamos num campo hidden pq ao fazer logoff o cookie nao está mais ativo
        hiddenServidor_ID.Value = this.ID_Servidor.ToString();
        ChatsAtivos.Clear();
        timerPopup.Enabled = true;
        //AtualizaStatusUsuarios();
    }

    [Anthem.Method]
    public void LogoffUser()
    {
        ChatManager.Remove(Convert.ToInt32(hiddenServidor_ID.Value));
        Anthem.AnthemClientMethods.Redirect(System.Web.Security.FormsAuthentication.LoginUrl);
        ChatManager.Remove(Convert.ToInt32(hiddenServidor_ID.Value));
        FormsAuthentication.SignOut();
    }

    void timerPopup_Tick(object sender, EventArgs e)
    {
        List<int> listaChats = MensagemChat.SelectJanelasParaAbrir(this.ID_Servidor, this.ChatsAtivos);
        foreach (int id_pessoa in listaChats)
        {
            Anthem.AnthemClientMethods.Popup(this, "Chat/frmChat.aspx?id_servidor=" +
                id_pessoa.ToString(), id_pessoa.ToString(),
                false, false, false, false, false, false, false, 30, 100, 550, 474);
        }
    }

    #endregion
}

