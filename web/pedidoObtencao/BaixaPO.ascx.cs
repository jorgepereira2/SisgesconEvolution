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

public partial class BaixaPO : System.Web.UI.UserControl
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
        winBaixaPO.Hide();
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

    //public string NumeroOrdemBancaria
    //{
    //    get { return txtNumeroOrdemBancaria.Text; }
    //}

    //public string Data
    //{
    //    get { return txtData.Text; }
    //}

    public string Valor
    {
        get { return txtValor.Text; }
    }

    public int ID_Empenho
    {
        get { return Convert.ToInt32(ddlEmpenho.SelectedValue); }
    }

    //public string ValorImposto
    //{
    //    get { return txtValorImposto.Text; }
    //}

    //public string ValorDesconto
    //{
    //    get { return txtValorDesconto.Text; }
    //}

    //public string Observacao
    //{
    //    get { return txtObservacao.Text; }
    //}
    
    public void Show(PedidoObtencao po)
    {
        lnkNumeroPO.Text = po.CodigoComAno;
        lblValorTotal.Text = po.ValorTotal.ToString("N2");
        lblValorRestante.Text = (po.ValorTotal - po.ValorPago).ToString("N2");
        lblFornecedor.Text = po.Fornecedor.RazaoSocial;
        lblCNPJ.Text = po.Fornecedor.CNPJ;
      
        txtValor.Text = lblValorRestante.Text;
        //txtValorImposto.Text = "0,00";
        //txtData.Text = DateTime.Today.ToShortDateString();
        //txtValorDesconto.Text = "0,00";
        //txtObservacao.Text = "";

        ddlEmpenho.Items.Clear();
        foreach (PedidoObtencaoEmpenho empenho in po.Empenhos)
        {
            ddlEmpenho.Items.Add(new ListItem(empenho.NumeroEmpenho, empenho.ID.ToString()));
        }
        ddlEmpenho.UpdateAfterCallBack = true;

        Anthem.AnthemClientMethods.Popup(lnkNumeroPO, "fchPedidoObtencao.aspx?id_pedido=" + po.ID, false, false, false, true, true, true, true, 20, 30, 700, 550, false);

        RefreshFields();
        winBaixaPO.Show();

        
    }
    
    private void RefreshFields()
    {
        lnkNumeroPO.UpdateAfterCallBack = true;
        lblValorTotal.UpdateAfterCallBack = true;
        lblValorRestante.UpdateAfterCallBack = true;
        lblFornecedor.UpdateAfterCallBack = true;
        lblCNPJ.UpdateAfterCallBack = true;
        txtValor.UpdateAfterCallBack = true;
        //txtData.UpdateAfterCallBack = true;
        //txtValorImposto.UpdateAfterCallBack = true;
        //txtObservacao.UpdateAfterCallBack = true;
        //txtValorDesconto.UpdateAfterCallBack = true;
    }
    
    public void Close()
    {
        txtNotaFiscal.Text = "";
        txtNotaFiscal.UpdateAfterCallBack = true;
        //txtOrdemBancaria.Text = "";
        //txtOrdemBancaria.UpdateAfterCallBack = true;
        winBaixaPO.Hide();
       
    }
}
