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

public partial class RecusarAprovacao : System.Web.UI.UserControl
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
        if(PedidoRecusado != null)
            PedidoRecusado(this, new EventArgs());
       
    }

    void btnCancelar_Click(object sender, EventArgs e)
    {
        winRecusa.Hide();
    }

    public event EventHandler PedidoRecusado;    

    public string Justificativa
    {
        get { return txtJustificativa.Text; }
    }
    
    public void Show()
    {
        winRecusa.Show();
    }
    
    public void Close()
    {
        winRecusa.Hide();
        txtJustificativa.Text = "";
        txtJustificativa.UpdateAfterCallBack = true;
    }
}
