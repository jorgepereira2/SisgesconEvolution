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

public partial class frmOMFCadastro : MarinhaPageBase
{

    #region Private Member
    [TransientPageState]
    protected NotaEntregaMaterialOMF _omf;

    #endregion 

    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.btnSalvar.Click += new EventHandler(btnSalvar_Click);
        this.btnNovo.Click += new EventHandler(btnNovo_Click);
        this.btnEnviar.Click += new EventHandler(btnEnviar_Click);
    }

	
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
        	FillPage();
            if (Request["ID_OMF"] != null)
            {
                _omf = NotaEntregaMaterialOMF.Get(Convert.ToInt32(Request["ID_OMF"]));
                PopulateFields();
            }
            else
            {
                _omf = new NotaEntregaMaterialOMF();
            }

            
            Anthem.AnthemClientMethods.Redirect("frmOMFPesquisa.aspx", btnVoltar);
            
            
        }
    }

	private void FillPage()
	{
		Util.FillDropDownList(ddlRecebedor, Servidor.List(null), ESCOLHA_OPCAO);
        Util.FillDropDownList(ddlTipoEmprego, TipoEmprego.List(), ESCOLHA_OPCAO);
	}

    private void PopulateFields()
    {
        ddlRecebedor.SelectedValue = ObjectReader.ReadID(_omf.Recebedor);
        txtDataEntrega.Text = _omf.DataEntrega.ToShortDateString();
        txtDescriminacaoMaterial.Text = _omf.DescriminacaoMaterial;
        txtNumeroEmpenho.Text = _omf.NumeroEmpenho;
        txtNumeroNota.Text = _omf.NumeroNota;
        ucFornecedor.SelectedValue = ObjectReader.ReadID(_omf.Fornecedor);
        ucFornecedor.Text = _omf.Fornecedor.RazaoSocial;
        ddlTipoEmprego.SelectedValue = ObjectReader.ReadID(_omf.TipoEmprego);
        lblNumero.Text = _omf.ID.ToString();
        lblStatus.Text = _omf.Status.Descricao;
    }

    private void ClearFields()
    {
        ddlRecebedor.SelectedIndex = -1;
        ddlTipoEmprego.SelectedIndex = -1;
        txtDataEntrega.Text = "";
        txtDescriminacaoMaterial.Text = "";
        txtNumeroEmpenho.Text = "";
        txtNumeroNota.Text = "";
        lblNumero.Text = "";
        lblStatus.Text = "";
        ucFornecedor.Reset();

        ddlRecebedor.UpdateAfterCallBack = true;
        ddlTipoEmprego.UpdateAfterCallBack = true;
        txtDataEntrega.UpdateAfterCallBack = true;
        txtDescriminacaoMaterial.UpdateAfterCallBack = true;
        txtNumeroEmpenho.UpdateAfterCallBack = true;
        txtNumeroNota.UpdateAfterCallBack = true;
        lblNumero.UpdateAfterCallBack = true;
        lblStatus.UpdateAfterCallBack = true;
    }

    private void FillObject()
    {
        _omf.DataEntrega = PageReader.ReadDate(txtDataEntrega);
        _omf.DescriminacaoMaterial = PageReader.ReadString(txtDescriminacaoMaterial);
        _omf.Fornecedor = Fornecedor.Get(Convert.ToInt32(ucFornecedor.SelectedValue));
        _omf.NumeroEmpenho = PageReader.ReadString(txtNumeroEmpenho);
        _omf.NumeroNota = PageReader.ReadString(txtNumeroNota);
        _omf.Recebedor = Servidor.Get(Convert.ToInt32(ddlRecebedor.SelectedValue));
        _omf.TipoEmprego = TipoEmprego.Get(Convert.ToInt32(ddlTipoEmprego.SelectedValue));
    }
    #endregion

    #region Events 
    void btnSalvar_Click(object sender, EventArgs e)
    {
        if(_omf.IsPersisted && _omf.Status.StatusOMFEnum != StatusOMFEnum.NaoEnviado)
        {
            ShowMessage("Esta OMF já foi enviada.");
            return;
        }
        FillObject();
        _omf.Save();
        ShowSuccessMessage();

        lblNumero.Text = _omf.ID.ToString();
        lblStatus.Text = _omf.Status.Descricao;
        lblNumero.UpdateAfterCallBack = true;
        lblStatus.UpdateAfterCallBack = true;
    }
	
    void btnNovo_Click(object sender, EventArgs e)
    {
        ClearFields();
        _omf = new NotaEntregaMaterialOMF();
    }

    void btnEnviar_Click(object sender, EventArgs e)
    {
        _omf.Enviar(this.ID_Servidor);
        ShowSuccessMessage();
        btnEnviar.Enabled = false;
        btnEnviar.UpdateAfterCallBack = true;

        lblStatus.Text = _omf.Status.Descricao;
        lblStatus.UpdateAfterCallBack = true;
        
    }
    #endregion
    
   
}
