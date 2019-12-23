using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
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

public partial class frmClienteCadastro : MarinhaPageBase
{

    #region Private Member
    [TransientPageState]
    protected Cliente _cliente;

    #endregion 

    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.btnSalvar.Click += new EventHandler(btnSalvar_Click);
        this.btnNovo.Click += new EventHandler(btnNovo_Click);

        ddlEstado.SelectedIndexChanged += delegate { EstadoChanged(); };
    }

	
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
        	FillPage();
            if (Request["ID_Cliente"] != null)
            {
                _cliente = Cliente.Get(Convert.ToInt32(Request["ID_Cliente"]));
                PopulateFields();
                RegisterDelete();
            }
            else
            {
                _cliente = new Cliente();
            }

            if (Request["popup"] == "true")
            {
                btnVoltar.Text = "Fechar";
                btnVoltar.Attributes.Add("onclick", "window.close();");
            }
            else
             Anthem.AnthemClientMethods.Redirect("frmClientePesquisa.aspx", btnVoltar);
             RegisterDeleteScript();
            
        }
    }

	private void FillPage()
	{
		Util.FillDropDownList(ddlEstado, Estado.List(), ESCOLHA_OPCAO);
		Util.FillDropDownList(ddlTipoCliente, TipoCliente.List(), ESCOLHA_OPCAO);
		Util.InsertDefaultItem(ddlMunicipio, ESCOLHA_OPCAO);
	}

    private void PopulateFields()
    {
        ddlTipoCliente.SelectedValue = ObjectReader.ReadID(_cliente.TipoCliente);
        txtCNPJCPF.Text = _cliente.CNPJCPF;
        txtCodigo.Text = _cliente.Codigo;
        txtIndicativoNaval.Text = _cliente.IndicativoNaval;
        txtDescricao.Text = _cliente.Descricao;
        txtObservacao.Text = _cliente.Observacao;
        txtEndereco.Text = _cliente.Endereco;
        txtBairro.Text = _cliente.Bairro;
        txtCEP.Text = _cliente.CEP;
        ddlEstado.SelectedValue = ObjectReader.ReadID(_cliente.Estado);
        EstadoChanged();
        ddlMunicipio.SelectedValue = ObjectReader.ReadID(_cliente.Municipio);
        txtTelefone.Text = _cliente.Telefone;
        txtFax.Text = _cliente.Fax;
        txtEmail.Text = _cliente.Email;
        txtHomePage.Text = _cliente.HomePage;
        chkFlagAtivo.Checked = _cliente.FlagAtivo;
        if(_cliente.ClientePagador != null)
        {
            ucClientePagador.SelectedValue = _cliente.ClientePagador.ID.ToString();
            ucClientePagador.Text = _cliente.Descricao;
        }
    }

    private void ClearFields()
    {
        ddlTipoCliente.SelectedIndex = -1;
        txtCNPJCPF.Text = "";
        txtCodigo.Text = "";
        txtIndicativoNaval.Text = "";
        txtDescricao.Text = "";
        txtObservacao.Text = "";
        txtEndereco.Text = "";
        txtBairro.Text = "";
        txtCEP.Text = "";
        ddlEstado.SelectedIndex = -1;
        ddlMunicipio.SelectedIndex = -1;
        txtTelefone.Text = "";
        txtFax.Text = "";
        txtEmail.Text = "";
        txtHomePage.Text = "";
        chkFlagAtivo.Checked = true;
        ucClientePagador.Reset();

        ddlTipoCliente.UpdateAfterCallBack = true;
        txtCNPJCPF.UpdateAfterCallBack = true;
        txtCodigo.UpdateAfterCallBack = true;
        txtIndicativoNaval.UpdateAfterCallBack = true;
        txtDescricao.UpdateAfterCallBack = true;
        txtObservacao.UpdateAfterCallBack = true;
        txtEndereco.UpdateAfterCallBack = true;
        txtBairro.UpdateAfterCallBack = true;
        txtCEP.UpdateAfterCallBack = true;
        ddlEstado.UpdateAfterCallBack = true;
        ddlMunicipio.UpdateAfterCallBack = true;
        txtTelefone.UpdateAfterCallBack = true;
        txtFax.UpdateAfterCallBack = true;
        txtEmail.UpdateAfterCallBack = true;
        txtHomePage.UpdateAfterCallBack = true;
        chkFlagAtivo.UpdateAfterCallBack = true;

    }

    private void FillObject()
    {
        _cliente.TipoCliente = TipoCliente.Get(Convert.ToInt32(ddlTipoCliente.SelectedValue));
        _cliente.CNPJCPF = PageReader.ReadString(txtCNPJCPF);
        _cliente.Codigo = PageReader.ReadString(txtCodigo);
        _cliente.IndicativoNaval = PageReader.ReadString(txtIndicativoNaval);
        _cliente.Descricao = PageReader.ReadString(txtDescricao);
        _cliente.Observacao = PageReader.ReadString(txtObservacao);
        _cliente.Endereco = PageReader.ReadString(txtEndereco);
        _cliente.Bairro = PageReader.ReadString(txtBairro);
        _cliente.CEP = PageReader.ReadString(txtCEP);
        _cliente.Estado = Estado.Get(Convert.ToInt32(ddlEstado.SelectedValue));
        _cliente.Municipio = Municipio.Get(Convert.ToInt32(ddlMunicipio.SelectedValue));
        _cliente.Telefone = PageReader.ReadString(txtTelefone);
        _cliente.Fax = PageReader.ReadString(txtFax);
        _cliente.Email = PageReader.ReadString(txtEmail);
        _cliente.HomePage = PageReader.ReadString(txtHomePage);
        _cliente.FlagAtivo = chkFlagAtivo.Checked;
        _cliente.ClientePagador = Cliente.Get(Convert.ToInt32(ucClientePagador.SelectedValue));

    }
    #endregion

    #region Events 
    void btnSalvar_Click(object sender, EventArgs e)
    {
        FillObject();
        _cliente.Save();
        ShowSuccessMessage();
        RegisterDelete();
    }
	
    void btnNovo_Click(object sender, EventArgs e)
    {
        ClearFields();
        _cliente = new Cliente();
    }

    private void EstadoChanged()
    {
        if (ddlEstado.SelectedValue != "0")
        {
            Util.FillDropDownList(ddlMunicipio, Municipio.List(Convert.ToInt32(ddlEstado.SelectedValue)), ESCOLHA_OPCAO);
            ddlMunicipio.UpdateAfterCallBack = true;
        }
    }
    #endregion
    
    [Anthem.Method]
    public void Excluir()
    {
        try
        {
            _cliente.Delete();
            ShowSuccessMessage();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message);
        }
    }

    private void RegisterDelete()
    {
        Anthem.Manager.AddScriptAttribute(btnExcluir, "onclick", string.Format("javascript:Excluir({0});", _cliente.ID));
        btnExcluir.UpdateAfterCallBack = true;
    }
   
}
