using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Drawing;
using System.Text;
using System.Web;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Marinha.Business;
using Shared.SessionState;
using ComponentArt.Web.UI;
using Shared.Common;

public partial class frmPedidoObtencaoAprovacaoLote : SortingPageBase
{
    [TransientPageState] 
    protected PedidoObtencao _pedido;

    [TransientPageState]
    protected List<IPedidoObtencao> _pedidos;
    
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
		this.RegisterSortingControl(this.gvPesquisa);
        this.gvPesquisa.RowDataBound += new GridViewRowEventHandler(gvPesquisa_RowDataBound);
        this.gvPesquisa.RowCommand +=new GridViewCommandEventHandler(gvPesquisa_RowCommand);
        btnAprovar.Click += new EventHandler(btnAprovar_Click);
       
        ucConfirmarAprovacao.OkClicked += new EventHandler(ucConfirmarAprovacao_OkClicked);
        this.ucComentario.OkClicked += UcComentario_OnOkClicked;
        btnFiltrar.Click += new EventHandler(btnFiltrar_Click);
    }

    void btnFiltrar_Click(object sender, EventArgs e)
    {
        Bind();
    }

    void gvPesquisa_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if(e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btnVisualizar = (LinkButton)e.Row.FindControl("btnVisualizar");
            LinkButton btnDocumentos = (LinkButton)e.Row.FindControl("btnDocumentos");
            PedidoObtencao pedido = (PedidoObtencao)e.Row.DataItem;
            
            if(pedido.TipoPedidoSigla == "POPS")
                e.Row.ForeColor = Color.Blue;

            Anthem.AnthemClientMethods.Popup(btnVisualizar, "fchPedidoObtencaoCompleto.aspx?id_pedido=" + pedido.ID.ToString(),
            false, false, false, true, true, true, true, 10, 30, 700, 520, false);

            Anthem.AnthemClientMethods.Popup(btnDocumentos, "fchPedidoObtencaoDocumentos.aspx?id_pedido=" + pedido.ID.ToString(),
            false, false, false, true, true, true, true, 10, 30, 700, 520, false);

            e.Row.Cells[5].Attributes.Add("onmouseover", string.Format("Tip('{0}<br>', SHADOW, true, PADDING, 7, FOLLOWMOUSE, false);",
                    GetTextoComentarios(pedido)));

            if (pedido.Status.StatusPedidoObtencaoEnum == StatusPedidoObtencaoEnum.AguardandoAprovacaoEncarregadoDepartamento)
            {
                HtmlControl brPar = (HtmlControl)e.Row.FindControl("brPar");
                RadioButton rbPar = (RadioButton)e.Row.FindControl("rbPar");
                RadioButton rbAprovar = (RadioButton)e.Row.FindControl("rbAprovar");
                rbPar.Visible = brPar.Visible = true;
                rbAprovar.ToolTip = "Aprovar Diretamente";
            }
        }
    }

    public static string GetTextoComentarios(PedidoObtencao po)
    {
        StringBuilder str = new StringBuilder();
       

        str.Append("<b>Comentários:</b><br>");
        for (int i = po.Historico.Count - 1; i >= 0; i--)
        {
            HistoricoPedidoObtencao historico = po.Historico[i];
            if (!string.IsNullOrEmpty(historico.Justificativa))
                str.AppendFormat("- {0} ({1}):<br> {2}<br><br>", historico.Servidor.NomeGuerra, historico.Data.ToShortDateString(),
                                 historico.Justificativa);
        }
        return str.ToString();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            Anthem.Manager.Register(this);

            Util.FillDropDownList(ddlDepartamento, Celula.List(TipoCelula.Departamento), "Todos");

            Bind();
        }
    }
    #endregion  
    
    protected override void Bind()
    {
        _pedidos = PedidoObtencao.SelectPedidosParaAprovacao(this.ID_Servidor, Convert.ToInt32(ddlDepartamento.SelectedValue));
        this.Sort(_pedidos);
        gvPesquisa.DataSource = _pedidos;
        gvPesquisa.DataKeyNames = new string[] {"ID"};
        gvPesquisa.DataBind();
        gvPesquisa.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();

        gvPesquisa.Visible = _pedidos.Count > 0;
        pnMensagem.Visible = _pedidos.Count == 0;
        pnMensagem.UpdateAfterCallBack = true;
    }

    void ucConfirmarAprovacao_OkClicked(object sender, EventArgs e)
    {
        int count = 0;
        foreach (GridViewRow gridViewRow in gvPesquisa.Rows)
        {
            if (gridViewRow.RowType == DataControlRowType.DataRow)
            {
                RadioButton rbAprovar = (RadioButton)gridViewRow.FindControl("rbAprovar");
                RadioButton rbRecusar = (RadioButton)gridViewRow.FindControl("rbRecusar");
                RadioButton rbPar = (RadioButton)gridViewRow.FindControl("rbPar");
                if (rbAprovar.Checked || rbPar.Checked)
                {
                    IPedidoObtencao pedido = _pedidos.Find(new Predicate<IPedidoObtencao>(delegate(IPedidoObtencao match)
                    {
                        return match.ID == Convert.ToInt32(gvPesquisa.DataKeys[gridViewRow.RowIndex][0]);
                    }));

                    TextBox txtComentario = FindTextBoxComentario(pedido.ID);

                    if(rbPar.Checked)
                        ((PedidoObtencao)pedido).EnviarParaPAR(this.ID_Servidor, txtComentario.Text);
                    else
                        ((PedidoObtencao)pedido).IrParaProximoStatus(this.ID_Servidor, txtComentario.Text);
                    count++;
                }
                else if (rbRecusar.Checked)
                {
                    IPedidoObtencao pedido = _pedidos.Find(new Predicate<IPedidoObtencao>(delegate(IPedidoObtencao match)
                    {
                        return match.ID == Convert.ToInt32(gvPesquisa.DataKeys[gridViewRow.RowIndex][0]);
                    }));

                    TextBox txtComentario = FindTextBoxComentario(pedido.ID);
                     ((PedidoObtencao)pedido).Recusar(this.ID_Servidor, txtComentario.Text);
                    count++;
                }
            }
        }
        if (count == 0)
        {
            ShowMessage("Nenhum pedido foi selecionado.");
            return;
        }
                
        Bind();
        ucConfirmarAprovacao.Close();
    }

    void btnAprovar_Click(object sender, EventArgs e)
    {
        ucConfirmarAprovacao.Show();
    }

    #region Edicao Comentario
    void gvPesquisa_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Comentar")
        {
            _pedido = PedidoObtencao.Get(Convert.ToInt32(e.CommandArgument));
            TextBox txtComentario = FindTextBoxComentario(_pedido.ID);

            this.ucComentario.Comentario = txtComentario.Text;
            this.ucComentario.Show();
        }
    }

    private void UcComentario_OnOkClicked(object sender, EventArgs e)
    {
        Anthem.TextBox txtComentario = FindTextBoxComentario(_pedido.ID);
        txtComentario.Text = this.ucComentario.Comentario;
        txtComentario.UpdateAfterCallBack = true;

        Anthem.LinkButton btnComentar = FindLinkComentario(_pedido.ID);
        btnComentar.Font.Bold = this.ucComentario.Comentario.Trim() != "";
        btnComentar.UpdateAfterCallBack = true;

        this.ucComentario.Close();
        _pedido = null;
    }

    private Anthem.TextBox FindTextBoxComentario(int id_orcamento)
    {

        foreach (GridViewRow row in gvPesquisa.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                if (id_orcamento == Convert.ToInt32(gvPesquisa.DataKeys[row.RowIndex][0]))
                {
                    return (Anthem.TextBox)row.FindControl("txtComentario");
                }
            }
        }
        return null;
    }

    private Anthem.LinkButton FindLinkComentario(int id_orcamento)
    {
        foreach (GridViewRow row in gvPesquisa.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                if (id_orcamento == Convert.ToInt32(gvPesquisa.DataKeys[row.RowIndex][0]))
                {
                    return (Anthem.LinkButton)row.FindControl("btnComentar");
                }
            }
        }
        return null;
    }
    #endregion

    protected void btnLimpar_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow item = (GridViewRow)btn.Parent.Parent;
        Anthem.RadioButton rbAprovar = (Anthem.RadioButton)item.FindControl("rbAprovar");
        Anthem.RadioButton rbRecusar = (Anthem.RadioButton)item.FindControl("rbRecusar");
        Anthem.TextBox txtComentario = (Anthem.TextBox)item.FindControl("txtComentario");
        rbAprovar.Checked = false;
        rbRecusar.Checked = false;
        txtComentario.Text = "";
        rbAprovar.UpdateAfterCallBack = true;
        rbRecusar.UpdateAfterCallBack = true;
        txtComentario.UpdateAfterCallBack = true;
    }
}
