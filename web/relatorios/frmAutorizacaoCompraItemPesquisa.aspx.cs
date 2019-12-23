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

public partial class frmAutorizacaoCompraItemPesquisa : MarinhaPageBase
{
   
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.btnImprimir.Click += new EventHandler(btnImprimir_Click);
        ddlclasseMaterial.SelectedIndexChanged += new EventHandler(ddlclasseMaterial_SelectedIndexChanged);
    }

    void ddlclasseMaterial_SelectedIndexChanged(object sender, EventArgs e)
    {
        Util.FillDropDownList(ddlSubClasseMaterial, SubClasseServicoMaterial.List(Convert.ToInt32(ddlclasseMaterial.SelectedValue)), "Todos");
        ddlSubClasseMaterial.UpdateAfterCallBack = true;
    }
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            Util.FillDropDownList(ddlSJB, SJB.List(), "Todos");
            Util.FillDropDownList(ddlFabricante, Fabricante.List(), "Todos");
            Util.FillDropDownList(ddlclasseMaterial, ClasseServicoMaterial.List(), "Todos");
            Util.InsertDefaultItem(ddlSubClasseMaterial, "Todos");
        }
    }
    #endregion  
    
    void btnImprimir_Click(object sender, EventArgs e)
    {
        Dictionary<string, string> list = new Dictionary<string, string>();
        list.Add("id_classeServicoMaterial", ddlclasseMaterial.SelectedValue);
        list.Add("id_subClasseServicoMaterial", ddlSubClasseMaterial.SelectedValue);
        list.Add("id_sjb", ddlSJB.SelectedValue);
        list.Add("id_fabricante", ddlFabricante.SelectedValue);
        list.Add("id_servicoMaterial", ucServicoMaterial.SelectedValue);
        list.Add("id_fornecedor", ucFornecedor.SelectedValue);
        list.Add("numeroPO", HttpUtility.UrlEncode(txtNumeroPO.Text));
        list.Add("texto", HttpUtility.UrlEncode(txtTexto.Text));
       

        string address = "frmAutorizacaoCompraItem.aspx?" + Util.GetUrlParameterString(list);

        Anthem.AnthemClientMethods.Popup(this, address, false, false, false, true, true, true, true,
            60, 60, 700, 500);
    }

 
}
