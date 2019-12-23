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

public partial class DadosComplementaresItemServico : System.Web.UI.UserControl
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
        //if(txtObservacao.Text.Length > 500)
        //{
        //    Anthem.AnthemClientMethods.Alert("O campo observação não pode ter mais de 500 caracteres. Tamaho atual: " + txtObservacao.Text.Length, this.Page);
        //    return;
        //}
        Close();
        if (OkClicked != null)
            OkClicked(this, new EventArgs());
       
    }

    void btnCancelar_Click(object sender, EventArgs e)
    {
        winDados.Hide();
    }

    public event EventHandler OkClicked;    

    public string Observacao
    {
        get { return txtObservacao.Text; }
    }

    //public string ID_Fornecedor
    //{
    //    get { return ucFornecedor.SelectedValue; }
    //}
    
    public int ID_Item
    {
        get{ return (int) ViewState["ID_Item"];}
        set{ ViewState["ID_Item"] = value;}
    }
    
    public void Show()
    {
        txtObservacao.Text = "";
        txtObservacao.UpdateAfterCallBack = true;
        //ucFornecedor.SelectedValue = "0";
        //ucFornecedor.Text = "";
        //ucFornecedor.UpdateAfterCallBack = true;
        winDados.Show();
    }
    
    public void Close()
    {
        winDados.Hide();
      
    }
    
    public void Fill(PedidoServicoItemOrcamento itemOrcamento)
    {
        //if(itemOrcamento.Fornecedor != null)
        //{
        //    ucFornecedor.SelectedValue = itemOrcamento.Fornecedor.ID.ToString();
        //    ucFornecedor.Text = itemOrcamento.Fornecedor.RazaoSocial;
        //    ucFornecedor.UpdateAfterCallBack = true;
        //}
        txtObservacao.Text = itemOrcamento.Observacao;
        txtObservacao.UpdateAfterCallBack = true;
        
    }
}
