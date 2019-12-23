using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Marinha.Business;
using Shared.SessionState;
using ComponentArt.Web.UI;

public partial class Acesso_frmProcessoCadastro : MarinhaPageBase
{

    #region Private Member


    private ProcessoCollection _processos
    {
        get { return (ProcessoCollection)Session["_processos"]; }
        set { Session["_processos"] = value; }
    }

    private Processo _processoAtual
    {
        get { return (Processo)Session["_processoAtual"]; }
        set { Session["_processoAtual"] = value; }
    }

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
            _processos = Processo.Select();

            foreach (Processo p in _processos)
            {
                TreeViewNode node = GetNode(p);
                tvProcesso.Nodes.Add(node);
                AddChildren(p, node.Nodes);
            }
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
        return node;
    }

    #endregion

    #region Events Processo
    void btnSalvar_Click(object sender, EventArgs e)
    {
        bool novo = !_processoAtual.IsPersisted;

        _processoAtual.Nome = txtNome.Text;
        _processoAtual.Ordem = Convert.ToInt32(txtOrdem.Text);
        _processoAtual.Link = txtLink.Text;

        _processoAtual.Save();
		ShowSuccessMessage();

        if (novo)
        {
            Anthem.Manager.AddScriptForClientSideEval(
            string.Format("AddNode('{0}', '{1}', '{2}');",
                _processoAtual.ID,
                _processoAtual.Nome,
                _processoAtual.ProcessoPai == null ? null : _processoAtual.ProcessoPai.ID.ToString()));
            _processos.Add(_processoAtual);
        }
        else
            Anthem.Manager.AddScriptForClientSideEval(
            string.Format("UpdateNode('{0}', '{1}');", _processoAtual.ID, _processoAtual.Nome));

    }

    [Anthem.Method]
    public void NovoProcesso(string parentNodeID)
    {
        _processoAtual = new Processo();
        if (parentNodeID != null)
            _processoAtual.ProcessoPai = Processo.Get(Convert.ToInt32(parentNodeID));

        ClearFields();

        lblProcessoPai.Text = _processoAtual.ProcessoPai.Nome;
        lblProcessoPai.UpdateAfterCallBack = true;
    }

    [Anthem.Method]
    public void ExcluirProcesso(string nodeID)
    {
        try
        {
            Processo p = Processo.Get(Convert.ToInt32(nodeID));
            //Antes de chamar o Delete do objeto, devemos removê-lo de qualquer coleção
            _processos.Remove(p);
            p.Delete();
            //Chama a função javascript que remove o nó da árvore
            Anthem.Manager.AddScriptForClientSideEval(
                string.Format("RemoveNode({0});", nodeID));
        }
        catch (Exception ex)
        {
            Anthem.AnthemClientMethods.Alert(ex.Message, this);
        }
    }

    [Anthem.Method]
    public void NodeSelected(string nodeID)
    {
        _processoAtual = _processos.Find(Convert.ToInt32(nodeID));
        Populate();
    }

    #endregion

    #region Processo
    private void Populate()
    {
        lblProcessoPai.Text = _processoAtual.ProcessoPai != null ? _processoAtual.ProcessoPai.Nome : "-";
        txtNome.Text = _processoAtual.Nome;
        txtOrdem.Text = _processoAtual.Ordem.ToString();
        txtLink.Text = _processoAtual.Link;
        RefreshFields();       
    }

    /// <summary>
    /// Atualiza os controles na página após o callback
    /// </summary>
    private void RefreshFields()
    {
        lblProcessoPai.UpdateAfterCallBack = txtNome.UpdateAfterCallBack = txtOrdem.UpdateAfterCallBack
            = txtLink.UpdateAfterCallBack = true;
    }

    private void ClearFields()
    {
        txtNome.Text = "";
        txtLink.Text = "";
        txtOrdem.Text = "";
        lblProcessoPai.Text = "";
    
        RefreshFields();
    }

    #endregion

  
}
