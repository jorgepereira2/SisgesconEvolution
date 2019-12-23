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
using Shared.NHibernateDAL;

public partial class ucNaturezaDespesa : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            Util.FillDropDownList(ddlNaturezaDespesa, NaturezaDespesa.List(), "-- Escolha uma opção --");
            Util.FillDropDownList(ddlPTRES, PTRES.List(), "-- Escolha uma opção --");
        }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        btnCancelar.Click += new EventHandler(btnCancelar_Click);
        btnOk.Click += new EventHandler(btnOk_Click);
        
    }

    void btnCancelar_Click(object sender, EventArgs e)
    {
        winNaturezaDespesa.Hide();
        if(OperacaoCancelada != null)
            OperacaoCancelada(this, new EventArgs());
    }

    void btnOk_Click(object sender, EventArgs e)
    {
        if (NaturezaInformada != null)
            NaturezaInformada(this, new EventArgs());
       
    }
   
    public event EventHandler NaturezaInformada;
    public event EventHandler OperacaoCancelada;

   
    
    public string ID_NaturezaDespesa
    {
        get { return ddlNaturezaDespesa.SelectedValue; }
    }

    public string ID_PTRES
    {
        get { return ddlPTRES.SelectedValue; }
    }

    public string Comentario
    {
        get { return txtComentario.Text; }
    }

    private int id_ac
    {
        get { return (int) ViewState["id_ac"]; } 
        set { ViewState["id_ac"] = value; } 
    }

    public void Show(AutorizacaoCompra ac)
    {
        ddlNaturezaDespesa.SelectedValue = ObjectReader.ReadID(ac.NaturezaDespesa);
        ddlPTRES.SelectedValue = ObjectReader.ReadID(ac.PTRES);
        id_ac = ac.ID;
        AtualizaSaldo(ac);

        Refresh();

        winNaturezaDespesa.Show();
    }

    private void Refresh()
    {
        ddlNaturezaDespesa.UpdateAfterCallBack = true;
        ddlPTRES.UpdateAfterCallBack = true;
        txtComentario.UpdateAfterCallBack = true;
    }

    public void Close()
    {
        winNaturezaDespesa.Hide();
       
        ddlNaturezaDespesa.SelectedIndex = -1;
        ddlPTRES.SelectedIndex = -1;
        txtComentario.Text = "";
       
        Refresh();
       
    }

    protected void AtualizaSaldo(object sender, EventArgs e)
    {
        AtualizaSaldo(AutorizacaoCompra.Get(id_ac));
    }

    private void AtualizaSaldo(AutorizacaoCompra ac)
    {
        if (ac == null) return;
        ac.NaturezaDespesa = NaturezaDespesa.Get(Convert.ToInt32(ddlNaturezaDespesa.SelectedValue));
        ac.PTRES = PTRES.Get(Convert.ToInt32(ddlPTRES.SelectedValue));

        DataSet ds = EntradaValores.SelectSaldo(ac.ChaveFinanceiro);
        if(ds.Tables[0].Rows.Count > 0)
        {
            DataRow row = ds.Tables[0].Rows[0];
            lblComprometido.Text = Convert.ToDecimal(row["ValorComprometido"]).ToString("c");
            lblSaldo.Text = (Convert.ToDecimal(row["ValorEntrada"]) - Convert.ToDecimal(row["ValorEmpenhado"])).ToString("c");
            lblSaldoTotal.Text = (Convert.ToDecimal(row["ValorEntrada"]) - Convert.ToDecimal(row["ValorComprometido"]) - Convert.ToDecimal(row["ValorEmpenhado"]) - ac.ValorTotal).ToString("c");
            lblCusto.Text = ac.ValorTotal.ToString("c");

            lblCusto.UpdateAfterCallBack = lblSaldoTotal.UpdateAfterCallBack = lblSaldo.UpdateAfterCallBack = lblComprometido.UpdateAfterCallBack = true;
        }
    }
}
