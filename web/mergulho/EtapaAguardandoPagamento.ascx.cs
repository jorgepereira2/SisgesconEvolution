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

public partial class EtapaAguardandoPagamento : System.Web.UI.UserControl
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
    
    public event EventHandler MensagemInformada;    

    public string NLPagamento
    {
        get { return txtNLPagamento.Text; }
    }

    public string MensagemIndicacaoRecurso
    {
        get { return txtMensagemIndicacaoRecurso.Text; }
    }
    
    public void Show()
    {
        winMensagem.Show();
    }
    
    public void Close()
    {
        winMensagem.Hide();
        txtMensagemIndicacaoRecurso.Text = "";
        txtMensagemIndicacaoRecurso.UpdateAfterCallBack = true;

        txtNLPagamento.Text = "";
        txtNLPagamento.UpdateAfterCallBack = true;
    }
}
