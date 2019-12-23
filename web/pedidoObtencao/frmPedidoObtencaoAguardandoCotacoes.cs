using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Drawing;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Anthem;
using Shared.NHibernateDAL;
using Marinha.Business;
using Shared.SessionState;
using Shared.Common;
using Label = System.Web.UI.WebControls.Label;

public partial class frmPedidoObtencaoAguardandoCotacoes : MarinhaPageBase
{
    #region Private Member

    [TransientPageState]
    protected PedidoObtencao _pedido;

    #endregion 

    #region Initialization

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.btnSalvar.Click += new EventHandler(btnSalvar_Click);
        this.btnEnviar.Click += btnEnviar_Click;
        this.dgItem.DeleteCommand += new DataGridCommandEventHandler(dgItem_DeleteCommand);
        this.ucCancelarItem.ItemCancelado += UcCancelarItem_OnItemCancelado;
        dgItem.ItemDataBound += new DataGridItemEventHandler(dgItem_ItemDataBound);
    }

    void dgItem_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if(e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            PedidoObtencaoItem item = (PedidoObtencaoItem)e.Item.DataItem;
            if(item.ServicoMaterial.TipoServicoMaterial == TipoServicoMaterial.Servico)
                return;
            Label lblQuantidadeEstoque = (Label) e.Item.FindControl("lblQuantidadeEstoque");
            QuantidadeEstoque qtd = MovimentoEstoque.GetQuantidadeEstoque(item.ServicoMaterial.ID, Convert.ToInt32(item.OrigemMaterial));
            lblQuantidadeEstoque.Text = qtd.QuantidadeDisponivel.ToString();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            if (Request["ID_Pedido"] != null)
            {
                _pedido = PedidoObtencao.Get(Convert.ToInt32(Request["ID_Pedido"]));
                dgItem.DataSource = _pedido.Itens;
                dgItem.DataKeyField = "ID";

                Anthem.Manager.Register(this);

                PopulatePage();

                DataBind();
                
                if (_pedido.DelineamentoOrcamento != null)
                {
                    lnkPS.Text = _pedido.DelineamentoOrcamento.CodigoComAno;
                    Anthem.AnthemClientMethods.Popup(lnkPS,
                                                     "../servico/fchPedidoServico.aspx?id_pedido=" +
                                                     _pedido.DelineamentoOrcamento.PedidoServico.ID.ToString(),
                                                     false, false, false, true, true, true, true, 20, 30, 900, 600,
                                                     false);
                }

                if (_pedido.TipoPedido != TipoPedido.PedidoMaterial)
                {
                    dgItem.Columns[dgItem.Columns.Count - 4].Visible = false;
                    dgItem.Columns[dgItem.Columns.Count - 3].Visible = false;
                    dgItem.Columns[dgItem.Columns.Count - 2].Visible = false;
                }
            }
            
            Anthem.AnthemClientMethods.Redirect("frmPedidoObtencaoPendente.aspx", btnVoltar);
            Anthem.AnthemClientMethods.Popup(btnImprimir,
                                             "fchPedidoObtencaoCompleto.aspx?id_pedido=" + _pedido.ID.ToString(),
                                             false, false, false, true, true, true, true, 10, 30, 700, 550, false);
        }
    }

    private void PopulatePage()
    {
        if (_pedido.FornecedorCotacao1 != null)
        {
            ucBuscaFornecedor.SelectedValue = _pedido.FornecedorCotacao1.ID.ToString();
            ucBuscaFornecedor.Text = _pedido.FornecedorCotacao1.RazaoSocial;
        }
        if (_pedido.FornecedorCotacao2 != null)
        {
            ucBuscaFornecedor2.SelectedValue = _pedido.FornecedorCotacao2.ID.ToString();
            ucBuscaFornecedor2.Text = _pedido.FornecedorCotacao2.RazaoSocial;
        }
        if (_pedido.FornecedorCotacao3 != null)
        {
            ucBuscaFornecedor3.SelectedValue = _pedido.FornecedorCotacao3.ID.ToString();
            ucBuscaFornecedor3.Text = _pedido.FornecedorCotacao3.RazaoSocial;
        }
        if (_pedido.FornecedorCotacao4 != null)
        {
            ucBuscaFornecedor4.SelectedValue = _pedido.FornecedorCotacao4.ID.ToString();
            ucBuscaFornecedor4.Text = _pedido.FornecedorCotacao4.RazaoSocial;
        }
    }

    #endregion

    void dlHistorico_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            HistoricoPedidoObtencao historico = (HistoricoPedidoObtencao)e.Item.DataItem;

            if (historico.FlagReprovado)
                e.Item.ForeColor = Color.Red;
        }
    }
 
    private void btnSalvar_Click(object sender, EventArgs e)
    {
        //TODO: salvar cotacoes
        _pedido.FornecedorCotacao1 = Fornecedor.Get(Convert.ToInt32(ucBuscaFornecedor.SelectedValue));
        _pedido.FornecedorCotacao2 = Fornecedor.Get(Convert.ToInt32(ucBuscaFornecedor2.SelectedValue));
        _pedido.FornecedorCotacao3 = Fornecedor.Get(Convert.ToInt32(ucBuscaFornecedor3.SelectedValue));
        _pedido.FornecedorCotacao4 = Fornecedor.Get(Convert.ToInt32(ucBuscaFornecedor4.SelectedValue));

        foreach (DataGridItem item in dgItem.Items)
        {
            if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
            {
                var txtValor = (NumericTextBox)item.FindControl("txtValor");
                var txtValor2 = (NumericTextBox)item.FindControl("txtValor2");
                var txtValor3 = (NumericTextBox)item.FindControl("txtValor3");
                var txtValor4 = (NumericTextBox)item.FindControl("txtValor4");
                int id = Convert.ToInt32(dgItem.DataKeys[item.ItemIndex]);

                var itemPedido = _pedido.Itens[item.ItemIndex];//.Find(id);
                itemPedido.Valor = txtValor.RawValue;
                itemPedido.ValorCotacao2 = txtValor2.RawValue;
                itemPedido.ValorCotacao3 = txtValor3.RawValue;
                itemPedido.Save();
            }
        }

        //_pedido.Recusar(ID_Servidor, txtJustificativa.Text);
        
        _pedido.AtualizaCotacao();
    }

    void btnEnviar_Click(object sender, EventArgs e)
    {
        btnSalvar_Click(null, null);
        _pedido.IrParaProximoStatus(ID_Servidor, txtComentario.Text);
        Anthem.AnthemClientMethods.Redirect("frmPedidoObtencaoPendente.aspx");
    }

    void dgItem_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        int id = Convert.ToInt32(dgItem.DataKeys[e.Item.ItemIndex]);
        ucCancelarItem.Show(id);
    }
  
    private void UcCancelarItem_OnItemCancelado(object sender, EventArgs e)
    {
        _pedido.Itens.Find(ucCancelarItem.ID_Item).Cancelar(ID_Servidor, ucCancelarItem.Justificativa, null);

        dgItem.DataSource = _pedido.Itens;
        dgItem.DataBind();
        dgItem.UpdateAfterCallBack = true;
    }  
}
