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

public partial class UserControls_CabecalhoRelatorio : System.Web.UI.UserControl
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Parametro parametro = Parametro.Get();
        lblOrganizacaoMilitar.Text = parametro.OrganizacaoMilitar;
    }
    
    public string Titulo
    {
        get{ return lblTitulo.Text;}
        set{ lblTitulo.Text = value;}
    }
}
