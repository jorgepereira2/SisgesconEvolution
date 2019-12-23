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

public partial class frmEntradaValoresCadastro : MarinhaPageBase
{

    #region Private Member
    [TransientPageState]
    protected EntradaValores _entrada;

    #endregion 

    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.btnSalvar.Click += new EventHandler(btnSalvar_Click);
        this.btnNovo.Click += new EventHandler(btnNovo_Click);
        ddlTipoOperacao.SelectedIndexChanged += new EventHandler(ddlTipoOperacao_SelectedIndexChanged);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            FillPage();
            if (Request["ID_entrada"] != null)
            {
                _entrada = EntradaValores.Get(Convert.ToInt32(Request["ID_entrada"]));
                PopulateFields();
                RegisterDelete();
            }
            else
            {
                _entrada = new EntradaValores();
                _entrada.TipoOperacaoFinanceira = TipoOperacaoFinanceira.Entrada;
            }

            OperacaoChanged();
            RegisterDeleteScript();
            Anthem.AnthemClientMethods.Redirect("frmEntradaValoresPEsquisa.aspx", btnVoltar);
        }
    }

    private void FillPage()
	{
        Util.FillDropDownList(ddlProjeto, Projeto.List(), ESCOLHA_OPCAO);
        Util.FillDropDownList(ddlNaturezaDespesa, NaturezaDespesa.ListPai(), ESCOLHA_OPCAO);
        Util.FillDropDownList(ddlFonteRecurso, FonteRecurso.List(), ESCOLHA_OPCAO);
        Util.FillDropDownList(ddlPTRES, PTRES.List(), ESCOLHA_OPCAO);

        Util.FillDropDownList(ddlProjetoSaida, Projeto.List(), ESCOLHA_OPCAO);
        Util.FillDropDownList(ddlNaturezaDespesaSaida, NaturezaDespesa.ListPai(), ESCOLHA_OPCAO);
        Util.FillDropDownList(ddlFonteRecursoSaida, FonteRecurso.List(), ESCOLHA_OPCAO);
        Util.FillDropDownList(ddlPTRESSaida, PTRES.List(), ESCOLHA_OPCAO);

        Util.FillDropDownList(ddlTipoOperacao, typeof(TipoOperacaoFinanceira));
        ddlTipoOperacao.Items.RemoveAt(2);
	}

    private void PopulateFields()
    {
        txtNumeroDocumento.Text = _entrada.NumeroDocumento;
        txtValor.Text = _entrada.Valor.ToString("N2");
        ddlProjeto.SelectedValue = _entrada.Projeto.ID.ToString();
        ddlNaturezaDespesa.SelectedValue = _entrada.NaturezaDespesa.ID.ToString();
        ddlFonteRecurso.SelectedValue = ObjectReader.ReadID(_entrada.FonteRecurso);
        ddlPTRES.SelectedValue = ObjectReader.ReadID(_entrada.PTRES);
        ddlTipoOperacao.SelectedValue = Convert.ToInt32(_entrada.TipoOperacaoFinanceira).ToString();
    }

    private void ClearFields()
    {
        txtNumeroDocumento.Text = "";
        txtValor.Text = "";
        
        txtValor.UpdateAfterCallBack = true;
        txtNumeroDocumento.UpdateAfterCallBack = true;
    }

    private void FillObject()
    {
        _entrada.NumeroDocumento = PageReader.ReadString(txtNumeroDocumento);
        _entrada.Valor = PageReader.ReadDecimal(txtValor);
        _entrada.Projeto = Projeto.Get(Convert.ToInt32(ddlProjeto.SelectedValue));
        _entrada.NaturezaDespesa = NaturezaDespesa.Get(Convert.ToInt32(ddlNaturezaDespesa.SelectedValue));
        _entrada.Servidor = Servidor.Get(this.ID_Servidor);
        _entrada.FonteRecurso = FonteRecurso.Get(Convert.ToInt32(ddlFonteRecurso.SelectedValue));
        _entrada.PTRES = PTRES.Get(Convert.ToInt32(ddlPTRES.SelectedValue));
        _entrada.TipoMovimentoFinanceiro = TipoMovimentoFinanceiro.Entrada;
        _entrada.TipoOperacaoFinanceira = (TipoOperacaoFinanceira) Convert.ToInt32(ddlTipoOperacao.SelectedValue);
    }
    #endregion

    #region Events 
    void btnSalvar_Click(object sender, EventArgs e)
    {
        if(_entrada.TipoOperacaoFinanceira == TipoOperacaoFinanceira.Transferencia && _entrada.IsPersisted)
        {
            ShowMessage("Não é possível alterar uma transferência.");
            return;
        }
        FillObject();
        _entrada.Save();

        if (_entrada.TipoOperacaoFinanceira == TipoOperacaoFinanceira.Transferencia)
        {
            EntradaValores saida = new EntradaValores();
            saida.NumeroDocumento = PageReader.ReadString(txtNumeroDocumento);
            saida.Valor = PageReader.ReadDecimal(txtValor);
            saida.Projeto = Projeto.Get(Convert.ToInt32(ddlProjetoSaida.SelectedValue));
            saida.NaturezaDespesa = NaturezaDespesa.Get(Convert.ToInt32(ddlNaturezaDespesaSaida.SelectedValue));
            saida.Servidor = Servidor.Get(this.ID_Servidor);
            saida.FonteRecurso = FonteRecurso.Get(Convert.ToInt32(ddlFonteRecursoSaida.SelectedValue));
            saida.PTRES = PTRES.Get(Convert.ToInt32(ddlPTRESSaida.SelectedValue));
            saida.TipoMovimentoFinanceiro = TipoMovimentoFinanceiro.SaidaFinalizado;
            saida.TipoOperacaoFinanceira = (TipoOperacaoFinanceira)Convert.ToInt32(ddlTipoOperacao.SelectedValue);
            saida.Save();
        }

		ShowSuccessMessage();
        RegisterDelete();
    }
	
    void btnNovo_Click(object sender, EventArgs e)
    {
        ClearFields();
        _entrada = new EntradaValores();
    }

    void ddlTipoOperacao_SelectedIndexChanged(object sender, EventArgs e)
    {
        _entrada.TipoOperacaoFinanceira = (TipoOperacaoFinanceira) Convert.ToInt32(ddlTipoOperacao.SelectedValue);
        OperacaoChanged();
    }

    private void OperacaoChanged()
    {
        pnSaida.Visible = valPTRESSaida.Enabled = valProjetoSaida.Enabled = valNaturezaDespesaSaida.Enabled = valFonteRecursoSaida.Enabled =
            _entrada.TipoOperacaoFinanceira == TipoOperacaoFinanceira.Transferencia;
        pnSaida.UpdateAfterCallBack = true;
    }

    #endregion

    [Anthem.Method]
    public void Excluir()
    {
        try
        {
            _entrada.Delete();
            ShowSuccessMessage();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message);
        }
    }

    private void RegisterDelete()
    {
        Anthem.Manager.AddScriptAttribute(btnExcluir, "onclick", string.Format("javascript:Excluir({0});", _entrada.ID));
        btnExcluir.UpdateAfterCallBack = true;
    }
}
