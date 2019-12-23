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

public partial class frmOrcamentoEdicao : MarinhaPageBase
{

    #region Private Member
    [TransientPageState]
    protected DelineamentoOrcamento _orcamento;

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
            if (Request["ID_Orcamento"] != null)
            {
                _orcamento = DelineamentoOrcamento.Get(Convert.ToInt32(Request["ID_Orcamento"]));
                PopulateFields();
                this.DataBind();
            }
        }
    }

	private void FillPage()
	{
		
	}
    #endregion
    
    #region Object
    private void PopulateFields()
    {
        txtMensagemEnvioCliente.Text = _orcamento.MensagemEnvioCliente;
        txtMensagemAprovacaoCliente.Text = _orcamento.MensagemAprovacaoCliente;
    }
       
    private void FillObject()
    {
        _orcamento.MensagemEnvioCliente = PageReader.ReadString(txtMensagemEnvioCliente);
        _orcamento.MensagemAprovacaoCliente = PageReader.ReadString(txtMensagemAprovacaoCliente);
    }
    #endregion

    #region Events 
    void btnSalvar_Click(object sender, EventArgs e)
    {
        FillObject();
        _orcamento.Save();
        ShowSuccessMessage();
    }
    #endregion
   
}
