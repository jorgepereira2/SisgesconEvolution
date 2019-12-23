using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Marinha.Business;
using Shared.Common;
using Shared.NHibernateDAL;
using Shared.SessionState;

public partial class frmPedidoServicoMergulhoDetalhamento : MarinhaPageBase
{
    #region private variables
    
    protected PedidoServicoMergulho _pedido
    {
        get{ return (PedidoServicoMergulho) Session["frmPedidoServicoMergulhoOrcamento._pedido"]; }
        set{ Session["frmPedidoServicoMergulhoOrcamento._pedido"] = value;}
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
        
        btnImprimir.Click += new EventHandler(btnImprimir_Click);

        dgServicos.ItemDataBound += new DataGridItemEventHandler(dgServicos_OnItemDataBound);
        dgServicos.CancelCommand +=new DataGridCommandEventHandler(dgServicos_CancelCommand);
        dgServicos.ItemCommand +=new DataGridCommandEventHandler(dgServicos_ItemCommand);
        dgServicos.EditCommand +=new DataGridCommandEventHandler(dgServicos_EditCommand);
        dgServicos.UpdateCommand +=new DataGridCommandEventHandler(dgServicos_UpdateCommand);
        this.btnNovoServico.Click += new EventHandler(btnNovoServico_Click);
        ucDadosComplementares.OkClicked += UcDadosComplementares_OnOkClicked;
        ucComentario.OkClicked += new EventHandler(ucComentario_OkClicked);


    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Anthem.Manager.Register(this);
        if (!this.IsPostBack)
        {
            _pedido = PedidoServicoMergulho.Get(Convert.ToInt32(Request["id_pedido"]));
         

            BindDelineamento();
            BindMaterial();
            BindServico();
            Anthem.AnthemClientMethods.Redirect("frmPedidoServicoMergulhoPendente.aspx", btnVoltar);
            

            //Anthem.AnthemClientMethods.Popup(btnDefinirFornecedor, "frmPedidoServicoMergulhoOrcamentoFornecedor.aspx", false, false, false, true, true, true, true, 80, 120, 650, 500, false);
            
            RegisterDeleteScript();
            RegisterDeleteScript("ExcluirDelineamento");
            RegisterDeleteScript("ExcluirItemServico");

            RegisterConfirmScript("AdicionarRotina", "Os itens deste orçamento serão sobrescritos. Deseja prosseguir?");
            
            
            if(_pedido.ItensOrcamento.Count == 0)
                dgMaterial.ShowFooter = true;

            if (_pedido.ItensOrcamento.Count == 0)
                dgServicos.ShowFooter = true;

            if (_pedido.ItensDelineamento.Count == 0)
                dgDelineamento.ShowFooter = true;
            
            if(_pedido.Status.StatusPedidoServicoMergulhoEnum == StatusPedidoServicoMergulhoEnum.AguardandoDetalhamentoParaSupervidor)
            {
                dgServicos.Columns[dgServicos.Columns.Count - 3].Visible = false;
                dgMaterial.Columns[dgMaterial.Columns.Count - 3].Visible = false;
            }

            FillPage();
            Populate();

           // btnEnviar.Visible = !_delineamentoOficina.Enviado;
        }
    }
    
    private  void FillPage()
    {
        //Util.FillDropDownList(ddlRotina, Rotina.List(), ESCOLHA_OPCAO);
        Util.FillDropDownList(ddlEmbarcacao, Embarcacao.List(), ESCOLHA_OPCAO);
    }

