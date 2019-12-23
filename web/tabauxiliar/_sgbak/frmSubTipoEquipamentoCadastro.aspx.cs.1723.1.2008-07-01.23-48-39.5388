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

public partial class frmSubTipoEquipamentoCadastro : CadastroSimples<SubTipoEquipamento>
{
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        RegistraControlesCadastro(dgCadastro, btnNovo);
        base.BeforeSave += new CadastroSimples<SubTipoEquipamento>.BeforeSaveEventHandler(frmSubTipoEquipamentoCadastro_BeforeSave);
        dgCadastro.ItemDataBound += new DataGridItemEventHandler(dgCadastro_ItemDataBound);
       
    }

    void dgCadastro_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if(e.Item.ItemType == ListItemType.EditItem)
        {
            DropDownList ddlTipoEquipamento = (DropDownList)e.Item.FindControl("ddlTipoEquipamento");
            FillTipoEquipamento(ddlTipoEquipamento);

            SubTipoEquipamento s = (SubTipoEquipamento) e.Item.DataItem;
            ddlTipoEquipamento.SelectedValue = s.TipoEquipamento.ID.ToString();
        }
        else  if(e.Item.ItemType == ListItemType.Footer)
        {
            DropDownList ddlTipoEquipamento = (DropDownList)e.Item.FindControl("ddlTipoEquipamentoNovo");
            FillTipoEquipamento(ddlTipoEquipamento);
        }
    }
    
    void FillTipoEquipamento(DropDownList ddl)
    {
        Util.FillDropDownList(ddl, TipoEquipamento.List());
    }

    void frmSubTipoEquipamentoCadastro_BeforeSave(object sender, BeforeSaveEventArgs<SubTipoEquipamento> e)
    {
        string controleID = "ddlTipoEquipamento" + (e.IsNew ? "Novo" : "");
        DropDownList ddlTipoEquipamento = (DropDownList)e.DataGridItem.FindControl(controleID);
        e.Object.TipoEquipamento = TipoEquipamento.Get(Convert.ToInt32(ddlTipoEquipamento.SelectedValue));
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    #endregion

    #region Bind
    protected override void Bind()
    {
        BindToGrid(SubTipoEquipamento.Select());
    }
    #endregion

 
}
