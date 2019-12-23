using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Marinha.Business;

using Shared.SessionState;
using ComponentArt.Web.UI;
using Shared.Common;

public partial class frmExtratoMovimentoConta : SortingPageBase
{

    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);       
		//this.RegisterSortingControl(gvPesquisa);
        ucColumn.ColumnsChanged += new EventHandler(ucColumn_ColumnsChanged);
    }

    void ucColumn_ColumnsChanged(object sender, EventArgs e)
    {
        Bind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
			Bind();
			ucColumn.SetValues();
        }
    }
    #endregion     

    
	protected override void Bind()
    {
        List<MovimentoConta> list = MovimentoConta.SelectExtrato(
            Convert.ToInt32(Request["id_conta"]),
            IsNull(HttpUtility.UrlDecode(Request["dataInicio"]), DateTime.MinValue),
            IsNull(HttpUtility.UrlDecode(Request["dataFim"]), DateTime.MinValue));

        gvPesquisa.DataSource = list;		
        gvPesquisa.DataBind();
		pnGrid.UpdateAfterCallBack = true;

	    lblConta.Text = Conta.Get(Convert.ToInt32(Request["id_conta"])).Descricao;
	    lblDataInicio.Text = Convert.ToDateTime(HttpUtility.UrlDecode(Request["dataInicio"])).ToShortDateString();
        lblDataFim.Text = Convert.ToDateTime(HttpUtility.UrlDecode(Request["dataFim"])).ToShortDateString();

	  
	    decimal saldo = MovimentoConta.SelectSaldo(Convert.ToInt32(Request["id_conta"]), Convert.ToDateTime(HttpUtility.UrlDecode(Request["dataInicio"])).Year, Convert.ToDateTime(HttpUtility.UrlDecode(Request["dataInicio"])).AddDays(-1));
    
        lblSaldoInicial.Text = saldo.ToString("c");
        
    }

    protected string GetSaldoFinal()
    {
        decimal saldo = MovimentoConta.SelectSaldo(Convert.ToInt32(Request["id_conta"]), Convert.ToDateTime(HttpUtility.UrlDecode(Request["dataInicio"])).Year, Convert.ToDateTime(HttpUtility.UrlDecode(Request["dataFim"])));

        return saldo.ToString("c");
    }
}


