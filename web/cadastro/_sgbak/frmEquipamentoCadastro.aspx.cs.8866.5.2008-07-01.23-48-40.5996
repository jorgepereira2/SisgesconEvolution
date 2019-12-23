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

public partial class frmEquipamentoCadastro : MarinhaPageBase
{

    #region Private Member
    [TransientPageState]
    protected Equipamento _equipamento;

    #endregion 

    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.btnSalvar.Click += new EventHandler(btnSalvar_Click);
        this.btnNovo.Click += new EventHandler(btnNovo_Click);

        ddlTipoEquipamento.SelectedIndexChanged += delegate { TipoEquipamentoChanged(); };
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
        	FillPage();
            if (Request["ID_Equipamento"] != null)
            {
                _equipamento = Equipamento.Get(Convert.ToInt32(Request["ID_Equipamento"]));
                PopulateFields();
                RegisterDelete();
            }
            else
            {
                _equipamento = new Equipamento();
            }

            if (Request["popup"] == "true")
            {
                btnVoltar.Text = "Fechar";
                btnVoltar.Attributes.Add("onclick", "window.close();");
            }
            else
                Anthem.AnthemClientMethods.Redirect("frmEquipamentoPesquisa.aspx", btnVoltar);
         
         RegisterDeleteScript();   
        }
    }

	private void FillPage()
	{
		Util.FillDropDownList(ddlTipoEquipamento, TipoEquipamento.List(), ESCOLHA_OPCAO);
        Util.FillDropDownList(ddlTipoOperativo, typeof(TipoOperativo), ESCOLHA_OPCAO);
		Util.InsertDefaultItem(ddlSubTipoEquipamento, ESCOLHA_OPCAO);
	}

    private void PopulateFields()
    {
        ddlTipoEquipamento.SelectedValue = _equipamento.TipoEquipamento.ID.ToString();
        TipoEquipamentoChanged();
        ddlSubTipoEquipamento.SelectedValue = ObjectReader.ReadID(_equipamento.SubTipoEquipamento);
        txtDescricao.Text = _equipamento.Descricao;
        txtCodeq.Text = _equipamento.Codeq;
        txtDescricaoCodeq.Text = _equipamento.DescricaoCodeq;
        chkFlagAtivo.Checked = _equipamento.FlagAtivo;
        ddlTipoOperativo.SelectedValue = Convert.ToInt32(_equipamento.TipoOperativo).ToString();

    }

    private void ClearFields()
    {
        ddlSubTipoEquipamento.SelectedIndex = -1;
        ddlTipoOperativo.SelectedIndex = -1;
        txtDescricao.Text = "";
        txtCodeq.Text = "";
        txtDescricaoCodeq.Text = "";
        chkFlagAtivo.Checked = true;

        ddlSubTipoEquipamento.UpdateAfterCallBack = true;
        ddlTipoOperativo.UpdateAfterCallBack = true;
        txtDescricao.UpdateAfterCallBack = true;
        txtCodeq.UpdateAfterCallBack = true;
        txtDescricaoCodeq.UpdateAfterCallBack = true;
        chkFlagAtivo.UpdateAfterCallBack = true;

    }

    private void FillObject()
    {
        _equipamento.SubTipoEquipamento = SubTipoEquipamento.Get(Convert.ToInt32(ddlSubTipoEquipamento.SelectedValue));
        _equipamento.Descricao = PageReader.ReadString(txtDescricao);
        _equipamento.Codeq = PageReader.ReadString(txtCodeq);
        _equipamento.DescricaoCodeq = PageReader.ReadString(txtDescricaoCodeq);
        _equipamento.FlagAtivo = chkFlagAtivo.Checked;
        _equipamento.TipoOperativo = (TipoOperativo) Convert.ToInt32(ddlTipoOperativo.SelectedValue);
    }
    #endregion

    #region Events 
    void btnSalvar_Click(object sender, EventArgs e)
    {
        FillObject();
        _equipamento.Save();	
		ShowSuccessMessage();
        RegisterDelete();
    }
	
    void btnNovo_Click(object sender, EventArgs e)
    {
        ClearFields();
        _equipamento = new Equipamento();
    }

    private void TipoEquipamentoChanged()
    {
        if (ddlTipoEquipamento.SelectedValue != "0")
        {
            Util.FillDropDownList(ddlSubTipoEquipamento, SubTipoEquipamento.List(Convert.ToInt32(ddlTipoEquipamento.SelectedValue)), ESCOLHA_OPCAO);
            ddlSubTipoEquipamento.UpdateAfterCallBack = true;
        }

    }

    #endregion

    [Anthem.Method]
    public void Excluir()
    {
        try
        {
            _equipamento.Delete();
            ShowSuccessMessage();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message);
        }
    }

    private void RegisterDelete()
    {
        Anthem.Manager.AddScriptAttribute(btnExcluir, "onclick", string.Format("javascript:Excluir({0});", _equipamento.ID));
        btnExcluir.UpdateAfterCallBack = true;
    }
}
