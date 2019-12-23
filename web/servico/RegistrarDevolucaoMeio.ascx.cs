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

public partial class RegistrarDevolucaoMeio : System.Web.UI.UserControl
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
        if (OkClick != null)
            OkClick(this, new EventArgs());
       
    }

    void btnCancelar_Click(object sender, EventArgs e)
    {
        winRecusa.Hide();
    }

    public event EventHandler OkClick;    

    public string Comentario
    {
        get { return txtComentario.Text; }
    }
    
    public void Show()
    {
        winRecusa.Show();
    }
    
    public void Close()
    {
        winRecusa.Hide();
        txtComentario.Text = "";
        txtComentario.UpdateAfterCallBack = true;
    }
}
