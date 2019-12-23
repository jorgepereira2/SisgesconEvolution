using System;
using Marinha.Business;

public partial class UserControls_BuscaFornecedorCompra : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public bool Enabled
    {
        get
        {
            EnsureChildControls();
            return ucFornecedor.Enabled;
        }
        set
        {
            EnsureChildControls();
            ucFornecedor.Enabled = value;
        }
    }

    public string SelectedValue
    {
        get
        {
            EnsureChildControls();
            return ucFornecedor.SelectedValue;
        }
        set
        {
            EnsureChildControls();
            ucFornecedor.SelectedValue = value;
            FillData(Fornecedor.Get(Convert.ToInt32(value)));
        }
    }

    public string Text
    {
        get
        {
            EnsureChildControls();
            return ucFornecedor.Text;
        }
        set
        {
            EnsureChildControls();
            ucFornecedor.Text = value;
        }
    }

    protected void ucFornecedor_SelectedIndexChanged(object source, BuscaFornecedorEventArgs e)
    {
        FillData(e.Fornecedor);
    }

    private void FillData(Fornecedor fornecedor)
    {
        if(fornecedor != null)
        {
            lblTelefone.Text = fornecedor.Telefone;
            AtualizarSaldo();
        }
        else
        {
            lblTelefone.Text = "";
        }
        lblTelefone.UpdateAfterCallBack = true;
    }
    
    public void AtualizarSaldo()
    {
        if(ID_TipoCompra <= 0) return;
        
        TipoCompra tipoCompra = TipoCompra.Get(ID_TipoCompra);
        lblLimiteAnual.Text = tipoCompra.LimiteAnual.ToString("N2");
        SaldoFornecedor saldo =
            AutorizacaoCompra.GetSaldoComprasUtilizado(Convert.ToInt32(ucFornecedor.SelectedValue), tipoCompra.ID,
                                                DateTime.Today.Year);
        lblValorUtilizado.Text = (tipoCompra.LimiteAnual - saldo.SaldoTotal).ToString("N2");
        lblSaldoDisponivel.Text = saldo.SaldoTotal.ToString("N2"); 

        lblLimiteAnual.UpdateAfterCallBack = true;
        lblSaldoDisponivel.UpdateAfterCallBack = true;
        lblValorUtilizado.UpdateAfterCallBack = true;
    }
    
    public int ID_TipoCompra
    {
        get{ return (int) ViewState["ID_TipoCompra"];}
        set { ViewState["ID_TipoCompra"] = value; }
    }
    
    public decimal GetValorSaldo(decimal totalPC)
    {
        if(ucFornecedor.SelectedValue == "0") return 0;
        if (lblSaldoDisponivel.Text == "") return 0;
        return Convert.ToDecimal(lblSaldoDisponivel.Text) - totalPC;
    }
}
