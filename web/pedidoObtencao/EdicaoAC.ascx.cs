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

public partial class EdicaoAC : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            Util.FillDropDownList(ddlNaturezaDespesa, NaturezaDespesa.List(), "-- Escolha um opção --");
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
        winEdicaoAC.Hide();
    }

    void btnOk_Click(object sender, EventArgs e)
    {
        if (OkClicked != null)
            OkClicked(this, new EventArgs());
       
    }
   
    public event EventHandler OkClicked;
    
    
    public string CodigoGestao
    {
        get { return txtCodigoGestao.Text; }
    }
    public string NumeroLancamento
    {
        get { return txtNumeroLancamento.Text; }
    }
    public string NumeroEmpenho
    {
        get { return txtNumeroEmpenho.Text; }
    }
    public string Lista
    {
        get { return txtLista.Text; }
    }

    public int ID_NaturezaDespesa
    {
        get { return Convert.ToInt32(ddlNaturezaDespesa.SelectedValue); }
    }
    
    public string Observacao
    {
        get { return txtObservacao.Text; }
    }
   
    public void Show(AutorizacaoCompra ac)
    {
        txtObservacao.Text = ac.Observacao;
        txtCodigoGestao.Text = ac.CodigoGestao;
        txtLista.Text = ac.Lista;
        txtNumeroLancamento.Text = ac.NumeroLancamento;
        ddlNaturezaDespesa.SelectedValue = ObjectReader.ReadID(ac.NaturezaDespesa);

        Refresh();

        winEdicaoAC.Show();
    }

    private void Refresh()
    {
        txtCodigoGestao.UpdateAfterCallBack = true;
        txtLista.UpdateAfterCallBack = true;
        txtNumeroLancamento.UpdateAfterCallBack = true;
        txtObservacao.UpdateAfterCallBack = true;
        ddlNaturezaDespesa.UpdateAfterCallBack = true;
    }

    public void Close()
    {
        winEdicaoAC.Hide();

        txtObservacao.Text = "";
        txtCodigoGestao.Text = "";
        txtLista.Text = "";
        txtNumeroLancamento.Text = "";
        ddlNaturezaDespesa.SelectedIndex = -1;

        Refresh();
            
        
    }
}
