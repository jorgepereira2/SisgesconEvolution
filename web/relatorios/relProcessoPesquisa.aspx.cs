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
using Shared.Common;
using System.Text;
using System.Collections.Generic;

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
        this.btnImprimir.Click += new EventHandler(btnImprimir_Click);
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
        node.ShowCheckBox = true;
        return node;
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


    #region Imprimir

    void btnImprimir_Click(object sender, EventArgs e)
    {
        string ListaProcesso = GetListaProcesso();

        if (ListaProcesso == null || ListaProcesso == "")
        {
            ShowMessage("Selecione um processo!");
            return;
        }

        string address = "";

        object[] param = new object[42];

        param[1] = HttpUtility.UrlEncode(txtNome.Text);
        param[2] = ListaProcesso;

        if(ddlTipo.SelectedValue == "1")
            address = string.Format(@"relProcessoPesquisaDetalhadoListagem.aspx?nome={1}&processo={2}", param);

        else if(ddlTipo.SelectedValue == "2")
            address = string.Format(@"relProcessoPesquisaAgrupadoListagem.aspx?nome={1}&processo={2}", param);

        Anthem.AnthemClientMethods.Popup(this, address, false, false, false, true, true, true, true,
            60, 60, 700, 500);
    }

    private string GetListaProcesso()
    {
        StringBuilder sb = new StringBuilder();

        foreach (TreeViewNode node in tvProcesso.Nodes)
        {
            if (node.Checked)
            {
                sb.AppendFormat("{0},", node.ID);
                sb.Append(GetListaProcessoNode(node));
            }
        }

        if (sb.Length > 0)
            return Util.RemoveLastChar(sb.ToString());
        else
            return "";
    }

    private StringBuilder GetListaProcessoNode(TreeViewNode node)
    {
        StringBuilder sb = new StringBuilder();

        if (node.Nodes.Count > 0)
        {
            foreach (TreeViewNode node_node in node.Nodes)
            {
                if (node_node.Checked)
                {
                    sb.AppendFormat("{0},", node_node.ID);
                    sb.Append(GetListaProcessoNode(node_node));
                }
            }
        }

        return sb;
    }

    #endregion
}