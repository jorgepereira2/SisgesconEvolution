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

public partial class frmContaCadastro : CadastroSimples<Conta>
{
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        RegistraControlesCadastro(dgCadastro, btnNovo);
        base.BeforeSave += new CadastroSimples<Conta>.BeforeSaveEventHandler(frmContaCadastro_BeforeSave);
        dgCadastro.ItemDataBound += new DataGridItemEventHandler(dgCadastro_ItemDataBound);
       
    }

    void dgCadastro_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.EditItem || e.Item.ItemType == ListItemType.Footer)
        {
            DropDownList ddlProjeto = (DropDownList)e.Item.FindControl("ddlProjeto");
            Util.FillDropDownList(ddlProjeto, Projeto.List());
            DropDownList ddlFase = (DropDownList)e.Item.FindControl("ddlFase");
            Util.FillDropDownList(ddlFase, Fase.List());
            DropDownList ddlNaturezaDespesa = (DropDownList)e.Item.FindControl("ddlNaturezaDespesa");
            Util.FillDropDownList(ddlNaturezaDespesa, NaturezaDespesa.List());
            DropDownList ddlAno = (DropDownList)e.Item.FindControl("ddlAno");
            Util.FillDropDownList(ddlAno, DateTimeManager.Anos(2008, DateTime.Today.Year + 1));
            DropDownList ddlPTRES = (DropDownList)e.Item.FindControl("ddlPTRES");
            Util.FillDropDownList(ddlPTRES, PTRES.List());
            DropDownList ddlUGE = (DropDownList)e.Item.FindControl("ddlUGE");
            Util.FillDropDownList(ddlUGE, UGE.List());

            if (e.Item.ItemType == ListItemType.EditItem)
            {
                Conta conta = (Conta)e.Item.DataItem;
                ddlProjeto.SelectedValue = conta.Projeto.ID.ToString();
                ddlFase.SelectedValue = conta.Fase.ID.ToString();
                ddlNaturezaDespesa.SelectedValue = conta.NaturezaDespesa.ID.ToString();
                ddlAno.SelectedValue = conta.Ano.ToString();
                ddlPTRES.SelectedValue = ReadID(conta.PTRES);
                ddlUGE.SelectedValue = ReadID(conta.UGE);
            }
        }
    }
    
    void frmContaCadastro_BeforeSave(object sender, BeforeSaveEventArgs<Conta> e)
    {
        DropDownList ddlProjeto = (DropDownList)e.DataGridItem.FindControl("ddlProjeto");
        DropDownList ddlFase = (DropDownList)e.DataGridItem.FindControl("ddlFase");
        DropDownList ddlNaturezaDespesa = (DropDownList)e.DataGridItem.FindControl("ddlNaturezaDespesa");
        DropDownList ddlAno = (DropDownList)e.DataGridItem.FindControl("ddlAno");
        DropDownList ddlPTRES = (DropDownList)e.DataGridItem.FindControl("ddlPTRES");
        DropDownList ddlUGE = (DropDownList)e.DataGridItem.FindControl("ddlUGE");
        
        
        e.Object.Projeto = Projeto.Get(Convert.ToInt32(ddlProjeto.SelectedValue));
        e.Object.Fase = Fase.Get(Convert.ToInt32(ddlFase.SelectedValue));
        e.Object.NaturezaDespesa = NaturezaDespesa.Get(Convert.ToInt32(ddlNaturezaDespesa.SelectedValue));
        e.Object.Ano = Convert.ToInt32(ddlAno.SelectedValue);
        e.Object.PTRES = PTRES.Get(Convert.ToInt32(ddlPTRES.SelectedValue));
        e.Object.UGE = UGE.Get(Convert.ToInt32(ddlUGE.SelectedValue));
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    #endregion

    #region Bind
    protected override void Bind()
    {
        BindToGrid(Conta.Select());
    }
    #endregion

 
}
