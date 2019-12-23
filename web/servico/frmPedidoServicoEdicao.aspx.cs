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

public partial class frmPedidoServicoEdicao : MarinhaPageBase
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
        ucCliente.SelectedValueChanged += new BuscaCliente.SelectedValueChangedHandler(ucCliente_SelectedValueChanged);
        dgOrcamento.ItemDataBound += new DataGridItemEventHandler(dgOrcamento_ItemDataBound);

        btnNovoDelineador.Click += new EventHandler(btnNovoDelineador_Click);
        dgDelineadores.ItemCommand += new DataGridCommandEventHandler(dgDelineadores_ItemCommand);
        dgDelineadores.ItemDataBound += new DataGridItemEventHandler(dgDelineadores_ItemDataBound);
        dgDelineadores.CancelCommand += new DataGridCommandEventHandler(dgDelineadores_CancelCommand);

        dgEquipamento.UpdateCommand += new DataGridCommandEventHandler(dgEquipamento_UpdateCommand);
        dgEquipamento.CancelCommand += new DataGridCommandEventHandler(dgEquipamento_CancelCommand);
        dgEquipamento.DeleteCommand += new DataGridCommandEventHandler(dgEquipamento_DeleteCommand);
        dgEquipamento.EditCommand += new DataGridCommandEventHandler(dgEquipamento_EditCommand);
        dgEquipamento.ItemCommand += new DataGridCommandEventHandler(dgEquipamento_ItemCommand);
        dgEquipamento.ItemDataBound += new DataGridItemEventHandler(dgEquipamento_ItemDataBound);
        btnNovoEquipamento.Click += new EventHandler(btnNovoEquipamento_Click);
    }

   

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            Anthem.Manager.Register(this);
        	FillPage();
            if (Request["ID_Pedido"] != null)
            {
                _pedido = PedidoServico.Get(Convert.ToInt32(Request["ID_Pedido"]));
                PopulateFields();
            }
            
            Anthem.AnthemClientMethods.Redirect("frmPedidoServicoEdicaoPesquisa.aspx", btnVoltar);

            RegisterDeleteScript("ExcluirDelineamento");
        }
    }

	private void FillPage()
	{
        Util.FillDropDownList(ddlCategoria, CategoriaServico.List(false));
        Util.FillDropDownList(ddlDivisao, Celula.List(TipoCelula.Divisao, true), ESCOLHA_OPCAO);
        Util.FillDropDownList(ddlGerente, Servidor.List(FuncaoServidor.GerenteDPCP), ESCOLHA_OPCAO);
        //Util.FillDropDownList(ddlPrioridade, Prioridade.List(), ESCOLHA_OPCAO);
	}
	
    private void PopulateFields()
    {
        
        UpdateLabels();

        SetFields(_pedido);
    }

    private void SetFields(PedidoServico ps)
    {
        txtCodigoPedidoCliente.Text = ps.CodigoPedidoCliente;
        txtObservacao.Text = ps.Observacao;
        //txtNumeroRegistro.Text = ps.NumeroRegistro;
       // txtDefeitoReclamado.Text = ps.DefeitoReclamado;
        txtContatos.Text = ps.Contatos;
        txtTelefoneContatos.Text = ps.TelefoneContatos;
       // txtQuantidade.Text = ObjectReader.ReadInt(ps.Quantidade);
        chkFlagProgem.Checked = ps.FlagProgem;
        ddlCategoria.SelectedValue = ObjectReader.ReadID(ps.CategoriaServico);
        txtDataPlanejamento.Text = ObjectReader.ReadDate(ps.DataPlanejamentoPS);
        txtDataPronto.Text = ObjectReader.ReadDate(ps.DataPronto);
        txtDiversos.Text = ps.Diversos;
        ddlDivisao.SelectedValue = ObjectReader.ReadID(ps.Celula);
        ddlGerente.SelectedValue = ObjectReader.ReadID(ps.ServidorGerente);
        //ddlPrioridade.SelectedValue = ObjectReader.ReadID(ps.Prioridade);
        txtLocalizacao.Text = _pedido.Localizacao;


        ucCliente.SelectedValue = ObjectReader.ReadID(ps.Cliente);
        ucCliente.Text = ps.Cliente.DescricaoCompleta;
        ucClientePagador.SelectedValue = ObjectReader.ReadID(ps.ClientePagador);
        ucClientePagador.Text = ps.ClientePagador.DescricaoCompleta;
        //ucEquipamento.SelectedValue = ObjectReader.ReadID(ps.Equipamento);
        //ucEquipamento.Text = ps.Equipamento.DescricaoCompleta;

        dgOrcamento.DataSource = ps.Orcamentos;
        dgOrcamento.DataBind();

        BindDelineadores();
        BindEquipamento();
    }

    private void UpdateLabels()
    {
        lblCodigo.Text = _pedido.CodigoComAno;
        lblDataEmissao.Text = ObjectReader.ReadDate(_pedido.DataEmissao);
        lblStatus.Text = _pedido.Status.Descricao;

        lblCodigo.UpdateAfterCallBack = true;
        lblDataEmissao.UpdateAfterCallBack = true;
        lblStatus.UpdateAfterCallBack = true;
    }

   

    private void FillObject()
    {
        _pedido.Cliente = Cliente.Get(Convert.ToInt32(ucCliente.SelectedValue));
        _pedido.ClientePagador = Cliente.Get(Convert.ToInt32(ucClientePagador.SelectedValue));
        _pedido.CodigoPedidoCliente = PageReader.ReadString(txtCodigoPedidoCliente);
        //_pedido.Equipamento = Equipamento.Get(Convert.ToInt32(ucEquipamento.SelectedValue));
        _pedido.Observacao = PageReader.ReadString(txtObservacao);
       // _pedido.NumeroRegistro = PageReader.ReadString(txtNumeroRegistro);
       // _pedido.DefeitoReclamado = PageReader.ReadString(txtDefeitoReclamado);
        _pedido.Contatos = PageReader.ReadString(txtContatos);
        _pedido.TelefoneContatos = PageReader.ReadString(txtTelefoneContatos);
       // _pedido.Quantidade = PageReader.ReadInt(txtQuantidade);
        _pedido.FlagProgem = chkFlagProgem.Checked;
        _pedido.CategoriaServico = CategoriaServico.Get(Convert.ToInt32(ddlCategoria.SelectedValue));
        _pedido.Diversos = PageReader.ReadString(txtDiversos);
        _pedido.DataPronto = PageReader.ReadNullableDate(txtDataPronto);
        _pedido.DataPlanejamentoPS = PageReader.ReadNullableDate(txtDataPlanejamento);
        _pedido.Celula = Celula.Get(Convert.ToInt32(ddlDivisao.SelectedValue));
        _pedido.ServidorGerente = Servidor.Get(Convert.ToInt32(ddlGerente.SelectedValue));
        _pedido.Localizacao = txtLocalizacao.Text;
        //_pedido.Prioridade = Prioridade.Get(Convert.ToInt32(ddlPrioridade.SelectedValue));

        foreach (DataGridItem item in dgOrcamento.Items)
        {
            DropDownList ddlCategoriaOrcamento = (DropDownList)item.FindControl("ddlCategoriaOrcamento");
            DropDownList ddlCotacao = (DropDownList)item.FindControl("ddlCotador");
            _pedido.Orcamentos[item.ItemIndex].CategoriaServico = CategoriaServico.Get(Convert.ToInt32(ddlCategoriaOrcamento.SelectedValue));
            _pedido.Orcamentos[item.ItemIndex].ServidorCotador = Servidor.Get(Convert.ToInt32(ddlCotacao.SelectedValue));
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
        foreach (DelineamentoOrcamento orcamento in _pedido.Orcamentos)
            orcamento.Save();
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

    void dgOrcamento_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            DropDownList ddlCategoriaOrcamento = (DropDownList)e.Item.FindControl("ddlCategoriaOrcamento");
            DropDownList ddlCotador = (DropDownList)e.Item.FindControl("ddlCotador");
            DelineamentoOrcamento orcamento = (DelineamentoOrcamento)e.Item.DataItem;
            Util.FillDropDownList(ddlCategoriaOrcamento, CategoriaServico.List(false));
            Util.FillDropDownList(ddlCotador, Servidor.List(FuncaoServidor.Comprador));
            ddlCategoriaOrcamento.SelectedValue = ObjectReader.ReadID(orcamento.CategoriaServico);
            ddlCotador.SelectedValue = ObjectReader.ReadID(orcamento.ServidorCotador);
        }
    }

    #region Delineadores

    private void BindDelineadores()
    {
        dgDelineadores.DataSource = _pedido.Orcamentos[0].Delineamentos;
        dgDelineadores.DataKeyField = "ID";
        dgDelineadores.DataBind();
        dgDelineadores.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();
    }

    void dgDelineadores_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Footer)
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

                _pedido.Orcamentos[0].Delineamentos.Add(delineamento);

                BindDelineadores();
                dgDelineadores.ShowFooter = false;
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
        Anthem.TextBox txtQuantidade = (Anthem.TextBox)e.Item.FindControl("txtQuantidade");
        BuscaEquipamento ucBuscaEquipamento = (BuscaEquipamento)e.Item.FindControl("ucEquipamento");

        PedidoServicoEquipamento psEquipamento = _pedido.Equipamentos.Find(Convert.ToInt32(dgEquipamento.DataKeys[e.Item.ItemIndex]));
        psEquipamento.Equipamento = Equipamento.Get(Convert.ToInt32(ucBuscaEquipamento.SelectedValue));
        psEquipamento.Quantidade = PageReader.ReadInt(txtQuantidade);
        psEquipamento.DefeitoReclamado = PageReader.ReadString(txtDefeitoReclamado);

        psEquipamento.Save();
        //_pedido.Equipamentos.Add(psEquipamento);

        dgEquipamento.ShowFooter = false;
        dgEquipamento.EditItemIndex = -1;
        BindEquipamento();

    }

    void dgEquipamento_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Insert")
        {
            Anthem.TextBox txtDefeitoReclamado = (Anthem.TextBox)e.Item.FindControl("txtDefeitoReclamado");
            Anthem.TextBox txtQuantidade = (Anthem.TextBox)e.Item.FindControl("txtQuantidade");
            BuscaEquipamento ucBuscaEquipamento = (BuscaEquipamento)e.Item.FindControl("ucEquipamento");

            PedidoServicoEquipamento psEquipamento = new PedidoServicoEquipamento();
            psEquipamento.Equipamento = Equipamento.Get(Convert.ToInt32(ucBuscaEquipamento.SelectedValue));
            psEquipamento.Quantidade = PageReader.ReadInt(txtQuantidade);
            psEquipamento.DefeitoReclamado = PageReader.ReadString(txtDefeitoReclamado);
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
        if (e.Item.ItemType == ListItemType.EditItem)
        {
            PedidoServicoEquipamento psEquipamento = (PedidoServicoEquipamento)e.Item.DataItem;
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
   
}
