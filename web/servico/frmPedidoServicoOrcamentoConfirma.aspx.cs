using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Marinha.Business;
using Shared.Common;
using Shared.NHibernateDAL;
using Shared.SessionState;

public partial class frmPedidoServicoOrcamento : MarinhaPageBase
{
    #region private variables
    
    protected PedidoServico _pedido
    {
        get{ return (PedidoServico) Session["frmPedidoServicoOrcamento._pedido"]; }
        set{ Session["frmPedidoServicoOrcamento._pedido"] = value;}
    }


    protected DelineamentoOrcamento _delineamentoOrcamento
    {
        get { return (DelineamentoOrcamento)Session["frmPedidoServicoOrcamento._delineamentoOrcamento"]; }
        set{ Session["frmPedidoServicoOrcamento._delineamentoOrcamento"] = value;}
    }

    protected DelineamentoOficina _delineamentoOficina
    {
        get { return _delineamentoOrcamento.Delineamentos.Where(d => d.Servidor.ID == this.ID_Servidor).FirstOrDefault(); }
    }
    
    #endregion
    
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.btnNovoMaterial.Click += new EventHandler(btnNovo_Click);
        this.btnNovoDelineamento.Click += new EventHandler(btnNovoDelineamento_Click);
        this.dgMaterial.CancelCommand += new DataGridCommandEventHandler(dgMaterial_CancelCommand);
        this.dgMaterial.ItemCommand += new DataGridCommandEventHandler(dgMaterial_ItemCommand);        
		this.dgMaterial.ItemDataBound += dgMaterial_OnItemDataBound;
		
        this.btnEnviar.Click += new EventHandler(btnEnviar_Click);
        btnSalvar.Click += new EventHandler(btnSalvar_Click);

        dgDelineamento.ItemDataBound += new DataGridItemEventHandler(dgDelineamento_ItemDataBound);
        dgDelineamento.ItemCommand += new DataGridCommandEventHandler(dgDelineamento_ItemCommand);
        dgDelineamento.CancelCommand += new DataGridCommandEventHandler(dgDelineamento_CancelCommand);
        dgDelineamento.EditCommand += new DataGridCommandEventHandler(dgDelineamento_EditCommand);
        dgDelineamento.UpdateCommand += new DataGridCommandEventHandler(dgDelineamento_UpdateCommand);

        dgMaterial.EditCommand += new DataGridCommandEventHandler(dgMaterial_EditCommand);
        dgMaterial.UpdateCommand += new DataGridCommandEventHandler(dgMaterial_UpdateCommand);

        btnRecalcularTaxas.Click += new EventHandler(btnRecalcularTaxas_Click);
        btnImprimir.Click += new EventHandler(btnImprimir_Click);

        dgServicos.ItemDataBound += new DataGridItemEventHandler(dgServicos_OnItemDataBound);
        dgServicos.CancelCommand +=new DataGridCommandEventHandler(dgServicos_CancelCommand);
        dgServicos.ItemCommand +=new DataGridCommandEventHandler(dgServicos_ItemCommand);
        dgServicos.EditCommand +=new DataGridCommandEventHandler(dgServicos_EditCommand);
        dgServicos.UpdateCommand +=new DataGridCommandEventHandler(dgServicos_UpdateCommand);
        this.btnNovoServico.Click += new EventHandler(btnNovoServico_Click);
        ucDadosComplementares.OkClicked += UcDadosComplementares_OnOkClicked;
        ucComentario.OkClicked += new EventHandler(ucComentario_OkClicked);

