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

public partial class frmMovimentoContaCadastro : MarinhaPageBase
{

    #region Private Member
    [TransientPageState]
    protected MovimentoConta _movimento;

    #endregion 

    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.btnSalvar.Click += new EventHandler(btnSalvar_Click);
        this.btnNovo.Click += new EventHandler(btnNovo_Click);
        ddlTipoOperacao.SelectedIndexChanged += new EventHandler(ddlTipoOperacao_SelectedIndexChanged);
        ddlConta.SelectedIndexChanged += new EventHandler(ddlConta_SelectedIndexChanged);
        ddlContaSaida.SelectedIndexChanged += new EventHandler(ddlContaSaida_SelectedIndexChanged);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            FillPage();
            if (Request["ID_movimento"] != null)
            {
                _movimento = MovimentoConta.Get(Convert.ToInt32(Request["ID_movimento"]));
                PopulateFields();
                RegisterDelete();
            }
            else
            {
                _movimento = new MovimentoConta();
                _movimento.TipoOperacaoFinanceira = TipoOperacaoFinanceira.Entrada;
                txtData.Text = DateTime.Today.ToShortDateString();
            }

            OperacaoChanged();
            RegisterDeleteScript();
            Anthem.AnthemClientMethods.Redirect("frmMovimentoContaPesquisa.aspx", btnVoltar);
        }
    }

    private void FillPage()
	{
        Util.FillDropDownList(ddlConta, Conta.List(), ESCOLHA_OPCAO);
        Util.FillDropDownList(ddlContaSaida, Conta.List(), ESCOLHA_OPCAO);

        Util.FillDropDownList(ddlTipoOperacao, typeof(TipoOperacaoFinanceira));
        ddlTipoOperacao.Items.RemoveAt(2);
	}

    private void PopulateFields()
    {
        txtNumeroDocumento.Text = _movimento.NumeroDocumento;
        txtObservacao.Text = _movimento.Observacao;
        txtValor.Text = _movimento.Valor.ToString("N2");
        txtData.Text = _movimento.Data.ToShortDateString();
        ddlConta.SelectedValue = _movimento.Conta.ID.ToString();
        ddlTipoOperacao.SelectedValue = Convert.ToInt32(_movimento.TipoOperacaoFinanceira).ToString();
    }

    private void ClearFields()
    {
        txtObservacao.Text = "";
        txtNumeroDocumento.Text = "";
        txtValor.Text = "";
        
        txtValor.UpdateAfterCallBack = true;
        txtNumeroDocumento.UpdateAfterCallBack = true;
    }

    private void FillObject()
    {
        _movimento.Observacao = PageReader.ReadString(txtObservacao);
        _movimento.NumeroDocumento = PageReader.ReadString(txtNumeroDocumento);
        _movimento.Valor = PageReader.ReadDecimal(txtValor);
        _movimento.Data = Convert.ToDateTime(txtData.Text);
        _movimento.Conta = Conta.Get(Convert.ToInt32(ddlConta.SelectedValue));
        _movimento.TipoOperacaoFinanceira = (TipoOperacaoFinanceira)Convert.ToInt32(ddlTipoOperacao.SelectedValue);
        _movimento.TipoMovimentoFinanceiro = _movimento.TipoOperacaoFinanceira == TipoOperacaoFinanceira.SaidaDireta ? TipoMovimentoFinanceiro.SaidaFinalizado : TipoMovimentoFinanceiro.Entrada;
        _movimento.Servidor = Servidor.Get(this.ID_Servidor);
    }
    #endregion

    #region Events 
    void btnSalvar_Click(object sender, EventArgs e)
    {
        if (_movimento.TipoOperacaoFinanceira == TipoOperacaoFinanceira.Transferencia && _movimento.IsPersisted)
        {
            ShowMessage("Não é possível alterar uma transferência.");
            return;
        }
        FillObject();
        _movimento.Save();

        if (_movimento.TipoOperacaoFinanceira == TipoOperacaoFinanceira.Transferencia)
        {
            MovimentoConta saida = new MovimentoConta();
            saida.Observacao = PageReader.ReadString(txtObservacao);
            saida.NumeroDocumento = PageReader.ReadString(txtNumeroDocumento);
            saida.Valor = PageReader.ReadDecimal(txtValor);
            saida.Conta = Conta.Get(Convert.ToInt32(ddlContaSaida.SelectedValue));
            saida.Data = Convert.ToDateTime(txtData.Text);
            saida.Servidor = Servidor.Get(this.ID_Servidor);
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
        _movimento = new MovimentoConta();
    }

    void ddlTipoOperacao_SelectedIndexChanged(object sender, EventArgs e)
    {
        _movimento.TipoOperacaoFinanceira = (TipoOperacaoFinanceira)Convert.ToInt32(ddlTipoOperacao.SelectedValue);
        OperacaoChanged();
    }

    private void OperacaoChanged()
    {
        pnSaida.Visible = valConta.Enabled = _movimento.TipoOperacaoFinanceira == TipoOperacaoFinanceira.Transferencia;
        pnSaida.UpdateAfterCallBack = true;

        lblEntradaSaida.Text = _movimento.TipoOperacaoFinanceira == TipoOperacaoFinanceira.SaidaDireta ? "Saída Direta" : "Entrada";
        lblEntradaSaida.UpdateAfterCallBack = true;
    }

    #endregion

    [Anthem.Method]
    public void Excluir()
    {
        try
        {
            _movimento.Delete();
            ShowSuccessMessage();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message);
        }
    }

    private void RegisterDelete()
    {
        Anthem.Manager.AddScriptAttribute(btnExcluir, "onclick", string.Format("javascript:Excluir({0});", _movimento.ID));
        btnExcluir.UpdateAfterCallBack = true;
    }

    #region Saldos
    void ddlContaSaida_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowSaldoConta(ddlContaSaida, lblSaldoContaSaida);
    }

    void ddlConta_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowSaldoConta(ddlConta, lblSaldoConta);
    }

    private void ShowSaldoConta(DropDownList ddlContaEscolhida, Anthem.Label lblSaldo)
    {
        DataSet ds = Conta.SelectSaldo(Convert.ToInt32(ddlContaEscolhida.SelectedValue), DateTime.Today.Year);
        if (ds.Tables[0].Rows.Count > 0)
            lblSaldo.Text = string.Format("Saldo Geral: {0:C2}<br>Valor Comprometido: {1:C2}<br>Saldo Disponível: {2:C2}",
                ds.Tables[0].Rows[0]["SaldoGeral"], ds.Tables[0].Rows[0]["ValorComprometido"], ds.Tables[0].Rows[0]["SaldoDisponivel"]);
        lblSaldo.UpdateAfterCallBack = true;
    }
    #endregion
}
