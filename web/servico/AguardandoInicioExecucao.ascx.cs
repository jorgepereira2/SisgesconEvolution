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

public partial class ucAguardandoInicioExecucao : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        btnCancelar.Click += delegate {winRecusa.Hide();};
        btnOk.Click += new EventHandler(btnOk_Click);
    }

    void btnOk_Click(object sender, EventArgs e)
    {
        if(DataPrevisaoEntrega < DateTime.Today)
        {
            Anthem.AnthemClientMethods.Alert("A data de entrega não pode ser anterior a hoje.", this.Page);
            return;
        }
        if (OkClicked != null)
            OkClicked(this, new EventArgs());
       
    }
    
    public event EventHandler OkClicked;    

    public DateTime DataPrevisaoEntrega
    {
        get { return Convert.ToDateTime(txtDataPrevisaoEntrega.Text); }
        set{ txtDataPrevisaoEntrega.Text = value.ToShortDateString();
            txtDataPrevisaoEntrega.UpdateAfterCallBack = true;}
    }

    public string Comentario
    {
        get { return txtComentario.Text; }
        set
        {
            txtComentario.Text = value;
            txtComentario.UpdateAfterCallBack = true;
        }
    }
    
    public void Show()
    {
        winRecusa.Show();
    }
    
    public void Close()
    {
        winRecusa.Hide();
        txtDataPrevisaoEntrega.Text = "";
        txtDataPrevisaoEntrega.UpdateAfterCallBack = true;
        txtComentario.Text = "";
        txtComentario.UpdateAfterCallBack = true;
    }
}
