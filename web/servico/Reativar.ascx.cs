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

public partial class ucReativar : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            
        }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        btnCancelar.Click += delegate {winReativar.Hide();};
        btnOk.Click += new EventHandler(btnOk_Click);
    }

    void btnOk_Click(object sender, EventArgs e)
    {
        if (OkClick != null)
            OkClick(this, new EventArgs());
       
    }

    void btnCancelar_Click(object sender, EventArgs e)
    {
        winReativar.Hide();
    }

    public event EventHandler OkClick;    

    public bool ExcluirOrcamentos
    {
        get { return rbExcluir.Checked; }
    }

    
    public int ID_Objeto
    {
        get { return Convert.ToInt32(ViewState["ID_Objeto"]); }
    }

   
    
    public void Show(int id_objeto)
    {
        ViewState["ID_Objeto"] = id_objeto;
        winReativar.Show();
    }
    
    public void Close()
    {
        winReativar.Hide();
    }
}
