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
using Shared.NHibernateDAL;
using Shared.SessionState;
using ComponentArt.Web.UI;
using Shared.Common;

public partial class frmAutorizacaoCompraAprovacaoLote : SortingPageBase
{
    [TransientPageState] 
    protected AutorizacaoCompra _ac;

    [TransientPageState]
    protected List<AutorizacaoCompra> _acs;
    
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
		this.RegisterSortingControl(this.gvPesquisa);
        this.gvPesquisa.RowDataBound += new GridViewRowEventHandler(gvPesquisa_RowDataBound);
        gvPesquisa.RowUpdating += new GridViewUpdateEventHandler(gvPesquisa_RowUpdating);
       gvPesquisa.RowCommand +=new GridViewCommandEventHandler(gvPesquisa_RowCommand);
        this.gvPesquisa.RowEditing += new GridViewEditEventHandler(gvPesquisa_RowEditing);
        this.gvPesquisa.RowCancelingEdit += new GridViewCancelEditEventHandler(gvPesquisa_RowCancelingEdit);

        btnAprovar.Click += new EventHandler(btnAprovar_Click);
        ucConfirmarAprovacao.OkClicked += new EventHandler(ucConfirmarAprovacao_OkClicked);
        this.ucComentario.OkClicked += UcComentario_OnOkClicked;

        dgSaldo.RowDataBound += new GridViewRowEventHandler(dgSaldo_RowDataBound);
        dgPedidoServico.RowDataBound += new GridViewRowEventHandler(dgPedidoServico_RowDataBound);
        
        btnOk.Click += new EventHandler(btnOk_Click);
        btnFecharSaldo.Click += delegate { winSaldo.Hide(); };
        btnFecharPS.Click += delegate { winPedidoServico.Hide(); };

