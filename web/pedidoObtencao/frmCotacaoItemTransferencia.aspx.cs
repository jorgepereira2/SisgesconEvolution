using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Drawing;
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

public partial class frmCotacaoItemTransferencia : SortingPageBase
{
   
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.btnPesquisar.Click += new EventHandler(btnPesquisar_Click);
		this.RegisterSortingControl(this.dgPesquisa);
		dgPesquisa.ItemDataBound += DgPesquisa_OnItemDataBound;
        dgPesquisa.EditCommand += DgPesquisa_OnEditCommand;      
        dgPesquisa.CancelCommand += DgPesquisa_OnCancelCommand;
        dgPesquisa.UpdateCommand += DgPesquisa_OnUpdateCommand;
    }

    void btnPesquisar_Click(object sender, EventArgs e)
    {
        Bind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            Anthem.Manager.Register(this);

            Util.FillDropDownList(ddlComprador, Servidor.List(FuncaoServidor.Comprador), "Todos");
        }
    }
    #endregion  
    
    protected override void Bind()
    {
        List<PedidoObtencaoItem> list = PedidoObtencaoItem.SelectItemCotacaoPendente(
            Convert.ToInt32(ddlComprador.SelectedValue),
            Convert.ToInt32(ucServicoMaterial.SelectedValue));
		this.Sort(list);
		dgPesquisa.DataSource = list;
        dgPesquisa.DataKeyField = "ID";
        dgPesquisa.DataBind();
        dgPesquisa.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();

        dgPesquisa.Visible = list.Count > 0;
        pnMensagem.Visible = list.Count == 0;
        pnMensagem.UpdateAfterCallBack = true;
    }
       
    private void DgPesquisa_OnUpdateCommand(object source, DataGridCommandEventArgs e)
    {
        int id = Convert.ToInt32(dgPesquisa.DataKeys[e.Item.ItemIndex]);
        PedidoObtencaoItem item = PedidoObtencaoItem.Get(id);
        DropDownList ddlNovoComprador = (DropDownList)e.Item.FindControl("ddlNovoComprador");
        //item.Comprador = Servidor.Get(Convert.ToInt32(ddlNovoComprador.SelectedValue));
        item.Save();
        dgPesquisa.EditItemIndex = -1;
        Bind();
    }

    private void DgPesquisa_OnCancelCommand(object source, DataGridCommandEventArgs e)
    {
        dgPesquisa.EditItemIndex = -1;
        Bind();
    }

    private void DgPesquisa_OnEditCommand(object source, DataGridCommandEventArgs e)
    {
        dgPesquisa.EditItemIndex = e.Item.ItemIndex;
        Bind();
    }

    private void DgPesquisa_OnItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if(e.Item.ItemType == ListItemType.EditItem)
        {
            DropDownList ddlNovoComprador = (DropDownList)e.Item.FindControl("ddlNovoComprador");
            PedidoObtencaoItem item = (PedidoObtencaoItem)e.Item.DataItem;

            Util.FillDropDownList(ddlNovoComprador, Servidor.List(FuncaoServidor.Comprador));
           // ddlNovoComprador.SelectedValue = item.Comprador.ID.ToString();
        }
    }
}
