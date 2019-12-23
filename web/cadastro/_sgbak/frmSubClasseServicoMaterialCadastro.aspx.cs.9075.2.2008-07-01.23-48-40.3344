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

public partial class frmSubClasseServicoMaterialCadastro : MarinhaPageBase
{

    #region Private Member
    [TransientPageState]
    protected SubClasseServicoMaterial _subClasse;

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
            if (Request["id_subclasse"] != null)
            {
                _subClasse = SubClasseServicoMaterial.Get(Convert.ToInt32(Request["id_subclasse"]));
                PopulateFields();
                RegisterDelete();
            }
            else
            {
                _subClasse = new SubClasseServicoMaterial();
            }

            if (Request["popup"] == "true")
            {
                btnVoltar.Text = "Fechar";
                btnVoltar.Attributes.Add("onclick", "window.close();");
            }
            else
                Anthem.AnthemClientMethods.Redirect("frmSubClasseServicoMaterialPesquisa.aspx", btnVoltar);
         
         RegisterDeleteScript();   
        }
    }

	private void FillPage()
	{
        Util.FillDropDownList(ddlClasseServicoMaterial, ClasseServicoMaterial.List(), ESCOLHA_OPCAO);
	}

    private void PopulateFields()
    {
        txtDescricao.Text = _subClasse.Descricao;
        txtCodigo.Text = _subClasse.Codigo;
        chkFlagAtivo.Checked = _subClasse.FlagAtivo;
        ddlClasseServicoMaterial.SelectedValue = _subClasse.ClasseServicoMaterial.ID.ToString();
        txtEspecificacao.Text = _subClasse.Especificacao;
    }

    private void ClearFields()
    {
        ddlClasseServicoMaterial.SelectedIndex = -1;
        txtDescricao.Text = "";
        txtCodigo.Text = "";
        chkFlagAtivo.Checked = true;
        txtEspecificacao.Text = "";

        
        ddlClasseServicoMaterial.UpdateAfterCallBack = true;
        txtDescricao.UpdateAfterCallBack = true;
        txtCodigo.UpdateAfterCallBack = true;
        chkFlagAtivo.UpdateAfterCallBack = true;
        txtEspecificacao.UpdateAfterCallBack = true;
    }

    private void FillObject()
    {
        _subClasse.ClasseServicoMaterial = ClasseServicoMaterial.Get(Convert.ToInt32(ddlClasseServicoMaterial.SelectedValue));
        _subClasse.Descricao = PageReader.ReadString(txtDescricao);
        _subClasse.Codigo = PageReader.ReadString(txtCodigo);
        _subClasse.FlagAtivo = chkFlagAtivo.Checked;
        _subClasse.Especificacao = PageReader.ReadString(txtEspecificacao);
        
    }
    #endregion

    #region Events 
    void btnSalvar_Click(object sender, EventArgs e)
    {
        FillObject();
        _subClasse.Save();	
		ShowSuccessMessage();
        RegisterDelete();
    }
	
    void btnNovo_Click(object sender, EventArgs e)
    {
        ClearFields();
        _subClasse = new SubClasseServicoMaterial();
    }
    #endregion

    [Anthem.Method]
    public void Excluir()
    {
        try
        {
            _subClasse.Delete();
            ShowSuccessMessage();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message);
        }
    }

    private void RegisterDelete()
    {
        Anthem.Manager.AddScriptAttribute(btnExcluir, "onclick", string.Format("javascript:Excluir({0});", _subClasse.ID));
        btnExcluir.UpdateAfterCallBack = true;
    }
}
