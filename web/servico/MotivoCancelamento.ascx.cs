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

public partial class ucMotivoCancelamento : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            Shared.Common.Util.FillDropDownList(ddlMotivoCancelamento, MotivoCancelamento.List(_tipoObjeto), "-- Escolha uma opção --");
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

    public int ID_MotivoCancelamento
    {
        get { return Convert.ToInt32(ddlMotivoCancelamento.SelectedValue); }
    }

    public string Comentario
    {
        get { return txtComentario.Text; }
    }


    public int ID_Objeto
    {
        get { return Convert.ToInt32(ViewState["ID_Objeto"]); }
    }

    private ObjetoCancelavel _tipoObjeto;

    public virtual ObjetoCancelavel TipoObjeto
    {
        get { return _tipoObjeto; }
        set { _tipoObjeto = value; }
    }
    
    public void Show(int id_objeto)
    {
        ViewState["ID_Objeto"] = id_objeto;
        winRecusa.Show();
    }
    
    public void Close()
    {
        winRecusa.Hide();
        ddlMotivoCancelamento.SelectedIndex = -1;
        ddlMotivoCancelamento.UpdateAfterCallBack = true;
        txtComentario.Text = "";
        txtComentario.UpdateAfterCallBack = true;
    }
}
