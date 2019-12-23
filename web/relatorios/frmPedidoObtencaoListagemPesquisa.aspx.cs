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

public partial class frmPedidoObtencaoListagemPesquisa : MarinhaPageBase
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
            Util.FillDropDownList(ddlStatus, StatusPedidoObtencao.List(), "Todos");
            Util.FillDropDownList(ddlCelula, Celula.List(), "Todas");
            Util.FillDropDownList(ddlServidor, Servidor.List(null), "Todos");
            Util.FillDropDownList(ddlTipoPedido, typeof(TipoPedido), "Todos");
            Util.FillDropDownList(ddlAno, DateTimeManager.Anos(DateTime.Today.AddYears(-6).Year, DateTime.Today.Year), "Todos");
            ddlAno.SelectedValue = DateTime.Today.Year.ToString();
            Util.FillDropDownList(ddlDepartamento, Celula.List(TipoCelula.Departamento), "Todos");

            ddlStatus.Items.Insert(1, new ListItem("Todos (Exceto Cancelados)", Int32.MinValue.ToString()));
            ddlStatus.SelectedIndex = 1;

            KeyedList list = DateTimeManager.Anos(DateTime.Today.AddYears(-6).Year, DateTime.Today.Year);
        }
    }

    
    #endregion  
    
    void btnImprimir_Click(object sender, EventArgs e)
    {
        Dictionary<string, string> list = new Dictionary<string, string>();
        list.Add("id_celula", ddlCelula.SelectedValue);
        list.Add("id_servidor", ddlServidor.SelectedValue);
        list.Add("id_status", ddlStatus.SelectedValue);
        list.Add("id_tipoPedido", ddlTipoPedido.SelectedValue);
        list.Add("flagDireto", ddlDireto.SelectedValue);
        list.Add("dataInicio", HttpUtility.UrlEncode(txtDataInicio.Text));
        list.Add("dataFim", HttpUtility.UrlEncode(txtDataInicio.Text));
        list.Add("numeroPO", HttpUtility.UrlEncode(txtNumeroPO.Text));
        list.Add("ano", ddlAno.SelectedValue);
        list.Add("id_departamento", ddlDepartamento.SelectedValue);

        string address = "frmPedidoObtencaoListagem.aspx?" + Util.GetUrlParameterString(list);

        Anthem.AnthemClientMethods.Popup(this, address, false, false, false, true, true, true, true,
            60, 60, 700, 500);
    }

 
}
