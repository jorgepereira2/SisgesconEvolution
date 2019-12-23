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

public partial class frmServidorDetalheCadastro : MarinhaPageBase
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
      

        this.dgDependentes.EditCommand += new DataGridCommandEventHandler(dgDependentes_EditCommand);
        this.dgDependentes.DeleteCommand += new DataGridCommandEventHandler(dgDependentes_DeleteCommand);
        this.dgDependentes.UpdateCommand += new DataGridCommandEventHandler(dgDependentes_UpdateCommand);
        this.dgDependentes.ItemCommand += new DataGridCommandEventHandler(dgDependentes_ItemCommand);
        this.dgDependentes.CancelCommand += new DataGridCommandEventHandler(dgDependentes_CancelCommand);
        btnNovoDependente.Click += new EventHandler(btnNovoDependente_Click);

        this.dgCondecoracoes.EditCommand += new DataGridCommandEventHandler(dgCondecoracoes_EditCommand);
        this.dgCondecoracoes.DeleteCommand += new DataGridCommandEventHandler(dgCondecoracoes_DeleteCommand);
        this.dgCondecoracoes.UpdateCommand += new DataGridCommandEventHandler(dgCondecoracoes_UpdateCommand);
        this.dgCondecoracoes.ItemCommand += new DataGridCommandEventHandler(dgCondecoracoes_ItemCommand);
        this.dgCondecoracoes.CancelCommand += new DataGridCommandEventHandler(dgCondecoracoes_CancelCommand);
        btnNovaCondecoracao.Click += new EventHandler(btnNovaCondecoracao_Click);

        this.dgCursosMilitares.EditCommand += new DataGridCommandEventHandler(dgCursosMilitares_EditCommand);
        this.dgCursosMilitares.DeleteCommand += new DataGridCommandEventHandler(dgCursosMilitares_DeleteCommand);
        this.dgCursosMilitares.UpdateCommand += new DataGridCommandEventHandler(dgCursosMilitares_UpdateCommand);
        this.dgCursosMilitares.ItemCommand += new DataGridCommandEventHandler(dgCursosMilitares_ItemCommand);
        this.dgCursosMilitares.CancelCommand += new DataGridCommandEventHandler(dgCursosMilitares_CancelCommand);
        btnNovoCursoMilitar.Click += new EventHandler(btnNovoCursoMilitar_Click);

        ddlEstado.SelectedIndexChanged += delegate { EstadoChanged(); };
        ddlEstadoContato.SelectedIndexChanged += delegate { EstadoContatoChanged(); };

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
                BindDependentes();
                BindCondecoracoes();
                BindCursosMilitares();
            }
           
            Anthem.AnthemClientMethods.Redirect("frmServidorPesquisa.aspx", btnVoltar);
            
        }
    }

	private void FillPage()
	{
		Util.FillDropDownList(ddlTipoSanguineo, typeof(TipoSanguineo));
        Util.FillDropDownList(ddlEstadoCivil, typeof(EstadoCivil));
        Util.FillDropDownList(ddlEstado, Estado.List(), ESCOLHA_OPCAO);
        Util.InsertDefaultItem(ddlMunicipio, ESCOLHA_OPCAO);
        Util.FillDropDownList(ddlEstadoContato, Estado.List(), ESCOLHA_OPCAO);
        Util.InsertDefaultItem(ddlMunicipioContato, ESCOLHA_OPCAO);
	}

	private void PopulateFields()
    {
        //detalhes
	    txtIdentidade.Text = _servidor.Identidade;
        txtOrgaoEmissor.Text = _servidor.OrgaoEmissor;
        txtCPF.Text = _servidor.CPF;
        txtPASEP.Text = _servidor.PASEP;
        txtNumeroTituloEleitoral.Text = _servidor.NumeroTituloEleitoral;
        txtSecaoTituloEleitoral.Text = _servidor.SecaoTituloEleitoral;
        txtZonaTituloEleitoral.Text = _servidor.ZonaTituloEleitoral;
        txtNumeroCarteiraMotorista.Text = _servidor.NumeroCarteiraMotorista;
        chkDoadorOrgaos.Checked = _servidor.DoadorOrgaos;
	    ddlTipoSanguineo.SelectedValue = _servidor.TipoSanguineo.GetHashCode().ToString();
        txtFatorRH.Text = _servidor.FatorRH;
        txtDataNascimento.Text = ObjectReader.ReadDate(_servidor.DataNascimento);
        txtNaturalidade.Text = _servidor.Naturalidade;
        txtNomePai.Text = _servidor.NomePai;
        txtNomeMae.Text = _servidor.NomeMae;
        txtEndereco.Text = _servidor.Endereco;
        txtBairro.Text = _servidor.Bairro;
	    ddlEstado.SelectedValue = ObjectReader.ReadID(_servidor.Estado);
        if(ddlEstado.SelectedValue != "0")
        {
            EstadoChanged();
            ddlMunicipio.SelectedValue = ObjectReader.ReadID(_servidor.Municipio);
        }
        txtCEP.Text = _servidor.CEP;
        txtTelefoneResidencial.Text = _servidor.TelefoneResidencial;

        txtNomePessoaContato.Text = _servidor.NomePessoaContato;
        txtEnderecoContato.Text = _servidor.EnderecoContato;
        txtBairroContato.Text = _servidor.BairroContato;
        txtCEPContato.Text = _servidor.CEPContato;
        txtTelefoneContato.Text = _servidor.TelefoneContato;
        txtCelularContato.Text = _servidor.CelularContato;
        ddlEstadoContato.SelectedValue = ObjectReader.ReadID(_servidor.EstadoContato);
        if (ddlEstadoContato.SelectedValue != "0")
        {
            EstadoContatoChanged();
            ddlMunicipioContato.SelectedValue = ObjectReader.ReadID(_servidor.MunicipioContato);
        }
        ddlEstadoCivil.SelectedValue = _servidor.EstadoCivil.GetHashCode().ToString();
        txtNomeConjuge.Text = _servidor.NomeConjuge;
        txtDataIncorporacaoMB.Text = ObjectReader.ReadDate(_servidor.DataIncorporacaoMB);
        txtDataultimaPromocao.Text = ObjectReader.ReadDate(_servidor.DataUltimaPromocao);
        txtDataApresentacao.Text = ObjectReader.ReadDate(_servidor.DataApresentacao);
	    chkLinguaEstrangeira.Checked = _servidor.LinguaEstrangeira;
        txtDescricaoLinguaEstrangeira.Text = _servidor.DescricaoLinguaEstrangeira;
        chkPraticaEsporte.Checked = _servidor.PraticaEsporte;
        txtEspecificacaoEsporte.Text = _servidor.EspecificacaoEsporte;
        txtReligiao.Text = _servidor.Religiao;
        txtOutrasHabilidades.Text = _servidor.OutrasHabilidades;
        chkDesejaHorasFunebres.Checked = _servidor.DesejaHorasFunebres;

        ucOMIncorporacao.SelectedValue = ObjectReader.ReadID(_servidor.OMIncorporacao);
	    ucOMIncorporacao.Text = _servidor.OMIncorporacao == null ? "" : _servidor.OMIncorporacao.DescricaoCompleta;
        ucOMOrigem.SelectedValue = ObjectReader.ReadID(_servidor.OMOrigem);
        ucOMOrigem.Text = _servidor.OMOrigem == null ? "" : _servidor.OMOrigem.DescricaoCompleta;

    }
		
    private void ClearFields()
    {
        txtIdentidade.Text = "";
        txtOrgaoEmissor.Text = "";
        txtCPF.Text = "";
        txtPASEP.Text = "";
        txtNumeroTituloEleitoral.Text = "";
        txtSecaoTituloEleitoral.Text = "";
        txtZonaTituloEleitoral.Text = "";
        txtNumeroCarteiraMotorista.Text = "";
        chkDoadorOrgaos.Checked = false;
        ddlTipoSanguineo.SelectedValue = "0";
        txtFatorRH.Text = _servidor.FatorRH;
        txtDataNascimento.Text = "";
        txtNaturalidade.Text = "";
        txtNomePai.Text = "";
        txtNomeMae.Text = "";
        txtEndereco.Text = "";
        txtBairro.Text = "";
        ddlEstado.SelectedValue = "0";
        ddlMunicipio.SelectedValue = "0";
        txtCEP.Text = "";
        txtTelefoneResidencial.Text = "";
        txtNomePessoaContato.Text = "";
        txtEnderecoContato.Text = "";
        txtBairroContato.Text = "";
        txtCEPContato.Text = "";
        txtTelefoneContato.Text = "";
        txtCelularContato.Text = "";
        ddlEstadoContato.SelectedValue = "0";
        ddlMunicipioContato.SelectedValue = "0";
        ddlEstadoCivil.SelectedValue = "0";
        txtNomeConjuge.Text = "";
        txtDataIncorporacaoMB.Text = "";
        ucOMIncorporacao.SelectedValue = "";
        ucOMOrigem.SelectedValue = "";
        txtDataultimaPromocao.Text = "";
        txtDataApresentacao.Text = "";
        chkLinguaEstrangeira.Checked = false;
        txtDescricaoLinguaEstrangeira.Text = "";
        chkPraticaEsporte.Checked = false;
        txtEspecificacaoEsporte.Text = "";
        txtReligiao.Text = "";
        txtOutrasHabilidades.Text = "";
        chkDesejaHorasFunebres.Checked = false;

        txtIdentidade.UpdateAfterCallBack = true;
        txtOrgaoEmissor.UpdateAfterCallBack = true;
        txtCPF.UpdateAfterCallBack = true;
        txtPASEP.UpdateAfterCallBack = true;
        txtNumeroTituloEleitoral.UpdateAfterCallBack = true;
        txtSecaoTituloEleitoral.UpdateAfterCallBack = true;
        txtZonaTituloEleitoral.UpdateAfterCallBack = true;
        txtNumeroCarteiraMotorista.UpdateAfterCallBack = true;
        chkDoadorOrgaos.UpdateAfterCallBack = true;
        ddlTipoSanguineo.UpdateAfterCallBack = true;
        txtFatorRH.UpdateAfterCallBack = true;
        txtDataNascimento.UpdateAfterCallBack = true;
        txtNaturalidade.UpdateAfterCallBack = true;
        txtNomePai.UpdateAfterCallBack = true;
        txtNomeMae.UpdateAfterCallBack = true;
        txtEndereco.UpdateAfterCallBack = true;
        txtBairro.UpdateAfterCallBack = true;
        ddlEstado.UpdateAfterCallBack = true;
        ddlMunicipio.UpdateAfterCallBack = true;
        txtCEP.UpdateAfterCallBack = true;
        txtTelefoneResidencial.UpdateAfterCallBack = true;
        txtNomePessoaContato.UpdateAfterCallBack = true;
        txtEnderecoContato.UpdateAfterCallBack = true;
        txtBairroContato.UpdateAfterCallBack = true;
        txtCEPContato.UpdateAfterCallBack = true;
        txtTelefoneContato.UpdateAfterCallBack = true;
        txtCelularContato.UpdateAfterCallBack = true;
        ddlEstadoContato.UpdateAfterCallBack = true;
        ddlMunicipioContato.UpdateAfterCallBack = true;
        ddlEstadoCivil.UpdateAfterCallBack = true;
        txtNomeConjuge.UpdateAfterCallBack = true;
        txtDataIncorporacaoMB.UpdateAfterCallBack = true;
        ucOMIncorporacao.UpdateAfterCallBack = true;
        ucOMOrigem.UpdateAfterCallBack = true;
        txtDataultimaPromocao.UpdateAfterCallBack = true;
        txtDataApresentacao.UpdateAfterCallBack = true;
        chkLinguaEstrangeira.UpdateAfterCallBack = true;
        txtDescricaoLinguaEstrangeira.UpdateAfterCallBack = true;
        chkPraticaEsporte.UpdateAfterCallBack = true;
        txtEspecificacaoEsporte.UpdateAfterCallBack = true;
        txtReligiao.UpdateAfterCallBack = true;
        txtOutrasHabilidades.UpdateAfterCallBack = true;
        chkDesejaHorasFunebres.UpdateAfterCallBack = true;
    }

    private void FillObject()
    {
        _servidor.Identidade = PageReader.ReadString(txtIdentidade);
        _servidor.OrgaoEmissor = PageReader.ReadString(txtOrgaoEmissor);
        _servidor.CPF = PageReader.ReadString(txtCPF);
        _servidor.PASEP = PageReader.ReadString(txtPASEP);
        _servidor.NumeroTituloEleitoral = PageReader.ReadString(txtNumeroTituloEleitoral);
        _servidor.SecaoTituloEleitoral = PageReader.ReadString(txtSecaoTituloEleitoral);
        _servidor.ZonaTituloEleitoral = PageReader.ReadString(txtZonaTituloEleitoral);
        _servidor.NumeroCarteiraMotorista = PageReader.ReadString(txtNumeroCarteiraMotorista);
        _servidor.DoadorOrgaos = chkDoadorOrgaos.Checked;
        _servidor.TipoSanguineo = (TipoSanguineo)Convert.ToInt32(ddlTipoSanguineo.SelectedValue);
        _servidor.FatorRH = PageReader.ReadString(txtFatorRH);
        _servidor.DataNascimento = PageReader.ReadNullableDate(txtDataNascimento);
        _servidor.Naturalidade = PageReader.ReadString(txtNaturalidade);
        _servidor.NomePai = PageReader.ReadString(txtNomePai);
        _servidor.NomeMae = PageReader.ReadString(txtNomeMae);
        _servidor.Endereco = PageReader.ReadString(txtEndereco);
        _servidor.Bairro = PageReader.ReadString(txtBairro);
        _servidor.Estado = Estado.Get(Convert.ToInt32(ddlEstado.SelectedValue));
        _servidor.Municipio = Municipio.Get(Convert.ToInt32(ddlMunicipio.SelectedValue));
        _servidor.CEP = PageReader.ReadString(txtCEP);
        _servidor.TelefoneResidencial = PageReader.ReadString(txtTelefoneResidencial);
        _servidor.NomePessoaContato = PageReader.ReadString(txtNomePessoaContato);
        _servidor.EnderecoContato = PageReader.ReadString(txtEnderecoContato);
        _servidor.BairroContato = PageReader.ReadString(txtBairroContato);
        _servidor.EstadoContato = Estado.Get(Convert.ToInt32(ddlEstadoContato.SelectedValue));
        _servidor.MunicipioContato = Municipio.Get(Convert.ToInt32(ddlMunicipioContato.SelectedValue));
        _servidor.CEPContato = PageReader.ReadString(txtCEPContato);
        _servidor.TelefoneContato = PageReader.ReadString(txtTelefoneContato);
        _servidor.CelularContato = PageReader.ReadString(txtCelularContato);
        _servidor.EstadoCivil = (EstadoCivil)Convert.ToInt32(ddlEstadoCivil.SelectedValue);
        _servidor.NomeConjuge = PageReader.ReadString(txtNomeConjuge);
        _servidor.DataIncorporacaoMB = PageReader.ReadNullableDate(txtDataIncorporacaoMB);
        _servidor.OMIncorporacao = Cliente.Get(Convert.ToInt32(ucOMIncorporacao.SelectedValue));
        _servidor.OMOrigem= Cliente.Get(Convert.ToInt32(ucOMOrigem.SelectedValue));
        _servidor.DataUltimaPromocao = PageReader.ReadNullableDate(txtDataultimaPromocao);
        _servidor.DataApresentacao = PageReader.ReadNullableDate(txtDataApresentacao);
        _servidor.LinguaEstrangeira = chkLinguaEstrangeira.Checked;
        _servidor.DescricaoLinguaEstrangeira = PageReader.ReadString(txtDescricaoLinguaEstrangeira);
        _servidor.PraticaEsporte = chkPraticaEsporte.Checked;
        _servidor.EspecificacaoEsporte = PageReader.ReadString(txtEspecificacaoEsporte);
        _servidor.Religiao = PageReader.ReadString(txtReligiao);
        _servidor.OutrasHabilidades = PageReader.ReadString(txtOutrasHabilidades);
        chkDesejaHorasFunebres.Checked = _servidor.DesejaHorasFunebres;
    }
    #endregion

    #region Dependentes
    private void BindDependentes()
    {
        dgDependentes.DataSource = _servidor.Dependentes;
        dgDependentes.DataKeyField = "ID";
        dgDependentes.DataBind();

        dgDependentes.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();
    }

    void dgDependentes_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Insert")
        {
            try
            {
                Anthem.TextBox txtNomeDependente = (Anthem.TextBox)e.Item.FindControl("txtNomeDependente");
                Anthem.TextBox txtGrauParentesco = (Anthem.TextBox)e.Item.FindControl("txtGrauParentesco");
                
                ServidorDependente dependente = new ServidorDependente();
                dependente.Servidor = _servidor;
                dependente.Nome = txtNomeDependente.Text;
                dependente.GrauParentesco = txtGrauParentesco.Text;
                dependente.Save();
                _servidor.Dependentes.Add(dependente);

                BindDependentes();
                dgDependentes.ShowFooter = false;
            }
            catch (Exception ex)
            {
                Anthem.AnthemClientMethods.Alert(ex.Message, this);
            }
        }
    }

    void dgDependentes_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        Anthem.TextBox txtNomeDependente = (Anthem.TextBox)e.Item.FindControl("txtNomeDependente");
        Anthem.TextBox txtGrauParentesco = (Anthem.TextBox)e.Item.FindControl("txtGrauParentesco");

        ServidorDependente dependente = _servidor.Dependentes.Find(Convert.ToInt32(dgDependentes.DataKeys[e.Item.ItemIndex]));
        dependente.Nome = txtNomeDependente.Text;
        dependente.GrauParentesco = txtGrauParentesco.Text;
        dependente.Save();

        dgDependentes.EditItemIndex = -1;
        BindDependentes();
    }

   

    void dgDependentes_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        ServidorDependente dependente = _servidor.Dependentes.Find(Convert.ToInt32(dgDependentes.DataKeys[e.Item.ItemIndex]));
        dependente.Delete();
        _servidor.Dependentes.Remove(dependente);
        BindDependentes();
    }

    void dgDependentes_EditCommand(object source, DataGridCommandEventArgs e)
    {
        dgDependentes.EditItemIndex = e.Item.ItemIndex;
        BindDependentes();
    }

    void btnNovoDependente_Click(object sender, EventArgs e)
    {
        dgDependentes.ShowFooter = true;
        BindDependentes();
        dgDependentes.UpdateAfterCallBack = true;
    }

    
    void dgDependentes_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dgDependentes.EditItemIndex = -1;
        dgDependentes.ShowFooter = false;
        BindDependentes();
    }
    #endregion

    #region Condecoracoes
    private void BindCondecoracoes()
    {
        dgCondecoracoes.DataSource = _servidor.Condecoracoes;
        dgCondecoracoes.DataKeyField = "ID";
        dgCondecoracoes.DataBind();

        dgCondecoracoes.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();
    }

    void dgCondecoracoes_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Insert")
        {
            try
            {
                Anthem.TextBox txtDescricaoCondecoracao = (Anthem.TextBox)e.Item.FindControl("txtDescricaoCondecoracao");
                
                ServidorCondecoracao condecoracao = new ServidorCondecoracao();
                condecoracao.Servidor = _servidor;
                condecoracao.Descricao = txtDescricaoCondecoracao.Text;
                condecoracao.Save();
                _servidor.Condecoracoes.Add(condecoracao);

                BindCondecoracoes();
                dgCondecoracoes.ShowFooter = false;
            }
            catch (Exception ex)
            {
                Anthem.AnthemClientMethods.Alert(ex.Message, this);
            }
        }
    }

    void dgCondecoracoes_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        Anthem.TextBox txtDescricaoCondecoracao = (Anthem.TextBox)e.Item.FindControl("txtDescricaoCondecoracao");

        ServidorCondecoracao condecoracao = _servidor.Condecoracoes.Find(Convert.ToInt32(dgCondecoracoes.DataKeys[e.Item.ItemIndex]));
        condecoracao.Descricao = txtDescricaoCondecoracao.Text;
        condecoracao.Save();

        dgCondecoracoes.EditItemIndex = -1;
        BindCondecoracoes();
    }
    
    void dgCondecoracoes_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        ServidorCondecoracao condecoracao = _servidor.Condecoracoes.Find(Convert.ToInt32(dgCondecoracoes.DataKeys[e.Item.ItemIndex]));
        condecoracao.Delete();
        _servidor.Condecoracoes.Remove(condecoracao);
        BindCondecoracoes();
    }

    void dgCondecoracoes_EditCommand(object source, DataGridCommandEventArgs e)
    {
        dgCondecoracoes.EditItemIndex = e.Item.ItemIndex;
        BindCondecoracoes();
    }

    void btnNovaCondecoracao_Click(object sender, EventArgs e)
    {
        dgCondecoracoes.ShowFooter = true;
        BindCondecoracoes();
        dgCondecoracoes.UpdateAfterCallBack = true;
    }


    void dgCondecoracoes_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dgCondecoracoes.EditItemIndex = -1;
        dgCondecoracoes.ShowFooter = false;
        BindCondecoracoes();
    }
    #endregion

    #region Cursos Militares
    private void BindCursosMilitares()
    {
        dgCursosMilitares.DataSource = _servidor.CursosMilitares;
        dgCursosMilitares.DataKeyField = "ID";
        dgCursosMilitares.DataBind();

        dgCursosMilitares.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();
    }

    void dgCursosMilitares_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Insert")
        {
            try
            {
                Anthem.TextBox txtDescricaoCursoMilitar = (Anthem.TextBox)e.Item.FindControl("txtDescricaoCursoMilitar");

                ServidorCursoMilitar curso = new ServidorCursoMilitar();
                curso.Servidor = _servidor;
                curso.Descricao = txtDescricaoCursoMilitar.Text;
                curso.Save();
                _servidor.CursosMilitares.Add(curso);

                BindCursosMilitares();
                dgCursosMilitares.ShowFooter = false;
            }
            catch (Exception ex)
            {
                Anthem.AnthemClientMethods.Alert(ex.Message, this);
            }
        }
    }

    void dgCursosMilitares_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        Anthem.TextBox txtDescricaoCursoMilitar = (Anthem.TextBox)e.Item.FindControl("txtDescricaoCursoMilitar");

        ServidorCursoMilitar curso = _servidor.CursosMilitares.Find(Convert.ToInt32(dgCursosMilitares.DataKeys[e.Item.ItemIndex]));
        curso.Descricao = txtDescricaoCursoMilitar.Text;
        curso.Save();

        dgCursosMilitares.EditItemIndex = -1;
        BindCursosMilitares();
    }



    void dgCursosMilitares_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        ServidorCursoMilitar curso = _servidor.CursosMilitares.Find(Convert.ToInt32(dgCursosMilitares.DataKeys[e.Item.ItemIndex]));
        curso.Delete();
        _servidor.CursosMilitares.Remove(curso);
        BindCursosMilitares();
    }

    void dgCursosMilitares_EditCommand(object source, DataGridCommandEventArgs e)
    {
        dgCursosMilitares.EditItemIndex = e.Item.ItemIndex;
        BindCursosMilitares();
    }

    void btnNovoCursoMilitar_Click(object sender, EventArgs e)
    {
        dgCursosMilitares.ShowFooter = true;
        BindCursosMilitares();
        dgCursosMilitares.UpdateAfterCallBack = true;
    }


    void dgCursosMilitares_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dgCursosMilitares.EditItemIndex = -1;
        dgCursosMilitares.ShowFooter = false;
        BindCursosMilitares();
    }
    #endregion

    #region Events
    void btnSalvar_Click(object sender, EventArgs e)
    {
        FillObject();
        _servidor.Save();	
		ShowSuccessMessage();
    }
	
    private void EstadoChanged()
    {
        if (ddlEstado.SelectedValue != "0")
        {
            Util.FillDropDownList(ddlMunicipio, Municipio.List(Convert.ToInt32(ddlEstado.SelectedValue)), ESCOLHA_OPCAO);
            ddlMunicipio.UpdateAfterCallBack = true;
        }
    }
    private void EstadoContatoChanged()
    {
        if (ddlEstado.SelectedValue != "0")
        {
            Util.FillDropDownList(ddlMunicipio, Municipio.List(Convert.ToInt32(ddlEstado.SelectedValue)), ESCOLHA_OPCAO);
            ddlMunicipio.UpdateAfterCallBack = true;
        }
    }
    #endregion
   
}
