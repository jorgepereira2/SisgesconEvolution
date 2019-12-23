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

public partial class frmPedidoServicoMergulhoListagemPesquisa : MarinhaPageBase
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
            Util.FillDropDownList(ddlStatus, StatusPedidoServicoMergulho.List(), "Todos");
            Util.FillDropDownList(ddlDivisao, Celula.List(TipoCelula.Divisao, true), "Todas");
            Util.FillDropDownList(ddlAno, DateTimeManager.Anos(DateTime.Today.AddYears(-6).Year, DateTime.Today.Year), "Todos");
            ddlAno.SelectedValue = DateTime.Today.Year.ToString();

            ddlStatus.Items.Insert(1, new ListItem("Todos (Exceto Cancelados)", Int32.MinValue.ToString()));
            ddlStatus.SelectedIndex = 1;
        }
    }
    #endregion  
    
    void btnImprimir_Click(object sender, EventArgs e)
    {
        Dictionary<string, string> list = new Dictionary<string, string>();
        list.Add("id_status", ddlStatus.SelectedValue);
        list.Add("id_celula", ddlDivisao.SelectedValue);
        list.Add("ano", ddlAno.SelectedValue);
        list.Add("id_cliente", ucCliente.SelectedValue);
        list.Add("dataInicio", HttpUtility.UrlEncode(txtDataInicio.Text));
        list.Add("dataFim", HttpUtility.UrlEncode(txtDataFim.Text));
        list.Add("numeroRegistro", HttpUtility.UrlEncode(txtNumeroRegistro.Text));
        list.Add("observacao", HttpUtility.UrlEncode(txtObservacao.Text));
        list.Add("numeroPS", HttpUtility.UrlEncode(txtNumeroPS.Text));
        list.Add("dataPrevisaoEntregaInicio", HttpUtility.UrlEncode(txtDataPrevisaoEntregaInicio.Text));
        list.Add("dataPrevisaoEntregaFim", HttpUtility.UrlEncode(txtDataPrevisaoEntregaFim.Text));
        list.Add("dataRealizadoInicio", HttpUtility.UrlEncode(txtDataRealizadoInicio.Text));
        list.Add("dataRealizadoFim", HttpUtility.UrlEncode(txtDataRealizadoFim.Text));
        list.Add("codigoPedidoCliente", HttpUtility.UrlEncode(txtCodigoPedidoCliente.Text));

        string address = "frmPedidoServicoMergulhoListagem.aspx?" + Util.GetUrlParameterString(list);

        Anthem.AnthemClientMethods.Popup(this, address, false, false, false, true, true, true, true,
            60, 60, 700, 500);
    }
}
