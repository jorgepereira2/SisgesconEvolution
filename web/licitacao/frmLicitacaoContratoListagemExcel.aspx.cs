using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Linq;
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

public partial class frmLicitacaoContratoListagemExcel : SortingPageBase
{

    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);       
		this.RegisterSortingControl(gvPesquisa);
        ucColumn.ColumnsChanged += new EventHandler(ucColumn_ColumnsChanged);
        gvPesquisa.RowDataBound += new GridViewRowEventHandler(gvPesquisa_RowDataBound);
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
		List<Licitacao> list = Licitacao.Select(
			HttpUtility.UrlDecode(Request["numero"]),
            Convert.ToInt32(Request["id_status"]),
            IsNull(HttpUtility.UrlDecode(Request["dataEmissaoInicio"]), DateTime.MinValue),
            IsNull(HttpUtility.UrlDecode(Request["dataEmissaoFim"]), DateTime.MinValue),
			Convert.ToInt32(Request["id_fornecedor"]),
            Convert.ToInt32(Request["id_material"]),
            Convert.ToInt32(Request["ano"])
			);

	    var contratos = list.SelectMany(x => x.Contratos).ToList();

		this.Sort(contratos);

        gvPesquisa.DataSource = contratos;		
        gvPesquisa.DataBind();
		pnGrid.UpdateAfterCallBack = true;

    }

    void gvPesquisa_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if(e.Row.RowType == DataControlRowType.DataRow)
        {
            LicitacaoContrato contrato = (LicitacaoContrato) e.Row.DataItem;

            LinkButton btnDetalhes = (LinkButton)e.Row.FindControl("lnkDetalhes");
            Anthem.AnthemClientMethods.Popup(btnDetalhes,
                                             "fchLicitacao.aspx?id_licitacao=" + contrato.Licitacao.ID,
                                             "detalhe_licitacao",
                                             false, false, false, true, true, true, true, 10, 40, 700, 520, false);

            if((Request["Validade"] == "1" && contrato.DataVigencia < DateTime.Today) || (Request["Validade"] == "2" && contrato.DataVigencia > DateTime.Today))
                e.Row.Visible = false;
        }
    }
}


