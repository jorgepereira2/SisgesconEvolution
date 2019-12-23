using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Marinha.Business;
using Shared.Common;
using Shared.NHibernateDAL;
using Shared.SessionState;

public partial class frmCotacaoItem : MarinhaPageBase
{
    #region private variables

    [TransientPageState] 
    protected List<PedidoObtencaoItem> _itens;
    
    #endregion
    
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.dgItem.ItemDataBound += dgItem_OnItemDataBound;
        this.btnInserir.Click += new EventHandler(btnInserir_Click);
       
        rbPCExistente.CheckedChanged += new EventHandler(PC_CheckedChanged);
        rbCriarNovoPC.CheckedChanged += new EventHandler(PC_CheckedChanged);
        btnInserirItem.Click += new EventHandler(btnInserirItem_Click);
        btnCancelarInserirItem.Click += new EventHandler(btnCancelarInserirItem_Click);
        ucDescartarItem.ItemDescartado += new EventHandler(ucDescartarItem_ItemDescartado);
        dgItem.DeleteCommand += new DataGridCommandEventHandler(dgItem_DeleteCommand);
    }

    void dgItem_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        ucDescartarItem.Show(Convert.ToInt32(dgItem.DataKeys[e.Item.ItemIndex]));
    }

    void ucDescartarItem_ItemDescartado(object sender, EventArgs e)
    {
        PedidoObtencaoItem item = PedidoObtencaoItem.Get(ucDescartarItem.ID_Item);
        item.Descartar(ID_Servidor, ucDescartarItem.Justificativa);
        ucDescartarItem.Close();
        SetDataSource();
        Bind(); 
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Anthem.Manager.Register(this);
        if (!this.IsPostBack)
        {
            Util.FillDropDownList(ddlPC, PedidoCotacao.List(ID_Servidor), ESCOLHA_OPCAO);
            SetDataSource();
            Bind();
        }
    }

    private void SetDataSource()
    {
        _itens = PedidoObtencaoItem.SelectItemCotacaoPendente(ID_Servidor);
    }
    
    #endregion
   
    #region Bind
    private void Bind()
    {
		dgItem.DataSource = _itens; 
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
            PedidoObtencaoItem item = (PedidoObtencaoItem)e.Item.DataItem;
            
            Anthem.AnthemClientMethods.Popup(lnkPO, "fchPedidoObtencao.aspx?id_pedido=" + item.PedidoObtencao.ID.ToString(), false, false, false, true, true, true, true, 20, 50, 700, 500, false);
            
            if(item.ItemLicitacaoDisponivel != null)
            {
                LinkButton lnkLicitacao = (LinkButton)e.Item.FindControl("lnkLicitacao");
                Anthem.AnthemClientMethods.Popup(lnkLicitacao, "../licitacao/fchLicitacao.aspx?id_licitacao=" + item.ItemLicitacaoDisponivel.Licitacao.ID.ToString(), false, false, false, true, true, true, true, 20, 50, 700, 500, false);
                
            }
        }
    }
    
    #region Inserir Itens no Pedido deCotacao
    void PC_CheckedChanged(object sender, EventArgs e)
    {
        pnEscolherPC.Visible = rbPCExistente.Checked;
        pnEscolherPC.UpdateAfterCallBack = true;
    }

    void btnInserir_Click(object sender, EventArgs e)
    {
        //Verifica se os itens escolhidos tem saldo em licitacao
        if(ItensSelecionadosTemSaldoLicitacao())
        {
            chkUtilizarSaldoLicitacao.Visible = true;
            chkUtilizarSaldoLicitacao.Checked = true;
        }
        else
        {
            chkUtilizarSaldoLicitacao.Visible = false;
            chkUtilizarSaldoLicitacao.Checked = false;
        }

        chkUtilizarSaldoLicitacao.UpdateAfterCallBack = true;

        winInserirItemPC.Show();
    }
    
    bool ItensSelecionadosTemSaldoLicitacao()
    {
        foreach (DataGridItem gridItem in dgItem.Items)
        {
            if (gridItem.ItemType == ListItemType.Item || gridItem.ItemType == ListItemType.AlternatingItem)
            {
                CheckBox chkMarcado = (CheckBox)gridItem.FindControl("chkMarcado");
                if (chkMarcado.Checked)
                {
                    int id = Convert.ToInt32(dgItem.DataKeys[gridItem.ItemIndex]);
                    PedidoObtencaoItem item = _itens.Find(delegate (PedidoObtencaoItem match){ return match.ID == id;});
                    if(item.ItemLicitacaoDisponivel != null)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    void btnInserirItem_Click(object sender, EventArgs e)
    {
        if(ddlPC.SelectedValue == "0" && rbPCExistente.Checked)
        {
            ShowMessage("Selecione um Pedido de Cotação.");
            return;
        }
        List<PedidoObtencaoItem> list = new List<PedidoObtencaoItem>();
        foreach (DataGridItem item in dgItem.Items)
        {
            if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
            {
                CheckBox chkMarcado = (CheckBox)item.FindControl("chkMarcado");
                if (chkMarcado.Checked)
                {
                    int id = Convert.ToInt32(dgItem.DataKeys[item.ItemIndex]);
                    list.Add(PedidoObtencaoItem.Get(id));
                }
            }
        }

        
        //if(rbCriarNovoPC.Checked)
        //    PedidoCotacao.CriarNovo(list, chkUtilizarSaldoLicitacao.Checked);
        //else
        //{
        //    PedidoCotacao cotacao = PedidoCotacao.Get(Convert.ToInt32(ddlPC.SelectedValue));
        //    cotacao.InserirItens(list, chkUtilizarSaldoLicitacao.Checked);
        //}
        
        ShowSuccessMessage();
        winInserirItemPC.Hide();
        SetDataSource();
        Bind();
    }

    void btnCancelarInserirItem_Click(object sender, EventArgs e)
    {
        winInserirItemPC.Hide();
    }
    #endregion
    
    private PedidoObtencaoItem FindItem(int id)
    {
        return _itens.Find(delegate(PedidoObtencaoItem match) { return match.ID == id; });
    }

    protected void chkTodos_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkTodos = (CheckBox) sender;
        foreach (DataGridItem item in dgItem.Items)
        {
            if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
            {
                Anthem.CheckBox chkMarcado = (Anthem.CheckBox)item.FindControl("chkMarcado");
                chkMarcado.Checked = chkTodos.Checked;
                chkMarcado.UpdateAfterCallBack = true;
            }
        }
    }
}
