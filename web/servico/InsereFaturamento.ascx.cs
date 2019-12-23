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
using Shared.Common;

public partial class InsereFaturamento : System.Web.UI.UserControl
{
    private DelineamentoOrcamento _orcamento
    {
        get { return (DelineamentoOrcamento) Session["_orcamento"]; }
        set { Session["_orcamento"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        btnCancelar.Click += delegate {winDados.Hide();};
        btnFaturar.Click += new EventHandler(btnOk_Click);
        
        btnEnviar.Click += new EventHandler(btnEnviar_Click);
    }

    void btnEnviar_Click(object sender, EventArgs e)
    {
        //_orcamento.RegistrarEmissaoFaturamento(((MarinhaPageBase)this.Page).ID_Servidor);
        Close();
        if (FaturamentoInserido != null)
            FaturamentoInserido(this, new EventArgs());
    }
  
    public void Show(int id_orcamento)
    {
        _orcamento = DelineamentoOrcamento.Get(id_orcamento);
        lblValorOrcamento.Text = _orcamento.ValorTotalOrcamento.ToString("C2");
        lblValorFaturado.Text = _orcamento.ValorFaturado.ToString("C2");
        lblValorOrcamento.UpdateAfterCallBack = true;
        lblValorFaturado.UpdateAfterCallBack = true;

        txtData.Text = DateTime.Today.ToShortDateString();
        txtValor.Text = _orcamento.ValorAFaturar.ToString("N2");
        txtNumeroNL.Text = _orcamento.NumeroNL;
        txtData.UpdateAfterCallBack = true;
        txtValor.UpdateAfterCallBack = true;
        txtNumeroNL.UpdateAfterCallBack = true;
        
        Parametro parametro = Parametro.Get();
        txtGarantia.Text = parametro.GarantiaFaturamento;
        txtValidade.Text = parametro.ValidadeFaturamento;
        txtGarantia.UpdateAfterCallBack = true;
        txtValidade.UpdateAfterCallBack = true;
        
        winDados.Show();

        //So mostra o botao de enviar se estiver no status AguardandoEmissaoFaturamentoFinal
        btnEnviar.Visible = _orcamento.Status.StatusPedidoServicoEnum ==
                            StatusPedidoServicoEnum.AguardandoEmissaoFaturamentoFinal;
        btnEnviar.UpdateAfterCallBack = true;
    }
    
    public void Close()
    {
        txtData.Text = "";
        txtValor.Text = "";
        txtGarantia.Text = "";
        txtValidade.Text = "";
        txtObservacao.Text = "";
        txtNumeroNL.Text = "";

        txtData.UpdateAfterCallBack = true;
        txtValor.UpdateAfterCallBack = true;
        txtValidade.UpdateAfterCallBack = true;
        txtGarantia.UpdateAfterCallBack = true;
        txtObservacao.UpdateAfterCallBack = true;
        txtNumeroNL.UpdateAfterCallBack = true;

        winDados.Hide();
    }

    public event EventHandler FaturamentoInserido;

    void btnOk_Click(object sender, EventArgs e)
    {
        DelineamentoOrcamentoFaturamento faturamento = new DelineamentoOrcamentoFaturamento();

        faturamento.Data = PageReader.ReadDate(txtData);
        faturamento.Valor = PageReader.ReadDecimal(txtValor);
        faturamento.Garantia = PageReader.ReadString(txtGarantia);
        faturamento.Validade = PageReader.ReadString(txtValidade);
        faturamento.Observacao = PageReader.ReadString(txtObservacao);
        faturamento.NumeroNL = PageReader.ReadString(txtNumeroNL);
        _orcamento.InsereFaturamento(faturamento, ((MarinhaPageBase) this.Page).ID_Servidor);

        Anthem.AnthemClientMethods.Popup(this.Page, "fchFaturamento.aspx?id_faturamento=" + faturamento.ID.ToString(),
            false, false, false, true, true, true, true, 40, 80, 700, 500);

        if (_orcamento.Status.StatusPedidoServicoEnum != StatusPedidoServicoEnum.AguardandoEmissaoFaturamentoFinal)
        {
            Close();

            if (FaturamentoInserido != null)
                FaturamentoInserido(this, new EventArgs());
        }
    }

    void btnCancelar_Click(object sender, EventArgs e)
    {
        winDados.Hide();
    }
}