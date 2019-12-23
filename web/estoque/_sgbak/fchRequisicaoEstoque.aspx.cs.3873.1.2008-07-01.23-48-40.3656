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


public partial class fchRequisicaoEstoque : MarinhaPageBase
{
    protected RequisicaoEstoque _requisicao;
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!this.IsPostBack)
		{
            _requisicao = RequisicaoEstoque.Get(Convert.ToInt32(Request["id_requisicao"]));
           
            dgItem.DataSource = _requisicao.Itens;
			Page.DataBind();
		}
	}
}
