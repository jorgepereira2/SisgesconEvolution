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

public partial class DescartarItem : System.Web.UI.UserControl
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
        if (ItemDescartado != null)
            ItemDescartado(this, new EventArgs());
       
    }

    void btnDescartar_Click(object sender, EventArgs e)
    {
        winRecusa.Hide();
    }

    public event EventHandler ItemDescartado;    

    public string Justificativa
    {
        get { return txtJustificativa.Text; }
    }
    
    public void Show(int id_item)
    {
        this.ID_Item = id_item;
        winRecusa.Show();
    }
    
    public int ID_Item
    {
        get{ return Convert.ToInt32(ViewState["ID_Item"]);}
        private set{ ViewState["ID_Item"] = value;}
    }
    
    public void Close()
    {
        winRecusa.Hide();
        txtJustificativa.Text = "";
        txtJustificativa.UpdateAfterCallBack = true;
    }
}
