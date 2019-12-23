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

public partial class frmCotaMaterialCadastro : MarinhaPageBase
{

    #region Private Member
    [TransientPageState]
    protected CotaMaterial _cota;

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
            if (Request["ID_Cota"] != null)
            {
                _cota = CotaMaterial.Get(Convert.ToInt32(Request["ID_Cota"]));
                PopulateFields();
            }
            else
            {
                _cota = new CotaMaterial();
            }
            Anthem.AnthemClientMethods.Redirect("frmCotaMaterialPesquisa.aspx", btnVoltar);
            
        }
    }

	private void FillPage()
	{
        Util.FillDropDownList(ddlCelula, Celula.List(TipoCelula.Departamento, TipoCelula.Divisao), ESCOLHA_OPCAO);
        Util.FillDropDownList(ddlAno, DateTimeManager.Anos(2008, DateTime.Today.Year + 1), ESCOLHA_OPCAO);
        Util.FillDropDownList(ddlMes, DateTimeManager.Meses(), "Todos os meses do ano");

	    ddlAno.SelectedValue = DateTime.Today.Year.ToString();
	}

    private void PopulateFields()
    {
        ddlCelula.SelectedValue = ObjectReader.ReadID(_cota.Celula);
        ucMaterial.SelectedValue = ObjectReader.ReadID(_cota.Material);
        ucMaterial.Text = _cota.Material.Descricao;
        ddlAno.SelectedValue = _cota.Ano.ToString();
        ddlMes.SelectedValue = _cota.Mes.ToString();
        txtQuantidade.Text = _cota.Quantidade.ToString("N0");
    }

    private void ClearFields()
    {   
        ucMaterial.Reset();
        txtQuantidade.Text = "";

        txtQuantidade.UpdateAfterCallBack = true;

    }

    private void FillObject()
    {
        _cota.Ano = PageReader.ReadInt(ddlAno);
        _cota.Quantidade = Convert.ToDecimal(txtQuantidade.Text);
        _cota.Celula = Celula.Get(Convert.ToInt32(ddlCelula.SelectedValue));
        _cota.Material = ServicoMaterial.Get(Convert.ToInt32(ucMaterial.SelectedValue));
        _cota.Mes = PageReader.ReadInt(ddlMes);
    }
    #endregion

    #region Events 
    void btnSalvar_Click(object sender, EventArgs e)
    {
        FillObject();
        _cota.Save();	
		ShowSuccessMessage();
    }
	
    void btnNovo_Click(object sender, EventArgs e)
    {
        ClearFields();
        _cota = new CotaMaterial();
    }
    #endregion
   
}
