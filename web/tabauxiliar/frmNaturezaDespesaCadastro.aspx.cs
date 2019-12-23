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

public partial class frmNaturezaDespesaCadastro : CadastroSimples<NaturezaDespesa>
{
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        RegistraControlesCadastro(dgCadastro, btnNovo);
        base.BeforeSave += new CadastroSimples<NaturezaDespesa>.BeforeSaveEventHandler(frmNaturezaDespesaCadastro_BeforeSave);
        dgCadastro.ItemDataBound += new DataGridItemEventHandler(dgCadastro_ItemDataBound);
    }

    void dgCadastro_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.EditItem)
        {
            DropDownList ddlTipoNaturezaDespesa = (DropDownList)e.Item.FindControl("ddlTipoNaturezaDespesa");
            FillTipoNatureza(ddlTipoNaturezaDespesa);

            NaturezaDespesa s = (NaturezaDespesa)e.Item.DataItem;
            ddlTipoNaturezaDespesa.SelectedValue = s.TipoNaturezaDespesa.GetHashCode().ToString();
        }
        else if (e.Item.ItemType == ListItemType.Footer)
        {
            DropDownList ddlTipoNaturezaDespesa = (DropDownList)e.Item.FindControl("ddlTipoNaturezaDespesaNovo");
            FillTipoNatureza(ddlTipoNaturezaDespesa);
        }
    }

    void FillTipoNatureza(DropDownList ddl)
    {
        Util.FillDropDownList(ddl, typeof(TipoNaturezaDespesa));
    }

    void frmNaturezaDespesaCadastro_BeforeSave(object sender, BeforeSaveEventArgs<NaturezaDespesa> e)
    {
        string controleID = "txtCodigo" + (e.IsNew ? "Novo" : "");
        TextBox txtCodigo = (TextBox)e.DataGridItem.FindControl(controleID);
        e.Object.Codigo = txtCodigo.Text;

        string controleId2 = "ddlTipoNaturezaDespesa" + (e.IsNew ? "Novo" : "");
        DropDownList ddlTipoNaturezaDespesa = (DropDownList)e.DataGridItem.FindControl(controleId2);
        e.Object.TipoNaturezaDespesa = (TipoNaturezaDespesa)Convert.ToInt32(ddlTipoNaturezaDespesa.SelectedValue);

        string controleID3 = "txtObservacao" + (e.IsNew ? "Novo" : "");
        TextBox txtObservacao = (TextBox)e.DataGridItem.FindControl(controleID3);
        e.Object.Observacao = txtObservacao.Text;

       
    }
    #endregion

    #region Bind
    protected override void Bind()
    {
        BindToGrid(NaturezaDespesa.Select());
    }
    #endregion
}
