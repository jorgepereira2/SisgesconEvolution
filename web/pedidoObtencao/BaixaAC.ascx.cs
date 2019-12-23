using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Marinha.Business;

public partial class BaixaAC : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        btnCancelar.Click += new EventHandler(btnCancelar_Click);
        btnOk.Click += new EventHandler(btnOk_Click);
        
    }

    void btnCancelar_Click(object sender, EventArgs e)
    {
        winBaixaAC.Hide();
        if(OperacaoCancelada != null)
            OperacaoCancelada(this, new EventArgs());
    }

    void btnOk_Click(object sender, EventArgs e)
    {
        if (BaixaInformada != null)
            BaixaInformada(this, new EventArgs());
       
    }
   
    public event EventHandler BaixaInformada;
    public event EventHandler OperacaoCancelada;

    public string NotaFiscal
    {
        get { return txtNotaFiscal.Text; }
    }

    public string OrdemBancaria
    {
        get { return txtOrdemBancaria.Text; }
    }

    public string Data
    {
        get { return txtData.Text; }
    }

    public string Valor
    {
        get { return txtValor.Text; }
    }

    public string ValorImposto
    {
        get { return txtValorImposto.Text; }
    }

    public string ValorDesconto
    {
        get { return txtValorDesconto.Text; }
    }

    public string Observacao
    {
        get { return txtObservacao.Text; }
    }
    
    public void Show(AutorizacaoCompra ac)
    {
        lnkNumeroAC.Text = ac.CodigoComAno;
        lblValorTotal.Text = ac.ValorTotal.ToString("N2");
        lblValorRestante.Text = (ac.ValorTotal - ac.ValorPago).ToString("N2");
        lblFornecedor.Text = ac.Fornecedor.RazaoSocial;
        lblCNPJ.Text = ac.Fornecedor.CNPJ;
        lblNumeroEmpenho.Text = ac.NumeroEmpenho;
        txtValor.Text = lblValorRestante.Text;
        txtValorImposto.Text = "0,00";
        txtData.Text = DateTime.Today.ToShortDateString();
        txtValorDesconto.Text = "0,00";
        txtObservacao.Text = "";

        Anthem.AnthemClientMethods.Popup(lnkNumeroAC, "fchAutorizacaoCompra.aspx?id_ac=" + ac.ID, false, false, false, true, true, true, true, 20, 30, 700, 550, false);

        RefreshFields();
        winBaixaAC.Show();

        
    }
    
    private void RefreshFields()
    {
        lnkNumeroAC.UpdateAfterCallBack = true;
        lblValorTotal.UpdateAfterCallBack = true;
        lblValorRestante.UpdateAfterCallBack = true;
        lblFornecedor.UpdateAfterCallBack = true;
        lblCNPJ.UpdateAfterCallBack = true;
        lblNumeroEmpenho.UpdateAfterCallBack = true;
        txtValor.UpdateAfterCallBack = true;
        txtData.UpdateAfterCallBack = true;
        txtValorImposto.UpdateAfterCallBack = true;
        txtObservacao.UpdateAfterCallBack = true;
        txtValorDesconto.UpdateAfterCallBack = true;
    }
    
    public void Close()
    {
        txtNotaFiscal.Text = "";
        txtNotaFiscal.UpdateAfterCallBack = true;
        txtOrdemBancaria.Text = "";
        txtOrdemBancaria.UpdateAfterCallBack = true;
        winBaixaAC.Hide();
       
    }
}
