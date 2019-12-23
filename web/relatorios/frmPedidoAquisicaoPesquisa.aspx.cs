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

public partial class frmPedidoAquisicaoPesquisa : MarinhaPageBase
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
            Util.FillDropDownList(ddlStatus, StatusPedidoAquisicao.List(), "Todos");
            Util.FillDropDownList(ddlCelula, Celula.List(), "Todas");
            Util.FillDropDownList(ddlFornecedor, Fornecedor.List(), "Todos");
            Util.FillDropDownList(ddlTipo, TipoPedidoAquisicao.List(), "Todos");
            Util.FillDropDownList(ddlConta, Conta.List(), "Todas");
            Util.FillDropDownList(ddlMeta, Meta.List(), "Todas");
        }
    }
    #endregion  
    
    void btnImprimir_Click(object sender, EventArgs e)
    {
        Dictionary<string, string> list = new Dictionary<string, string>();
        list.Add("id_celula", ddlCelula.SelectedValue);
        list.Add("id_fornecedor", ddlFornecedor.SelectedValue);
        list.Add("id_tipo", ddlTipo.SelectedValue);
        list.Add("id_status", ddlStatus.SelectedValue);
        list.Add("id_conta", ddlConta.SelectedValue);
        list.Add("id_meta", ddlMeta.SelectedValue);
        list.Add("dataInicio", HttpUtility.UrlEncode(txtDataInicio.Text));
        list.Add("dataFim", HttpUtility.UrlEncode(txtDataInicio.Text));

        string address = "frmPedidoAquisicao.aspx?" + Util.GetUrlParameterString(list);

        Anthem.AnthemClientMethods.Popup(this, address, false, false, false, true, true, true, true,
            60, 60, 700, 500);
    }

 
}
