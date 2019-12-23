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

public partial class frmFamodRelatorioPesquisa : MarinhaPageBase
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
            Util.FillDropDownList(ddlAtividade, Atividade.List(), "Todas");
            Util.FillDropDownList(ddlOficina, Celula.List(null, true), "Todas");
            Util.FillDropDownList(ddlServidor, Servidor.List(null), "Todos");
            Util.FillDropDownList(ddlSituacao, typeof(SituacaoFAMOD), "Todas");
            ddlSituacao.SelectedValue = Convert.ToInt32(SituacaoFAMOD.Aprovado).ToString();
        }
    }
    #endregion  
    
    void btnImprimir_Click(object sender, EventArgs e)
    {
        Dictionary<string, string> list = new Dictionary<string, string>();
        list.Add("id_atividade", ddlAtividade.SelectedValue);
        list.Add("tipoAtividade", ddlTipoAtividade.SelectedValue);
        list.Add("id_oficina", ddlOficina.SelectedValue);
        list.Add("id_servidor", ddlServidor.SelectedValue);
        list.Add("id_situacao", ddlSituacao.SelectedValue);
        list.Add("dataInicio", HttpUtility.UrlEncode(txtDataInicio.Text));
        list.Add("dataFim", HttpUtility.UrlEncode(txtDataFim.Text));

        string address = "frmFamodRelatorio.aspx?" + Util.GetUrlParameterString(list);

        Anthem.AnthemClientMethods.Popup(this, address, false, false, false, true, true, true, true,
            60, 60, 700, 500);
    }

 
}
