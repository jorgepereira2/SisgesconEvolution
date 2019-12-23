using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Marinha.Business;
using Shared.Common;
using Shared.NHibernateDAL;
using Shared.SessionState;

public partial class frmPedidoCotacaoItemDescartado : MarinhaPageBase
{
    
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.dgItem.ItemDataBound += dgItem_OnItemDataBound;
        dgItem.DeleteCommand += new DataGridCommandEventHandler(dgItem_DeleteCommand);
        ucJustificativaReativacao.JustificativaInformada += new EventHandler(ucJustificativaReativacao_JustificativaInformada);
        dgItem.EditCommand += new DataGridCommandEventHandler(dgItem_EditCommand);
        ucJustificativaCancelamento.JustificativaInformada += new EventHandler(ucJustificativaCancelamento_JustificativaInformada);
        btnPesquisar.Click += new EventHandler(btnPesquisar_Click);
    }

    

   
    void dgItem_EditCommand(object source, DataGridCommandEventArgs e)
    {
        ucJustificativaReativacao.Show(Convert.ToInt32(dgItem.DataKeys[e.Item.ItemIndex]));
    }

    void ucJustificativaReativacao_JustificativaInformada(object sender, EventArgs e)
    {
        PedidoCotacaoItemDescartado item = PedidoCotacaoItemDescartado.Get(ucJustificativaReativacao.ID_Item);
        item.Reativar(ucJustificativaReativacao.Justificativa);
        ucJustificativaReativacao.Close();
        Bind(); 
    }

    void dgItem_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        ucJustificativaCancelamento.Show(Convert.ToInt32(dgItem.DataKeys[e.Item.ItemIndex]));
    }

    void ucJustificativaCancelamento_JustificativaInformada(object sender, EventArgs e)
    {
        PedidoCotacaoItemDescartado item = PedidoCotacaoItemDescartado.Get(ucJustificativaCancelamento.ID_Item);
        item.Cancelar(ucJustificativaCancelamento.Justificativa);
        ucJustificativaCancelamento.Close();
        Bind(); 
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Anthem.Manager.Register(this);
        if (!this.IsPostBack)
        {
            Util.FillDropDownList(ddlComprador, Servidor.List(FuncaoServidor.Comprador), "Todos");
            
        }
    }

    
    #endregion

    void btnPesquisar_Click(object sender, EventArgs e)
    {
        Bind();
    }
   
    #region Bind
    private void Bind()
    {
		dgItem.DataSource = PedidoCotacaoItemDescartado.Select(txtPO.Text, Convert.ToInt32(ddlComprador.SelectedValue), false, false); 
        dgItem.DataKeyField = "ID";
        dgItem.DataBind();
        dgItem.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();
    }
    #endregion

  

    private void dgItem_OnItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            LinkButton lnkPO = (LinkButton) e.Item.FindControl("lnkPO");
            PedidoCotacaoItemDescartado item = (PedidoCotacaoItemDescartado)e.Item.DataItem;
            
            Anthem.AnthemClientMethods.Popup(lnkPO, "fchPedidoObtencao.aspx?id_pedido=" + item.PedidoObtencao.ID.ToString(), false, false, false, true, true, true, true, 20, 50, 700, 500, false);
   
        }
    }
}
