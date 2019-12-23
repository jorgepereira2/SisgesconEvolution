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

public partial class frmServidorCadastro : MarinhaPageBase
{

    #region Private Member
    [TransientPageState]
    protected Servidor _servidor;

    #endregion 

    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.btnSalvar.Click += new EventHandler(btnSalvar_Click);
        this.btnNovo.Click += new EventHandler(btnNovo_Click);
        this.dgHistorico.EditCommand += new DataGridCommandEventHandler(dgHistorico_EditCommand);
        this.dgHistorico.DeleteCommand += new DataGridCommandEventHandler(dgHistorico_DeleteCommand);
        this.dgHistorico.UpdateCommand += new DataGridCommandEventHandler(dgHistorico_UpdateCommand);
        this.dgHistorico.ItemCommand += new DataGridCommandEventHandler(dgHistorico_ItemCommand);
        this.dgHistorico.ItemDataBound += new DataGridItemEventHandler(dgHistorico_ItemDataBound);
        this.dgHistorico.CancelCommand += new DataGridCommandEventHandler(dgHistorico_CancelCommand);
        btnNovoHistorico.Click += new EventHandler(btnNovoHistorico_Click);
    }

  

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
        	FillPage();
            if (Request["ID_Servidor"] != null)
            {
                _servidor = Servidor.Get(Convert.ToInt32(Request["ID_Servidor"]));
                PopulateFields();
                BindHistorico();
            }
            else
            {
                _servidor = new Servidor();
            }
            Anthem.AnthemClientMethods.Redirect("frmServidorPesquisa.aspx", btnVoltar);
            RegisterDeleteScript();
            txtDataSaida.Attributes.Add("onblur", "if(this.value != '')alert('Ao definir uma data de saída o servidor será desativado.');");
        }
    }

	private void FillPage()
	{
		Util.FillDropDownList(ddlCelula, Celula.List(), ESCOLHA_OPCAO);
		Util.FillDropDownList(ddlTipoServidor, typeof(TipoServidor), ESCOLHA_OPCAO);
        Util.FillDropDownList(ddlFuncaoServidor, typeof(FuncaoServidor), ESCOLHA_OPCAO);
	}

	private void PopulateFields()
    {
        txtNome.Text = _servidor.NomeCompleto;		
		txtEmail.Text = _servidor.Email;
		txtDataSaida.Text = ObjectReader.ReadDate(_servidor.DataSaida);
    	txtNomeGuerra.Text = _servidor.NomeGuerra;
    	txtGraduacao.Text = _servidor.Graduacao;
	    txtTelefone.Text = _servidor.Telefone;
	    txtCelular.Text = _servidor.Celular;
	    txtNIP.Text = _servidor.NIP;
		ddlTipoServidor.SelectedValue = Convert.ToInt32(_servidor.TipoServidor).ToString();
        ddlFuncaoServidor.SelectedValue = Convert.ToInt32(_servidor.Funcao).ToString();
		ddlCelula.SelectedValue = _servidor.Celula.ID.ToString();
	    chkFlagFazAtividadeDireta.Checked = _servidor.FlagFazAtividadeDireta;
	    txtDiscriminacaoFuncao.Text = _servidor.DiscriminacaoFuncao;
     
    }
		
    private void ClearFields()
    {
		txtNome.Text = "";		
		txtEmail.Text = "";
		txtDataSaida.Text = "";
		txtNomeGuerra.Text = "";
		txtGraduacao.Text = "";
        txtTelefone.Text = "";
        txtCelular.Text = "";
        txtNIP.Text = "";
        txtDiscriminacaoFuncao.Text = "";
        ddlFuncaoServidor.SelectedIndex = -1;
        chkFlagFazAtividadeDireta.Checked = false;

        txtNome.UpdateAfterCallBack = true;		
		txtEmail.UpdateAfterCallBack = true;
		txtDataSaida.UpdateAfterCallBack = true;
		txtNomeGuerra.UpdateAfterCallBack = true;
        txtGraduacao.UpdateAfterCallBack = true;
        txtTelefone.UpdateAfterCallBack = true;
        txtCelular.UpdateAfterCallBack = true;
        txtNIP.UpdateAfterCallBack = true;
        ddlFuncaoServidor.UpdateAfterCallBack = true;
        chkFlagFazAtividadeDireta.UpdateAfterCallBack = true;
        txtDiscriminacaoFuncao.UpdateAfterCallBack = true;
    }

    private void FillObject()
    {
        _servidor.NomeCompleto = txtNome.Text;
		_servidor.Email = PageReader.ReadString(txtEmail);
		_servidor.DataSaida = PageReader.ReadNullableDate(txtDataSaida);
    	_servidor.Celula = Celula.Get(Convert.ToInt32(ddlCelula.SelectedValue));
    	_servidor.NomeGuerra = txtNomeGuerra.Text;
    	_servidor.Graduacao = txtGraduacao.Text;
        _servidor.DiscriminacaoFuncao = txtDiscriminacaoFuncao.Text;
    	_servidor.TipoServidor = (TipoServidor) Convert.ToInt32(ddlTipoServidor.SelectedValue);
        _servidor.Funcao = (FuncaoServidor)Convert.ToInt32(ddlFuncaoServidor.SelectedValue);
        _servidor.Celular = PageReader.ReadString(txtCelular);
        _servidor.Telefone = PageReader.ReadString(txtTelefone);
        _servidor.NIP = PageReader.ReadString(txtNIP);
        _servidor.FlagFazAtividadeDireta = chkFlagFazAtividadeDireta.Checked;
    }

    #endregion

    #region Historico
    private void BindHistorico()
    {
        dgHistorico.DataSource = _servidor.Historicos;
        dgHistorico.DataKeyField = "ID";
        dgHistorico.DataBind();

        dgHistorico.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();
    }

    void dgHistorico_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Insert")
        {
            try
            {
                Anthem.TextBox txtDataHistorico = (Anthem.TextBox)e.Item.FindControl("txtDataHistorico");
                Anthem.CheckBox chkFazAtividadeDiretaHistorico = (Anthem.CheckBox)e.Item.FindControl("chkFazAtividadeDiretaHistorico");
                Anthem.DropDownList ddlCelulaHistorico = (Anthem.DropDownList)e.Item.FindControl("ddlCelulaHistorico");

                ServidorHistorico historico = new ServidorHistorico();
                historico.Servidor = _servidor;
                historico.FlagFazAtividadeDireta = chkFazAtividadeDiretaHistorico.Checked;
                historico.Data = Convert.ToDateTime(txtDataHistorico.Text);
                historico.Celula = Celula.Get(Convert.ToInt32(ddlCelulaHistorico.SelectedValue));
                
                historico.Save();
                _servidor.Historicos.Add(historico);

                AtualizaAtividade();

                BindHistorico();
                dgHistorico.ShowFooter = false;
            }
            catch (Exception ex)
            {
                Anthem.AnthemClientMethods.Alert(ex.Message, this);
            }
        }
    }

    void dgHistorico_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        Anthem.TextBox txtDataHistorico = (Anthem.TextBox)e.Item.FindControl("txtDataHistorico");
        Anthem.CheckBox chkFazAtividadeDiretaHistorico = (Anthem.CheckBox)e.Item.FindControl("chkFazAtividadeDiretaHistorico");
        Anthem.DropDownList ddlCelulaHistorico = (Anthem.DropDownList)e.Item.FindControl("ddlCelulaHistorico");

        ServidorHistorico historico = _servidor.Historicos.Find(Convert.ToInt32(dgHistorico.DataKeys[e.Item.ItemIndex]));
        historico.FlagFazAtividadeDireta = chkFazAtividadeDiretaHistorico.Checked;
        historico.Data = Convert.ToDateTime(txtDataHistorico.Text);
        historico.Celula = Celula.Get(Convert.ToInt32(ddlCelulaHistorico.SelectedValue));

        historico.Save();

        AtualizaAtividade();

        dgHistorico.EditItemIndex = -1;
        BindHistorico();
    }

    void AtualizaAtividade()
    {
        DateTime ultimaData = DateTime.MinValue;
        bool flagFazAtividadeDireta = false;
        foreach (ServidorHistorico historico in _servidor.Historicos)
        {
            if (historico.Data > ultimaData)
            {
                flagFazAtividadeDireta = historico.FlagFazAtividadeDireta;
                ultimaData = historico.Data;
            }
        }
        if(ultimaData != DateTime.MinValue && flagFazAtividadeDireta != _servidor.FlagFazAtividadeDireta)
        {
            _servidor.FlagFazAtividadeDireta = flagFazAtividadeDireta;
            _servidor.Save();
            chkFlagFazAtividadeDireta.Checked = _servidor.FlagFazAtividadeDireta;
            chkFlagFazAtividadeDireta.UpdateAfterCallBack = true;
        }
    }

    void dgHistorico_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        ServidorHistorico historico = _servidor.Historicos.Find(Convert.ToInt32(dgHistorico.DataKeys[e.Item.ItemIndex]));
        historico.Delete();
        _servidor.Historicos.Remove(historico);
        BindHistorico();
    }

    void dgHistorico_EditCommand(object source, DataGridCommandEventArgs e)
    {
        dgHistorico.EditItemIndex = e.Item.ItemIndex;
        BindHistorico();
    }

    void btnNovoHistorico_Click(object sender, EventArgs e)
    {
        dgHistorico.ShowFooter = true;
        BindHistorico();
        dgHistorico.UpdateAfterCallBack = true;
    }


    void dgHistorico_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if(e.Item.ItemType == ListItemType.EditItem || e.Item.ItemType == ListItemType.Footer)
        {
            Anthem.DropDownList ddlCelulaHistorico = (Anthem.DropDownList)e.Item.FindControl("ddlCelulaHistorico");
            Util.FillDropDownList(ddlCelulaHistorico, Celula.List(), ESCOLHA_OPCAO);

            if(e.Item.ItemType == ListItemType.EditItem)
            {
                ServidorHistorico historico = _servidor.Historicos.Find(Convert.ToInt32(dgHistorico.DataKeys[e.Item.ItemIndex]));
                ddlCelulaHistorico.SelectedValue = historico.Celula.ID.ToString();
            }
        }
    }

    void dgHistorico_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dgHistorico.EditItemIndex = -1;
        dgHistorico.ShowFooter = false;
        BindHistorico();
    }
    #endregion

    #region Events
    void btnSalvar_Click(object sender, EventArgs e)
    {
        FillObject();
        _servidor.Save();	
		ShowSuccessMessage();
    }
	
    void btnNovo_Click(object sender, EventArgs e)
    {
        ClearFields();
        _servidor = new Servidor();
    }

    [Anthem.Method]
    public void Excluir()
    {
        try
        {
            _servidor.Delete();
            ShowSuccessMessage();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message);
        }
    }
    #endregion
   
}
