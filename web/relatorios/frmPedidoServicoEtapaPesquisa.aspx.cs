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

public partial class frmPedidoServicoEtapaPesquisa : MarinhaPageBase
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
            Util.FillDropDownList(ddlDivisao, Celula.List(TipoCelula.Divisao, null), "Todas");
            Util.FillDropDownList(ddlGerente, Servidor.List(FuncaoServidor.GerenteDPCP), "Todos");
        }
    }
    #endregion  
    
    void btnImprimir_Click(object sender, EventArgs e)
    {
        Dictionary<string, string> list = new Dictionary<string, string>();
        list.Add("progem", ddlProgem.SelectedValue);
        list.Add("id_status", ddlStatus.SelectedValue);
        list.Add("id_celula", ddlDivisao.SelectedValue);
        list.Add("id_gerente", ddlGerente.SelectedValue);
        list.Add("id_equipamento", ucEquipamento.SelectedValue);
        list.Add("dataInicio", HttpUtility.UrlEncode(txtDataInicio.Text));
        list.Add("dataFim", HttpUtility.UrlEncode(txtDataInicio.Text));

        string address = "frmPedidoServicoEtapa.aspx?" + Util.GetUrlParameterString(list);

        Anthem.AnthemClientMethods.Popup(this, address, false, false, false, true, true, true, true,
            60, 60, 700, 500);
    }

 
}
