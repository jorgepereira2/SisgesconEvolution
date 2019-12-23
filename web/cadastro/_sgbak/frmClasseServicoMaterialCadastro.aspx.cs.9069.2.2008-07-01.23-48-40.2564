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

public partial class frmClasseServicoMaterialCadastro : MarinhaPageBase
{

    #region Private Member
    [TransientPageState]
    protected ClasseServicoMaterial _classe;

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
            if (Request["ID_classe"] != null)
            {
                _classe = ClasseServicoMaterial.Get(Convert.ToInt32(Request["ID_classe"]));
                PopulateFields();
                RegisterDelete();
            }
            else
            {
                _classe = new ClasseServicoMaterial();
            }

            if (Request["popup"] == "true")
            {
                btnVoltar.Text = "Fechar";
                btnVoltar.Attributes.Add("onclick", "window.close();");
            }
            else
                Anthem.AnthemClientMethods.Redirect("frmClasseServicoMaterialPesquisa.aspx", btnVoltar);
         
         RegisterDeleteScript();   
        }
    }

	private void FillPage()
	{
        
	}

    private void PopulateFields()
    {
        txtDescricao.Text = _classe.Descricao;
        txtCodigo.Text = _classe.Codigo;
        chkFlagAtivo.Checked = _classe.FlagAtivo;
    }

    private void ClearFields()
    {
        txtDescricao.Text = "";
        txtCodigo.Text = "";
        chkFlagAtivo.Checked = true;

        
        txtDescricao.UpdateAfterCallBack = true;
        txtCodigo.UpdateAfterCallBack = true;
        chkFlagAtivo.UpdateAfterCallBack = true;

    }

    private void FillObject()
    {
        _classe.Descricao = PageReader.ReadString(txtDescricao);
        _classe.Codigo = PageReader.ReadString(txtCodigo);
        _classe.FlagAtivo = chkFlagAtivo.Checked;
        
    }
    #endregion

    #region Events 
    void btnSalvar_Click(object sender, EventArgs e)
    {
        FillObject();
        _classe.Save();	
		ShowSuccessMessage();
        RegisterDelete();
    }
	
    void btnNovo_Click(object sender, EventArgs e)
    {
        ClearFields();
        _classe = new ClasseServicoMaterial();
    }
    #endregion

    [Anthem.Method]
    public void Excluir()
    {
        try
        {
            _classe.Delete();
            ShowSuccessMessage();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message);
        }
    }

    private void RegisterDelete()
    {
        Anthem.Manager.AddScriptAttribute(btnExcluir, "onclick", string.Format("javascript:Excluir({0});", _classe.ID));
        btnExcluir.UpdateAfterCallBack = true;
    }
}
