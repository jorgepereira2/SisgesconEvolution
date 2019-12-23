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

public partial class frmPedidoServicoMergulhoCadastro : MarinhaPageBase
{

    #region Private Member
    [TransientPageState]
    protected PedidoServicoMergulho _pedido;

    #endregion 

    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.btnSalvar.Click += new EventHandler(btnSalvar_Click);
        this.btnNovo.Click += new EventHandler(btnNovo_Click);
        this.btnEnviar.Click += BtnEnviar_OnClick;
        ucCliente.SelectedValueChanged += new BuscaCliente.SelectedValueChangedHandler(ucCliente_SelectedValueChanged);
      
       

        Anthem.Manager.Register(this);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
        	FillPage();
            if (Request["ID_Pedido"] != null)
            {
                _pedido = PedidoServicoMergulho.Get(Convert.ToInt32(Request["ID_Pedido"]));
                PopulateFields();
            }
            else
            {
                _pedido = new PedidoServicoMergulho();
                
            }

            Anthem.AnthemClientMethods.Redirect("frmPedidoServicoMergulhoPesquisa.aspx", btnVoltar);

            //Anthem.AnthemClientMethods.Popup(btnBuscarPedidoServicoMergulho, "frmPedidoServicoMergulhoBusca.aspx", false, false, false, true, true, true, true, 20, 50, 700, 500, false);


            if (Request["edit"] != null)
                btnEnviar.Visible = false;
        }
    }

	private void FillPage()
	{
        Util.FillDropDownList(ddlDivisao, Celula.List(TipoCelula.Divisao, true), ESCOLHA_OPCAO);
        Util.FillDropDownList(ddlCategoriaServico, CategoriaServico.List(true), ESCOLHA_OPCAO);
        Util.FillDropDownList(ddlPrioridade, Prioridade.List(), ESCOLHA_OPCAO);
        Util.FillDropDownList(ddlEmbarcacao, Embarcacao.List(), ESCOLHA_OPCAO);
	}
	
    private void PopulateFields()
    {
        FillPage();

        UpdateLabels();

        SetFields(_pedido);
    }

    private void SetFields(PedidoServicoMergulho ps)
    {
        txtCodigoPedidoCliente.Text = ps.CodigoPedidoCliente;
        txtObservacao.Text = ps.Observacao;
        txtContatos.Text = ps.Contatos;
        txtTelefoneContatos.Text = ps.TelefoneContatos;
        txtLocalizacao.Text = ps.Localizacao;
        txtMensagemSolicitacao.Text = ps.MensagemSolicitacao;

        ucCliente.SelectedValue = ObjectReader.ReadID(ps.Cliente);
        ucCliente.Text = ps.Cliente.DescricaoCompleta;
        ucClientePagador.SelectedValue = ObjectReader.ReadID(ps.ClientePagador);
        ucClientePagador.Text = ps.ClientePagador.DescricaoCompleta;
        ddlPrioridade.SelectedValue = ObjectReader.ReadID(ps.Prioridade);
        ddlEmbarcacao.SelectedValue = ObjectReader.ReadID(ps.Embarcacao);
        ddlCategoriaServico.SelectedValue = ObjectReader.ReadID(ps.CategoriaServico);
        ddlDivisao.SelectedValue = ObjectReader.ReadID(ps.Celula);
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

    private void ClearFields()
    {
        lblCodigo.Text = "";
        lblStatus.Text = "";
        lblDataEmissao.Text = "";
        ucCliente.Reset();
        ucClientePagador.Reset();
        txtCodigoPedidoCliente.Text = "";
        txtObservacao.Text = "";
        txtContatos.Text = "";
        txtTelefoneContatos.Text = "";
        ddlDivisao.SelectedIndex = -1;
        ddlCategoriaServico.SelectedIndex = -1;
        ddlPrioridade.SelectedIndex = -1;
        ddlEmbarcacao.SelectedIndex = -1;
        txtLocalizacao.Text = "";
        txtMensagemSolicitacao.Text = "";
        
        RefreshFields();
    }

    private void RefreshFields()
    {
        lblCodigo.UpdateAfterCallBack = true;
        lblDataEmissao.UpdateAfterCallBack = true;
        txtCodigoPedidoCliente.UpdateAfterCallBack = true;
        txtObservacao.UpdateAfterCallBack = true;
        txtContatos.UpdateAfterCallBack = true;
        txtTelefoneContatos.UpdateAfterCallBack = true;
        ddlDivisao.UpdateAfterCallBack = true;
        ddlCategoriaServico.UpdateAfterCallBack = true;
        ddlPrioridade.UpdateAfterCallBack = true;
        ddlEmbarcacao.UpdateAfterCallBack = true;
        txtLocalizacao.UpdateAfterCallBack = true;
        txtMensagemSolicitacao.UpdateAfterCallBack = true;
        
        lblStatus.UpdateAfterCallBack = true;
        
        ucClientePagador.UpdateAfterCallBack = true;
        ucCliente.UpdateAfterCallBack = true;
    }

    private void FillObject()
    {
        _pedido.Cliente = Cliente.Get(Convert.ToInt32(ucCliente.SelectedValue));
        _pedido.ClientePagador = Cliente.Get(Convert.ToInt32(ucClientePagador.SelectedValue));
        _pedido.CodigoPedidoCliente = PageReader.ReadString(txtCodigoPedidoCliente);
        _pedido.Observacao = PageReader.ReadString(txtObservacao);
        _pedido.Contatos = PageReader.ReadString(txtContatos);
        _pedido.TelefoneContatos = PageReader.ReadString(txtTelefoneContatos);
        _pedido.Localizacao = txtLocalizacao.Text;
        _pedido.CategoriaServico = CategoriaServico.Get(Convert.ToInt32(ddlCategoriaServico.SelectedValue));
        _pedido.Prioridade = Prioridade.Get(Convert.ToInt32(ddlPrioridade.SelectedValue));
        _pedido.Embarcacao = Embarcacao.Get(Convert.ToInt32(ddlEmbarcacao.SelectedValue));
        _pedido.MensagemSolicitacao = txtMensagemSolicitacao.Text;

        //if (Request["edit"] == null)
        {
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
        _pedido = new PedidoServicoMergulho();
    }

    private void BtnEnviar_OnClick(object sender, EventArgs e)
    {
        FillObject();
        
        _pedido.Enviar(this.ID_Servidor);
        UpdateLabels();
        ShowSuccessMessage();
        
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

   
    [Anthem.Method]
    public void CopiarPedidoServicoMergulho(int id)
    {
        try
        {
            PedidoServicoMergulho ps = PedidoServicoMergulho.Get(id);
            SetFields(ps);
            RefreshFields();
          
        }
        catch (Exception ex)
        {
            ShowMessage(GetCompleteErrorMessage(ex));
        }
    }

 


}
