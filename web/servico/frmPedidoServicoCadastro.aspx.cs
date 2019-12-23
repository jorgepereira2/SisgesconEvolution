using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Shared.NHibernateDAL;
using Marinha.Business;
using Shared.SessionState;
using Shared.Common;

public partial class frmPedidoServicoCadastro : MarinhaPageBase
{
    #region Private Member

    [TransientPageState]
    protected PedidoServico _pedido;

    #endregion 

    #region Initialization

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.btnSalvar.Click += new EventHandler(btnSalvar_Click);
        this.btnNovo.Click += new EventHandler(btnNovo_Click);
        this.btnEnviar.Click += BtnEnviar_OnClick;
        ucCliente.SelectedValueChanged += new BuscaCliente.SelectedValueChangedHandler(ucCliente_SelectedValueChanged);
        btnNovoDelineador.Click += new EventHandler(btnNovoDelineador_Click);
        dgDelineadores.ItemCommand += new DataGridCommandEventHandler(dgDelineadores_ItemCommand);
        dgDelineadores.ItemDataBound += new DataGridItemEventHandler(dgDelineadores_ItemDataBound);
        dgDelineadores.CancelCommand +=new DataGridCommandEventHandler(dgDelineadores_CancelCommand);

        dgEquipamento.UpdateCommand += new DataGridCommandEventHandler(dgEquipamento_UpdateCommand);
        dgEquipamento.CancelCommand += new DataGridCommandEventHandler(dgEquipamento_CancelCommand);
        dgEquipamento.DeleteCommand += new DataGridCommandEventHandler(dgEquipamento_DeleteCommand);
        dgEquipamento.EditCommand += new DataGridCommandEventHandler(dgEquipamento_EditCommand);
        dgEquipamento.ItemCommand += new DataGridCommandEventHandler(dgEquipamento_ItemCommand);
        dgEquipamento.ItemDataBound += new DataGridItemEventHandler(dgEquipamento_ItemDataBound);
        btnNovoEquipamento.Click += new EventHandler(btnNovoEquipamento_Click);

        dgRotina.CancelCommand += dgRotina_CancelCommand;
        dgRotina.DeleteCommand += dgRotina_DeleteCommand;
        dgRotina.ItemCommand += dgRotina_ItemCommand;
        dgRotina.ItemDataBound += dgRotina_ItemDataBound;
        btnNovaRotina.Click += btnNovaRotina_Click;

        Anthem.Manager.Register(this);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
        	FillPage();
            if (Request["ID_Pedido"] != null)
            {
                _pedido = PedidoServico.Get(Convert.ToInt32(Request["ID_Pedido"]));
                PopulateFields();
            }
            else
            {
                _pedido = new PedidoServico();
                txtDataEmissao.Text = DateTime.Today.ToShortDateString();

            }

            if (_pedido.Equipamentos.Count == 0)
            {
                dgEquipamento.ShowFooter = true;
            }
            BindEquipamento();

            BindRotina();

            if(FaseEncaminhar)
                Anthem.AnthemClientMethods.Redirect("frmPedidoServicoPendente.aspx", btnVoltar);
            else
                Anthem.AnthemClientMethods.Redirect("frmPedidoServicoPesquisa.aspx", btnVoltar);

            Anthem.AnthemClientMethods.Popup(btnBuscarPedidoServico, "frmPedidoServicoBusca.aspx", false, false, false, true, true, true, true, 20, 50, 700, 500, false);

            RegisterDeleteScript("ExcluirDelineamento");

