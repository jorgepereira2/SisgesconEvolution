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

public partial class servico_DetalhamentoOrcamento : System.Web.UI.UserControl
{
    protected DelineamentoOrcamento _orcamento;
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    
    public DelineamentoOrcamento Orcamento
    {
        set{ _orcamento = value;}
    }
}
