using System;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Marinha.Business;
using Shared.Common;
using Shared.NHibernateDAL;
using Shared.SessionState;

public partial class frmVerificacaoPaiol : MarinhaPageBase
{
    #region private variables

    [TransientPageState] protected PedidoObtencao _pedido;
    #endregion
    
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.dgItem.ItemDataBound += dgItem_OnItemDataBound;
        this.btnEnviar.Click += new EventHandler(btnEnviar_Click);
        this.dgItem.EditCommand += new DataGridCommandEventHandler(dgItem_EditCommand);
        this.dgItem.UpdateCommand += new DataGridCommandEventHandler(dgItem_UpdateCommand);
        this.dgItem.DeleteCommand += new DataGridCommandEventHandler(dgItem_DeleteCommand);
        this.dgItem.CancelCommand +=new DataGridCommandEventHandler(dgItem_CancelCommand);
        this.ucCancelarItem.ItemCancelado += UcCancelarItem_OnItemCancelado;
        this.dgItem.ItemCommand += new DataGridCommandEventHandler(dgItem_ItemCommand);
    }

    void dgItem_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        BuscaFornecedor ucBuscaFornecedor = (BuscaFornecedor)e.Item.FindControl("ucBuscaFornecedor");
        HtmlGenericControl divFornecedor = (HtmlGenericControl)e.Item.FindControl("divFornecedor");
        Anthem.LinkButton lnkFornecedor = (Anthem.LinkButton)e.Item.FindControl("lnkFornecedor");

        if(e.CommandName == "DefinirFornecedor")
        {
            Anthem.AnthemClientMethods.ShowHide(divFornecedor, true);
            Anthem.AnthemClientMethods.ShowHide(lnkFornecedor, false);
            
        }
        else if(e.CommandName == "SalvarFornecedor")
        {
            int id = Convert.ToInt32(dgItem.DataKeys[e.Item.ItemIndex]);
            PedidoObtencaoItem itemPO = _pedido.Itens.Find(id);
            itemPO.Fornecedor = Fornecedor.Get(Convert.ToInt32(ucBuscaFornecedor.SelectedValue));
            itemPO.Save();
            if (itemPO.Fornecedor != null)
            {
                lnkFornecedor.Text = itemPO.Fornecedor.RazaoSocial;
                lnkFornecedor.UpdateAfterCallBack = true;
                ucBuscaFornecedor.SelectedValue = itemPO.Fornecedor.ID.ToString();
                ucBuscaFornecedor.Text = itemPO.Fornecedor.RazaoSocial;
                ucBuscaFornecedor.UpdateAfterCallBack = true;
            }
            else
            {
                lnkFornecedor.Text = "Definir Fornecedor";
                lnkFornecedor.UpdateAfterCallBack = true;
            }
            Anthem.AnthemClientMethods.ShowHide(divFornecedor, false);
            Anthem.AnthemClientMethods.ShowHide(lnkFornecedor, true);
        }
        else if (e.CommandName == "CancelarFornecedor")
        {
            int id = Convert.ToInt32(dgItem.DataKeys[e.Item.ItemIndex]);
            PedidoObtencaoItem itemPO = _pedido.Itens.Find(id);
            if (itemPO.Fornecedor != null)
            {
                lnkFornecedor.Text = itemPO.Fornecedor.RazaoSocial;
                lnkFornecedor.UpdateAfterCallBack = true;
                ucBuscaFornecedor.SelectedValue = itemPO.Fornecedor.ID.ToString();
                ucBuscaFornecedor.Text = itemPO.Fornecedor.RazaoSocial;
                ucBuscaFornecedor.UpdateAfterCallBack = true;
            }
            else
            {
                lnkFornecedor.Text = "Definir Fornecedor";
                lnkFornecedor.UpdateAfterCallBack = true;
            }
            Anthem.AnthemClientMethods.ShowHide(divFornecedor, false);
            Anthem.AnthemClientMethods.ShowHide(lnkFornecedor, true);

        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Anthem.Manager.Register(this);
        if (!this.IsPostBack)
        {

            if (Request["id_pedido"] != null)
            {
                _pedido = PedidoObtencao.Get(Convert.ToInt32(Request["id_pedido"]));
                
                if(_pedido.DelineamentoOrcamento != null)
                {
                    lnkPS.Text = _pedido.DelineamentoOrcamento.CodigoComAno;
                    Anthem.AnthemClientMethods.Popup(lnkPS, 
                    "../servico/fchPedidoServico.aspx?id_pedido=" + _pedido.DelineamentoOrcamento.PedidoServico.ID.ToString(),
                    false, false, false, true, true, true, true, 20, 30, 700, 500, false);
                }
            }

            Bind();
            Populate();
            
            Anthem.AnthemClientMethods.Redirect("frmPedidoObtencaoPendente.aspx", btnVoltar);
        }
    }
    
    private void Populate()
    {
        lblDataEmissao.Text = _pedido.DataEmissao.ToShortDateString();
    }
    #endregion
   
    #region Bind
    private void Bind()
    {
        dgItem.DataSource = _pedido.Itens;
        dgItem.DataKeyField = "ID";
        dgItem.DataBind();
        dgItem.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();
    }
    #endregion

    #region Orcamento

    private void dgItem_OnItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Anthem.NumericTextBox txtQuantidade = (Anthem.NumericTextBox) e.Item.FindControl("txtQuantidade");
            DropDownList ddlOrigemMaterial = (DropDownList)e.Item.FindControl("ddlOrigemMaterial");
            PedidoObtencaoItem item = (PedidoObtencaoItem)e.Item.DataItem;

            if(item.ServicoMaterial.TipoServicoMaterial == TipoServicoMaterial.Material)
                Util.FillDropDownList(ddlOrigemMaterial, OrigemMaterialManager.ListSemSingra());
            else
                Util.FillDropDownList(ddlOrigemMaterial, OrigemMaterialManager.ListApenasObtencao());

            //Fornecedor
            if (item.Fornecedor != null)
            {
                BuscaFornecedor ucBuscaFornecedor = (BuscaFornecedor) e.Item.FindControl("ucBuscaFornecedor");
                ucBuscaFornecedor.SelectedValue = item.Fornecedor.ID.ToString();
                ucBuscaFornecedor.Text = item.Fornecedor.RazaoSocial;
            }

            txtQuantidade.Text = item.Quantidade.ToString();
            ddlOrigemMaterial.SelectedValue = Convert.ToInt32(item.OrigemMaterial).ToString();
            
            //Quantidade estoque
            Anthem.Label lblQuantidadeEstoque = ((Anthem.Label)e.Item.FindControl("lblQuantidadeEstoque"));
            if (Convert.ToInt32(ddlOrigemMaterial.SelectedValue) == Convert.ToInt32(OrigemMaterial.Obtencao))
            {
                lblQuantidadeEstoque.Text = "0";
                return;
            }
            QuantidadeEstoque quantidade = MovimentoEstoque.GetQuantidadeEstoque(item.ServicoMaterial.ID, Convert.ToInt32(ddlOrigemMaterial.SelectedValue));
            lblQuantidadeEstoque.Text = quantidade.QuantidadeDisponivel.ToString();

           
        }
        else if(e.Item.ItemType == ListItemType.Footer && dgItem.ShowFooter)
        {
            DropDownList ddlOrigemMaterial = (DropDownList)e.Item.FindControl("ddlOrigemMaterialNovo");
            Label lblServicoMaterial = (Label)e.Item.FindControl("lblServicoMaterial");
            Label lblID = (Label)e.Item.FindControl("lblID");
            Label lblID_Material = (Label)e.Item.FindControl("lblID_Material");
            
            lblServicoMaterial.Text = _itemSeparacao.ServicoMaterial.Descricao;
            lblID.Text = _itemSeparacao.ID.ToString();
            lblID_Material.Text = _itemSeparacao.ServicoMaterial.ID.ToString();
            if(_itemSeparacao.ServicoMaterial.TipoServicoMaterial ==TipoServicoMaterial.Material)
                Util.FillDropDownList(ddlOrigemMaterial, OrigemMaterialManager.ListSemSingra());
            else
                Util.FillDropDownList(ddlOrigemMaterial, OrigemMaterialManager.ListApenasObtencao());
        }
    }

    void dgItem_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dgItem.ShowFooter = false;
        dgItem.EditItemIndex = -1;
        Bind();
    }

	#endregion
    
    void btnEnviar_Click(object sender, EventArgs e)
    {
        foreach (DataGridItem item in dgItem.Items)
        {
            if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
            {
                Anthem.NumericTextBox txtQuantidade = (Anthem.NumericTextBox)item.FindControl("txtQuantidade");
                DropDownList ddlOrigemMaterial = (DropDownList)item.FindControl("ddlOrigemMaterial");
                TextBox txtEspecificacao = (TextBox)item.FindControl("txtEspecificacao");
                if(Convert.ToDecimal(txtQuantidade.Text) > 0)
                {
                    int id = Convert.ToInt32(dgItem.DataKeys[item.ItemIndex]);
                    PedidoObtencaoItem itemPO = _pedido.Itens.Find(id);

                    if (Convert.ToDecimal(txtQuantidade.Text) > itemPO.Quantidade)
                    {
                        ShowMessage("A quantidade atendida não pode ser maior que a quantidade original.");
                        return;
                    }
                    
                    itemPO.OrigemMaterial = (OrigemMaterial) Convert.ToInt32(ddlOrigemMaterial.SelectedValue);
                    itemPO.Quantidade = Convert.ToDecimal(txtQuantidade.Text);
                    itemPO.Especificacao = txtEspecificacao.Text;
                    itemPO.Save();
                }
            }
        }

        _pedido.IrParaProximoStatus(ID_Servidor, null);
        
        //if(_pedido.Status.StatusPedidoObtencaoEnum == StatusPedidoObtencaoEnum.AguardandoVerificacaoPaiol)
        //    _pedido.EnviarVerificacaoPaiol(ID_Servidor);
        //else
        //    _pedido.AprovarVerificacaoPaiol(ID_Servidor);

        Anthem.AnthemClientMethods.Redirect("frmPedidoObtencaoPendente.aspx");
    }

    void dgItem_UpdateCommand(object source, DataGridCommandEventArgs e)
    {

        Label lblID = (Label)e.Item.FindControl("lblID");
        Anthem.NumericTextBox txtQuantidade = (Anthem.NumericTextBox)e.Item.FindControl("txtQuantidadeNovo");
        DropDownList ddlOrigemMaterial = (DropDownList)e.Item.FindControl("ddlOrigemMaterialNovo");

        _pedido.SepararItem(Convert.ToInt32(lblID.Text), Convert.ToInt32(ddlOrigemMaterial.SelectedValue), Convert.ToDecimal(txtQuantidade.Text));
        dgItem.ShowFooter = false;
        Bind();
    }

    private PedidoObtencaoItem _itemSeparacao;
    void dgItem_EditCommand(object source, DataGridCommandEventArgs e)
    {
        int id = Convert.ToInt32(dgItem.DataKeys[e.Item.ItemIndex]);
        _itemSeparacao = _pedido.Itens.Find(id);

        dgItem.ShowFooter = true;
        Bind();
    }

    void dgItem_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        int id = Convert.ToInt32(dgItem.DataKeys[e.Item.ItemIndex]);
        ucCancelarItem.Show(id);
    }

    private void UcCancelarItem_OnItemCancelado(object sender, EventArgs e)
    {
        _pedido.Itens.Find(ucCancelarItem.ID_Item).Cancelar(ID_Servidor, ucCancelarItem.Justificativa, null);
        Bind();
    }

    protected void ddlOrigemMaterial_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlOrigemMaterial = (DropDownList) sender;
        DataGridItem item = (DataGridItem) ddlOrigemMaterial.Parent.Parent;
        Anthem.Label lblQuantidadeEstoque = ((Anthem.Label) item.FindControl("lblQuantidadeEstoque"));
        if(Convert.ToInt32(ddlOrigemMaterial.SelectedValue) == Convert.ToInt32(OrigemMaterial.Obtencao))
        {
            lblQuantidadeEstoque.Text = "0";
            lblQuantidadeEstoque.UpdateAfterCallBack = true;
            return;
        }

        int id_material = Convert.ToInt32(((Label) item.FindControl("lblID_Material")).Text);

        QuantidadeEstoque quantidade =
            MovimentoEstoque.GetQuantidadeEstoque(id_material, Convert.ToInt32(ddlOrigemMaterial.SelectedValue));

        lblQuantidadeEstoque.Text = quantidade.QuantidadeDisponivel.ToString();
        lblQuantidadeEstoque.UpdateAfterCallBack = true;
    }
}