    private void Populate()
    {
        lblCodigo.Text = _pedido.CodigoComAno;
        lblDataEmissao.Text = _pedido.DataEmissao.ToShortDateString();
        lblStatus.Text = _pedido.Status.Descricao;

       
        trJustificativaRecusa.Visible = _pedido.FlagRecusado;
        lblJustificativaRecusa.Text = _pedido.UltimoHistorico.JustificativaRecusa;
        ddlEmbarcacao.SelectedValue = ObjectReader.ReadID(_pedido.Embarcacao);
        txtDataPrevisaoInicio.Text = ObjectReader.ReadDate(_pedido.DataPrevisaoInicio);
        txtDataPrevisaoFim.Text = ObjectReader.ReadDate(_pedido.DataPrevisaoFim);
            
       

        Anthem.AnthemClientMethods.Popup(lnkDetalhes, "fchPedidoServicoMergulho.aspx?id_pedido=" + _pedido.ID.ToString(),
            false, false, false, true, true, true, true, 10, 40, 700, 520, false);
    }
    #endregion
    
    #region Delineamento
    private void BindDelineamento()
    {
        dgDelineamento.DataSource = _pedido.ItensDelineamento;
        dgDelineamento.DataKeyField = "ID";
        dgDelineamento.DataBind();
        dgDelineamento.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();

        lblHomemHoraTotal.Text = string.Format("HH Total: {0}", _pedido.HomemHoraTotal);
        lblHomemHoraTotal.UpdateAfterCallBack = true;
    }