        dlRotinas.DeleteCommand += dlRotinas_DeleteCommand;

    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Anthem.Manager.Register(this);
        if (!this.IsPostBack)
        {
            //if (Request["id_pedido"] != null)
            //{
            //    _pedido = PedidoServico.Get(Convert.ToInt32(Request["id_pedido"]));
            //    if (_pedido.ExisteOrcamentoNaoEnviado)
            //        _delineamentoOrcamento = _pedido.GetOrcamentoNaoEnviado();
            //    else    
            //        _delineamentoOrcamento = new DelineamentoOrcamento();
            //}
            //else
            {
                _delineamentoOrcamento = DelineamentoOrcamento.Get(Convert.ToInt32(Request["id_delineamentoOrcamento"]));
                _pedido = _delineamentoOrcamento.PedidoServico;
            }

            BindDelineamento();
            BindMaterial();
            BindServico();
            BindRotina();
            Anthem.AnthemClientMethods.Redirect("frmPedidoServicoPendente.aspx", btnVoltar);
            Anthem.AnthemClientMethods.Popup(btnBuscarOrcamento, "frmOrcamentoBusca.aspx", false, false, false, true, true, true, true, 20, 50, 700, 500, false);

            Anthem.AnthemClientMethods.Popup(btnDefinirFornecedor, "frmPedidoServicoOrcamentoFornecedor.aspx", false, false, false, true, true, true, true, 80, 120, 650, 500, false);
            
            RegisterDeleteScript();
            RegisterDeleteScript("ExcluirDelineamento");
            RegisterDeleteScript("ExcluirItemServico");

            RegisterConfirmScript("AdicionarRotina", "Os itens deste orçamento serão sobrescritos. Deseja prosseguir?");
            
            
            if(_delineamentoOrcamento.ItensOrcamento.Count == 0)
                dgMaterial.ShowFooter = true;

            if (_delineamentoOrcamento.ItensOrcamento.Count == 0)
                dgServicos.ShowFooter = true;

            if (_delineamentoOrcamento.ItensDelineamento.Count == 0)
                dgDelineamento.ShowFooter = true;
            
            FillPage();
            Populate();

           // btnEnviar.Visible = !_delineamentoOficina.Enviado;

            btnNovoServico.Visible = false;
            btnNovoServico.UpdateAfterCallBack = true;

            btnRecalcularTaxas.Visible = false;
            btnRecalcularTaxas.UpdateAfterCallBack = true;

            btnBuscarOrcamento.Visible = false;
            btnBuscarOrcamento.UpdateAfterCallBack = true;

            btnDefinirFornecedor.Visible = false;
            btnDefinirFornecedor.UpdateAfterCallBack = true;

            btnNovoMaterial.Visible = false;
            btnNovoMaterial.UpdateAfterCallBack = true;

            btnNovoDelineamento.Visible = false;
            btnNovoDelineamento.UpdateAfterCallBack = true;

            btnSalvar.Visible = false;
            btnSalvar.UpdateAfterCallBack = true;
        }
    }
    
    private  void FillPage()
    {
        //Util.FillDropDownList(ddlRotina, Rotina.List(), ESCOLHA_OPCAO);
    }

    private void Populate()
    {
        lblCodigo.Text = _pedido.CodigoComAno;
        lblEquipamento.Text = _pedido.DescricaoEquipamentos;
        lblDataEmissao.Text = _pedido.DataEmissao.ToShortDateString();
        lblStatus.Text = _pedido.Status.Descricao;

        if (_delineamentoOficina != null)
        {
            trJustificativaRecusa.Visible = _delineamentoOficina.FlagRecusado;
            lblJustificativaRecusa.Text = _delineamentoOficina.Justificativa;
            //ddlRotina.SelectedValue = ObjectReader.ReadID(_delineamentoOrcamento.Rotina);
        }

        Anthem.AnthemClientMethods.Popup(lnkDetalhes, "fchPedidoServico.aspx?id_pedido=" + _pedido.ID.ToString(),
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

    void dgDelineamento_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Footer)
        {
            DropDownList ddlCelula = (DropDownList)e.Item.FindControl("ddlCelulaNovo");
            Util.FillDropDownList(ddlCelula, Celula.List(TipoCelula.Secao, true), ESCOLHA_OPCAO);
        }
        else if (e.Item.ItemType == ListItemType.EditItem)
        {
            DropDownList ddlCelula = (DropDownList)e.Item.FindControl("ddlCelula");
            Util.FillDropDownList(ddlCelula, Celula.List(TipoCelula.Secao, true), ESCOLHA_OPCAO);
            PedidoServicoDelineamento delineamento = (PedidoServicoDelineamento)e.Item.DataItem;

            ddlCelula.SelectedValue = delineamento.Celula.ID.ToString();
        }
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            PedidoServicoDelineamento delineamento = (PedidoServicoDelineamento)e.Item.DataItem;
            if(delineamento.ServidorDelineamento.ID != this.ID_Servidor)
            {
                //e.Item.Cells[e.Item.Cells.Count - 1].Visible = false;
            }
        }
    }

    void dgDelineamento_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Insert")
        {
            try
            {
                DropDownList ddlCelula = (DropDownList)e.Item.FindControl("ddlCelulaNovo");
                Anthem.NumericTextBox txtHomemHora = (Anthem.NumericTextBox)e.Item.FindControl("txtHomemHoraNovo");
                TextBox txtDescricaoServico = (TextBox)e.Item.FindControl("txtDescricaoServicoNovo");
                
                PedidoServicoDelineamento delineamento = new PedidoServicoDelineamento();
                delineamento.DelineamentoOrcamento = _delineamentoOrcamento;
                delineamento.Celula = Celula.Get(Convert.ToInt32(ddlCelula.SelectedValue));
                delineamento.HomemHora = Convert.ToInt32(txtHomemHora.Text);
                delineamento.DescricaoServicoOficina = PageReader.ReadString(txtDescricaoServico);
                delineamento.ServidorDelineamento = _delineamentoOrcamento.Delineamentos.Where(d => d.Servidor.ID == this.ID_Servidor).First().Servidor;
                _delineamentoOrcamento.AddDelineamento(delineamento);

                BindDelineamento();
                dgDelineamento.ShowFooter = false;
            }
            catch (Exception ex)
            {
                Anthem.AnthemClientMethods.Alert(ex.Message, this);
            }
        }
    }

    void dgDelineamento_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        DropDownList ddlCelula = (DropDownList)e.Item.FindControl("ddlCelula");
        Anthem.NumericTextBox txtHomemHora = (Anthem.NumericTextBox)e.Item.FindControl("txtHomemHora");
        TextBox txtDescricaoServico = (TextBox)e.Item.FindControl("txtDescricaoServico");

        int id = Convert.ToInt32(dgDelineamento.DataKeys[e.Item.ItemIndex]);
        
        PedidoServicoDelineamento delineamento = _delineamentoOrcamento.ItensDelineamento.Find(id);
        delineamento.HomemHora = Convert.ToInt32(txtHomemHora.Text);
        delineamento.Celula = Celula.Get(Convert.ToInt32(ddlCelula.SelectedValue));
        delineamento.DescricaoServicoOficina = PageReader.ReadString(txtDescricaoServico);
        delineamento.Save();

        
        dgDelineamento.EditItemIndex = -1;
        BindDelineamento();
    }

    void dgDelineamento_EditCommand(object source, DataGridCommandEventArgs e)
    {
        dgDelineamento.ShowFooter = false;
        dgDelineamento.EditItemIndex = e.Item.ItemIndex;
        BindDelineamento();
    }

    void dgDelineamento_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dgDelineamento.EditItemIndex = -1;
        dgDelineamento.ShowFooter = false;
        BindDelineamento();
    }
    #endregion

    #region Bind
    private void BindMaterial()
    {
		dgMaterial.DataSource = _delineamentoOrcamento.GetItensOrcamento(TipoServicoMaterial.Material); 
        dgMaterial.DataKeyField = "ID";
        dgMaterial.DataBind();
        dgMaterial.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();

    }

    private void BindServico()
    {
        dgServicos.DataSource = _delineamentoOrcamento.GetItensOrcamento(TipoServicoMaterial.Servico);
        dgServicos.DataKeyField = "ID";
        dgServicos.DataBind();
        dgServicos.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();

        //lblValorTotal.Text = string.Format("Valor Total: {0:c2}", _delineamentoOrcamento.ValorTotalOrcamento);
        //lblValorTotal.UpdateAfterCallBack = true;
    }

    [Anthem.Method]
    public void ExcluirDelineamento(int id)
    {
        _delineamentoOrcamento.RemoveDelineamento(_delineamentoOrcamento.ItensDelineamento.Find(id));
        BindDelineamento();
    }

    void btnNovoDelineamento_Click(object sender, EventArgs e)
    {
        dgDelineamento.ShowFooter = true;
        //Bind();
        dgDelineamento.UpdateAfterCallBack = true;
    }
    #endregion

    #region Material

    void dgMaterial_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Insert")
        {
            try
            {
                DropDownList ddlOrigemMaterial = (DropDownList)e.Item.FindControl("ddlOrigemMaterialNovo");
                BuscaServicoMaterial ucServicoMaterial = (BuscaServicoMaterial)e.Item.FindControl("ucServicoMaterialNovo");
                Anthem.NumericTextBox txtQuantidade = (Anthem.NumericTextBox)e.Item.FindControl("txtQuantidadeNovo");
                
				if(ucServicoMaterial.SelectedValue == "" || ucServicoMaterial.SelectedValue == "0")
				{
				    ShowMessage("Campo Serviço/Material obrigatório");
				    return;
				}
                
                PedidoServicoItemOrcamento item = new PedidoServicoItemOrcamento();
                
                item.DelineamentoOrcamento = _delineamentoOrcamento;
                item.OrigemMaterial =  (OrigemMaterial)Convert.ToInt32(ddlOrigemMaterial.SelectedValue);
                item.Quantidade = Convert.ToDecimal(txtQuantidade.Text);
                item.ServicoMaterial = ServicoMaterial.Get(Convert.ToInt32(ucServicoMaterial.SelectedValue));
                item.Valor = item.ServicoMaterial.PrecoEstimadoVenda;
                item.Celula = _delineamentoOficina.Oficina;
                item.ServidorDelineamento = _delineamentoOficina.Servidor;

                //item.Fornecedor = Fornecedor.Get(Convert.ToInt32(ucDadosComplementares.ID_Fornecedor));
                item.Observacao = ucDadosComplementares.Observacao;

                //if(Convert.ToInt32(ddlOrigemMaterial.SelectedValue) != Convert.ToInt32(OrigemMaterial.Singra) )
                //    item.Valor = Convert.ToDecimal(txtValor.Text);
                //else
                //    item.RMC = txtRMC.Text;
                
                _delineamentoOrcamento.AddItemOrcamento(item);
            	
                BindMaterial();
                dgMaterial.ShowFooter = false;
            }
            catch (Exception ex)
            {
                Anthem.AnthemClientMethods.Alert(ex.Message, this);
            }
        }
        else if (e.CommandName == "DadosComplementares")
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.EditItem)
            {
                int id = Convert.ToInt32(dgMaterial.DataKeys[e.Item.ItemIndex]);
                PedidoServicoItemOrcamento item = _delineamentoOrcamento.ItensOrcamento.Find(id);
                ucDadosComplementares.Show();
                ucDadosComplementares.Fill(item);
                ucDadosComplementares.ID_Item = item.ID;
            }
            else
            {
                ucDadosComplementares.ID_Item = Int32.MinValue;
                ucDadosComplementares.Show();
            }

        }
    }

    void dgMaterial_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        DropDownList ddlOrigemMaterial = (DropDownList)e.Item.FindControl("ddlOrigemMaterial");
        //BuscaServicoMaterial ucServicoMaterial = (BuscaServicoMaterial)e.Item.FindControl("ucServicoMaterial");
        //Anthem.NumericTextBox txtQuantidade = (Anthem.NumericTextBox)e.Item.FindControl("txtQuantidade");
        
        //if (ucServicoMaterial.SelectedValue == "" || ucServicoMaterial.SelectedValue == "0")
        //{
        //    ShowMessage("Campo Serviço/Material obrigatório");
        //    return;
        //}

        int id = Convert.ToInt32(dgMaterial.DataKeys[e.Item.ItemIndex]);

        PedidoServicoItemOrcamento item = _delineamentoOrcamento.ItensOrcamento.Find(id);

        item.OrigemMaterial = (OrigemMaterial)Convert.ToInt32(ddlOrigemMaterial.SelectedValue);
        //item.Quantidade = Convert.ToDecimal(txtQuantidade.Text);
        //item.ServicoMaterial = ServicoMaterial.Get(Convert.ToInt32(ucServicoMaterial.SelectedValue));
        //if (Convert.ToInt32(ddlOrigemMaterial.SelectedValue) != Convert.ToInt32(OrigemMaterial.Singra))
        //    item.Valor = Convert.ToDecimal(txtValor.Text);
        //else
        //    item.RMC = txtRMC.Text;

        item.Save();

        dgMaterial.EditItemIndex = -1;
        BindMaterial();
    }

    void dgMaterial_EditCommand(object source, DataGridCommandEventArgs e)
    {
        dgMaterial.ShowFooter = false;
        dgMaterial.EditItemIndex = e.Item.ItemIndex;
        BindMaterial();
    }

    private void dgMaterial_OnItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Footer)
        {
            //DropDownList ddlOrigemMaterial = (DropDownList)e.Item.FindControl("ddlOrigemMaterialNovo");
            //Util.FillDropDownList(ddlOrigemMaterial, typeof(OrigemMaterial));
            //OrigemMaterialChanged(ddlOrigemMaterial, e.Item);
            
            //e.Item.Cells[1].Attributes.Add("colspan","4");
            //e.Item.Cells[2].Visible = false;
            //e.Item.Cells[3].Visible = false;
            //e.Item.Cells[4].Visible = false;                        
        }
        else if (e.Item.ItemType == ListItemType.EditItem)
        {
            DropDownList ddlOrigemMaterial = (DropDownList)e.Item.FindControl("ddlOrigemMaterial");
            Util.FillDropDownList(ddlOrigemMaterial, typeof(OrigemMaterial));

            PedidoServicoItemOrcamento orcamento = (PedidoServicoItemOrcamento) e.Item.DataItem;
            ddlOrigemMaterial.SelectedValue = Convert.ToInt32(orcamento.OrigemMaterial).ToString();
            OrigemMaterialChanged(ddlOrigemMaterial, e.Item);

            e.Item.Cells[1].Attributes.Add("colspan", "4");
            e.Item.Cells[2].Visible = false;
            e.Item.Cells[3].Visible = false;
            e.Item.Cells[4].Visible = false;
        }
        else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            PedidoServicoItemOrcamento orcamento = (PedidoServicoItemOrcamento)e.Item.DataItem;
            //liberada a edicao por pedido do emerson email do dia 14/07/2011
           // if (orcamento.ServidorDelineamento.ID != this.ID_Servidor || _delineamentoOficina.Enviado)
            //    e.Item.Cells[e.Item.Cells.Count - 1].Visible = false;

            MostrarSaldoLicitacao(e.Item);

        }
        MostrarQuantidadeEstoque(e.Item);
    }

    void dgMaterial_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dgMaterial.ShowFooter = false;
        dgMaterial.EditItemIndex = -1;
        BindMaterial();
    }

	[Anthem.Method]
	public void Excluir(int id)
	{
	    _delineamentoOrcamento.RemoveItemOrcamento(_delineamentoOrcamento.ItensOrcamento.Find(id));
		BindMaterial();
	}

    void btnNovo_Click(object sender, EventArgs e)
    {
        dgMaterial.ShowFooter = true;
        dgMaterial.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();
    }
    #endregion

    #region Servico

    void dgServicos_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Insert")
        {
            try
            {
                BuscaServicoMaterial ucServicoMaterial = (BuscaServicoMaterial)e.Item.FindControl("ucServicoMaterialNovo");
                Anthem.NumericTextBox txtQuantidade = (Anthem.NumericTextBox)e.Item.FindControl("txtQuantidadeNovo");

                if (ucServicoMaterial.SelectedValue == "" || ucServicoMaterial.SelectedValue == "0")
                {
                    ShowMessage("Campo Serviço obrigatório");
                    return;
                }
                
                PedidoServicoItemOrcamento item = new PedidoServicoItemOrcamento();
                item.DelineamentoOrcamento = _delineamentoOrcamento;
                item.OrigemMaterial = OrigemMaterial.Obtencao;
                item.Quantidade = Convert.ToDecimal(txtQuantidade.Text);
                item.ServicoMaterial = ServicoMaterial.Get(Convert.ToInt32(ucServicoMaterial.SelectedValue));
                //item.Fornecedor = Fornecedor.Get(Convert.ToInt32(ucDadosComplementares.ID_Fornecedor));
                item.Observacao = ucDadosComplementares.Observacao;
                item.Celula = _delineamentoOficina.Oficina;
                item.ServidorDelineamento = _delineamentoOficina.Servidor;

                _delineamentoOrcamento.AddItemOrcamento(item);

                BindServico();
                dgServicos.ShowFooter = false;
            }
            catch (Exception ex)
            {
                Anthem.AnthemClientMethods.Alert(ex.Message, this);
            }
        }
        else if (e.CommandName == "DadosComplementares")
        {
            if(e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.EditItem)
            {
                int id = Convert.ToInt32(dgServicos.DataKeys[e.Item.ItemIndex]);
                PedidoServicoItemOrcamento item = _delineamentoOrcamento.ItensOrcamento.Find(id);
                ucDadosComplementares.Show();
                ucDadosComplementares.Fill(item);
                ucDadosComplementares.ID_Item = item.ID;
            }
            else
            {
                ucDadosComplementares.ID_Item = Int32.MinValue;
                ucDadosComplementares.Show();
            }

            
        }
    }

    private void UcDadosComplementares_OnOkClicked(object sender, EventArgs e)
    {
        if(ucDadosComplementares.ID_Item > 0)
        {
            PedidoServicoItemOrcamento item = _delineamentoOrcamento.ItensOrcamento.Find(ucDadosComplementares.ID_Item);
            //item.Fornecedor = Fornecedor.Get(Convert.ToInt32(ucDadosComplementares.ID_Fornecedor));
            item.Observacao = ucDadosComplementares.Observacao;
            item.Save();
        }
    }

    void dgServicos_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        BuscaServicoMaterial ucServicoMaterial = (BuscaServicoMaterial)e.Item.FindControl("ucServicoMaterial");
        Anthem.NumericTextBox txtQuantidade = (Anthem.NumericTextBox)e.Item.FindControl("txtQuantidade");

        if (ucServicoMaterial.SelectedValue == "" || ucServicoMaterial.SelectedValue == "0")
        {
            ShowMessage("Campo Serviço obrigatório");
            return;
        }

        int id = Convert.ToInt32(dgServicos.DataKeys[e.Item.ItemIndex]);
        PedidoServicoItemOrcamento item = _delineamentoOrcamento.ItensOrcamento.Find(id);
        item.Quantidade = Convert.ToDecimal(txtQuantidade.Text);
        item.ServicoMaterial = ServicoMaterial.Get(Convert.ToInt32(ucServicoMaterial.SelectedValue));
        
        item.Save();

        dgServicos.EditItemIndex = -1;
        BindServico();
    }

    void dgServicos_EditCommand(object source, DataGridCommandEventArgs e)
    {
        dgServicos.ShowFooter = false;
        dgServicos.EditItemIndex = e.Item.ItemIndex;
        BindServico();
    }

    private void dgServicos_OnItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Footer)
        {
            e.Item.Cells[1].Attributes.Add("colspan", "4");
            e.Item.Cells[2].Visible = false;
            e.Item.Cells[3].Visible = false;
            e.Item.Cells[4].Visible = false;
        }
        else if (e.Item.ItemType == ListItemType.EditItem)
        {
            e.Item.Cells[1].Attributes.Add("colspan", "4");
            e.Item.Cells[2].Visible = false;
            e.Item.Cells[3].Visible = false;
            e.Item.Cells[4].Visible = false;
        }
        else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            PedidoServicoItemOrcamento orcamento = (PedidoServicoItemOrcamento)e.Item.DataItem;
            //if (orcamento.ServidorDelineamento.ID != this.ID_Servidor || _delineamentoOficina.Enviado)
            //    e.Item.Cells[e.Item.Cells.Count - 1].Visible = false;
            MostrarSaldoLicitacao(e.Item);
        }
    }

    void dgServicos_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dgServicos.ShowFooter = false;
        dgServicos.EditItemIndex = -1;
        BindServico();
    }

    [Anthem.Method]
    public void ExcluirItemServico(int id)
    {
        _delineamentoOrcamento.RemoveItemOrcamento(_delineamentoOrcamento.ItensOrcamento.Find(id));
        BindServico();
    }

    void btnNovoServico_Click(object sender, EventArgs e)
    {
        dgServicos.ShowFooter = true;
        dgServicos.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();
    }
    #endregion
    
    #region Selected Index Changed
    protected void ddlOrigemMaterial_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList) sender;
        DataGridItem item = (DataGridItem)ddl.Parent.Parent;
        OrigemMaterialChanged(ddl, item);
        MostrarQuantidadeEstoque(item);
    }

    private void OrigemMaterialChanged(DropDownList ddl, DataGridItem item)
    {
        
        
    }

    protected void ucServicoMaterial_SelectedValueChanged(object sender, BuscaServicoMaterialEventArgs e)
    {
        BuscaServicoMaterial uc = (BuscaServicoMaterial)sender;
        DataGridItem item = (DataGridItem)uc.Parent.Parent;
        Anthem.DropDownList ddlOrigemMaterial = (Anthem.DropDownList)item.FindControl("ddlOrigemMaterial" + (item.ItemType == ListItemType.Footer ? "Novo" : ""));
       

        if(e.ServicoMaterial.TipoServicoMaterial == TipoServicoMaterial.Material)
        {
            ddlOrigemMaterial.SelectedValue = Convert.ToInt32(e.ServicoMaterial.OrigemMaterial).ToString();
            ddlOrigemMaterial.Enabled = true;
            ddlOrigemMaterial.UpdateAfterCallBack = true;
            OrigemMaterialChanged(ddlOrigemMaterial, item);
            MostrarQuantidadeEstoque(item);
            Anthem.TextBox txtCodigoMaterial = (Anthem.TextBox)item.FindControl("txtCodigoMaterial");
            txtCodigoMaterial.Text = e.ServicoMaterial.CodigoInterno;
            txtCodigoMaterial.UpdateAfterCallBack = true;
        }
    }

    private void MostrarQuantidadeEstoque(DataGridItem itemGrid)
    {
        //if(itemGrid.ItemType == ListItemType.EditItem || (itemGrid.ItemType == ListItemType.Footer && itemGrid.Visible))
        //{
        //    Anthem.DropDownList ddlOrigemMaterial =(Anthem.DropDownList)itemGrid.FindControl("ddlOrigemMaterial" + (itemGrid.ItemType == ListItemType.Footer ? "Novo" : ""));
        //    BuscaServicoMaterial ucServicoMaterial = (BuscaServicoMaterial)itemGrid.FindControl("ucServicoMaterial" + (itemGrid.ItemType == ListItemType.Footer ? "Novo" : ""));
           
        //    if(ucServicoMaterial.SelectedValue != "0" && ddlOrigemMaterial.SelectedValue != "0")
        //    {
        //        Anthem.Label lblQuantidadeEstoque = (Anthem.Label) itemGrid.FindControl("lblQuantidadeEstoque");
        //        lblQuantidadeEstoque.Text = MovimentoEstoque.GetQuantidadeEstoque(Convert.ToInt32(ucServicoMaterial.SelectedValue),
        //                                             Convert.ToInt32(ddlOrigemMaterial.SelectedValue)).QuantidadeDisponivel.ToString();
        //        lblQuantidadeEstoque.UpdateAfterCallBack = true;

               

        //        //if (Convert.ToInt32(ddlOrigemMaterial.SelectedValue) != Convert.ToInt32(OrigemMaterial.Singra)
        //        //        && Convert.ToInt32(ddlOrigemMaterial.SelectedValue) != Convert.ToInt32(OrigemMaterial.Rodizio))
        //        //{
        //        //    txtValor.Text = PedidoServicoItemOrcamento.GetUltimoOrcamento(
        //        //        Convert.ToInt32(ucServicoMaterial.SelectedValue)).ToString("N2");
        //        //    txtValor.UpdateAfterCallBack = true;
        //        //}
        //    }
        //}
        //else if(itemGrid.ItemType == ListItemType.AlternatingItem || itemGrid.ItemType == ListItemType.Item)
        //{
        //    PedidoServicoItemOrcamento item = (PedidoServicoItemOrcamento) itemGrid.DataItem;
        //    Anthem.Label lblQuantidadeEstoque = (Anthem.Label)itemGrid.FindControl("lblQuantidadeEstoque");
        //    lblQuantidadeEstoque.Text = MovimentoEstoque.GetQuantidadeEstoque(item.ServicoMaterial.ID, Convert.ToInt32(item.OrigemMaterial)).QuantidadeDisponivel.ToString();
        //    lblQuantidadeEstoque.UpdateAfterCallBack = true;
        //}
    }

    private void MostrarSaldoLicitacao(DataGridItem itemGrid)
    {
        Label lblSaldoLicitacao = (Label)itemGrid.FindControl("lblSaldoLicitacao");
        PedidoServicoItemOrcamento item = (PedidoServicoItemOrcamento)itemGrid.DataItem;
        LicitacaoItem itemLicitacao = LicitacaoItem.GetItemAberto(item.ServicoMaterial.ID, 0);
        if (itemLicitacao != null)
            lblSaldoLicitacao.Text = itemLicitacao.QuantidadeDisponivel.ToString();
        else
            lblSaldoLicitacao.Text = "0";
    }
    #endregion

    void btnEnviar_Click(object sender, EventArgs e)
    {
        //ucComentario.Show();

        //if (ddlRotina.SelectedValue == "0")
        //{
        //    ShowMessage("Selecione a Rotina");
        //    return;
        //}

        //_delineamentoOrcamento.Rotina = Rotina.Get(Convert.ToInt32(ddlRotina.SelectedValue));
        //_delineamentoOrcamento.FinalizarDelineamento(this.ID_Servidor, ucComentario.Comentario);
        _delineamentoOrcamento.RegistrarMensagemCliente(this.ID_Servidor, null);
        Anthem.AnthemClientMethods.Redirect("frmPedidoServicoPendente.aspx");
    }

    void ucComentario_OkClicked(object sender, EventArgs e)
    {
        //_delineamentoOrcamento.FinalizarOrcamento(this.ID_Servidor, ucComentario.Comentario);
        //Anthem.AnthemClientMethods.Redirect("frmPedidoServicoPendente.aspx");
    }
    
    [Anthem.Method]
    public void CopiarOrcamento(int id)
    {
        try
        {
            DelineamentoOrcamento orcamento = DelineamentoOrcamento.Get(id);
            _delineamentoOrcamento = new DelineamentoOrcamento();
            _delineamentoOrcamento.PedidoServico = _pedido;
            _delineamentoOrcamento.CategoriaServico = orcamento.CategoriaServico;
            
            foreach (PedidoServicoDelineamento delineamento in orcamento.ItensDelineamento)
            {
                PedidoServicoDelineamento novoDelineamento = new PedidoServicoDelineamento();
                novoDelineamento.DelineamentoOrcamento = _delineamentoOrcamento;
                novoDelineamento.DescricaoServicoOficina = delineamento.DescricaoServicoOficina;
                novoDelineamento.HomemHora = delineamento.HomemHora;
                novoDelineamento.Celula = delineamento.Celula;
                novoDelineamento.Data = DateTime.Today;
                _delineamentoOrcamento.AddDelineamento(novoDelineamento);
            }

            foreach (PedidoServicoItemOrcamento itemOrcamento in orcamento.ItensOrcamento)
            {
                PedidoServicoItemOrcamento novoOrcamento = new PedidoServicoItemOrcamento();
                novoOrcamento.DelineamentoOrcamento = _delineamentoOrcamento;
                novoOrcamento.OrigemMaterial = itemOrcamento.OrigemMaterial;
                novoOrcamento.Quantidade = itemOrcamento.Quantidade;
                novoOrcamento.ServicoMaterial = itemOrcamento.ServicoMaterial;
                novoOrcamento.Valor = itemOrcamento.Valor;
                novoOrcamento.Celula = itemOrcamento.Celula;
                novoOrcamento.Fornecedor = itemOrcamento.Fornecedor;
                novoOrcamento.Observacao = itemOrcamento.Observacao;
                _delineamentoOrcamento.AddItemOrcamento(novoOrcamento);
            }

            Populate();
            BindMaterial();
            BindServico();
            BindDelineamento();
            dgMaterial.ShowFooter = false;
            dgDelineamento.ShowFooter = false;
        }
        catch(Exception ex)
        {
            ShowMessage(GetCompleteErrorMessage(ex));
        }
    }

    protected void txtCodigoMaterial_TextChanged(object sender, EventArgs e)
    {
        TextBox txtCodigoMaterial = (TextBox) sender;

        TipoServicoMaterial tipo;
        if(txtCodigoMaterial.ID == "txtCodigoMaterial")
            tipo = TipoServicoMaterial.Material;
        else
            tipo = TipoServicoMaterial.Servico;
            
        ServicoMaterial sm = ServicoMaterial.GetByCodigo(txtCodigoMaterial.Text, tipo);
        if(sm != null)
        {
            DataGridItem item = (DataGridItem)txtCodigoMaterial.Parent.Parent;
            BuscaServicoMaterial ucServicoMaterial = (BuscaServicoMaterial) item.FindControl("ucServicoMaterialNovo");
            ucServicoMaterial.FireEvent(sm.ID.ToString());
        }
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

    void btnImprimir_Click(object sender, EventArgs e)
    {
        Anthem.AnthemClientMethods.Popup(this, "fchOrcamento.aspx?id_orcamento=" + _delineamentoOrcamento.ID, false, false, false, true, true, true, true, 20, 50, 700, 500);
    }

    #region Rotina
    [Anthem.Method]
    public void AdicionarRotina(int id)
    {
        //Rotina rotina = Rotina.Get(Convert.ToInt32(ddlRotina.SelectedValue));

        //if(_delineamentoOrcamento.Rotinas.Find(rotina.ID) != null)
        //{
        //    ShowMessage("Esta rotina já foi adicionada.");
        //    return;
        //}

        //RotinaCategoriaServico rotinaCategoriaServico = rotina.CategoriasServico.Where(c => c.CategoriaServico.ID == _delineamentoOrcamento.CategoriaServico.ID).FirstOrDefault();
        //if(rotinaCategoriaServico == null)
        //{
        //    ShowMessage("Esta rotina não contem a categoria " + _delineamentoOrcamento.CategoriaServico.Descricao  + ".");
        //    return;
        //}

        //List<PedidoServicoDelineamento> delineamentosRemover = _delineamentoOrcamento.ItensDelineamento.Where(d => d.ServidorDelineamento.ID == this.ID_Servidor).ToList();
        //foreach (PedidoServicoDelineamento d in delineamentosRemover)
        //{
        //    _delineamentoOrcamento.RemoveDelineamento(d);
        //}

        
        //foreach (RotinaCategoriaServicoDelineamento delineamento in rotinaCategoriaServico.ItensDelineamento)
        //{
        //    PedidoServicoDelineamento novoDelineamento = new PedidoServicoDelineamento();
        //    novoDelineamento.Celula = delineamento.Celula;
        //    novoDelineamento.DescricaoServicoOficina = delineamento.DescricaoServicoOficina;
        //    novoDelineamento.HomemHora = delineamento.HomemHora;
        //    novoDelineamento.ServidorDelineamento = _delineamentoOficina.Servidor;
        //    novoDelineamento.DelineamentoOrcamento = _delineamentoOrcamento;
        //    novoDelineamento.Data = DateTime.Today;
        //    _delineamentoOrcamento.ItensDelineamento.Add(novoDelineamento);
        //    novoDelineamento.Save();
        //}
        //BindDelineamento();

        //List<PedidoServicoItemOrcamento> itemRemover = _delineamentoOrcamento.ItensOrcamento.Where(d => d.ServidorDelineamento.ID == this.ID_Servidor).ToList();
        //foreach (PedidoServicoItemOrcamento d in itemRemover)
        //{
        //    _delineamentoOrcamento.RemoveItemOrcamento(d);
        //}


        //foreach (RotinaCategoriaServicoItemOrcamento item in rotinaCategoriaServico.ItensOrcamento)
        //{
        //    PedidoServicoItemOrcamento novoItem = new PedidoServicoItemOrcamento();
        //    novoItem.Celula = item.Celula;
        //    novoItem.OrigemMaterial = item.OrigemMaterial;
        //    novoItem.ServicoMaterial = item.ServicoMaterial;
        //    novoItem.Valor = item.ServicoMaterial.PrecoEstimadoVenda;
        //    novoItem.Quantidade = item.Quantidade;
        //    novoItem.ServidorDelineamento = _delineamentoOficina.Servidor;
        //    novoItem.DelineamentoOrcamento = _delineamentoOrcamento;
        //    _delineamentoOrcamento.ItensOrcamento.Add(novoItem);
        //    novoItem.Save();
        //}
        //BindServico();
        //BindMaterial();
        //_delineamentoOrcamento.Rotinas.Add(rotina);
        //BindRotina();
        //dgMaterial.ShowFooter = dgServicos.ShowFooter = dgDelineamento.ShowFooter = false;
    }
    #endregion

    #region Rotinas

    private void BindRotina()
    {
        dlRotinas.DataSource = _delineamentoOrcamento.Rotinas;
        dlRotinas.DataKeyField = "ID";
        dlRotinas.DataBind();
        dlRotinas.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();
    }

    #endregion

    void btnSalvar_Click(object sender, EventArgs e)
    {
        //_delineamentoOrcamento.Rotina = Rotina.Get(Convert.ToInt32(ddlRotina.SelectedValue));
        _delineamentoOrcamento.Save();
        ShowSuccessMessage();
    }

    void dlRotinas_DeleteCommand(object source, DataListCommandEventArgs e)
    {
        var rotina = _delineamentoOrcamento.Rotinas.Find(Convert.ToInt32(e.CommandArgument));
        _delineamentoOrcamento.Rotinas.Remove(rotina);
        _delineamentoOrcamento.Save();
        BindRotina();
    }
}
