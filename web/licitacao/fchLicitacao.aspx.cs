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
using Shared.NHibernateDAL;


public partial class fchLicitacao : MarinhaPageBase
{
    protected Licitacao _licitacao;
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!this.IsPostBack)
		{
		    _licitacao = Licitacao.Get(Convert.ToInt32(Request["id_licitacao"]));
		    dgItem.DataSource = _licitacao.Itens;
            dlHistorico.DataSource = _licitacao.Historico;
			Page.DataBind();

		    
            

		}
	}
}
