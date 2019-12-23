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
using Marinha.Business;

public partial class RecebedorEmpenhoPO : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        btnCancelar.Click += delegate {winDados.Hide();};
        btnOk.Click += new EventHandler(btnOk_Click);
    }


    void btnOk_Click(object sender, EventArgs e)
    {
        Close();
        if (OkClicked != null)
            OkClicked(this, new EventArgs());
       
    }

    void btnCancelar_Click(object sender, EventArgs e)
    {
        winDados.Hide();
    }

    public event EventHandler OkClicked;
    

    public string NomeRecebedorEmpenho
    {
        get { return txtNomeRecebedorEmpenho.Text; }
    }

    public string TelefoneRecebedorEmpenho
    {
        get { return txtTelefoneRecebedorEmpenho.Text; }
    }
    
    public void Show()
    {
        txtNomeRecebedorEmpenho.Text = "";
        txtNomeRecebedorEmpenho.UpdateAfterCallBack = true;
        
        txtTelefoneRecebedorEmpenho.Text = "";
        txtTelefoneRecebedorEmpenho.UpdateAfterCallBack = true;
        winDados.Show();
    }
    
    public void Close()
    {
        winDados.Hide();
      
    }
}
