using System;
using System.Data;
using System.Collections.Generic;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Marinha.Business;
using Shared.Common;
using Shared.NHibernateDAL;
using Shared.SessionState;
using ComponentArt.Web.UI;

public partial class frmPerfilAcessoCadastro : MarinhaPageBase
{
    #region Private Member

    [TransientPageState]
    protected PerfilAcesso _perfil;

	[TransientPageState]
	protected RegraAcessoPerfilAcesso _regras;
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


			if (Request["id_perfilAcesso"] != null)
			{
				_perfil = PerfilAcesso.Get(Convert.ToInt32(Request["ID_PerfilAcesso"]));
				lblPerfilAcesso.Text = _perfil.Nome;
				PopulateFields();
				PopulateTreeView();
				PopulateRegrasAcesso();
				_regras = RegraAcessoPerfilAcesso.Get(_perfil.ID);
			}
			else
			{
				_perfil = new PerfilAcesso();				
			}

			if (_regras == null)
				_regras = new RegraAcessoPerfilAcesso();

            RegisterDeleteScript();  
        }
        Anthem.AnthemClientMethods.Redirect("frmPerfilAcessoPesquisa.aspx", btnVoltar);
        
    }

	private void FillPage()
	{
		cblLocalAcesso.DataSource = LocalAcesso.List();
		cblLocalAcesso.DataTextField = "Value";
		cblLocalAcesso.DataValueField = "Key";		
		cblLocalAcesso.DataBind();

		ProcessoCollection processos = Processo.Select();

		foreach (Processo p in processos)
		{
			TreeViewNode node = GetNode(p);
			tvProcesso.Nodes.Add(node);
			AddChildren(p, node.Nodes);
		}
	}

    private void AddChildren(Processo processo, TreeViewNodeCollection nodes)
    {
        foreach (Processo p in processo.Processos)
        {
            TreeViewNode node = GetNode(p);
            nodes.Add(node);
            AddChildren(p, node.Nodes);
        }
    }

    private TreeViewNode GetNode(Processo p)
    {
        TreeViewNode node = new TreeViewNode();
        node.ID = p.ID.ToString();        
        node.Text = p.Nome;
        node.ShowCheckBox = true;
        return node;
    }

    #endregion

	#region Popula TreeView
	private void PopulateTreeView()
    {
        foreach (Processo p in _perfil.Processos)
        {
            TreeViewNode node = tvProcesso.FindNodeById(p.ID.ToString());
            if (node != null)
                node.Checked = true;
        }
    }

    private void CheckNode(Processo processo)
    {
        TreeViewNode node = tvProcesso.FindNodeById(processo.ID.ToString());
        if (node != null)
            node.Checked = true;
        foreach (Processo p in processo.Processos)
        {
            CheckNode(p);
        }
    }
    #endregion

    #region Events Processo
    void btnSalvar_Click(object sender, EventArgs e)
    {
		if (TabStrip1.SelectedTab.ID == tabPerfil.ID)
		{
			SalvarPerfil();
		}
		else if (TabStrip1.SelectedTab.ID == tabProcessos.ID)
		{
			List<string> list = new List<string>();
			CriaLista(list, tvProcesso.Nodes);
			_perfil.Processos = Processo.SelectByList(list);
			_perfil.Save();
		}
		else
		{
			SalvarRegrasAcesso();
		}

    }

    private void CriaLista(List<string> list, TreeViewNodeCollection nodes)
    {
        foreach (TreeViewNode node in nodes)
        {
            if (node.Checked)
            {
                list.Add(node.ID);
                if (node.Nodes.Count > 0)
                    CriaLista(list, node.Nodes);
            }
        }
    }
    #endregion

	#region Regras Acesso
	private void SalvarRegrasAcesso()
	{
		if(!ValidaRegrasAcesso())
			return;

		
		_regras.FlagControlaHorario = chkControlarHorario.Checked;
		_regras.FlagControlaLocalAcesso = chkControlarLocal.Checked;
		_regras.FlagDomingo = chkDomingo.Checked;
		_regras.FlagQuarta = chkQuarta.Checked;
		_regras.FlagQuinta = chkQuinta.Checked;
		_regras.FlagSabado = chkSabado.Checked;
		_regras.FlagSegunda = chkSegunda.Checked;
		_regras.FlagSexta = chkSexta.Checked;
		_regras.FlagTerca = chkTerca.Checked;
		_regras.HorarioFinal = PageReader.ReadNullableDate(txtHorarioFim);
        _regras.HorarioInicial = PageReader.ReadNullableDate(txtHorarioInicio);

		_regras.Locais.Clear();
		foreach (ListItem item in cblLocalAcesso.Items)
			if (item.Selected)
				_regras.Locais.Add(LocalAcesso.Get(Convert.ToInt32(item.Value)));

		_regras.ID = _perfil.ID;
		_regras.Save();
		ShowSuccessMessage();

	}

	private void PopulateRegrasAcesso()
	{
		_regras = RegraAcessoPerfilAcesso.Get(_perfil.ID);
		if (_regras == null)
			_regras = new RegraAcessoPerfilAcesso();
		else
		{
			chkControlarHorario.Checked = _regras.FlagControlaHorario;
			chkControlarLocal.Checked = _regras.FlagControlaLocalAcesso;
			txtHorarioInicio.Text = ObjectReader.ReadTime(_regras.HorarioInicial);
			txtHorarioFim.Text = ObjectReader.ReadTime(_regras.HorarioFinal);
			chkDomingo.Checked = _regras.FlagDomingo;
			chkQuarta.Checked = _regras.FlagQuarta;
			chkQuinta.Checked = _regras.FlagQuinta;
			chkSabado.Checked = _regras.FlagSabado;
			chkSegunda.Checked = _regras.FlagSegunda;
			chkSexta.Checked = _regras.FlagSexta;
			chkTerca.Checked = _regras.FlagTerca;

			foreach (ListItem item in cblLocalAcesso.Items)
				item.Selected = _regras.Locais.Contains(Convert.ToInt32(item.Value));

		}
	}



	private bool ValidaRegrasAcesso()
	{
		if (chkControlarHorario.Checked)
		{
			string msg = "";
			if (txtHorarioInicio.Text == "")
				msg = "- Campo Horário Inicial obrigatório";

			if (txtHorarioFim.Text == "")
				msg += "<br>- Campo Horário Final obrigatório";

			if (msg != "")
			{
				ShowMessage(msg);
				return false;
			}
		}

		return true;
	}
	#endregion

	#region Perfil
	private void SalvarPerfil()
	{
		FillObject();
		_perfil.Save();
		ShowSuccessMessage();
	}

	private void PopulateFields()
	{
		txtNome.Text = _perfil.Nome;
		txtObservacao.Text = _perfil.Observacao;		
		chkAtivo.Checked = _perfil.FlagAtivo;
	    chkFlagPodeFazerPOOutraCelula.Checked = _perfil.FlagPodeFazerPOOutraCelula;

	}

	private void ClearFields()
	{
		txtObservacao.Text = "";
		txtNome.Text = "";		
		chkAtivo.Checked = true;
	    chkFlagPodeFazerPOOutraCelula.Checked = false;

		txtObservacao.UpdateAfterCallBack = true;
		txtNome.UpdateAfterCallBack = true;
		chkAtivo.UpdateAfterCallBack = true;
	    chkFlagPodeFazerPOOutraCelula.UpdateAfterCallBack = true;
	}


	private bool Valida()
	{
		string msg = "";

		if (msg != "")
		{
			ShowMessage(msg);
			return false;
		}
		return true;
	}

	private void FillObject()
	{
		_perfil.Nome = txtNome.Text;
		_perfil.Observacao = PageReader.ReadString(txtObservacao);		
		_perfil.FlagAtivo = chkAtivo.Checked;
	    _perfil.FlagPodeFazerPOOutraCelula = chkFlagPodeFazerPOOutraCelula.Checked;
	}
	#endregion


    [Anthem.Method]
    public void Excluir()
    {
        try
        {
            _perfil.Delete();
            ShowSuccessMessage();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message);
        }
    }

}
