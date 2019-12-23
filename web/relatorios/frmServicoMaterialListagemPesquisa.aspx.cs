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

public partial class frmServicoMaterialListagemPesquisa : MarinhaPageBase
{
   
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.btnImprimir.Click += new EventHandler(btnImprimir_Click);
        ddlClasseServicoMaterial.SelectedIndexChanged += new EventHandler(ddlClasseServicoMaterial_SelectedIndexChanged);
    }

    void ddlClasseServicoMaterial_SelectedIndexChanged(object sender, EventArgs e)
    {
        Util.FillDropDownList(ddlSubClasseServicoMaterial, SubClasseServicoMaterial.List(Convert.ToInt32(ddlClasseServicoMaterial.SelectedValue)), "Todos");
        ddlSubClasseServicoMaterial.UpdateAfterCallBack = true;
    }
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            Util.FillDropDownList(ddlClasseServicoMaterial, ClasseServicoMaterial.List(), "Todos");
            Util.FillDropDownList(ddlSJB, SJB.List(), "Todos");
            Util.FillDropDownList(ddlNaturezaDespesa, NaturezaDespesa.List(), "Todas");
            Util.InsertDefaultItem(ddlSubClasseServicoMaterial, "Todos");
        }
    }
    #endregion  
    
    void btnImprimir_Click(object sender, EventArgs e)
    {
        Dictionary<string, string> list = new Dictionary<string, string>();
        list.Add("id_classeServicoMaterial", ddlClasseServicoMaterial.SelectedValue);
        list.Add("id_subClasseServicoMaterial", ddlSubClasseServicoMaterial.SelectedValue);
        list.Add("id_sjb", ddlSJB.SelectedValue);
        list.Add("id_naturezaDespesa", ddlNaturezaDespesa.SelectedValue);
        list.Add("texto", txtTexto.Text);

        string address = "frmServicoMaterialListagem.aspx?";
        if (ddlTipoRelatorio.SelectedValue == "2")
            address = "frmServicoMaterialPorNaturezaDespesa.aspx?";
        
        address += Util.GetUrlParameterString(list);

        Anthem.AnthemClientMethods.Popup(this, address, false, false, false, true, true, true, true,
            60, 60, 700, 500);
    }

 
}
