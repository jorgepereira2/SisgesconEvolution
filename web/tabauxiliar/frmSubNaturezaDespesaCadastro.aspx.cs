using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Marinha.Business;
using Shared.SessionState;
using Shared.Common;

public partial class frmSubNaturezaDespesaCadastro : CadastroSimples<SubNaturezaDespesa>
{
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        RegistraControlesCadastro(dgCadastro, btnNovo);
        base.BeforeSave += new CadastroSimples<SubNaturezaDespesa>.BeforeSaveEventHandler(frmSubNaturezaDespesaCadastro_BeforeSave);
        dgCadastro.ItemDataBound += new DataGridItemEventHandler(dgCadastro_ItemDataBound);
       
    }

    void dgCadastro_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if(e.Item.ItemType == ListItemType.EditItem)
        {
            var chkIndustrial = (CheckBox)e.Item.FindControl("chkIndustrial");
            var ddlNaturezaDespesa = (DropDownList)e.Item.FindControl("ddlNaturezaDespesa");
            FillNaturezaDespesa(ddlNaturezaDespesa);

            var s = (SubNaturezaDespesa) e.Item.DataItem;
            ddlNaturezaDespesa.SelectedValue = s.NaturezaDespesa.ID.ToString();
            chkIndustrial.Checked = s.Industrial;
        }
        else  if(e.Item.ItemType == ListItemType.Footer)
        {
            DropDownList ddlNaturezaDespesa = (DropDownList)e.Item.FindControl("ddlNaturezaDespesaNovo");
            FillNaturezaDespesa(ddlNaturezaDespesa);
        }
    }
    
    void FillNaturezaDespesa(DropDownList ddl)
    {
        Util.FillDropDownList(ddl, NaturezaDespesa.List());
    }

    void frmSubNaturezaDespesaCadastro_BeforeSave(object sender, BeforeSaveEventArgs<SubNaturezaDespesa> e)
    {
        string controleID = "ddlNaturezaDespesa" + (e.IsNew ? "Novo" : "");
        DropDownList ddlNaturezaDespesa = (DropDownList)e.DataGridItem.FindControl(controleID);
        e.Object.NaturezaDespesa = NaturezaDespesa.Get(Convert.ToInt32(ddlNaturezaDespesa.SelectedValue));
        var chkIndustrial = (CheckBox)e.DataGridItem.FindControl("chkIndustrial");
        e.Object.Industrial = chkIndustrial.Checked;

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    #endregion

    #region Bind
    protected override void Bind()
    {
        BindToGrid(SubNaturezaDespesa.Select());
    }
    #endregion

 
}
