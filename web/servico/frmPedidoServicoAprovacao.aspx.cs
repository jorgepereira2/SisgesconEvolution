using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Threading;
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

public partial class frmPedidoServicoAprovacao : SortingPageBase
{
    [TransientPageState] 
    protected DelineamentoOrcamento _orcamento;

    [TransientPageState]
    protected List<DelineamentoOrcamento> _orcamentos;

    [TransientPageState] 
    private Dictionary<int, string> _selecionados;
    
    #region Initialization

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
		this.RegisterSortingControl(this.gvPesquisa);
        this.gvPesquisa.RowDataBound += new GridViewRowEventHandler(gvPesquisa_RowDataBound);
        gvPesquisa.RowUpdating += new GridViewUpdateEventHandler(gvPesquisa_RowUpdating);
        btnAprovar.Click += new EventHandler(btnAprovar_Click);
        gvPesquisa.RowEditing += new GridViewEditEventHandler(gvPesquisa_RowEditing);
        gvPesquisa.RowCancelingEdit += new GridViewCancelEditEventHandler(gvPesquisa_RowCancelingEdit);
        ucConfirmarAprovacao.OkClicked += new EventHandler(ucConfirmarAprovacao_OkClicked);
        this.ucComentario.OkClicked += UcComentario_OnOkClicked;
        gvPesquisa.RowCommand += new GridViewCommandEventHandler(gvPesquisa_RowCommand);
        Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
        Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            Anthem.Manager.Register(this);
            SetDataSource();
            Bind();
        }
    }

    #endregion         

    void gvPesquisa_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btnVisualizar = (LinkButton)e.Row.FindControl("btnVisualizar");
            DelineamentoOrcamento orcamento = (DelineamentoOrcamento)e.Row.DataItem;

            Anthem.AnthemClientMethods.Popup(btnVisualizar, "fchPedidoServico.aspx?id_pedido=" + orcamento.PedidoServico.ID.ToString(),
            false, false, false, true, true, true, true, 10, 30, 700, 520, false);

            if (orcamento.FlagRecusado)
            {
                e.Row.ForeColor = Color.Red;
                //e.Row.ToolTip = orcamento.UltimoHistorico.JustificativaRecusa;
            }
            e.Row.Cells[5].Attributes.Add("onmouseover", string.Format("Tip('{0}<br>', SHADOW, true, PADDING, 7, FOLLOWMOUSE, false);",
                    GetTextoComentarios(orcamento.PedidoServico)));

            if(_selecionados.ContainsKey(orcamento.ID))
            {
                if (_selecionados[orcamento.ID] == "A")
                {
                    RadioButton rbAprovar = (RadioButton) e.Row.FindControl("rbAprovar");
                    rbAprovar.Checked = true;
                }
                else if (_selecionados[orcamento.ID] == "R")
                {
                    RadioButton rbReprovar = (RadioButton) e.Row.FindControl("rbRecusar");
                    rbReprovar.Checked = true;
                }
            }


            Anthem.NumericTextBox txtDescontoMO = e.Row.FindControl("txtDescontoMO") as Anthem.NumericTextBox;
            if(txtDescontoMO != null && gvPesquisa.Columns[gvPesquisa.Columns.Count - 1].Visible)
                Anthem.Manager.AddScriptForClientSideEval(string.Format("document.getElementById('{0}').select();", txtDescontoMO.ClientID));
        }                
    }

    public static string GetTextoComentarios(PedidoServico ps)
    {
        StringBuilder str = new StringBuilder();
        str.AppendFormat("<b>Categoria:</b>{0}<br>", ps.CategoriaServico);

        str.Append("<b>Comentários:</b><br>");
        for(int i = ps.Historico.Count - 1; i >= 0;i--)
        {
            HistoricoPedidoServico historico = ps.Historico[i];
            if (!string.IsNullOrEmpty(historico.JustificativaRecusa))
                str.AppendFormat("- {0} ({1}):<br> {2}<br><br>", historico.Servidor.NomeGuerra, historico.Data.ToShortDateString(),
                                 historico.JustificativaRecusa);
        }
        return str.ToString();
    }
  
    private  void SetDataSource()
    {
        _orcamentos = DelineamentoOrcamento.SelectOrcamentosParaAprovacao(this.ID_Servidor); 
    }
  
    protected override void Bind()
    {
        StoreGridState();
       this.Sort(_orcamentos);
        gvPesquisa.DataSource = _orcamentos;
		gvPesquisa.DataKeyNames = new string[]{"ID"};
        gvPesquisa.DataBind();
        gvPesquisa.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();

        gvPesquisa.Visible = _orcamentos.Count > 0;
        pnMensagem.Visible = _orcamentos.Count == 0;
        pnMensagem.UpdateAfterCallBack = true;

        Servidor servidor = Servidor.Get(ID_Servidor);
        //if(servidor.Funcao != FuncaoServidor.Comandante)
         //  gvPesquisa.Columns[gvPesquisa.Columns.Count - 1].Visible = false;
    }

    private void StoreGridState()
    {
        _selecionados = new Dictionary<int, string>();
        foreach (GridViewRow row in gvPesquisa.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                RadioButton rbAprovar = (RadioButton) row.FindControl("rbAprovar");
                RadioButton rbReprovar = (RadioButton) row.FindControl("rbRecusar");

                int id = Convert.ToInt32(gvPesquisa.DataKeys[row.RowIndex]["ID"]);
                if (rbAprovar.Checked)
                    _selecionados.Add(id, "A");
                else if (rbReprovar.Checked)
                    _selecionados.Add(id, "R");
            }
        }
    }

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
                    DelineamentoOrcamento orcamento = _orcamentos.Find(new Predicate<DelineamentoOrcamento>(delegate(DelineamentoOrcamento match)
                    {
                        return match.ID == Convert.ToInt32(gvPesquisa.DataKeys[gridViewRow.RowIndex][0]);
                    }));

                    TextBox txtComentario = FindTextBoxComentario(orcamento.ID);

                    orcamento.Aprovar(this.ID_Servidor, txtComentario.Text);
                    count++;
                }
                else if(rbRecusar.Checked)
                {
                    DelineamentoOrcamento orcamento = _orcamentos.Find(new Predicate<DelineamentoOrcamento>(delegate(DelineamentoOrcamento match)
                    {
                        return match.ID == Convert.ToInt32(gvPesquisa.DataKeys[gridViewRow.RowIndex][0]);
                    }));
                                        
                    TextBox txtComentario = FindTextBoxComentario(orcamento.ID);
                    orcamento.Recusar(this.ID_Servidor, txtComentario.Text);
                    count++;
                }
            }
        }
        if (count == 0)
        {
            ShowMessage("Nenhum pedido foi selecionado.");
            return;
        }
        
        SetDataSource();
        Bind();
        ucConfirmarAprovacao.Close();
    }

    #region Edicao Comentario
    void gvPesquisa_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Comentar")
        {
            _orcamento = DelineamentoOrcamento.Get(Convert.ToInt32(e.CommandArgument));
            TextBox txtComentario = FindTextBoxComentario(_orcamento.ID);

            this.ucComentario.Comentario = txtComentario.Text;
            this.ucComentario.Show();
        }
        else if(e.CommandName == "Recalcular")
        {
            _orcamento = DelineamentoOrcamento.Get(Convert.ToInt32(e.CommandArgument));
            _orcamento.RecalcularRotinas();
            SetDataSource();
            Bind();
        }
    }

    private void UcComentario_OnOkClicked(object sender, EventArgs e)
    {
        Anthem.TextBox txtComentario = FindTextBoxComentario(_orcamento.ID);
        txtComentario.Text = this.ucComentario.Comentario;
        txtComentario.UpdateAfterCallBack = true;

        Anthem.LinkButton btnComentar = FindLinkComentario(_orcamento.ID);
        btnComentar.Font.Bold = this.ucComentario.Comentario.Trim() != "";
        btnComentar.UpdateAfterCallBack = true;

        this.ucComentario.Close();
        _orcamento = null;
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
    
    #region Edição Desconto
    void gvPesquisa_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvPesquisa.EditIndex = e.NewEditIndex;
        Bind();
    }

  

    void gvPesquisa_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvPesquisa.EditIndex = -1;
        Bind();
    }

    private void gvPesquisa_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        TextBox txtDescontoMO = (TextBox) gvPesquisa.Rows[e.RowIndex].FindControl("txtDescontoMO");
        TextBox txtDescontoMaterial = (TextBox)gvPesquisa.Rows[e.RowIndex].FindControl("txtDescontoMaterial");

        int id = Convert.ToInt32(gvPesquisa.DataKeys[e.RowIndex]["ID"]);
        DelineamentoOrcamento orcamento = _orcamentos.Find(delegate(DelineamentoOrcamento match) {return match.ID == id;});
        orcamento.PercentualDescontoSubTotalMaoObra = Convert.ToDecimal(txtDescontoMO.Text);
        orcamento.PercentualDescontoSubTotalMaterialServicoTerceiro = Convert.ToDecimal(txtDescontoMaterial.Text);
        orcamento.Save();

        gvPesquisa.EditIndex = -1;
        SetDataSource();
        Bind();
    }

    protected void DescontoMOChanged(object sender, EventArgs e)
    {
        Anthem.TextBox txtDescontoMO = (Anthem.TextBox)sender;

        if (Util.IsDecimal(txtDescontoMO.Text))
        {
            Anthem.TextBox txtValorDescontoMO = (Anthem.TextBox)((GridViewRow)txtDescontoMO.Parent.Parent).FindControl("txtValorDescontoMO");
            Label lblValorMaoObra = (Label)((GridViewRow)txtDescontoMO.Parent.Parent).FindControl("lblValorMaoObra");

            txtValorDescontoMO.Text =
                Util.GetValor(Convert.ToDecimal(txtDescontoMO.Text), Convert.ToDecimal(lblValorMaoObra.Text)).ToString("N2");
            txtValorDescontoMO.UpdateAfterCallBack = true;
        }
    }

    protected void ValorDescontoMOChanged(object sender, EventArgs e)
    {
        Anthem.TextBox txtValorDescontoMO = (Anthem.TextBox)sender;

        if (Util.IsDecimal(txtValorDescontoMO.Text))
        {
            Anthem.TextBox txtDescontoMO = (Anthem.TextBox)((GridViewRow)txtValorDescontoMO.Parent.Parent).FindControl("txtDescontoMO");
            GridViewRow row = (GridViewRow)txtDescontoMO.Parent.Parent;
            Label lblValorMaoObra = (Label)row.FindControl("lblValorMaoObra");

            int id_orcamento = Convert.ToInt32(gvPesquisa.DataKeys[row.RowIndex]["ID"]);
            DelineamentoOrcamento orcamento = _orcamentos.Find(delegate(DelineamentoOrcamento match) {return match.ID == id_orcamento; });

            txtDescontoMO.Text = Util.GetPercentual(Convert.ToDecimal(txtValorDescontoMO.Text), Convert.ToDecimal(lblValorMaoObra.Text)).ToString("N2");
            txtDescontoMO.UpdateAfterCallBack = true;
        }
    }

    protected void DescontoMaterialChanged(object sender, EventArgs e)
    {
        Anthem.TextBox txtDescontoMaterial = (Anthem.TextBox)sender;

        if (Util.IsDecimal(txtDescontoMaterial.Text))
        {
            Anthem.TextBox txtValorDescontoMaterial = (Anthem.TextBox)((GridViewRow)txtDescontoMaterial.Parent.Parent).FindControl("txtValorDescontoMaterial");
            GridViewRow row = (GridViewRow)txtValorDescontoMaterial.Parent.Parent;
            int id_orcamento = Convert.ToInt32(gvPesquisa.DataKeys[row.RowIndex]["ID"]);
            DelineamentoOrcamento orcamento = _orcamentos.Find(delegate(DelineamentoOrcamento match) { return match.ID == id_orcamento; });

            txtValorDescontoMaterial.Text =
                Util.GetValor(Convert.ToDecimal(txtDescontoMaterial.Text), orcamento.ValorTaxaContribuicaoOperacionalMaterial + orcamento.SubTotalMaterial).ToString("N2");
            txtValorDescontoMaterial.UpdateAfterCallBack = true;
        }
    }

    protected void ValorDescontoMaterialChanged(object sender, EventArgs e)
    {
        Anthem.TextBox txtValorDescontoMaterial = (Anthem.TextBox)sender;

        if (Util.IsDecimal(txtValorDescontoMaterial.Text))
        {
            Anthem.TextBox txtDescontoMaterial = (Anthem.TextBox)((GridViewRow)txtValorDescontoMaterial.Parent.Parent).FindControl("txtDescontoMaterial");
            

            GridViewRow row = (GridViewRow)txtValorDescontoMaterial.Parent.Parent;
            int id_orcamento = Convert.ToInt32(gvPesquisa.DataKeys[row.RowIndex]["ID"]);
            DelineamentoOrcamento orcamento = _orcamentos.Find(delegate(DelineamentoOrcamento match) { return match.ID == id_orcamento; });

            
            txtDescontoMaterial.Text =
                Util.GetPercentual(Convert.ToDecimal(txtValorDescontoMaterial.Text), orcamento.ValorTaxaContribuicaoOperacionalMaterial + orcamento.SubTotalMaterial).ToString("N2");
            txtDescontoMaterial.UpdateAfterCallBack = true;
        }
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
