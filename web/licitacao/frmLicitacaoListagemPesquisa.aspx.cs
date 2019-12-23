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

public partial class frmLicitacaoListagemPesquisa : MarinhaPageBase
{
   
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.btnImprimir.Click += new EventHandler(btnImprimir_Click);
		
    }
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            Util.FillDropDownList(ddlFornecedor, Fornecedor.List(), "Todos");
            Util.FillDropDownList(ddlStatus, typeof(StatusLicitacaoEnum), "Todos");
            Util.FillDropDownList(ddlAno, DateTimeManager.Anos(DateTime.Today.AddYears(-6).Year, DateTime.Today.Year), "Todos");
            ddlAno.SelectedValue = DateTime.Today.Year.ToString();
            ddlValidade.SelectedValue = "1";
        }
    }
    #endregion  
    
    void btnImprimir_Click(object sender, EventArgs e)
    {
        Dictionary<string, string> list = new Dictionary<string, string>();
        list.Add("id_fornecedor", ddlFornecedor.SelectedValue);
        list.Add("id_material", ucBuscaMaterial.SelectedValue);
        list.Add("id_status", ddlStatus.SelectedValue);
        list.Add("dataEmissaoInicio", txtDataInicio.Text);
        list.Add("dataEmissaoFim", txtDataFim.Text);
        list.Add("numero", txtNumero.Text);
        list.Add("validade", ddlValidade.SelectedValue);
        list.Add("ano", ddlAno.SelectedValue);

        string pagina = "frmLicitacaoListagem.aspx?";
        if (ddlTipoRelatorio.SelectedValue == "1")
            pagina = "frmLicitacaoItemListagem.aspx?";
        else if (ddlTipoRelatorio.SelectedValue == "2")
            pagina = "frmLicitacaoContratoListagem.aspx?";
        else if (ddlTipoRelatorio.SelectedValue == "3")
            pagina = "frmLicitacaoContratoListagemExcel.aspx?";

        string address = pagina + Util.GetUrlParameterString(list);

        Anthem.AnthemClientMethods.Popup(this, address, false, false, false, true, true, true, true,
            60, 60, 700, 500);
    }

 
}
