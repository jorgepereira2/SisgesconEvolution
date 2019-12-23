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

public partial class frmPedidoServicoListagemPesquisa : MarinhaPageBase
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
            Util.FillDropDownList(ddlStatus, StatusPedidoServico.List(), "Todos");
            Util.FillDropDownList(ddlStatusHistorico, StatusPedidoServico.List(), "Todos");
            Util.FillDropDownList(ddlDivisao, Celula.List(TipoCelula.Divisao, true), "Todas");
            Util.FillDropDownList(ddlGerente, Servidor.List(FuncaoServidor.GerenteDPCP), "Todos");
            Util.FillDropDownList(ddlAno, DateTimeManager.Anos(DateTime.Today.AddYears(-6).Year, DateTime.Today.Year), "Todos");
            Util.FillDropDownList(ddlCotador, Servidor.List(FuncaoServidor.Comprador), "Todos");
            Util.FillDropDownList(ddlOficinaDelineamento, Celula.List(null, true), "Todas");
            ddlAno.SelectedValue = DateTime.Today.Year.ToString();

            ddlStatus.Items.Insert(1, new ListItem("Todos (Exceto Cancelados)", Int32.MinValue.ToString()));
            ddlStatus.SelectedIndex = 1;
        }
    }
    #endregion  
    
    void btnImprimir_Click(object sender, EventArgs e)
    {
        if((txtDataHistoricoInicio.Text != "" && txtDataHistoricoFim.Text == "") || (txtDataHistoricoInicio.Text == "" && txtDataHistoricoFim.Text != "") ||
            (ddlStatusHistorico.SelectedValue != "0" && (txtDataHistoricoFim.Text == "" || txtDataHistoricoInicio.Text == "")))
        {
            ShowMessage("Para pesquisar o histórico forneça as duas datas.");
            return;
        }
        Dictionary<string, string> list = new Dictionary<string, string>();
        list.Add("progem", ddlProgem.SelectedValue);
        list.Add("id_status", ddlStatus.SelectedValue);
        list.Add("id_celula", ddlDivisao.SelectedValue);
        list.Add("id_gerente", ddlGerente.SelectedValue);
        list.Add("ano", ddlAno.SelectedValue);
        list.Add("id_equipamento", ucEquipamento.SelectedValue);
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
        list.Add("id_cotador", ddlCotador.SelectedValue);
        list.Add("id_oficina", ddlOficinaDelineamento.SelectedValue);
        list.Add("codigoPedidoCliente", HttpUtility.UrlEncode(txtCodigoPedidoCliente.Text));
        list.Add("id_statusHistorico", ddlStatusHistorico.SelectedValue);
        list.Add("dataHistoricoInicio", HttpUtility.UrlEncode(txtDataHistoricoInicio.Text));
        list.Add("dataHistoricoFim", HttpUtility.UrlEncode(txtDataHistoricoFim.Text));
        list.Add("recusado", chkRecusado.Checked.ToString());

        string address = "frmPedidoServicoListagem.aspx?" + Util.GetUrlParameterString(list);

        Anthem.AnthemClientMethods.Popup(this, address, false, false, false, true, true, true, true,
            60, 60, 700, 500);
    }
}
