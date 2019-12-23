using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class AguardandoIndicacaoRecurso : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        btnCancelar.Click += delegate {winMensagem.Hide();};
        btnOk.Click += new EventHandler(btnOk_Click);
    }

    void btnOk_Click(object sender, EventArgs e)
    {
        if (MensagemInformada != null)
            MensagemInformada(this, new EventArgs());
       
    }

    void btnCancelar_Click(object sender, EventArgs e)
    {
        winMensagem.Hide();
    }

    public event EventHandler MensagemInformada;    

    public string NumeroMensagem
    {
        get { return txtNumeroMensagem.Text; }
    }
  
    public void Show()
    {
        winMensagem.Show();
    }
    
    public void Close()
    {
        winMensagem.Hide();
        txtNumeroMensagem.Text = "";
        txtNumeroMensagem.UpdateAfterCallBack = true;
    }
}
