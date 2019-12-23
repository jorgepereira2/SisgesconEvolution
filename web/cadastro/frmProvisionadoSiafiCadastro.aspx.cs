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

public partial class frmProvisionadoSiafiCadastro : MarinhaPageBase
{

    #region Private Member
    [TransientPageState]
    protected ProvisionadoSiafi _provisionadoSiafi;

    #endregion 

    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.btnSalvar.Click += new EventHandler(btnSalvar_Click);
        this.btnNovo.Click += new EventHandler(btnNovo_Click);
    }
	
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
        	FillPage();
            if (Request["ID_ProvisionadoSiafi"] != null)
            {
                _provisionadoSiafi = ProvisionadoSiafi.Get(Convert.ToInt32(Request["ID_ProvisionadoSiafi"]));
                PopulateFields();
            }
            else
            {
                _provisionadoSiafi = new ProvisionadoSiafi();
            }
            Anthem.AnthemClientMethods.Redirect("frmProvisionadoSiafiPesquisa.aspx", btnVoltar);
            
        }
    }

	private void FillPage()
	{
        Util.FillDropDownList(ddlNaturezaDespesa, NaturezaDespesa.List(), ESCOLHA_OPCAO);
        Util.FillDropDownList(ddlProjeto, Projeto.List(), ESCOLHA_OPCAO);
        Util.FillDropDownList(ddlAno, DateTimeManager.Anos(2008, DateTime.Today.Year + 1), ESCOLHA_OPCAO);
        
	    ddlAno.SelectedValue = DateTime.Today.Year.ToString();
	}

    private void PopulateFields()
    {
        ddlNaturezaDespesa.SelectedValue = ObjectReader.ReadID(_provisionadoSiafi.NaturezaDespesa);
        ddlProjeto.SelectedValue = ObjectReader.ReadID(_provisionadoSiafi.Projeto);
        ddlAno.SelectedValue = _provisionadoSiafi.Ano.ToString();
        txtCodigoSiafi.Text = _provisionadoSiafi.CodigoSiafi;
    }

    private void ClearFields()
    {   
        txtCodigoSiafi.Text = "";
        
        txtCodigoSiafi.UpdateAfterCallBack = true;
    }

    private void FillObject()
    {
        _provisionadoSiafi.Ano = PageReader.ReadInt(ddlAno);
        _provisionadoSiafi.NaturezaDespesa = NaturezaDespesa.Get(Convert.ToInt32(ddlNaturezaDespesa.SelectedValue));
        _provisionadoSiafi.Projeto = Projeto.Get(Convert.ToInt32(ddlProjeto.SelectedValue));
        _provisionadoSiafi.CodigoSiafi = txtCodigoSiafi.Text;
    }
    #endregion

    #region Events 
    void btnSalvar_Click(object sender, EventArgs e)
    {
        FillObject();
        _provisionadoSiafi.Save();	
		ShowSuccessMessage();
    }
	
    void btnNovo_Click(object sender, EventArgs e)
    {
        ClearFields();
        _provisionadoSiafi = new ProvisionadoSiafi();
    }
    #endregion
   
}
