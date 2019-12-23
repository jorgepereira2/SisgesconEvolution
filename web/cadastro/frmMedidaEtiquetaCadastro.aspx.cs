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

public partial class frmMedidaEtiquetaCadastro : MarinhaPageBase
{

    #region Private Member
    [TransientPageState]
    protected MedidaEtiqueta _medida;

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
            if (Request["ID_medida"] != null)
            {
                _medida = MedidaEtiqueta.Get(Convert.ToInt32(Request["ID_medida"]));
                PopulateFields();
            }
            else
            {
                _medida = new MedidaEtiqueta();
            }
            Anthem.AnthemClientMethods.Redirect("frmMedidaEtiquetaPesquisa.aspx", btnVoltar);
            RegisterDeleteScript();
            
        }
    }

	private void FillPage()
	{
		
	}

	private void PopulateFields()
    {
        txtNome.Text = _medida.Nome;
	    txtLinhas.Text = _medida.Linhas.ToString();
        txtColunas.Text = _medida.Colunas.ToString();
	    txtAlturaPapel.Text = _medida.AlturaPapel.ToString("N2");
        txtLarguraPapel.Text = _medida.LarguraPapel.ToString("N2");
        txtMargemDireita.Text = _medida.MargemDireita.ToString("N2");
        txtMargemEsquerda.Text = _medida.MargemEsquerda.ToString("N2");
        txtMargemInferior.Text = _medida.MargemInferior.ToString("N2");
        txtMargemSuperior.Text = _medida.MargemSuperior.ToString("N2");
        txtSeparacaoHorizontal.Text = _medida.SeparacaoHorizontal.ToString("N2");
        txtSeparacaoVertical.Text = _medida.SeparacaoVertical.ToString("N2");
        txtAlturaConteudo.Text = _medida.AlturaConteudo.ToString("N2");
           
    }
		
    private void ClearFields()
    {
		txtNome.Text = "";
        txtLinhas.Text = "";
        txtColunas.Text = "";
        txtAlturaPapel.Text = "";
        txtLarguraPapel.Text = "";
        txtMargemDireita.Text = "";
        txtMargemEsquerda.Text = "";
        txtMargemInferior.Text = "";
        txtMargemSuperior.Text = "";
        txtSeparacaoHorizontal.Text = "";
        txtSeparacaoVertical.Text = "";
        txtAlturaConteudo.Text = "";

        txtNome.UpdateAfterCallBack = true;
        txtLinhas.UpdateAfterCallBack = true;
        txtColunas.UpdateAfterCallBack = true;
        txtAlturaPapel.UpdateAfterCallBack = true;
        txtLarguraPapel.UpdateAfterCallBack = true;
        txtMargemDireita.UpdateAfterCallBack = true;
        txtMargemEsquerda.UpdateAfterCallBack = true;
        txtMargemInferior.UpdateAfterCallBack = true;
        txtMargemSuperior.UpdateAfterCallBack = true;
        txtSeparacaoHorizontal.UpdateAfterCallBack = true;
        txtSeparacaoVertical.UpdateAfterCallBack = true;
        txtAlturaConteudo.UpdateAfterCallBack = true;
    }

    private void FillObject()
    {
        _medida.Nome = txtNome.Text;
		_medida.Linhas = PageReader.ReadInt(txtLinhas);
        _medida.Colunas = PageReader.ReadInt(txtColunas);
        _medida.AlturaPapel = PageReader.ReadDecimal(txtAlturaPapel);
        _medida.LarguraPapel = PageReader.ReadDecimal(txtLarguraPapel);
        _medida.MargemDireita = PageReader.ReadDecimal(txtMargemDireita);
        _medida.MargemEsquerda = PageReader.ReadDecimal(txtMargemEsquerda);
        _medida.MargemInferior = PageReader.ReadDecimal(txtMargemInferior);
        _medida.MargemSuperior = PageReader.ReadDecimal(txtMargemSuperior);
        _medida.SeparacaoHorizontal = PageReader.ReadDecimal(txtSeparacaoHorizontal);
        _medida.SeparacaoVertical = PageReader.ReadDecimal(txtSeparacaoVertical);
        _medida.AlturaConteudo = PageReader.ReadDecimal(txtAlturaConteudo);
    }
    #endregion

    #region Events 
    void btnSalvar_Click(object sender, EventArgs e)
    {
        FillObject();
        _medida.Save();	
		ShowSuccessMessage();
    }
	
    void btnNovo_Click(object sender, EventArgs e)
    {
        ClearFields();
        _medida = new MedidaEtiqueta();
    }

    [Anthem.Method]
    public void Excluir()
    {
        try
        {
            _medida.Delete();
            ShowSuccessMessage();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message);
        }
    }
    #endregion
   
}
