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

public partial class frmFamodApropriacaoPorServicoPesquisa : MarinhaPageBase
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
            Util.FillDropDownList(ddlCategoriaServico, CategoriaServico.List(false), "Todos");
            Util.FillDropDownList(ddlDivisao, Celula.List(TipoCelula.Divisao, true), "Todas");
        }
    }
    #endregion  
    
    void btnImprimir_Click(object sender, EventArgs e)
    {
        Dictionary<string, string> list = new Dictionary<string, string>();
        list.Add("id_categoriaServico", ddlCategoriaServico.SelectedValue);
        list.Add("id_divisao", ddlDivisao.SelectedValue);
        list.Add("id_cliente", ucBuscaCliente.SelectedValue);
        list.Add("id_equipamento", ucBuscaEquipamento.SelectedValue);
        list.Add("dataInicio", HttpUtility.UrlEncode(txtDataInicio.Text));
        list.Add("dataFim", HttpUtility.UrlEncode(txtDataFim.Text));

        string address = "frmFamodApropriacaoPorServico.aspx?" + Util.GetUrlParameterString(list);

        Dictionary<string, string> textoFiltros = new Dictionary<string, string>();
        textoFiltros.Add("Categoria", ddlCategoriaServico.SelectedItem.Text);
        textoFiltros.Add("Divisão", ddlDivisao.SelectedItem.Text);
        textoFiltros.Add("Cliente", ucBuscaCliente.Text);
        textoFiltros.Add("Equipamento", ucBuscaEquipamento.Text);
        textoFiltros.Add("Data Início", txtDataInicio.Text);
        textoFiltros.Add("Data Fim", txtDataFim.Text);

        Session["frmFamodApropriacaoPorServicoPesquisa.textoFiltros"] = textoFiltros;

        Anthem.AnthemClientMethods.Popup(this, address, false, false, false, true, true, true, true,
            60, 60, 700, 500);
    }

 
}
