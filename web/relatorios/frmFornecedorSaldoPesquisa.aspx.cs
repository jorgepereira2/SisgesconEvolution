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

public partial class frmFornecedorSaldoPesquisa : MarinhaPageBase
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
            Util.FillDropDownList(ddlTipoFornecedor, TipoFornecedor.List(), "Todos");
            Util.FillDropDownList(ddlTipoCompra, TipoCompra.List());
            Util.FillDropDownList(ddlAno, DateTimeManager.Anos(2008, DateTime.Now.Year));
            ddlAno.SelectedValue = DateTime.Today.Year.ToString();
        }
    }
    #endregion  
    
    void btnImprimir_Click(object sender, EventArgs e)
    {
        Dictionary<string, string> list = new Dictionary<string, string>();
        list.Add("id_tipoFornecedor", ddlTipoFornecedor.SelectedValue);
        list.Add("id_tipoCompra", ddlTipoCompra.SelectedValue);
        list.Add("ano", ddlAno.SelectedValue);
        list.Add("texto", txtTexto.Text);
        list.Add("materialFornecido", txtMaterialFornecido.Text);

        string address = "frmFornecedorSaldo.aspx?" + Util.GetUrlParameterString(list);

        Anthem.AnthemClientMethods.Popup(this, address, false, false, false, true, true, true, true,
            60, 60, 700, 500);
    }

 
}