    void dgDelineamento_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Footer)
        {
            DropDownList ddlCelula = (DropDownList)e.Item.FindControl("ddlCelulaNovo");
            Util.FillDropDownList(ddlCelula, Celula.List(null, true), ESCOLHA_OPCAO);
        }
        else if (e.Item.ItemType == ListItemType.EditItem)
        {
            DropDownList ddlCelula = (DropDownList)e.Item.FindControl("ddlCelula");
            Util.FillDropDownList(ddlCelula, Celula.List(null, true), ESCOLHA_OPCAO);
            PedidoServicoMergulhoDelineamento delineamento = (PedidoServicoMergulhoDelineamento)e.Item.DataItem;

            ddlCelula.SelectedValue = delineamento.Celula.ID.ToString();
        }
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            //PedidoServicoMergulhoDelineamento delineamento = (PedidoServicoMergulhoDelineamento)e.Item.DataItem;
            
        }
    }

    void dgDelineamento_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Insert")
        {
            try
            {
                DropDownList ddlCelula = (DropDownList)e.Item.FindControl("ddlCelulaNovo");
                Anthem.NumericTextBox txtQuantidadeMergulhadores = (Anthem.NumericTextBox)e.Item.FindControl("txtQuantidadeMergulhadoresNovo");
                Anthem.NumericTextBox txtTempoFainaDiaria = (Anthem.NumericTextBox)e.Item.FindControl("txtTempoFainaDiariaNovo");
                Anthem.NumericTextBox txtNumeroDias = (Anthem.NumericTextBox)e.Item.FindControl("txtNumeroDiasNovo");
                TextBox txtDescricaoServico = (TextBox)e.Item.FindControl("txtDescricaoServicoNovo");
                
                PedidoServicoMergulhoDelineamento delineamento = new PedidoServicoMergulhoDelineamento();
                delineamento.PedidoServicoMergulho = _pedido;
                delineamento.Celula = Celula.Get(Convert.ToInt32(ddlCelula.SelectedValue));
                delineamento.NumeroDias = Convert.ToInt32(txtNumeroDias.Text);
                delineamento.TempoFainaDiaria = Convert.ToInt32(txtTempoFainaDiaria.Text);
                delineamento.QuantidadeMergulhadores = Convert.ToInt32(txtQuantidadeMergulhadores.Text);
                delineamento.DescricaoServicoOficina = PageReader.ReadString(txtDescricaoServico);
                _pedido.AddDelineamento(delineamento);

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
        Anthem.NumericTextBox txtQuantidadeMergulhadores = (Anthem.NumericTextBox)e.Item.FindControl("txtQuantidadeMergulhadores");
        Anthem.NumericTextBox txtTempoFainaDiaria = (Anthem.NumericTextBox)e.Item.FindControl("txtTempoFainaDiaria");
        Anthem.NumericTextBox txtNumeroDias = (Anthem.NumericTextBox)e.Item.FindControl("txtNumeroDias");
        TextBox txtDescricaoServico = (TextBox)e.Item.FindControl("txtDescricaoServico");

        int id = Convert.ToInt32(dgDelineamento.DataKeys[e.Item.ItemIndex]);
        
        PedidoServicoMergulhoDelineamento delineamento = _pedido.ItensDelineamento.Find(id);
        delineamento.NumeroDias = Convert.ToInt32(txtNumeroDias.Text);
        delineamento.TempoFainaDiaria = Convert.ToInt32(txtTempoFainaDiaria.Text);
        delineamento.QuantidadeMergulhadores = Convert.ToInt32(txtQuantidadeMergulhadores.Text);
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
		dgMaterial.DataSource = _pedido.ItensOrcamento.Where(i => i.ServicoMaterial.TipoServicoMaterial == TipoServicoMaterial.Material); 
        dgMaterial.DataKeyField = "ID";
        dgMaterial.DataBind();
        dgMaterial.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();

    }

    private void BindServico()
    {
        dgServicos.DataSource = _pedido.ItensOrcamento.Where(i => i.ServicoMaterial.TipoServicoMaterial == TipoServicoMaterial.Servico); 
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
        _pedido.RemoveDelineamento(_pedido.ItensDelineamento.Find(id));
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
                DropDownList ddlCelula = (DropDownList)e.Item.FindControl("ddlCelulaNovo");
                DropDownList ddlOrigemMaterial = (DropDownList)e.Item.FindControl("ddlOrigemMaterialNovo");
                BuscaServicoMaterial ucServicoMaterial = (BuscaServicoMaterial)e.Item.FindControl("ucServicoMaterialNovo");
                Anthem.NumericTextBox txtQuantidade = (Anthem.NumericTextBox)e.Item.FindControl("txtQuantidadeNovo");
                Anthem.NumericTextBox txtValor = (Anthem.NumericTextBox)e.Item.FindControl("txtValorNovo");
                
				if(ucServicoMaterial.SelectedValue == "" || ucServicoMaterial.SelectedValue == "0")
				{
				    ShowMessage("Campo Serviço/Material obrigatório");
				    return;
				}
                
                PedidoServicoMergulhoItemOrcamento item = new PedidoServicoMergulhoItemOrcamento();
                item.PedidoServicoMergulho = _pedido;
                item.OrigemMaterial =  (OrigemMaterial)Convert.ToInt32(ddlOrigemMaterial.SelectedValue);
                item.Quantidade = Convert.ToDecimal(txtQuantidade.Text);
                item.ServicoMaterial = ServicoMaterial.Get(Convert.ToInt32(ucServicoMaterial.SelectedValue));
                item.Celula = Celula.Get(Convert.ToInt32(ddlCelula.SelectedValue));
                item.Valor = txtValor.Text == "" ? 0 : Convert.ToDecimal(txtValor.Text);

                //item.Fornecedor = Fornecedor.Get(Convert.ToInt32(ucDadosComplementares.ID_Fornecedor));
                item.Observacao = ucDadosComplementares.Observacao;
                
                _pedido.AddItemOrcamento(item);
            	
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
                PedidoServicoMergulhoItemOrcamento item = _pedido.ItensOrcamento.Find(id);
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
        BuscaServicoMaterial ucServicoMaterial = (BuscaServicoMaterial)e.Item.FindControl("ucServicoMaterial");
        Anthem.NumericTextBox txtQuantidade = (Anthem.NumericTextBox)e.Item.FindControl("txtQuantidade");
        Anthem.NumericTextBox txtValor = (Anthem.NumericTextBox)e.Item.FindControl("txtValor");
        DropDownList ddlCelula = (DropDownList)e.Item.FindControl("ddlCelula");

        if (ucServicoMaterial.SelectedValue == "" || ucServicoMaterial.SelectedValue == "0")
        {
            ShowMessage("Campo Serviço/Material obrigatório");
            return;
        }

        int id = Convert.ToInt32(dgMaterial.DataKeys[e.Item.ItemIndex]);
        PedidoServicoMergulhoItemOrcamento item = _pedido.ItensOrcamento.Find(id);
        item.OrigemMaterial = (OrigemMaterial)Convert.ToInt32(ddlOrigemMaterial.SelectedValue);
        item.Quantidade = Convert.ToDecimal(txtQuantidade.Text);
        item.ServicoMaterial = ServicoMaterial.Get(Convert.ToInt32(ucServicoMaterial.SelectedValue));
        item.Valor = txtValor.Text == "" ? 0 : Convert.ToDecimal(txtValor.Text);

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
            DropDownList ddlOrigemMaterial = (DropDownList)e.Item.FindControl("ddlOrigemMaterialNovo");
            Util.FillDropDownList(ddlOrigemMaterial, typeof(OrigemMaterial));
            OrigemMaterialChanged(ddlOrigemMaterial, e.Item);
            
            e.Item.Cells[1].Attributes.Add("colspan","4");
            e.Item.Cells[2].Visible = false;
            e.Item.Cells[3].Visible = false;
            e.Item.Cells[4].Visible = false;

            DropDownList ddlCelula = (DropDownList)e.Item.FindControl("ddlCelulaNovo");
            Util.FillDropDownList(ddlCelula, Celula.List(null, true), ESCOLHA_OPCAO);
            
            
        }
        else if (e.Item.ItemType == ListItemType.EditItem)
        {
            DropDownList ddlOrigemMaterial = (DropDownList)e.Item.FindControl("ddlOrigemMaterial");
            Util.FillDropDownList(ddlOrigemMaterial, typeof(OrigemMaterial));

            DropDownList ddlCelula = (DropDownList)e.Item.FindControl("ddlCelula");
            Util.FillDropDownList(ddlCelula, Celula.List(null, true), ESCOLHA_OPCAO);

            PedidoServicoMergulhoItemOrcamento item = (PedidoServicoMergulhoItemOrcamento)e.Item.DataItem;

            ddlCelula.SelectedValue = item.Celula.ID.ToString();

            PedidoServicoMergulhoItemOrcamento orcamento = (PedidoServicoMergulhoItemOrcamento) e.Item.DataItem;
            ddlOrigemMaterial.SelectedValue = Convert.ToInt32(orcamento.OrigemMaterial).ToString();
            OrigemMaterialChanged(ddlOrigemMaterial, e.Item);

            e.Item.Cells[1].Attributes.Add("colspan", "4");
            e.Item.Cells[2].Visible = false;
            e.Item.Cells[3].Visible = false;
            e.Item.Cells[4].Visible = false;
        }
        else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            PedidoServicoMergulhoItemOrcamento orcamento = (PedidoServicoMergulhoItemOrcamento)e.Item.DataItem;
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
	    _pedido.RemoveItemOrcamento(_pedido.ItensOrcamento.Find(id));
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
                DropDownList ddlCelula = (DropDownList)e.Item.FindControl("ddlCelulaNovo");
                Anthem.NumericTextBox txtValor = (Anthem.NumericTextBox)e.Item.FindControl("txtValor");

                if (ucServicoMaterial.SelectedValue == "" || ucServicoMaterial.SelectedValue == "0")
                {
                    ShowMessage("Campo Serviço obrigatório");
                    return;
                }
                
                PedidoServicoMergulhoItemOrcamento item = new PedidoServicoMergulhoItemOrcamento();
                item.PedidoServicoMergulho = _pedido;
                item.OrigemMaterial = OrigemMaterial.Obtencao;
                item.Quantidade = Convert.ToDecimal(txtQuantidade.Text);
                item.ServicoMaterial = ServicoMaterial.Get(Convert.ToInt32(ucServicoMaterial.SelectedValue));
                //item.Fornecedor = Fornecedor.Get(Convert.ToInt32(ucDadosComplementares.ID_Fornecedor));
                item.Observacao = ucDadosComplementares.Observacao;
                item.Celula = Celula.Get(Convert.ToInt32(ddlCelula.SelectedValue));
                item.Valor = txtValor.Text == "" ? 0 : Convert.ToDecimal(txtValor.Text);
                

                _pedido.AddItemOrcamento(item);

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
                PedidoServicoMergulhoItemOrcamento item = _pedido.ItensOrcamento.Find(id);
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
            PedidoServicoMergulhoItemOrcamento item = _pedido.ItensOrcamento.Find(ucDadosComplementares.ID_Item);
            //item.Fornecedor = Fornecedor.Get(Convert.ToInt32(ucDadosComplementares.ID_Fornecedor));
            item.Observacao = ucDadosComplementares.Observacao;
            item.Save();
        }
    }

    void dgServicos_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        BuscaServicoMaterial ucServicoMaterial = (BuscaServicoMaterial)e.Item.FindControl("ucServicoMaterial");
        Anthem.NumericTextBox txtQuantidade = (Anthem.NumericTextBox)e.Item.FindControl("txtQuantidade");
        DropDownList ddlCelula = (DropDownList)e.Item.FindControl("ddlCelula");
        Anthem.NumericTextBox txtValor = (Anthem.NumericTextBox)e.Item.FindControl("txtValor");

        if (ucServicoMaterial.SelectedValue == "" || ucServicoMaterial.SelectedValue == "0")
        {
            ShowMessage("Campo Serviço obrigatório");
            return;
        }

        int id = Convert.ToInt32(dgServicos.DataKeys[e.Item.ItemIndex]);
        PedidoServicoMergulhoItemOrcamento item = _pedido.ItensOrcamento.Find(id);
        item.Quantidade = Convert.ToDecimal(txtQuantidade.Text);
        item.ServicoMaterial = ServicoMaterial.Get(Convert.ToInt32(ucServicoMaterial.SelectedValue));
        item.Celula = Celula.Get(Convert.ToInt32(ddlCelula.SelectedValue));
        item.Valor = txtValor.Text == "" ? 0 : Convert.ToDecimal(txtValor.Text);
        
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

            DropDownList ddlCelula = (DropDownList)e.Item.FindControl("ddlCelulaNovo");
            Util.FillDropDownList(ddlCelula, Celula.List(null, true), ESCOLHA_OPCAO);
        }
        else if (e.Item.ItemType == ListItemType.EditItem)
        {
            e.Item.Cells[1].Attributes.Add("colspan", "4");
            e.Item.Cells[2].Visible = false;
            e.Item.Cells[3].Visible = false;
            e.Item.Cells[4].Visible = false;

            DropDownList ddlCelula = (DropDownList)e.Item.FindControl("ddlCelula");
            Util.FillDropDownList(ddlCelula, Celula.List(null, true), ESCOLHA_OPCAO);

            PedidoServicoMergulhoItemOrcamento item = (PedidoServicoMergulhoItemOrcamento)e.Item.DataItem;

            ddlCelula.SelectedValue = item.Celula.ID.ToString();
        }
        else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            PedidoServicoMergulhoItemOrcamento orcamento = (PedidoServicoMergulhoItemOrcamento)e.Item.DataItem;
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
        _pedido.RemoveItemOrcamento(_pedido.ItensOrcamento.Find(id));
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
        if(itemGrid.ItemType == ListItemType.EditItem || (itemGrid.ItemType == ListItemType.Footer && itemGrid.Visible))
        {
            Anthem.DropDownList ddlOrigemMaterial =(Anthem.DropDownList)itemGrid.FindControl("ddlOrigemMaterial" + (itemGrid.ItemType == ListItemType.Footer ? "Novo" : ""));
            BuscaServicoMaterial ucServicoMaterial = (BuscaServicoMaterial)itemGrid.FindControl("ucServicoMaterial" + (itemGrid.ItemType == ListItemType.Footer ? "Novo" : ""));
           
            if(ucServicoMaterial.SelectedValue != "0" && ddlOrigemMaterial.SelectedValue != "0")
            {
                Anthem.Label lblQuantidadeEstoque = (Anthem.Label) itemGrid.FindControl("lblQuantidadeEstoque");
                lblQuantidadeEstoque.Text = MovimentoEstoque.GetQuantidadeEstoque(Convert.ToInt32(ucServicoMaterial.SelectedValue),
                                                     Convert.ToInt32(ddlOrigemMaterial.SelectedValue)).QuantidadeDisponivel.ToString();
                lblQuantidadeEstoque.UpdateAfterCallBack = true;

               

                //if (Convert.ToInt32(ddlOrigemMaterial.SelectedValue) != Convert.ToInt32(OrigemMaterial.Singra)
                //        && Convert.ToInt32(ddlOrigemMaterial.SelectedValue) != Convert.ToInt32(OrigemMaterial.Rodizio))
                //{
                //    txtValor.Text = PedidoServicoMergulhoItemOrcamento.GetUltimoOrcamento(
                //        Convert.ToInt32(ucServicoMaterial.SelectedValue)).ToString("N2");
                //    txtValor.UpdateAfterCallBack = true;
                //}
            }
        }
        else if(itemGrid.ItemType == ListItemType.AlternatingItem || itemGrid.ItemType == ListItemType.Item)
        {
            PedidoServicoMergulhoItemOrcamento item = (PedidoServicoMergulhoItemOrcamento) itemGrid.DataItem;
            Anthem.Label lblQuantidadeEstoque = (Anthem.Label)itemGrid.FindControl("lblQuantidadeEstoque");
            lblQuantidadeEstoque.Text = MovimentoEstoque.GetQuantidadeEstoque(item.ServicoMaterial.ID, Convert.ToInt32(item.OrigemMaterial)).QuantidadeDisponivel.ToString();
            lblQuantidadeEstoque.UpdateAfterCallBack = true;
        }
    }

    private void MostrarSaldoLicitacao(DataGridItem itemGrid)
    {
        Label lblSaldoLicitacao = (Label)itemGrid.FindControl("lblSaldoLicitacao");
        PedidoServicoMergulhoItemOrcamento item = (PedidoServicoMergulhoItemOrcamento)itemGrid.DataItem;
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
        
        if(_pedido.Status.StatusPedidoServicoMergulhoEnum == StatusPedidoServicoMergulhoEnum.AguardandoDetalhamentoParaSupervidor)
            _pedido.FinalizarDetalhamento(this.ID_Servidor, ucComentario.Comentario);
        else
            _pedido.FinalizarExecucao(this.ID_Servidor, ucComentario.Comentario);

        Anthem.AnthemClientMethods.Redirect("frmPedidoServicoMergulhoPendente.aspx");
    }

    void ucComentario_OkClicked(object sender, EventArgs e)
    {
        //_delineamentoOrcamento.FinalizarOrcamento(this.ID_Servidor, ucComentario.Comentario);
        //Anthem.AnthemClientMethods.Redirect("frmPedidoServicoMergulhoPendente.aspx");
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



    void btnImprimir_Click(object sender, EventArgs e)
    {
        //Anthem.AnthemClientMethods.Popup(this, "fchOrcamento.aspx?id_orcamento=" + _pedido.ID, false, false, false, true, true, true, true, 20, 50, 700, 500);
    }

    #region Rotina
    //[Anthem.Method]
    //public void AdicionarRotina(int id)
    //{
    //    Rotina rotina = Rotina.Get(Convert.ToInt32(ddlRotina.SelectedValue));
    //    RotinaCategoriaServico rotinaCategoriaServico = rotina.CategoriasServico.Where(c => c.CategoriaServico.ID == _delineamentoOrcamento.CategoriaServico.ID).FirstOrDefault();
    //    if(rotinaCategoriaServico == null)
    //    {
    //        ShowMessage("Esta rotina não contem a categoria " + _delineamentoOrcamento.CategoriaServico.Descricao  + ".");
    //        return;
    //    }

    //    List<PedidoServicoMergulhoDelineamento> delineamentosRemover = _delineamentoOrcamento.ItensDelineamento.Where(d => d.ServidorDelineamento.ID == this.ID_Servidor).ToList();
    //    foreach (PedidoServicoMergulhoDelineamento d in delineamentosRemover)
    //    {
    //        _delineamentoOrcamento.RemoveDelineamento(d);
    //    }

        
    //    foreach (RotinaCategoriaServicoDelineamento delineamento in rotinaCategoriaServico.ItensDelineamento)
    //    {
    //        PedidoServicoMergulhoDelineamento novoDelineamento = new PedidoServicoMergulhoDelineamento();
    //        novoDelineamento.Celula = delineamento.Celula;
    //        novoDelineamento.DescricaoServicoOficina = delineamento.DescricaoServicoOficina;
    //        novoDelineamento.HomemHora = delineamento.HomemHora;
    //        novoDelineamento.ServidorDelineamento = _delineamentoOficina.Servidor;
    //        novoDelineamento.DelineamentoOrcamento = _delineamentoOrcamento;
    //        novoDelineamento.Data = DateTime.Today;
    //        _delineamentoOrcamento.ItensDelineamento.Add(novoDelineamento);
    //        novoDelineamento.Save();
    //    }
    //    BindDelineamento();

    //    List<PedidoServicoMergulhoItemOrcamento> itemRemover = _delineamentoOrcamento.ItensOrcamento.Where(d => d.ServidorDelineamento.ID == this.ID_Servidor).ToList();
    //    foreach (PedidoServicoMergulhoItemOrcamento d in itemRemover)
    //    {
    //        _delineamentoOrcamento.RemoveItemOrcamento(d);
    //    }


    //    foreach (RotinaCategoriaServicoItemOrcamento item in rotinaCategoriaServico.ItensOrcamento)
    //    {
    //        PedidoServicoMergulhoItemOrcamento novoItem = new PedidoServicoMergulhoItemOrcamento();
    //        novoItem.Celula = item.Celula;
    //        novoItem.OrigemMaterial = item.OrigemMaterial;
    //        novoItem.ServicoMaterial = item.ServicoMaterial;
    //        novoItem.Quantidade = item.Quantidade;
    //        novoItem.ServidorDelineamento = _delineamentoOficina.Servidor;
    //        novoItem.DelineamentoOrcamento = _delineamentoOrcamento;
    //        _delineamentoOrcamento.ItensOrcamento.Add(novoItem);
    //        novoItem.Save();
    //    }
    //    BindServico();
    //    BindMaterial();
    //    dgMaterial.ShowFooter = dgServicos.ShowFooter = dgDelineamento.ShowFooter = false;
    //}
    #endregion

    void btnSalvar_Click(object sender, EventArgs e)
    {
        //_pedido.Rotina = Rotina.Get(Convert.ToInt32(ddlRotina.SelectedValue));
        _pedido.Embarcacao = Embarcacao.Get(Convert.ToInt32(ddlEmbarcacao.SelectedValue));
        _pedido.DataPrevisaoInicio = PageReader.ReadNullableDate(txtDataPrevisaoInicio);
        _pedido.DataPrevisaoFim = PageReader.ReadNullableDate(txtDataPrevisaoFim);
        _pedido.Save();
        ShowSuccessMessage();
    }
}
