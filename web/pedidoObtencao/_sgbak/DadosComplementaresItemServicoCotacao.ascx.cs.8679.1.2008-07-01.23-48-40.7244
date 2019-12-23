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

public partial class DadosComplementaresItemServicoCotacao : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        btnOk.Click += delegate {Close();};
    }
    
    public void Show(PedidoCotacaoItem item)
    {
    
        PedidoObtencaoItem itemObtencao = item.ItensObtencao[0];
        if (itemObtencao.PedidoObtencao.DelineamentoOrcamento == null)
        {
            Anthem.AnthemClientMethods.Alert("Não existem dados complementares para este item.", this.Page);
            return;
        }
        foreach (PedidoServicoItemOrcamento itemOrcamento in itemObtencao.PedidoObtencao.DelineamentoOrcamento.ItensOrcamento)
        {
            if(itemOrcamento.ServicoMaterial.TipoServicoMaterial == TipoServicoMaterial.Servico && 
                itemOrcamento.ServicoMaterial.ID == item.ServicoMaterial.ID)
            {
                lblFornecedor.Text = itemOrcamento.Fornecedor == null ? "" : itemOrcamento.Fornecedor.RazaoSocial;
                lblCNPJ.Text = itemOrcamento.Fornecedor == null ? "" : itemOrcamento.Fornecedor.CNPJ;
                lblObservacao.Text = itemOrcamento.Observacao;
                
                lblObservacao.UpdateAfterCallBack = true;
                lblFornecedor.UpdateAfterCallBack = true;
                lblCNPJ.UpdateAfterCallBack = true;
                break;
            }
        }
        
        winDados.Show();
    }
    
    public void Close()
    {
        winDados.Hide();
        lblObservacao.Text = "";
        lblObservacao.UpdateAfterCallBack = true;
        lblFornecedor.Text = "";
        lblFornecedor.UpdateAfterCallBack = true;
        lblCNPJ.Text = "";
        lblCNPJ.UpdateAfterCallBack = true;
    }
    
}
