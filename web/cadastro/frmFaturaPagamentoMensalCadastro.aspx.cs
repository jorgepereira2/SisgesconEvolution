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

public partial class frmFaturaPagamentoMensalCadastro : MarinhaPageBase
{

    #region Private Member
    [TransientPageState]
    protected FaturaPagamentoMensal _fatura;

    #endregion 

    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.btnSalvar.Click += new EventHandler(btnSalvar_Click);
        this.btnNovo.Click += new EventHandler(btnNovo_Click);
        ddlTipoFatura.SelectedIndexChanged += new EventHandler(ddlTipoFatura_SelectedIndexChanged);
    }

    void ddlTipoFatura_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTipoFatura.SelectedValue != "0")
        {
            _fatura.TipoFaturaPagamento = TipoFaturaPagamento.Get(Convert.ToInt32(ddlTipoFatura.SelectedValue));
            Anthem.AnthemClientMethods.ShowHide(trFornecedor, !_fatura.TipoFaturaPagamento.FlagDiaria);
            Anthem.AnthemClientMethods.ShowHide(trServidor, _fatura.TipoFaturaPagamento.FlagDiaria);
        }
    }
	
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
        	FillPage();
            if (Request["id_fatura"] != null)
            {
                _fatura = FaturaPagamentoMensal.Get(Convert.ToInt32(Request["id_fatura"]));
                PopulateFields();
            }
            else
            {
                _fatura = new FaturaPagamentoMensal();
            }
            Anthem.AnthemClientMethods.Redirect("frmFaturaPagamentoMensalPesquisa.aspx", btnVoltar);
            
        }
    }

	private void FillPage()
	{
        Util.FillDropDownList(ddlTipoFatura, TipoFaturaPagamento.List(), ESCOLHA_OPCAO);
        Util.FillDropDownList(ddlMes, DateTimeManager.Meses(), ESCOLHA_OPCAO);

	}

    private void PopulateFields()
    {
        ddlTipoFatura.SelectedValue = ObjectReader.ReadID(_fatura.TipoFaturaPagamento);
        txtNumeroFatura.Text = _fatura.NumeroFaturaOS;
        txtDataEmissao.Text = ObjectReader.ReadDate(_fatura.DataEmissao);
        txtDataVencimento.Text = ObjectReader.ReadDate(_fatura.DataVencimento);
        txtValorTotal.Text = ObjectReader.ReadDecimal(_fatura.ValorTotal);
        ddlMes.SelectedValue = _fatura.MesReferencia.ToString();

        ddlTipoFatura_SelectedIndexChanged(null, null);

        if(_fatura.TipoFaturaPagamento.FlagDiaria)
        {
            ucServidor.SelectedValue = ObjectReader.ReadID(_fatura.Servidor);
            ucServidor.Text = _fatura.Servidor.NomeCompleto;
        }
        else
        {
            ucFornecedor.SelectedValue = ObjectReader.ReadID(_fatura.Fornecedor);
            ucFornecedor.Text = _fatura.Fornecedor.RazaoSocial;
        }
    }

    private void ClearFields()
    {   
        txtNumeroFatura.Text = "";
        txtDataEmissao.Text = "";
        txtDataVencimento.Text = "";
        txtValorTotal.Text = "";
        
        txtNumeroFatura.UpdateAfterCallBack = true;
        txtDataEmissao.UpdateAfterCallBack = true;
        txtDataVencimento.UpdateAfterCallBack = true;
        txtValorTotal.UpdateAfterCallBack = true;
    }

    private void FillObject()
    {
        _fatura.NumeroFaturaOS = txtNumeroFatura.Text;
        _fatura.MesReferencia = PageReader.ReadInt(ddlMes);
        _fatura.TipoFaturaPagamento = TipoFaturaPagamento.Get(Convert.ToInt32(ddlTipoFatura.SelectedValue));
        _fatura.DataEmissao = PageReader.ReadDate(txtDataEmissao);
        _fatura.DataVencimento = PageReader.ReadDate(txtDataVencimento);
        _fatura.ValorTotal = PageReader.ReadDecimal(txtValorTotal);

        if (_fatura.TipoFaturaPagamento.FlagDiaria)
        {
            _fatura.Fornecedor = null;
            _fatura.Servidor = Servidor.Get(Convert.ToInt32(ucServidor.SelectedValue));
        }
        else
        {
            _fatura.Servidor = null;
            _fatura.Fornecedor = Fornecedor.Get(Convert.ToInt32(ucFornecedor.SelectedValue));
        }
    }
    #endregion

    #region Events 
    void btnSalvar_Click(object sender, EventArgs e)
    {
        FillObject();
        _fatura.Save();	
		ShowSuccessMessage();
    }
	
    void btnNovo_Click(object sender, EventArgs e)
    {
        ClearFields();
        _fatura = new FaturaPagamentoMensal();
    }
    #endregion
   
}
