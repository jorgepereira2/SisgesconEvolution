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

public partial class DesignarCotador : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            Shared.Common.Util.FillDropDownList(ddlServidor, Servidor.List(FuncaoServidor.Comprador), "-- Escolha o Cotador --");
        }
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

    public event EventHandler OkClick;    

    public int ID_Servidor
    {
        get { return Convert.ToInt32(ddlServidor.SelectedValue); }
    }

    public string Comentario
    {
        get { return txtCometario.Text; }
    }
    
    public void Show()
    {
        winRecusa.Show();
    }
    
    public void Close()
    {
        winRecusa.Hide();
        ddlServidor.SelectedIndex = -1;
        ddlServidor.UpdateAfterCallBack = true;
    }
}