            if (Request["edit"] != null)
                btnEnviar.Visible = false;
        }
    }

	private void FillPage()
	{
	    Util.FillDropDownList(ddlServidorGerente, Servidor.List(FuncaoServidor.GerenteDPCP), ESCOLHA_OPCAO);
        Util.FillDropDownList(ddlDivisao, Celula.List(TipoCelula.Divisao, true), ESCOLHA_OPCAO);
        Util.FillDropDownList(ddlCategoriaServico, CategoriaServico.List(false), ESCOLHA_OPCAO);
        //Util.FillDropDownList(ddlPrioridade, Prioridade.List(), ESCOLHA_OPCAO);
	}
	
    private bool FaseEncaminhar
    {
        get
        {
            return _pedido.Status != null && _pedido.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.AguardandoDAC
                   && Request["encaminhar"] == "true";
        }
    }

    private void PopulateFields()
    {
        if (FaseEncaminhar)
        {
            FillPage();
            tbEncaminhar.Visible = true;
            BindDelineadores();

            //pnPS.Enabled = false;
            txtDataEmissao.Enabled = false;

            if(_pedido.Orcamentos[0].Delineamentos.Count == 0)
                btnNovoDelineador_Click(null, null);
        }
        
        UpdateLabels();

        SetFields(_pedido);
    }

    private void SetFields(PedidoServico ps)
    {
        txtCodigoPedidoCliente.Text = ps.CodigoPedidoCliente;
        txtNumeroCFN.Text = ps.NumeroCFN;
        txtObservacao.Text = ps.Observacao;
        txtContatos.Text = ps.Contatos;
        txtTelefoneContatos.Text = ps.TelefoneContatos;
        txtLocalizacao.Text = ps.Localizacao;
        chkProgem.Checked = ps.FlagProgem;
        txtDataEmissao.Text = _pedido.DataEmissao.ToShortDateString();

        ucCliente.SelectedValue = ObjectReader.ReadID(ps.Cliente);
        ucCliente.Text = ps.Cliente.DescricaoCompleta;
        ucClientePagador.SelectedValue = ObjectReader.ReadID(ps.ClientePagador);
        ucClientePagador.Text = ps.ClientePagador.DescricaoCompleta;
        //ddlPrioridade.SelectedValue = ObjectReader.ReadID(ps.Prioridade);
        ddlCategoriaServico.SelectedValue = ObjectReader.ReadID(ps.CategoriaServico);
        ddlServidorGerente.SelectedValue = ObjectReader.ReadID(ps.ServidorGerente);
        ddlDivisao.SelectedValue = ObjectReader.ReadID(ps.Celula);
    }

    private void UpdateLabels()
    {
        lblCodigo.Text = _pedido.CodigoComAno;
        lblStatus.Text = _pedido.Status.Descricao;

        lblCodigo.UpdateAfterCallBack = true;
        lblStatus.UpdateAfterCallBack = true;
    }

    private void ClearFields()
    {
        lblCodigo.Text = "";
        lblStatus.Text = "";
        txtDataEmissao.Text = DateTime.Today.ToShortDateString();
        ucCliente.Reset();
        ucClientePagador.Reset();
        txtCodigoPedidoCliente.Text = "";
        txtNumeroCFN.Text = "";
        txtObservacao.Text = "";
        txtContatos.Text = "";
        txtTelefoneContatos.Text = "";
        ddlServidorGerente.SelectedIndex = -1;
        ddlDivisao.SelectedIndex = -1;
        ddlCategoriaServico.SelectedIndex = -1;
        //ddlPrioridade.SelectedIndex = -1;
        txtLocalizacao.Text = "";
        chkProgem.Checked = false;
        RefreshFields();
    }

    private void RefreshFields()
    {
        lblCodigo.UpdateAfterCallBack = true;
        txtDataEmissao.UpdateAfterCallBack = true;
        txtCodigoPedidoCliente.UpdateAfterCallBack = true;
        txtNumeroCFN.UpdateAfterCallBack = true;
        txtObservacao.UpdateAfterCallBack = true;
        txtContatos.UpdateAfterCallBack = true;
        txtTelefoneContatos.UpdateAfterCallBack = true;
        ddlServidorGerente.UpdateAfterCallBack = true;
        ddlDivisao.UpdateAfterCallBack = true;
        ddlCategoriaServico.UpdateAfterCallBack = true;
        //ddlPrioridade.UpdateAfterCallBack = true;
        txtLocalizacao.UpdateAfterCallBack = true;

        lblStatus.UpdateAfterCallBack = true;
        
        ucClientePagador.UpdateAfterCallBack = true;
        ucCliente.UpdateAfterCallBack = true;
    }

    private void FillObject()
    {
        _pedido.Cliente = Cliente.Get(Convert.ToInt32(ucCliente.SelectedValue));
        _pedido.ClientePagador = Cliente.Get(Convert.ToInt32(ucClientePagador.SelectedValue));
        _pedido.CodigoPedidoCliente = PageReader.ReadString(txtCodigoPedidoCliente);
        _pedido.NumeroCFN = PageReader.ReadString(txtNumeroCFN);
        _pedido.Observacao = PageReader.ReadString(txtObservacao);
        _pedido.Contatos = PageReader.ReadString(txtContatos);
        _pedido.TelefoneContatos = PageReader.ReadString(txtTelefoneContatos);
        _pedido.Localizacao = txtLocalizacao.Text;
        _pedido.CategoriaServico = CategoriaServico.Get(Convert.ToInt32(ddlCategoriaServico.SelectedValue));
        //_pedido.Prioridade = Prioridade.Get(Convert.ToInt32(ddlPrioridade.SelectedValue));
        _pedido.FlagProgem = chkProgem.Checked;
        _pedido.DataEmissao = PageReader.ReadDate(txtDataEmissao);
        
        //if (Request["edit"] == null)
        {
            _pedido.ServidorGerente = Servidor.Get(Convert.ToInt32(ddlServidorGerente.SelectedValue));
            _pedido.Celula = Celula.Get(Convert.ToInt32(ddlDivisao.SelectedValue));
        }
    }
    #endregion

    #region Events 
    void btnSalvar_Click(object sender, EventArgs e)
    {
        if (!ValidatePage())
            return;
        FillObject();
        _pedido.Save();

        foreach (PedidoServicoEquipamento equipamento in _pedido.Equipamentos)
        {
            if(!equipamento.IsPersisted)
                equipamento.Save();
        }

        UpdateLabels();
		ShowSuccessMessage();
    }
    
    private bool ValidatePage()
    {
        if(ucCliente.SelectedValue == "0")
        {
            ShowMessage("Campo Cliente obrigatório.");
            return false;
        }
        if (ucClientePagador.SelectedValue == "0")
        {
            ShowMessage("Campo Pagador obrigatório.");
            return false;
        }
        return true;
    }
	
    void btnNovo_Click(object sender, EventArgs e)
    {
        ClearFields();
        _pedido = new PedidoServico();
    }

    private void BtnEnviar_OnClick(object sender, EventArgs e)
    {
        FillObject();

        if(FaseEncaminhar)
        {
            _pedido.Registrar(this.ID_Servidor);
            Anthem.AnthemClientMethods.Redirect("frmPedidoServicoPendente.aspx");
        }
        else
        {
            _pedido.Enviar(this.ID_Servidor);
            UpdateLabels();
            ShowSuccessMessage();
        }
    }

    void ucCliente_SelectedValueChanged(object source, BuscaClienteEventArgs e)
    {
        if (e.Cliente.ClientePagador != null)
        {
            ucClientePagador.Text = e.Cliente.ClientePagador.Descricao;
            ucClientePagador.SelectedValue = e.Cliente.ClientePagador.ID.ToString();
            ucClientePagador.UpdateAfterCallBack = true;
        }
    }
    #endregion

    #region Delineadores

    private void BindDelineadores()
    {
        dgDelineadores.DataSource = _pedido.Orcamentos[0].Delineamentos;
        dgDelineadores.DataKeyField = "ID";
        dgDelineadores.DataBind();
        dgDelineadores.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();

        btnNovoDelineador.Visible = _pedido.Orcamentos[0].Delineamentos.Count > 0 ? false : true;
        btnNovoDelineador.Enabled = _pedido.Orcamentos[0].Delineamentos.Count > 0 ? false : true;
        btnNovoDelineador.UpdateAfterCallBack = true;
    }

    void dgDelineadores_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if(e.Item.ItemType == ListItemType.Footer)
        {
            DropDownList ddlDelineador = (DropDownList)e.Item.FindControl("ddlDelineador");
            DropDownList ddlOficina = (DropDownList)e.Item.FindControl("ddlOficinaDelineador");

            Util.FillDropDownList(ddlDelineador, Servidor.List(FuncaoServidor.Delineador), ESCOLHA_OPCAO);
            Util.FillDropDownList(ddlOficina, Celula.List(null, true), ESCOLHA_OPCAO);
        }
    }

    void dgDelineadores_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Insert")
        {
            try
            {
                DropDownList ddlDelineador = (DropDownList)e.Item.FindControl("ddlDelineador");
                DropDownList ddlOficina = (DropDownList)e.Item.FindControl("ddlOficinaDelineador");

                DelineamentoOficina delineamento = new DelineamentoOficina();
                delineamento.Servidor = Servidor.Get(Convert.ToInt32(ddlDelineador.SelectedValue));
                delineamento.Oficina = Celula.Get(Convert.ToInt32(ddlOficina.SelectedValue));
                delineamento.DelineamentoOrcamento = _pedido.Orcamentos[0];
                delineamento.Save();

                if (_pedido.Orcamentos[0].Delineamentos.Count >= 1)
                {
                    ShowMessage("Permitido apenas 1 Delineador!");
                    return;
                }
                else
                {
                    _pedido.Orcamentos[0].Delineamentos.Add(delineamento);

                    BindDelineadores();
                    dgDelineadores.ShowFooter = false;
                }
            }
            catch (Exception ex)
            {
                Anthem.AnthemClientMethods.Alert(ex.Message, this);
            }
        }
    }

    void btnNovoDelineador_Click(object sender, EventArgs e)
    {
        dgDelineadores.ShowFooter = true;
        BindDelineadores();
        dgDelineadores.UpdateAfterCallBack = true;
    }

    [Anthem.Method]
    public void ExcluirDelineamento(int id)
    {
        //DelineamentoOficina delineamento = DelineamentoOficina.Get(id);
        //delineamento.Delete();
        DelineamentoOficina delineamento = _pedido.Orcamentos[0].Delineamentos.Where(d => d.ID == id).First();
        delineamento.Delete();
        _pedido.Orcamentos[0].Delineamentos.Remove(delineamento);
        //_pedido.Orcamentos[0].Save();
        BindDelineadores();
    }

    void dgDelineadores_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dgDelineadores.EditItemIndex = -1;
        dgDelineadores.ShowFooter = false;
        BindDelineadores();
    }

    private void FillDelineador(DropDownList ddlDelineador)
    {
        Util.FillDropDownList(ddlDelineador, Servidor.List(FuncaoServidor.Delineador), ESCOLHA_OPCAO);
    }

    #endregion

    #region Copiar PS

    [Anthem.Method]
    public void CopiarPedidoServico(int id)
    {
        try
        {
            PedidoServico ps = PedidoServico.Get(id);

            SetFields(ps);
            txtDataEmissao.Text = DateTime.Today.ToShortDateString();

            _pedido.Equipamentos.Clear();
            foreach (PedidoServicoEquipamento equipamento in ps.Equipamentos)
            {
                PedidoServicoEquipamento newEquipamento = equipamento.GetNewClone();
                newEquipamento.PedidoServico = _pedido;
                _pedido.Equipamentos.Add(newEquipamento);
            }
           
            BindEquipamento();

            dgEquipamento.UpdateAfterCallBack = true;

            RefreshFields();
          
        }
        catch (Exception ex)
        {
            ShowMessage(GetCompleteErrorMessage(ex));
        }
    }

    #endregion

    #region Equipamento

    private void BindEquipamento()
    {
        dgEquipamento.DataSource = _pedido.Equipamentos;
        dgEquipamento.DataKeyField = "ID";
        dgEquipamento.DataBind();

        dgEquipamento.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();
    }

    void dgEquipamento_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        PedidoServicoEquipamento psEquipamento = _pedido.Equipamentos.Find(Convert.ToInt32(dgEquipamento.DataKeys[e.Item.ItemIndex]));
        psEquipamento.Delete();
        _pedido.Equipamentos.Remove(psEquipamento);
        BindEquipamento();
    }

    void dgEquipamento_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        Anthem.TextBox txtDefeitoReclamado = (Anthem.TextBox)e.Item.FindControl("txtDefeitoReclamado");
        Anthem.TextBox txtNumeroSerie = (Anthem.TextBox)e.Item.FindControl("txtNumeroSerie");
        Anthem.TextBox txtQuantidade = (Anthem.TextBox)e.Item.FindControl("txtQuantidade");
        BuscaEquipamento ucBuscaEquipamento = (BuscaEquipamento)e.Item.FindControl("ucEquipamento");

        PedidoServicoEquipamento psEquipamento = _pedido.Equipamentos.Find(Convert.ToInt32(dgEquipamento.DataKeys[e.Item.ItemIndex]));
        psEquipamento.Equipamento = Equipamento.Get(Convert.ToInt32(ucBuscaEquipamento.SelectedValue));
        psEquipamento.Quantidade = PageReader.ReadInt(txtQuantidade);
        psEquipamento.DefeitoReclamado = PageReader.ReadString(txtDefeitoReclamado);
        psEquipamento.NumeroSerie = PageReader.ReadString(txtNumeroSerie);

        psEquipamento.Save();
        //_pedido.Equipamentos.Add(psEquipamento);

        dgEquipamento.ShowFooter = false;
        dgEquipamento.EditItemIndex = -1;
        BindEquipamento();        
    }

    void dgEquipamento_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if(e.CommandName == "Insert")
        {
            Anthem.TextBox txtDefeitoReclamado = (Anthem.TextBox)e.Item.FindControl("txtDefeitoReclamado");
            Anthem.TextBox txtNumeroSerie = (Anthem.TextBox)e.Item.FindControl("txtNumeroSerie");
            Anthem.TextBox txtQuantidade = (Anthem.TextBox)e.Item.FindControl("txtQuantidade");
            BuscaEquipamento ucBuscaEquipamento = (BuscaEquipamento)e.Item.FindControl("ucEquipamento");

            PedidoServicoEquipamento psEquipamento = new PedidoServicoEquipamento();
            psEquipamento.Equipamento = Equipamento.Get(Convert.ToInt32(ucBuscaEquipamento.SelectedValue));
            psEquipamento.Quantidade = PageReader.ReadInt(txtQuantidade);
            psEquipamento.DefeitoReclamado = PageReader.ReadString(txtDefeitoReclamado);
            psEquipamento.NumeroSerie = PageReader.ReadString(txtNumeroSerie);
            psEquipamento.PedidoServico = _pedido;

            if (!_pedido.IsPersisted)
                btnSalvar_Click(null, null);
            psEquipamento.Save();
            _pedido.Equipamentos.Add(psEquipamento);

            BindEquipamento();
            dgEquipamento.ShowFooter = false;
        }
    }

    void dgEquipamento_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if(e.Item.ItemType == ListItemType.EditItem)
        {
            PedidoServicoEquipamento psEquipamento = (PedidoServicoEquipamento) e.Item.DataItem;
            BuscaEquipamento ucBuscaEquipamento = (BuscaEquipamento)e.Item.FindControl("ucEquipamento");
            ucBuscaEquipamento.SelectedValue = psEquipamento.Equipamento.ID.ToString();
            ucBuscaEquipamento.Text = psEquipamento.Equipamento.Descricao;
        }
    }

    void dgEquipamento_EditCommand(object source, DataGridCommandEventArgs e)
    {
        dgEquipamento.EditItemIndex = e.Item.ItemIndex;
        dgEquipamento.ShowFooter = false;
        BindEquipamento();
    }

    void dgEquipamento_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dgEquipamento.EditItemIndex = -1;
        dgEquipamento.ShowFooter = false;
        BindEquipamento();
    }

    void btnNovoEquipamento_Click(object sender, EventArgs e)
    {
        dgEquipamento.ShowFooter = true;
        BindEquipamento();
        dgEquipamento.UpdateAfterCallBack = true;
    }

    #endregion

    #region Rotinas

    private void BindRotina()
    {
        dgRotina.DataSource = _pedido.Rotinas;
        dgRotina.DataKeyField = "ID";
        dgRotina.DataBind();

        dgRotina.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();
    }

    void dgRotina_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        var rotina = _pedido.Rotinas.Find(Convert.ToInt32(dgRotina.DataKeys[e.Item.ItemIndex]));
        _pedido.Rotinas.Remove(rotina);
        BindRotina();
    }

    void dgRotina_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Insert")
        {
            var ddlRotina = (Anthem.DropDownList)e.Item.FindControl("ddlRotina");
            
            
            var rotina = Rotina.Get(Convert.ToInt32(ddlRotina.SelectedValue));
            _pedido.Rotinas.Add(rotina);
            
            btnSalvar_Click(null, null);
            
            BindRotina();
            dgRotina.ShowFooter = false;
        }
    }

    void dgRotina_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Footer)
        {
            var ddlRotina = (Anthem.DropDownList)e.Item.FindControl("ddlRotina");
            Util.FillDropDownList(ddlRotina, Rotina.List(), ESCOLHA_OPCAO);
        }
    }
    
    void dgRotina_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dgRotina.EditItemIndex = -1;
        dgRotina.ShowFooter = false;
        BindRotina();
    }

    void btnNovaRotina_Click(object sender, EventArgs e)
    {
        dgRotina.ShowFooter = true;
        BindRotina();
        dgRotina.UpdateAfterCallBack = true;
    }
    #endregion
}