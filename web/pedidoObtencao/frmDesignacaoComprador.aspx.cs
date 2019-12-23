using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Marinha.Business;
using Shared.Common;
using Shared.NHibernateDAL;
using Shared.SessionState;

public partial class frmDesignacaoComprador : MarinhaPageBase
{
    #region private variables

    [TransientPageState] protected PedidoObtencao _pedido;
    
    #endregion
    
    #region Initialization

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        
        this.btnEnviar.Click += new EventHandler(btnEnviar_Click);
        this.btnSalvar.Click += new EventHandler(btnSalvar_Click);
        this.dgItem.ItemDataBound += new DataGridItemEventHandler(dgItem_ItemDataBound);
        this.dgItemDesignado.DeleteCommand += new DataGridCommandEventHandler(dgItemDesignado_DeleteCommand);
    }
    
    void dgItem_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            PedidoObtencaoItem item = (PedidoObtencaoItem)e.Item.DataItem;

            if (item.ItemLicitacaoDisponivel != null)
            {
                LinkButton lnkLicitacao = (LinkButton)e.Item.FindControl("lnkLicitacao");
                //Anthem.CheckBox chkMarcado = (Anthem.CheckBox)e.Item.FindControl("chkMarcado");
                //chkMarcado.Checked = true;
                //chkMarcado.Enabled = false;

                Anthem.AnthemClientMethods.Popup(lnkLicitacao, "../licitacao/fchLicitacao.aspx?id_licitacao=" + item.ItemLicitacaoDisponivel.Licitacao.ID, false, false, false, true, true, true, true, 20, 50, 700, 500, false);
            }
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
            }

            Bind();
            Populate();
            Anthem.AnthemClientMethods.Redirect("frmPedidoObtencaoPendente.aspx", btnVoltar);
        }
    }
    
    private void Populate()
    {
        lnkPS.Text = _pedido.DelineamentoOrcamento == null ? "" : _pedido.DelineamentoOrcamento.ToString();
        lnkPO.Text = _pedido.CodigoComAno;
        lblDataEmissao.Text = _pedido.DataEmissao.ToShortDateString();

        if(_pedido.DelineamentoOrcamento != null)
        Anthem.AnthemClientMethods.Popup(lnkPS, "../servico/fchPedidoServico.aspx?id_pedido=" + _pedido.DelineamentoOrcamento.ID_PedidoServico,
                   "ps", false, false, false, true, true, true, true, 20, 40, 700, 500, false);

        Anthem.AnthemClientMethods.Popup(lnkPO, "fchPedidoObtencao.aspx?id_pedido=" + _pedido.ID,
                   "po", false, false, false, true, true, true, true, 20, 40, 700, 500, false);
        
        Util.FillDropDownList(ddlComprador, Servidor.List(FuncaoServidor.Comprador), ESCOLHA_OPCAO);
    }

    #endregion
    
    #region Bind

    private void Bind()
    {
		dgItem.DataSource = _pedido.GetItensComprador(false); 
        dgItem.DataKeyField = "ID";
        dgItem.DataBind();
        dgItem.UpdateAfterCallBack = true;
        
        dgItemDesignado.DataSource = _pedido.GetItensComprador(true);
        dgItemDesignado.DataKeyField = "ID";
        dgItemDesignado.DataBind();
        dgItemDesignado.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();
    }

    #endregion
  
    void btnEnviar_Click(object sender, EventArgs e)
    {
        _pedido.IrParaProximoStatus(ID_Servidor, null);
        Anthem.AnthemClientMethods.Redirect("frmPedidoObtencaoPendente.aspx");
    }

    void btnSalvar_Click(object sender, EventArgs e)
    {
        List<int> itens = new List<int>();

        foreach (DataGridItem item in dgItem.Items)
        {
            if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
            {
                CheckBox chk = (CheckBox)item.FindControl("chkMarcado");
                
                int id = Convert.ToInt32(dgItem.DataKeys[item.ItemIndex]);

                PedidoObtencao po = PedidoObtencao.Get(PedidoObtencaoItem.Get(id).PedidoObtencao.ID);

                foreach (PedidoObtencaoItem itemPO in po.Itens)
                {
                    if (itemPO.ServidorRecebimento != null)
                    {
                        if (itemPO.ServidorRecebimento.ID != Convert.ToInt32(ddlComprador.SelectedValue))
                        {
                            Servidor comprador = Servidor.Get(itemPO.ServidorRecebimento.ID);
                            throw new Exception("Este Item só pode ser viculado ao comprador '" + comprador.Graduacao + " - " + comprador.NomeGuerra + "'.");
                        }
                    }
                }

                if (chk.Checked)
                {
                    itens.Add(id);
                }
            }
        }

        if(itens.Count > 0)
        {
            _pedido.DesignaComprador(itens, Convert.ToInt32(ddlComprador.SelectedValue));
            Bind();
        }
    }
    
    protected void chkTodos_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkTodos = (CheckBox) sender;

        foreach (DataGridItem item in dgItem.Items)
        {
            if(item.ItemType == ListItemType.AlternatingItem || item.ItemType == ListItemType.Item)
            {
                CheckBox chk = (CheckBox) item.FindControl("chkMarcado");
                chk.Checked = chkTodos.Checked;
            }
        }

        dgItem.UpdateAfterCallBack = true;
    }

    void dgItemDesignado_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        int id = Convert.ToInt32(dgItemDesignado.DataKeys[e.Item.ItemIndex]);

        _pedido.CancelaDesignacaoComprador(id);

        Bind();
    }
}
