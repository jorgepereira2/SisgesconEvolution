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

public partial class frmQuadroMensagemCadastro : MarinhaPageBase
{

    #region Private Member
    [TransientPageState]
    protected QuadroMensagem _mensagem;

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
            if (Request["ID_QuadroMensagem"] != null)
            {
                _mensagem = QuadroMensagem.Get(Convert.ToInt32(Request["ID_QuadroMensagem"]));
                PopulateFields();
            }
            else
            {
                _mensagem = new QuadroMensagem();
            }
            Anthem.AnthemClientMethods.Redirect("frmQuadroMensagemPesquisa.aspx", btnVoltar);
            
        }
    }

	private void FillPage()
	{
		
	}

    private void PopulateFields()
    {
        txtMensagem.Text = _mensagem.Mensagem;
        txtDataInicio.Text = ObjectReader.ReadDate(_mensagem.DataInicio);
        txtDataFim.Text = ObjectReader.ReadDate(_mensagem.DataFim);
        txtAssinatura.Text = _mensagem.Assinatura;
        txtTitulo.Text = _mensagem.Titulo;

    }

    private void ClearFields()
    {
        txtMensagem.Text = "";
        txtDataInicio.Text = "";
        txtDataFim.Text = "";
        txtAssinatura.Text = "";
        txtTitulo.Text = "";

        txtMensagem.UpdateAfterCallBack = true;
        txtDataInicio.UpdateAfterCallBack = true;
        txtDataFim.UpdateAfterCallBack = true;
        txtAssinatura.UpdateAfterCallBack = true;
        txtTitulo.UpdateAfterCallBack = true;

    }

    private void FillObject()
    {
        _mensagem.Mensagem = PageReader.ReadString(txtMensagem);
        _mensagem.DataInicio = PageReader.ReadDate(txtDataInicio);
        _mensagem.DataFim = PageReader.ReadDate(txtDataFim);
        _mensagem.Servidor = Servidor.Get(this.ID_Servidor);
        _mensagem.Assinatura = PageReader.ReadString(txtAssinatura);
        _mensagem.Titulo = PageReader.ReadString(txtTitulo);
    }
    #endregion

    #region Events 
    void btnSalvar_Click(object sender, EventArgs e)
    {
        FillObject();
        _mensagem.Save();	
		ShowSuccessMessage();
    }
	
    void btnNovo_Click(object sender, EventArgs e)
    {
        ClearFields();
        _mensagem = new QuadroMensagem();
    }
    #endregion
   
}
