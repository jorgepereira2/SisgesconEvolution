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

public partial class EnviarParaDelineamento : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            Shared.Common.Util.FillDropDownList(ddlCategoriaServico, CategoriaServico.List(false), "-- Escolha uma opção --");
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

    void btnCancelar_Click(object sender, EventArgs e)
    {
        winRecusa.Hide();
    }

    public event EventHandler OkClick;    

    public int ID_CategoriaServico
    {
        get { return Convert.ToInt32(ddlCategoriaServico.SelectedValue); }
    }
    
    public void Show()
    {
        winRecusa.Show();
    }
    
    public void Close()
    {
        winRecusa.Hide();
        ddlCategoriaServico.SelectedIndex = -1;
        ddlCategoriaServico.UpdateAfterCallBack = true;
    }
}
