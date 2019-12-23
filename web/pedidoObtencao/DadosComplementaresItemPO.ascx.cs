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

public partial class DadosComplementaresItemPO : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        btnCancelar.Click += delegate {winDados.Hide();};
        btnOk.Click += new EventHandler(btnOk_Click);
    }

    void btnOk_Click(object sender, EventArgs e)
    {
        Close();
        if (OkClicked != null)
            OkClicked(this, new EventArgs());
       
    }

    void btnCancelar_Click(object sender, EventArgs e)
    {
        winDados.Hide();
    }

    public event EventHandler OkClicked;    

    public string Especificacao
    {
        get { return txtObservacao.Text; }
    }

    public string ID_Fornecedor
    {
        get { return ucFornecedor.SelectedValue; }
    }
    
    public int ID_Item
    {
        get{ return (int) ViewState["ID_Item"];}
        set{ ViewState["ID_Item"] = value;}
    }
    
    public void Show()
    {
        txtObservacao.Text = "";
        txtObservacao.UpdateAfterCallBack = true;
        ucFornecedor.SelectedValue = "0";
        ucFornecedor.Text = "";
        ucFornecedor.UpdateAfterCallBack = true;
        winDados.Show();
    }
    
    public void Close()
    {
        winDados.Hide();
      
    }
    
    public void Fill(PedidoObtencaoItem item)
    {
        if (item.Fornecedor != null)
        {
            ucFornecedor.SelectedValue = item.Fornecedor.ID.ToString();
            ucFornecedor.Text = item.Fornecedor.RazaoSocial;
            ucFornecedor.UpdateAfterCallBack = true;
        }
        txtObservacao.Text = item.Especificacao;
        txtObservacao.UpdateAfterCallBack = true;
        
    }
}
