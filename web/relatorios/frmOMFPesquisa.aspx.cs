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

public partial class frmOMFPesquisa : MarinhaPageBase
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
            Util.FillDropDownList(ddlStatus, StatusOMF.List(), "Todos");
            Util.FillDropDownList(ddlRecebedor, Servidor.List(null), "Todos");
            Util.FillDropDownList(ddlTipoEmprego, TipoEmprego.List(), "Todos");
            //ddlStatus.Items.Insert(1, new ListItem("Todos (Exceto Cancelados)", Int32.MinValue.ToString()));
        }
    }
    #endregion  
    
    void btnImprimir_Click(object sender, EventArgs e)
    {
        Dictionary<string, string> list = new Dictionary<string, string>();
        list.Add("id_recebedor", ddlRecebedor.SelectedValue);
        list.Add("id_tipoEmprego", ddlTipoEmprego.SelectedValue);
        list.Add("id_status", ddlStatus.SelectedValue);
        list.Add("numeroEmpenho", HttpUtility.UrlEncode(txtNumeroEmpenho.Text));
        list.Add("numeroNota", HttpUtility.UrlEncode(txtNumeroNota.Text));
        list.Add("id_fornecedor", ucBuscaFornecedor.SelectedValue);
        list.Add("dataInicio", HttpUtility.UrlEncode(txtDataInicio.Text));
        list.Add("dataFim", HttpUtility.UrlEncode(txtDataInicio.Text));

        
        string address = ddlTipoRelatorio.SelectedValue + Util.GetUrlParameterString(list);

        Anthem.AnthemClientMethods.Popup(this, address, false, false, false, true, true, true, true,
            60, 60, 700, 500);
    }

 
}
