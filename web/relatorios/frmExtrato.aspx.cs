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

public partial class frmExtrato : SortingPageBase
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
        List<EntradaValores> list = EntradaValores.SelectExtrato(
            Convert.ToInt32(Request["id_projeto"]),
            Convert.ToInt32(Request["id_naturezaDespesa"]),
            Convert.ToInt32(Request["id_fonteRecurso"]),
            Convert.ToInt32(Request["id_ptres"]),
            IsNull(HttpUtility.UrlDecode(Request["dataInicio"]), DateTime.MinValue),
            IsNull(HttpUtility.UrlDecode(Request["dataFim"]), DateTime.MinValue));

        gvPesquisa.DataSource = list;		
        gvPesquisa.DataBind();
		pnGrid.UpdateAfterCallBack = true;

	    lblPTRES.Text = PTRES.Get(Convert.ToInt32(Request["id_ptres"])).Descricao;
        lblNaturezaDespesa.Text = NaturezaDespesa.Get(Convert.ToInt32(Request["id_naturezaDespesa"])).Descricao;
        lblFonteRecurso.Text = FonteRecurso.Get(Convert.ToInt32(Request["id_fonteRecurso"])).Descricao;
        lblProjeto.Text = Projeto.Get(Convert.ToInt32(Request["id_projeto"])).Descricao;
	    lblDataInicio.Text = Convert.ToDateTime(HttpUtility.UrlDecode(Request["dataInicio"])).ToShortDateString();
        lblDataFim.Text = Convert.ToDateTime(HttpUtility.UrlDecode(Request["dataFim"])).ToShortDateString();

	    DataSet ds = EntradaValores.SelectAgrupado(Convert.ToInt32(Request["id_projeto"]),
	                                               Convert.ToInt32(Request["id_natrezaDespesa"]),
	                                               Convert.ToInt32(Request["id_fonteRecurso"]),
	                                               Convert.ToInt32(Request["id_ptres"]),
	                                               DateTime.MinValue,
	                                               Convert.ToDateTime(HttpUtility.UrlDecode(Request["dataInicio"])).AddDays(-1));
        if (ds.Tables[0].Rows.Count == 0)
            lblSaldoInicial.Text = 0.ToString("c");
        else
            lblSaldoInicial.Text = (Convert.ToDecimal(ds.Tables[0].Rows[0]["ValorEntrada"]) - Convert.ToDecimal(ds.Tables[0].Rows[0]["ValorEmpenhado"])).ToString("c");
    }

    protected string GetSaldoFinal()
    {
        DataSet ds = EntradaValores.SelectAgrupado(Convert.ToInt32(Request["id_projeto"]),
                                                  Convert.ToInt32(Request["id_natrezaDespesa"]),
                                                  Convert.ToInt32(Request["id_fonteRecurso"]),
                                                  Convert.ToInt32(Request["id_ptres"]),
                                                  DateTime.MinValue,
                                                  Convert.ToDateTime(HttpUtility.UrlDecode(Request["dataFim"])));
        if (ds.Tables[0].Rows.Count == 0)
            return 0.ToString("c");
        else
            return (Convert.ToDecimal(ds.Tables[0].Rows[0]["ValorEntrada"]) - Convert.ToDecimal(ds.Tables[0].Rows[0]["ValorEmpenhado"])).ToString("c");
    }
}


