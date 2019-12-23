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

public partial class frmParametroPACadastro : MarinhaPageBase
{

    #region Private Member
    [TransientPageState]
    protected Parametro _parametro;

    #endregion 

    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.btnSalvar.Click += new EventHandler(btnSalvar_Click);
    }

	
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
        	FillPage();
           
            _parametro = Parametro.Get();
            PopulateFields();
        }
    }

	private void FillPage()
	{
		
	}

	private void PopulateFields()
    {
        txtNumeroMinimoCotacoes.Text = ObjectReader.ReadInt(_parametro.NumeroMinimoCotacoesCompra);
        chkEntradaItemManual.Checked = _parametro.EntradaItemCompraManual;
	    txtForca.Text = _parametro.Forca;
	    txtOrganizacaoMilitar.Text = _parametro.OrganizacaoMilitar;
	    txtCNPJ.Text = _parametro.CNPJ;
	    txtEndereco.Text = _parametro.Endereco;
	    txtTelefone.Text = _parametro.Telefone;
	    txtValorMaximoSemOrcamento.Text = _parametro.ValorMaximoSemOrcamentoPA.ToString("N2");

	    dgTipoPA.DataSource = TipoPedidoAquisicao.Select();
	    dgTipoPA.DataKeyField = "ID";
	    dgTipoPA.DataBind();
    }

    private void FillObject()
    {
        _parametro.NumeroMinimoCotacoesCompra = PageReader.ReadInt(txtNumeroMinimoCotacoes);
        _parametro.EntradaItemCompraManual = chkEntradaItemManual.Checked;
        _parametro.ValorMaximoSemOrcamentoPA = PageReader.ReadDecimal(txtValorMaximoSemOrcamento);

        _parametro.Forca = PageReader.ReadString(txtForca);
        _parametro.OrganizacaoMilitar = PageReader.ReadString(txtOrganizacaoMilitar);
        _parametro.CNPJ = PageReader.ReadString(txtCNPJ);
        _parametro.Endereco = PageReader.ReadString(txtEndereco);
        _parametro.Telefone = PageReader.ReadString(txtTelefone);
        
    }
    #endregion

    #region Events 
    void btnSalvar_Click(object sender, EventArgs e)
    {
        FillObject();
        _parametro.Save();

        SalvarTipoCompra();
		ShowSuccessMessage();
    }

    private void SalvarTipoCompra()
    {
        foreach (DataGridItem item in dgTipoPA.Items)
        {
            if(item.ItemType == ListItemType.AlternatingItem || item.ItemType == ListItemType.Item)
            {
                TipoPedidoAquisicao tipoPA = TipoPedidoAquisicao.Get(Convert.ToInt32(dgTipoPA.DataKeys[item.ItemIndex]));
                TextBox txtLimiteAnual = (TextBox) item.FindControl("txtLimiteAnual");
                TextBox txtLimitePA = (TextBox)item.FindControl("txtLimitePA");
                tipoPA.ValorLimiteAno = PageReader.ReadDecimal(txtLimiteAnual);
                tipoPA.ValorLimitePO = PageReader.ReadDecimal(txtLimitePA);
                tipoPA.Save();
            }
        }
    }

    #endregion
   
}
