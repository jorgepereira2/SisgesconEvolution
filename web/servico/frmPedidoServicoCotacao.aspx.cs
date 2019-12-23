using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Marinha.Business;
using Shared.Common;
using Shared.NHibernateDAL;
using Shared.SessionState;

public partial class frmPedidoServicoCotacao : MarinhaPageBase
{
    #region private variables
   
    protected DelineamentoOrcamento _delineamentoOrcamento
    {
        get { return (DelineamentoOrcamento)Session["frmPedidoServicoCotacao._delineamentoOrcamento"]; }
        set { Session["frmPedidoServicoCotacao._delineamentoOrcamento"] = value; }
    }

    #endregion
    
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
		this.dgMaterial.ItemDataBound += dgMaterial_OnItemDataBound;
		
        this.btnEnviar.Click += new EventHandler(btnEnviar_Click);
        
        btnRecalcularTaxas.Click += new EventHandler(btnRecalcularTaxas_Click);
        btnImprimirServico.Click += new EventHandler(btnImprimirServico_Click);
        btnImprimirMaterial.Click += new EventHandler(btnImprimirMaterial_Click);
        ucComentario.OkClicked += new EventHandler(ucComentario_OkClicked);

        btnSalvar.Click += new EventHandler(btnSalvar_Click);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Anthem.Manager.Register(this);
        if (!this.IsPostBack)
        {
            
            _delineamentoOrcamento = DelineamentoOrcamento.Get(Convert.ToInt32(Request["id_delineamentoOrcamento"]));
            
            BindDelineamento();
            BindMaterial();
            
            Anthem.AnthemClientMethods.Redirect("frmPedidoServicoPendente.aspx", btnVoltar);
            
            //Anthem.AnthemClientMethods.Popup(btnDefinirFornecedor, "frmPedidoServicoOrcamentoFornecedor.aspx", false, false, false, true, true, true, true, 80, 120, 650, 500, false);
            
            FillPage();
            Populate();
        }
    }
    
    private  void FillPage()
    {
        
    }

    private void Populate()
    {
        lblCodigo.Text = _delineamentoOrcamento.CodigoComAno;
        lblEquipamento.Text = _delineamentoOrcamento.DescricaoEquipamentos;
        lblDataEmissao.Text = _delineamentoOrcamento.DataEmissao.ToShortDateString();
        lblStatus.Text = _delineamentoOrcamento.Status.Descricao;

        trJustificativaRecusa.Visible = _delineamentoOrcamento.FlagRecusado;
        lblJustificativaRecusa.Text = _delineamentoOrcamento.UltimoHistorico.JustificativaRecusa;

        Anthem.AnthemClientMethods.Popup(lnkDetalhes, "fchPedidoServico.aspx?id_pedido=" + _delineamentoOrcamento.PedidoServico.ID,
            false, false, false, true, true, true, true, 10, 40, 700, 520, false);
    }
    #endregion
    
    #region Delineamento
    private void BindDelineamento()
    {
        dgDelineamento.DataSource = _delineamentoOrcamento.ItensDelineamento;
        dgDelineamento.DataKeyField = "ID";
        dgDelineamento.DataBind();
        dgDelineamento.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();

        lblHomemHoraTotal.Text = string.Format("HH Total: {0}", _delineamentoOrcamento.HomemHoraTotal);
        lblHomemHoraTotal.UpdateAfterCallBack = true;
    }

    #endregion

    #region Bind
    private void BindMaterial()
    {
		dgMaterial.DataSource = _delineamentoOrcamento.ItensOrcamento; 
        dgMaterial.DataKeyField = "ID";
        dgMaterial.DataBind();
        dgMaterial.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();

    }
  
    #endregion

    #region Material

  

    private void dgMaterial_OnItemDataBound(object sender, DataGridItemEventArgs e)
    {
       if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            PedidoServicoItemOrcamento item = (PedidoServicoItemOrcamento)e.Item.DataItem;
            
            BuscaFornecedor ucFornecedor = (BuscaFornecedor)e.Item.FindControl("ucFornecedor");
            DropDownList ddlOrigemMaterial = (DropDownList)e.Item.FindControl("ddlOrigemMaterial");
           
           Util.FillDropDownList(ddlOrigemMaterial, typeof(OrigemMaterial));
           ddlOrigemMaterial.SelectedValue = item.OrigemMaterial.GetHashCode().ToString();
           
           if(item.Fornecedor != null)
           {
               ucFornecedor.FireEvent(item.Fornecedor.ID.ToString());
               //ucFornecedor.Text = item.Fornecedor.RazaoSocial;
               //ucFornecedor.SelectedValue = item.Fornecedor.ID.ToString();
           }

           Anthem.Label lblQuantidadeEstoque = (Anthem.Label)e.Item.FindControl("lblQuantidadeEstoque");
           lblQuantidadeEstoque.Text = MovimentoEstoque.GetQuantidadeEstoque(item.ServicoMaterial.ID, Convert.ToInt32(item.OrigemMaterial)).QuantidadeDisponivel.ToString();

           Label lblSaldoLicitacao = (Label)e.Item.FindControl("lblSaldoLicitacao");
           LicitacaoItem itemLicitacao = LicitacaoItem.GetItemAberto(item.ServicoMaterial.ID, 0);
           if (itemLicitacao != null)
               lblSaldoLicitacao.Text = itemLicitacao.QuantidadeDisponivel.ToString();
           else
               lblSaldoLicitacao.Text = "0";
        }
    }
    #endregion



    void btnSalvar_Click(object sender, EventArgs e)
    {
        Salvar();

        ShowSuccessMessage();
    }

    private void Salvar()
    {
        foreach (DataGridItem item in dgMaterial.Items)
        {
            if(item.ItemType == ListItemType.AlternatingItem || item.ItemType == ListItemType.Item)
            {
                TextBox txtValor = (TextBox) item.FindControl("txtValor");
                BuscaFornecedor ucFornecedor = (BuscaFornecedor)item.FindControl("ucFornecedor");
                DropDownList ddlOrigemMaterial = (DropDownList)item.FindControl("ddlOrigemMaterial");
              
                _delineamentoOrcamento.ItensOrcamento[item.ItemIndex].Valor = PageReader.ReadDecimal(txtValor);
                _delineamentoOrcamento.ItensOrcamento[item.ItemIndex].OrigemMaterial = (OrigemMaterial)Convert.ToInt32(ddlOrigemMaterial.SelectedValue);
                if (ucFornecedor.SelectedValue != "0")
                    _delineamentoOrcamento.ItensOrcamento[item.ItemIndex].Fornecedor = Fornecedor.Get(Convert.ToInt32(ucFornecedor.SelectedValue));
                else
                    _delineamentoOrcamento.ItensOrcamento[item.ItemIndex].Fornecedor = null;

                _delineamentoOrcamento.ItensOrcamento[item.ItemIndex].Save();
            }
        }
        _delineamentoOrcamento.Save();
    }

    void btnEnviar_Click(object sender, EventArgs e)
    {
        ucComentario.Show();        
    }

    void ucComentario_OkClicked(object sender, EventArgs e)
    {
        Salvar();
        _delineamentoOrcamento.FinalizarCotacao(this.ID_Servidor, ucComentario.Comentario);
        Anthem.AnthemClientMethods.Redirect("frmPedidoServicoPendente.aspx");
    }
    
   


    void btnRecalcularTaxas_Click(object sender, EventArgs e)
    {
        if(_delineamentoOrcamento.IsPersisted)
        {
            _delineamentoOrcamento.RecalcularTaxas();
            ShowSuccessMessage();
        }
        else 
            ShowMessage("Não é preciso recalcular as taxas, pois o orçamento ainda nao foi criado.");
    }

    void btnImprimirServico_Click(object sender, EventArgs e)
    {
        //Anthem.AnthemClientMethods.Popup(this, "fchOrcamento.aspx?id_orcamento=" + _delineamentoOrcamento.ID, false, false, false, true, true, true, true, 20, 50, 700, 500);
        Anthem.AnthemClientMethods.Popup(this, "fchImpressaoParaCotacao.aspx?tipo=servico&id_orcamento=" + _delineamentoOrcamento.ID, false, false, false, true, true, true, true, 10, 40, 700, 520);
    }

    void btnImprimirMaterial_Click(object sender, EventArgs e)
    {
        Anthem.AnthemClientMethods.Popup(this, "fchImpressaoParaCotacao.aspx?tipo=material&id_orcamento=" + _delineamentoOrcamento.ID, false, false, false, true, true, true, true, 10, 40, 700, 520);
    }
}