        btnPesquisar.Click += delegate { Bind(); };

    }

    

    

    void dgSaldo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if(e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView row = (DataRowView)e.Row.DataItem;
            Label lblSaldo = (Label) e.Row.FindControl("lblSaldo");
            Label lblSaldoTotal = (Label)e.Row.FindControl("lblSaldoTotal");
            Label lblCustoSimulado = (Label)e.Row.FindControl("lblCustoSimulado");
            Label lblComprometido = (Label)e.Row.FindControl("lblComprometido");
            
            decimal valorUtilizado = GetValorUtilizado(Convert.ToInt32(row["ID_Projeto"]), Convert.ToInt32(row["ID_NaturezaDespesa"]), Convert.ToInt32(row["ID_FonteRecurso"]), Convert.ToInt32(row["ID_PTRES"]));
            lblSaldo.Text = (Convert.ToDecimal(row["ValorEntrada"]) - Convert.ToDecimal(row["ValorEmpenhado"])).ToString("N2");
            lblSaldoTotal.Text = (Convert.ToDecimal(row["ValorEntrada"]) - Convert.ToDecimal(row["ValorComprometido"]) - Convert.ToDecimal(row["ValorEmpenhado"]) - valorUtilizado).ToString("N2");
            lblComprometido.Text =  Convert.ToDecimal(row["ValorComprometido"]).ToString("N2");
            lblCustoSimulado.Text = valorUtilizado.ToString("N2");

        }
    }

    private decimal GetValorUtilizado(int id_projeto, int id_naturezaDespesa, int id_fonteRecurso, int id_ptres)
    {
        decimal valor = 0;
        foreach (GridViewRow row in gvPesquisa.Rows)
        {
            Anthem.RadioButton rbAprovar = (Anthem.RadioButton)row.FindControl("rbAprovar");
            if (rbAprovar.Checked)
            {
                int id_ac = Convert.ToInt32(gvPesquisa.DataKeys[row.RowIndex][0]);
                AutorizacaoCompra ac = _acs.Find(delegate(AutorizacaoCompra match) { return match.ID == id_ac; });
                NaturezaDespesa naturezaDespesa = null;
                if (ac.NaturezaDespesa != null)
                    naturezaDespesa = ac.NaturezaDespesa.GetPai();

                if (ac.Projeto != null && naturezaDespesa != null && ac.FonteRecurso != null && ac.PTRES != null && 
                    ac.Projeto.ID == id_projeto && naturezaDespesa.ID == id_naturezaDespesa && ac.FonteRecurso.ID == id_fonteRecurso && ac.PTRES.ID == id_ptres)
                    valor += ac.ValorTotal;
            }
        }
        return valor;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            Anthem.Manager.Register(this);

            Util.FillDropDownList(ddlFonteRecurso, FonteRecurso.List(), ESCOLHA_OPCAO);
            Util.FillDropDownList(ddlProjeto, Projeto.List(), ESCOLHA_OPCAO);
            Util.FillDropDownList(ddlPTRES, PTRES.List(), ESCOLHA_OPCAO);
            Util.FillDropDownList(ddlNaturezaDespesa, NaturezaDespesa.List(), ESCOLHA_OPCAO);
            ddlStatus.Items.Add(new ListItem("Todos", "0"));
            ddlStatus.Items.Add(new ListItem(Util.GetDescription(StatusAutorizacaoCompraEnum.AguardandoAprovacaoEncarregadoObtencao), StatusAutorizacaoCompraEnum.AguardandoAprovacaoEncarregadoObtencao.GetHashCode().ToString()));
            ddlStatus.Items.Add(new ListItem(Util.GetDescription(StatusAutorizacaoCompraEnum.AguardandoAprovacaoComandanteGeral), StatusAutorizacaoCompraEnum.AguardandoAprovacaoComandanteGeral.GetHashCode().ToString()));
            
            Bind();

            //Servidor servidor = Servidor.Get(ID_Servidor);
            //gvPesquisa.Columns[gvPesquisa.Columns.Count - 1].Visible = servidor.Funcao == FuncaoServidor.Comandante;
        }
    }
    #endregion  
    
    #region Aprovar
    void btnAprovar_Click(object sender, EventArgs e)
    {
        ucConfirmarAprovacao.Show();
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
                if (rbAprovar.Checked)
                {
                    AutorizacaoCompra ac = _acs.Find(new Predicate<AutorizacaoCompra>(delegate(AutorizacaoCompra match)
                    {
                        return match.ID == Convert.ToInt32(gvPesquisa.DataKeys[gridViewRow.RowIndex][0]);
                    }));

                    TextBox txtComentario = FindTextBoxComentario(ac.ID);
                    ac.IrParaProximoStatus(this.ID_Servidor, txtComentario.Text);
                    count++;
                }
                else if (rbRecusar.Checked)
                {
                    AutorizacaoCompra ac = _acs.Find(new Predicate<AutorizacaoCompra>(delegate(AutorizacaoCompra match)
                    {
                        return match.ID == Convert.ToInt32(gvPesquisa.DataKeys[gridViewRow.RowIndex][0]);
                    }));

                    TextBox txtComentario = FindTextBoxComentario(ac.ID);
                    ac.Recusar(this.ID_Servidor, txtComentario.Text);
                    count++;
                }
            }
        }
        if (count == 0)
        {
            ShowMessage("Nenhuma AC foi selecionada.");
            return;
        }

        Bind();
        ucConfirmarAprovacao.Close();
    }

    Dictionary<int, string> fonteRecursoList = null;
    Dictionary<int, string> projetoList = null;
    Dictionary<int, string> ptresList = null;

    void gvPesquisa_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btnVisualizar = (LinkButton)e.Row.FindControl("btnVisualizar");
            
            AutorizacaoCompra ac = (AutorizacaoCompra)e.Row.DataItem;
            
            Anthem.AnthemClientMethods.Popup(btnVisualizar, "fchAutorizacaoCompraCompleto.aspx?id_ac=" + ac.ID.ToString(),
            false, false, false, true, true, true, true, 10, 30, 700, 520, false);

            TextBox txtComentario = (TextBox)e.Row.FindControl("txtComentario");

            LinkButton btnComentar = (LinkButton) e.Row.FindControl("btnComentar");
            btnComentar.Attributes.Add("onmouseover", string.Format("Tip('<b>Comentário:</b><br><br>{0}', SHADOW, true, PADDING, 7, FOLLOWMOUSE, false);",
                    txtComentario.Text));

            if(ac.UltimoHistorico != null && ac.UltimoHistorico.FlagReprovado)
                e.Row.ForeColor = Color.Red;

            e.Row.Cells[5].Attributes.Add("onmouseover", string.Format("Tip('{0}<br>', SHADOW, true, PADDING, 7, FOLLOWMOUSE, false);",
                   GetTextoComentarios(ac)));
        }
    }
    #endregion
    
    #region Edição
    void gvPesquisa_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvPesquisa.EditIndex = -1;
        //Bind();
    }

    void gvPesquisa_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvPesquisa.EditIndex = e.NewEditIndex;
        Bind();
    }

    void gvPesquisa_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int id = Convert.ToInt32(gvPesquisa.DataKeys[e.RowIndex]["ID"]);
        _ac = _acs.Find(delegate(AutorizacaoCompra match) { return match.ID == id; });

        CheckBox chk = (CheckBox)gvPesquisa.Rows[e.RowIndex].FindControl("chkRequerAprovacao");
        _ac.FlagRequerAprovacaoConselhoEconomico = chk.Checked;
        _ac.Save();
        gvPesquisa.EditIndex = -1;
        Bind();
    }
    #endregion

    #region Edicao Detalhes

    void btnOk_Click(object sender, EventArgs e)
    {
        
        NaturezaDespesa natureza = NaturezaDespesa.Get(Convert.ToInt32(ddlNaturezaDespesa.SelectedValue));
        if(natureza != null && natureza.GetPai() == null)
        {
            ShowMessage("A natureza de despeza selecionada não possui natureza principal.");
            return;
        }
        
        AutorizacaoCompra ac = _acs.Find(delegate(AutorizacaoCompra match) { return _ac.ID == match.ID; });
        ac.NaturezaDespesa = natureza;
        ac.Projeto = Projeto.Get(Convert.ToInt32(ddlProjeto.SelectedValue));
        ac.FonteRecurso = FonteRecurso.Get(Convert.ToInt32(ddlFonteRecurso.SelectedValue));
        ac.PTRES= PTRES.Get(Convert.ToInt32(ddlPTRES.SelectedValue));


        winDetalhes.Hide();
    }

    #endregion

    protected override void Bind()
    {
        fonteRecursoList = FonteRecurso.List();
        projetoList = Projeto.List();
        ptresList = PTRES.List();

        _acs = AutorizacaoCompra.SelectPedidosParaAprovacao(this.ID_Servidor, Convert.ToInt32(ddlStatus.SelectedValue));
        this.Sort(_acs);
        gvPesquisa.DataSource = _acs;
        gvPesquisa.DataKeyNames = new string[] {"ID"};
        gvPesquisa.DataBind();
        gvPesquisa.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();

        gvPesquisa.Visible = _acs.Count > 0;
        pnMensagem.Visible = _acs.Count == 0;
        pnMensagem.UpdateAfterCallBack = true;
    }

    private void BindSaldo()
    {
        //pega os ids dos itens selecionados
        List<string> list = new List<string>();
        foreach (GridViewRow row in gvPesquisa.Rows)
        {
            Anthem.RadioButton rbAprovar = (Anthem.RadioButton)row.FindControl("rbAprovar");
            if (rbAprovar.Checked)
            {
                int id_ac = Convert.ToInt32(gvPesquisa.DataKeys[row.RowIndex][0]);
                AutorizacaoCompra ac = _acs.Find(delegate(AutorizacaoCompra match) { return match.ID == id_ac; });
                list.Add(ac.ChaveFinanceiro);
            }
        }

        dgSaldo.DataSource = EntradaValores.SelectSaldo(Util.CriaLista(list));
        dgSaldo.DataBind();
        dgSaldo.UpdateAfterCallBack = true;
    }

    public static string GetTextoComentarios(AutorizacaoCompra ac)
    {
        StringBuilder str = new StringBuilder();


        str.Append("<b>Comentários:</b><br>");
        for (int i = ac.Historico.Count - 1; i >= 0; i--)
        {
            HistoricoAutorizacaoCompra historico = ac.Historico[i];
            if (!string.IsNullOrEmpty(historico.Justificativa))
                str.AppendFormat("- {0} ({1}):<br> {2}<br><br>", historico.Servidor.NomeGuerra, historico.Data.ToShortDateString(),
                                 historico.Justificativa);
        }
        return str.ToString();
    }

    #region Edicao Comentario
    void gvPesquisa_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Comentar")
        {
            _ac = AutorizacaoCompra.Get(Convert.ToInt32(e.CommandArgument));
            TextBox txtComentario = FindTextBoxComentario(_ac.ID);

            this.ucComentario.Comentario = txtComentario.Text;
            this.ucComentario.Show();
        }
        else if(e.CommandName == "EditarDetalhes")
        {
            _ac = _acs.Find(delegate(AutorizacaoCompra match){ return Convert.ToInt32(e.CommandArgument) == match.ID; }); 

            ddlProjeto.SelectedValue = ObjectReader.ReadID(_ac.Projeto);
            ddlNaturezaDespesa.SelectedValue = ObjectReader.ReadID(_ac.NaturezaDespesa);
            ddlPTRES.SelectedValue = ObjectReader.ReadID(_ac.PTRES);
            ddlFonteRecurso.SelectedValue = ObjectReader.ReadID(_ac.FonteRecurso);

            ddlProjeto.UpdateAfterCallBack = true;
            ddlNaturezaDespesa.UpdateAfterCallBack = true;
            ddlPTRES.UpdateAfterCallBack = true;
            ddlFonteRecurso.UpdateAfterCallBack = true;

            winDetalhes.Show();
        }
        else if (e.CommandName == "PS")
        {
            _ac = _acs.Find(delegate(AutorizacaoCompra match) { return Convert.ToInt32(e.CommandArgument) == match.ID; });

            lblACPS.Text = _ac.CodigoComAno;
            DataSet ds = PedidoServico.SelectCustoRealizadoAC(_ac.ID);
            dgPedidoServico.DataSource = ds.Tables[0];
            dgPedidoServico.DataKeyNames = new string[] {"ID_PedidoServico"};
            dgPedidoServico.DataBind();
            dgPedidoServico.UpdateAfterCallBack = true;

            winPedidoServico.Show();
        }
        else if (e.CommandName == "AtualizarSaldo")
        {
            BindSaldo();
            winSaldo.Show();
        }
    }

    void dgPedidoServico_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if(e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btnVisualizarPS = (LinkButton) e.Row.FindControl("btnVisualizarPS");
            DataRowView row = (DataRowView) e.Row.DataItem;
            Anthem.AnthemClientMethods.Popup(btnVisualizarPS, "../servico/fchPedidoServico.aspx?id_pedido=" + row["ID_PedidoServico"],
            false, false, false, true, true, true, true, 10, 30, 700, 520, false);

        }
    }

    private void UcComentario_OnOkClicked(object sender, EventArgs e)
    {
        Anthem.TextBox txtComentario = FindTextBoxComentario(_ac.ID);
        txtComentario.Text = this.ucComentario.Comentario;
        txtComentario.UpdateAfterCallBack = true;

        Anthem.LinkButton btnComentar = FindLinkComentario(_ac.ID);
        btnComentar.Font.Bold = this.ucComentario.Comentario.Trim() != "";

        btnComentar.Attributes.Add("onmouseover", string.Format("Tip('<b>Comentário:</b><br><br>{0}', SHADOW, true, PADDING, 7, FOLLOWMOUSE, false);",
                    txtComentario.Text));

        btnComentar.UpdateAfterCallBack = true;

        this.ucComentario.Close();
        _ac= null;
    }

    private Anthem.TextBox FindTextBoxComentario(int id_ac)
    {

        foreach (GridViewRow row in gvPesquisa.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                if (id_ac == Convert.ToInt32(gvPesquisa.DataKeys[row.RowIndex][0]))
                {
                    return (Anthem.TextBox)row.FindControl("txtComentario");
                }
            }
        }
        return null;
    }

    private Anthem.LinkButton FindLinkComentario(int id_ac)
    {
        foreach (GridViewRow row in gvPesquisa.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                if (id_ac == Convert.ToInt32(gvPesquisa.DataKeys[row.RowIndex][0]))
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
