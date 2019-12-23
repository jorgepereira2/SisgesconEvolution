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

public partial class frmFornecedorCadastro : MarinhaPageBase
{
    #region Private Member
    [TransientPageState]
    protected Fornecedor _fornecedor;

    [TransientPageState]
    protected FornecedorContato _contato;

    #endregion 

    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.btnSalvar.Click += new EventHandler(btnSalvar_Click);
        this.btnNovo.Click += new EventHandler(btnNovo_Click);
        this.dgContato.DeleteCommand += new DataGridCommandEventHandler(dgContato_DeleteCommand);
        this.dgContato.EditCommand += new DataGridCommandEventHandler(dgContato_EditCommand);

        ddlEstado.SelectedIndexChanged += delegate { EstadoChanged(); };
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            FillPage();
            if (Request["ID_Fornecedor"] != null)
            {
                _fornecedor = Fornecedor.Get(Convert.ToInt32(Request["ID_Fornecedor"]));
                PopulateFields();
                RegisterDelete();
            }
            else
            {
                _fornecedor = new Fornecedor();
            }
            if(Request["popup"]=="true")
            {
                btnVoltar.Text = "Fechar";
                btnVoltar.Attributes.Add("onclick", "window.close();");
            }
            else
                Anthem.AnthemClientMethods.Redirect("frmFornecedorPesquisa.aspx", btnVoltar);
            chkFlagAtivo.Attributes.Add("onclick", "ShowMotivoInativo();");
            RegisterDeleteScript();
        }
    }

    private void FillPage()
    {
        Util.FillDropDownList(ddlEstado, Estado.List(), ESCOLHA_OPCAO);
        Util.FillDropDownList(ddlTipoFornecedor, TipoFornecedor.List(), ESCOLHA_OPCAO);
        Util.FillDropDownList(ddlTipoPessoa, typeof(TipoPessoa), ESCOLHA_OPCAO);
        Util.InsertDefaultItem(ddlMunicipio, ESCOLHA_OPCAO);
    }

    #endregion

    #region Events
    void btnSalvar_Click(object sender, EventArgs e)
    {
        if (TabStrip1.SelectedTab.ID == tabDadosBasicos.ID)
            SalvarFornecedor();
        else if (TabStrip1.SelectedTab.ID == tabContato.ID)
            SalvarContatoFornecedor();
    }

    void btnNovo_Click(object sender, EventArgs e)
    {
        if (TabStrip1.SelectedTab.ID == tabDadosBasicos.ID)
        {
            ClearFields();
            _fornecedor = new Fornecedor();
        }
        else if (TabStrip1.SelectedTab.ID == tabContato.ID)
            ClearFieldsContatoFornecedor();
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

    #region Fornecedor
    private void SalvarFornecedor()
    {
        if(!Util.ValidaCPF(txtCNPJ.Text))
        {
            ShowMessage("CNPJ/CPF inválido.");
            return;
        }
        FillObject();
        bool isNew = !_fornecedor.IsPersisted;
        _fornecedor.Save();

        if(isNew && Request["popup"] == "true")
            Anthem.Manager.AddScriptForClientSideEval(string.Format("FornecedorAdicionado('{0}','{1}');", _fornecedor.ID, _fornecedor.RazaoSocial));
        ShowSuccessMessage();
        RegisterDelete();
    }

    private void PopulateFields()
    {
        ddlTipoFornecedor.SelectedValue = ObjectReader.ReadID(_fornecedor.TipoFornecedor);
        ddlEstado.SelectedValue = ObjectReader.ReadID(_fornecedor.Estado);
        EstadoChanged();
        ddlMunicipio.SelectedValue = ObjectReader.ReadID(_fornecedor.Municipio);
        txtAgencia.Text = _fornecedor.Agencia;
        txtBairro.Text = _fornecedor.Bairro;
        txtCEP.Text = _fornecedor.CEP;
        txtCNPJ.Text = _fornecedor.CNPJ;
        txtContaCorrente.Text = _fornecedor.ContaCorrente;
        txtEmail.Text = _fornecedor.Email;
        txtEndereco.Text = _fornecedor.Endereco;
        txtFax.Text = _fornecedor.Fax;
        txtHomePage.Text = _fornecedor.HomePage;
        txtMaterialFornecido.Text = _fornecedor.DescricaoMaterialFornecido;
        txtMotivoInativo.Text = _fornecedor.MotivoInativo;
        txtNumeroBanco.Text = _fornecedor.NumeroBanco;
        txtObservacao.Text = _fornecedor.Observacao;
        txtRazaoSocial.Text = _fornecedor.RazaoSocial;
        txtTelefone.Text = _fornecedor.Telefone;
        txtNumeroContrato.Text = _fornecedor.NumeroContrato;
        txtValidadeCertidaoFGTS.Text = ObjectReader.ReadDate(_fornecedor.ValidadeCertidaoFGTS);
        txtValidadeCertidaoReceitaFederal.Text = ObjectReader.ReadDate(_fornecedor.ValidadeCertidaoReceitaFederal);
        txtValidadeCertidaoDividaUniao.Text = ObjectReader.ReadDate(_fornecedor.ValidadeCertidaoDividaUniao);
        chkFlagAtivo.Checked = _fornecedor.FlagAtivo;
        chkFlagOM.Checked = _fornecedor.FlagOM;
        chkFlagOptanteSimples.Checked = _fornecedor.FlagOptanteSimples;
        ddlTipoPessoa.SelectedValue = Convert.ToInt32(_fornecedor.TipoPessoa).ToString();
        BindContatoFornecedor();
    }

    private void ClearFields()
    {
        ddlTipoFornecedor.SelectedIndex = -1;
        ddlEstado.SelectedIndex = -1;
        ddlMunicipio.SelectedIndex = -1;
        txtAgencia.Text = "";
        txtBairro.Text = "";
        txtCEP.Text = "";
        txtCNPJ.Text = "";
        txtContaCorrente.Text = "";
        txtEmail.Text = "";
        txtEndereco.Text = "";
        txtFax.Text = "";
        txtHomePage.Text = "";
        txtMaterialFornecido.Text = "";
        txtMotivoInativo.Text = "";
        txtNumeroBanco.Text = "";
        txtObservacao.Text = "";
        txtRazaoSocial.Text = "";
        txtTelefone.Text = "";
        txtNumeroContrato.Text = "";
        txtValidadeCertidaoFGTS.Text = "";
        txtValidadeCertidaoReceitaFederal.Text = "";
        txtValidadeCertidaoDividaUniao.Text = "";
        chkFlagOM.Checked = false;
        chkFlagOptanteSimples.Checked = false;
        chkFlagAtivo.Checked = true;

        ddlTipoFornecedor.UpdateAfterCallBack = true;
        ddlEstado.UpdateAfterCallBack = true;
        ddlMunicipio.UpdateAfterCallBack = true;
        txtNumeroContrato.UpdateAfterCallBack = true;
        txtAgencia.UpdateAfterCallBack = true;
        txtBairro.UpdateAfterCallBack = true;
        txtCEP.UpdateAfterCallBack = true;
        txtCNPJ.UpdateAfterCallBack = true;
        txtContaCorrente.UpdateAfterCallBack = true;
        txtEmail.UpdateAfterCallBack = true;
        txtEndereco.UpdateAfterCallBack = true;
        txtFax.UpdateAfterCallBack = true;
        txtHomePage.UpdateAfterCallBack = true;
        txtMaterialFornecido.UpdateAfterCallBack = true;
        txtMotivoInativo.UpdateAfterCallBack = true;
        txtNumeroBanco.UpdateAfterCallBack = true;
        txtObservacao.UpdateAfterCallBack = true;
        txtRazaoSocial.UpdateAfterCallBack = true;
        txtTelefone.UpdateAfterCallBack = true;
        txtValidadeCertidaoFGTS.UpdateAfterCallBack = true;
        txtValidadeCertidaoReceitaFederal.UpdateAfterCallBack = true;
        txtValidadeCertidaoDividaUniao.UpdateAfterCallBack = true;
        chkFlagAtivo.UpdateAfterCallBack = true;
        chkFlagOM.UpdateAfterCallBack = true;
        chkFlagOptanteSimples.UpdateAfterCallBack = true;
    }

    private void FillObject()
    {
        _fornecedor.Agencia = PageReader.ReadString(txtAgencia);
        _fornecedor.Bairro = PageReader.ReadString(txtBairro);
        _fornecedor.CEP = PageReader.ReadString(txtCEP);
        _fornecedor.CNPJ = PageReader.ReadString(txtCNPJ).Replace("-", "").Replace("/","").Replace(".","");
        _fornecedor.ContaCorrente = PageReader.ReadString(txtContaCorrente);
        _fornecedor.Email = PageReader.ReadString(txtEmail);
        _fornecedor.Endereco = PageReader.ReadString(txtEndereco);
        _fornecedor.Fax = PageReader.ReadString(txtFax);
        _fornecedor.HomePage = PageReader.ReadString(txtHomePage);
        _fornecedor.DescricaoMaterialFornecido   = PageReader.ReadString(txtMaterialFornecido);
        _fornecedor.MotivoInativo = PageReader.ReadString(txtMotivoInativo);
        _fornecedor.NumeroBanco = PageReader.ReadString(txtNumeroBanco);
        _fornecedor.NumeroContrato = PageReader.ReadString(txtNumeroContrato);
        _fornecedor.Observacao = PageReader.ReadString(txtObservacao);
        _fornecedor.RazaoSocial = PageReader.ReadString(txtRazaoSocial);
        _fornecedor.Telefone = PageReader.ReadString(txtTelefone);
        _fornecedor.ValidadeCertidaoDividaUniao = PageReader.ReadNullableDate(txtValidadeCertidaoDividaUniao);
        _fornecedor.ValidadeCertidaoFGTS = PageReader.ReadNullableDate(txtValidadeCertidaoFGTS);
        _fornecedor.ValidadeCertidaoReceitaFederal = PageReader.ReadNullableDate(txtValidadeCertidaoReceitaFederal);
        _fornecedor.FlagAtivo = chkFlagAtivo.Checked;
        _fornecedor.Estado = Estado.Get(Convert.ToInt32(ddlEstado.SelectedValue));
        _fornecedor.Municipio = Municipio.Get(Convert.ToInt32(ddlMunicipio.SelectedValue));
        _fornecedor.TipoFornecedor = TipoFornecedor.Get(Convert.ToInt32(ddlTipoFornecedor.SelectedValue));
        _fornecedor.FlagOM = chkFlagOM.Checked;
        _fornecedor.FlagOptanteSimples = chkFlagOptanteSimples.Checked;
        _fornecedor.TipoPessoa = (TipoPessoa)Convert.ToInt32(ddlTipoPessoa.SelectedValue);
    }

    [Anthem.Method]
    public void Excluir()
    {
        try
        {
            if (TabStrip1.SelectedTab.ID == tabDadosBasicos.ID)
            {
                _fornecedor.Delete();
                ShowSuccessMessage();
            }
        }
        catch(Exception ex)
        {
            ShowMessage(ex.Message);
        }
    }

    private void RegisterDelete()
    {
        Anthem.Manager.AddScriptAttribute(btnExcluir, "onclick", string.Format("javascript:Excluir({0});", _fornecedor.ID));
        btnExcluir.UpdateAfterCallBack = true;
    }
    #endregion

    #region ContatoFornecedor
    private void BindContatoFornecedor()
    {
        dgContato.DataSource = _fornecedor.Contatos;
        dgContato.DataKeyField = "ID";
        dgContato.DataBind();
        dgContato.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();
    }

    void dgContato_EditCommand(object source, DataGridCommandEventArgs e)
    {
        int id = Convert.ToInt32(dgContato.DataKeys[e.Item.ItemIndex]);
        _contato = _fornecedor.Contatos.Find(id);
        PopulateFieldsContatoFornecedor();
    }

    void dgContato_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        int id = Convert.ToInt32(dgContato.DataKeys[e.Item.ItemIndex]);
        FornecedorContato contato = _fornecedor.Contatos.Find(id);
        _fornecedor.Contatos.Remove(contato);
        contato.Delete();
        BindContatoFornecedor();
    }

    private void PopulateFieldsContatoFornecedor()
    {
        txtNomeContato.Text = _contato.Nome;
        txtTelefoneContato.Text = _contato.Telefone;
        txtCelularContato.Text = _contato.Celular;
        txtSetorContato.Text = _contato.Celular;
        txtEmailContato.Text = _contato.Email;
        
        RefreshFieldsContatoFornecedor();
    }

    private void FillObjectContatoFornecedor()
    {
        if (_contato == null)
        {
            _contato = new FornecedorContato(this._fornecedor);
        }
        _contato.Nome = txtNomeContato.Text;
        _contato.Celular = PageReader.ReadString(txtCelularContato);
        _contato.Telefone = PageReader.ReadString(txtTelefoneContato);
        _contato.Setor = PageReader.ReadString(txtSetorContato);
        _contato.Email = PageReader.ReadString(txtEmailContato);
    }

    private void ClearFieldsContatoFornecedor()
    {
        txtNomeContato.Text = "";
        txtTelefoneContato.Text = "";
        txtCelularContato.Text = "";
        txtSetorContato.Text = "";
        txtEmailContato.Text = "";
        
        RefreshFieldsContatoFornecedor();
        _contato = null;
    }

    private void RefreshFieldsContatoFornecedor()
    {
        txtNomeContato.UpdateAfterCallBack = true;
        txtTelefoneContato.UpdateAfterCallBack = true;
        txtCelularContato.UpdateAfterCallBack = true;
        txtSetorContato.UpdateAfterCallBack = true;
        txtEmailContato.UpdateAfterCallBack = true;
    }

    private void SalvarContatoFornecedor()
    {
        FillObjectContatoFornecedor();
        _contato.Save();

        BindContatoFornecedor();
        ClearFieldsContatoFornecedor();
    }
    #endregion

   
}

