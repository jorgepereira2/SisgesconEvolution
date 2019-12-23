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

public partial class InputBox : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        btnCancelar.Click += delegate {winInputBox.Hide();};
        btnOk.Click += new EventHandler(btnOk_Click);
    }

    void btnOk_Click(object sender, EventArgs e)
    {
        if (TextoInformado != null)
            TextoInformado(this, new EventArgs());
    }

    void btnCancelar_Click(object sender, EventArgs e)
    {
        winInputBox.Hide();
    }

    public event EventHandler TextoInformado;    

    public string Texto
    {
        get { return txtTexto.Text; }
    }
    
    public void Show(int id_item, string label)
    {
        lblTexto.Text = label;
        lblTexto.UpdateAfterCallBack = true;
        this.ID_Item = id_item;
        winInputBox.Show();
    }
    
    public int ID_Item
    {
        get{ return Convert.ToInt32(ViewState["ID_Item"]);}
        private set{ ViewState["ID_Item"] = value;}
    }

    public bool ValidationEnabled
    {
        get { return valTexto.Enabled; }
        set { valTexto.Enabled = value; }
    }
    
    public void Close()
    {
        winInputBox.Hide();
        txtTexto.Text = "";
        txtTexto.UpdateAfterCallBack = true;
    }
}
