using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Marinha.Business;
using Shared.Common;
using Shared.NHibernateDAL;
using Shared.SessionState;

public partial class frmPedidoServicoSepararItemPO : MarinhaPageBase
{
    #region private variables

    //[TransientPageState] 
    //protected List<PedidoServicoItemOrcamento> _itens;

    [TransientPageState]
    protected DelineamentoOrcamento _orcamento;
    
    #endregion
    
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.dgItem.ItemDataBound += dgItem_OnItemDataBound;
        this.btnInserir.Click += new EventHandler(btnInserir_Click);
       
        rbPCExistente.CheckedChanged += new EventHandler(PC_CheckedChanged);
        rbCriarNovoPO.CheckedChanged += new EventHandler(PC_CheckedChanged);
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
        
        Bind(); 
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Anthem.Manager.Register(this);
        if (!this.IsPostBack)
        {

            _orcamento = DelineamentoOrcamento.Get(Convert.ToInt32(Request["id_orcamento"]));
            Util.FillDropDownList(ddlPO, PedidoObtencao.List(_orcamento.ID), ESCOLHA_OPCAO);
            
            Bind();
        }
    }

    #endregion
   
    #region Bind
    private void Bind()
    {
		dgItem.DataSource = _orcamento.ItensDisponiveisPO; 
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
            LinkButton lnkPS = (LinkButton) e.Item.FindControl("lnkPS");
            PedidoServicoItemOrcamento item = (PedidoServicoItemOrcamento)e.Item.DataItem;
            
            Anthem.AnthemClientMethods.Popup(lnkPS, "../servico/fchOrcamento.aspx?id_orcamento=" + item.DelineamentoOrcamento.ID, false, false, false, true, true, true, true, 20, 50, 700, 500, false);
            
            if(item.ServicoMaterial.ItemLicitacaoDisponivel != null)
            {
                LinkButton lnkLicitacao = (LinkButton)e.Item.FindControl("lnkLicitacao");
                Anthem.AnthemClientMethods.Popup(lnkLicitacao, "../licitacao/fchLicitacao.aspx?id_licitacao=" + item.ServicoMaterial.ItemLicitacaoDisponivel.Licitacao.ID.ToString(), false, false, false, true, true, true, true, 20, 50, 700, 500, false);
                
            }
        }
    }
    
    #region Inserir Itens no PO
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
                    PedidoServicoItemOrcamento item = _orcamento.ItensOrcamento.Where(i => i.ID == id).First();
                    if(item.ServicoMaterial.ItemLicitacaoDisponivel != null)
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
        if (ddlPO.SelectedValue == "0" && rbPCExistente.Checked)
        {
            ShowMessage("Selecione uma Autorização de Compra.");
            return;
        }
        List<PedidoServicoItemOrcamento> list = new List<PedidoServicoItemOrcamento>();
        foreach (DataGridItem item in dgItem.Items)
        {
            if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
            {
                CheckBox chkMarcado = (CheckBox)item.FindControl("chkMarcado");
                if (chkMarcado.Checked)
                {
                    int id = Convert.ToInt32(dgItem.DataKeys[item.ItemIndex]);
                    list.Add(PedidoServicoItemOrcamento.Get(id));
                }
            }
        }

        try
        {
            int id_po = 0;
            if (rbCriarNovoPO.Checked)
                id_po = PedidoObtencao.CriarNovoPOPS(list);
            else
            {
                PedidoObtencao po = PedidoObtencao.Get(Convert.ToInt32(ddlPO.SelectedValue));
                PedidoObtencao.InserirItensPS(po, list);
                id_po = po.ID;
            }

            Redirect("frmPedidoObtencaoCadastro.aspx?id_pedido=" + id_po);
        }
        catch(Exception ex)
        {
            ShowMessage(ex.Message);
        }
       


        
        //ShowSuccessMessage();
        //winInserirItemPC.Hide();
        //Bind();
    }

    void btnCancelarInserirItem_Click(object sender, EventArgs e)
    {
        winInserirItemPC.Hide();
    }
    #endregion
    
    

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
