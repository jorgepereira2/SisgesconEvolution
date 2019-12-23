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
using Shared.NHibernateDAL;
using Shared.SessionState;
using Shared.Common;

public partial class frmAtividadeCadastro : CadastroSimples<Atividade>
{
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        RegistraControlesCadastro(dgCadastro, btnNovo);
        base.BeforeSave += new CadastroSimples<Atividade>.BeforeSaveEventHandler(frmAtividadeCadastro_BeforeSave);
        dgCadastro.ItemDataBound += new DataGridItemEventHandler(dgCadastro_ItemDataBound);
    }

    void dgCadastro_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if(e.Item.ItemType ==ListItemType.EditItem || e.Item.ItemType == ListItemType.Footer)
        {
            string controleID = "ddlApropriacao" + (e.Item.ItemType == ListItemType.Footer ? "Novo" : "");
            DropDownList ddlApropriacao = (DropDownList)e.Item.FindControl(controleID);
            Util.FillDropDownList(ddlApropriacao, Apropriacao.List(), ESCOLHA_OPCAO);
            
            if(e.Item.ItemType == ListItemType.EditItem)
            {
                Atividade atividade = (Atividade) e.Item.DataItem;
                ddlApropriacao.SelectedValue = ObjectReader.ReadID(atividade.Apropriacao);
            }
        }
    }

    void frmAtividadeCadastro_BeforeSave(object sender, BeforeSaveEventArgs<Atividade> e)
    {
        string controleID = "chkAtividadeDireta" + (e.IsNew ? "Novo" : "");
        CheckBox chkAtividadeDireta = (CheckBox)e.DataGridItem.FindControl(controleID);
        string controle2ID = "ddlApropriacao" + (e.IsNew ? "Novo" : "");
        DropDownList ddlApropriacao = (DropDownList)e.DataGridItem.FindControl(controle2ID);
        
        e.Object.FlagAtividadeDireta = chkAtividadeDireta.Checked;
        e.Object.Apropriacao = Apropriacao.Get(Convert.ToInt32(ddlApropriacao.SelectedValue));
    }
    #endregion

    #region Bind
    protected override void Bind()
    {
        BindToGrid(Atividade.Select());
    }
    #endregion
}
