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

public partial class frmAutorizacaoCompraPendente : SortingPageBase
{
    protected AutorizacaoCompra _ac
    {
        get { return Session["frmAutorizacaoCompraPendente._ac"] == null ? null : (AutorizacaoCompra)Session["frmAutorizacaoCompraPendente._ac"]; }
        set { Session["frmAutorizacaoCompraPendente._ac"] = value; }
    }
    
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
		this.RegisterSortingControl(this.gvPesquisa);
        this.gvPesquisa.RowDataBound += new GridViewRowEventHandler(gvPesquisa_RowDataBound);
        this.gvPesquisa.RowEditing += new GridViewEditEventHandler(gvPesquisa_RowEditing);
        this.ucNotaEmpenho.NotaInformada += new EventHandler(ucNotaEmpenho_NotaInformada);
        this.ucNotaEmpenho.OperacaoCancelada += new EventHandler(ucNotaEmpenho_OperacaoCancelada);
        ucNaturezaDespesa.NaturezaInformada += new EventHandler(ucNaturezaDespesa_NaturezaInformada);
        ucMessageBox.MessageBoxClose += new UserControls_MessageBox.MessageBoxEventHandler(ucMessageBox_MessageBoxClose);
        btnPesquisar.Click += new EventHandler(btnPesquisar_Click);
    }

    void btnPesquisar_Click(object sender, EventArgs e)
    {
        Bind();
    }

    void ucMessageBox_MessageBoxClose(object sender, MessageBoxEventArgs e)
    {
        if(e.Result == MessageBoxResult.Sim)
        {
            _ac.IrParaProximoStatus(ID_Servidor, null);
            _ac = null;
            Bind();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            Anthem.Manager.Register(this);
            Util.FillDropDownList(ddlComprador, Servidor.List(FuncaoServidor.Comprador), "Todos");
            Util.FillDropDownList(ddlFonteRecurso, FonteRecurso.List(), "Todas");
            Bind();
        }
    }
    #endregion  
  
    protected override void Bind()
    {
        List<AutorizacaoCompra> list = AutorizacaoCompra.Select(this.ID_Servidor, Int32.MinValue, 
            Convert.ToInt32(ddlComprador.SelectedValue), Convert.ToInt32(ddlFonteRecurso.SelectedValue));
		this.Sort(list);
		gvPesquisa.DataSource = list;
		gvPesquisa.DataKeyNames = new string[]{"ID"};
        gvPesquisa.DataBind();
        gvPesquisa.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();

        gvPesquisa.Visible = list.Count > 0;
        pnMensagem.Visible = list.Count == 0;
        pnMensagem.UpdateAfterCallBack = true;
    }


    void gvPesquisa_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int id = Convert.ToInt32(gvPesquisa.DataKeys[e.NewEditIndex]["ID"]);
        StatusAutorizacaoCompraEnum status = (StatusAutorizacaoCompraEnum) Convert.ToInt32( ((Label)gvPesquisa.Rows[e.NewEditIndex].FindControl("lblID_Status")).Text);
        
        if(status == StatusAutorizacaoCompraEnum.AguardandoAprovacaoEncarregadoObtencao ||
            status == StatusAutorizacaoCompraEnum.AguardandoAprovacaoEncarregadoObtencao)
        {
            Anthem.AnthemClientMethods.Redirect("frmAutorizacaoCompraAprovacao.aspx?id_pedido=" + id.ToString());
        }
        else if(status == StatusAutorizacaoCompraEnum.AguardandoNotaEmpenho)
        {
            _ac = AutorizacaoCompra.Get(id);
           // ucNotaEmpenho.Show(_ac);
        }
        else if (status == StatusAutorizacaoCompraEnum.AguardandoDesignacaoDivisaoApoio)
        {
            _ac = AutorizacaoCompra.Get(id);
            ucNaturezaDespesa.Show(_ac);
        }
        else if (status == StatusAutorizacaoCompraEnum.AguardandoImpressao)
        {
            _ac = AutorizacaoCompra.Get(id);
            ucMessageBox.Show("A AC foi impressa com sucesso?", null);
        }
    }

    void gvPesquisa_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            AutorizacaoCompra ac = (AutorizacaoCompra)e.Row.DataItem;

            if (ac.Status.StatusAutorizacaoCompraEnum == StatusAutorizacaoCompraEnum.Reprovado)
            {
                e.Row.ForeColor = Color.Red;
                e.Row.ToolTip = ac.UltimoHistorico.Justificativa;

                e.Row.Attributes.Add("onmouseover", string.Format("Tip('<b>Justificativa:</b><br><br>{0}', SHADOW, true, PADDING, 7, FOLLOWMOUSE, false);",

                ac.UltimoHistorico.Justificativa));
            }


            if (ac.UltimoHistorico != null && ac.UltimoHistorico.FlagReprovado)
            {
                e.Row.ForeColor = Color.Red;
                e.Row.ToolTip = ac.UltimoHistorico.Justificativa;

                e.Row.Attributes.Add("onmouseover", string.Format("Tip('<b>Justificativa:</b><br><br>{0}', SHADOW, true, PADDING, 7, FOLLOWMOUSE, false);",
                    ac.UltimoHistorico.Justificativa));
            }

            LinkButton btnDetalhes = (LinkButton)e.Row.FindControl("lnkDetalhes");


            string address;
            if (ac.Status.StatusAutorizacaoCompraEnum == StatusAutorizacaoCompraEnum.AguardandoEntregaMercadoria || 
                ac.Status.StatusAutorizacaoCompraEnum == StatusAutorizacaoCompraEnum.AguardandoImpressao ||
                ac.Status.StatusAutorizacaoCompraEnum == StatusAutorizacaoCompraEnum.AguardandoNotaEmpenho)
                address = string.Format("fchAutorizacaoCompraAssinatura.aspx?id_ac={0}", ac.ID);
            else
                address = string.Format("fchAutorizacaoCompraCompleto.aspx?id_ac={0}", ac.ID);
            Anthem.AnthemClientMethods.Popup(btnDetalhes, address, false, false, false, true, true, true, true, 10, 40, 700, 520, false);

            e.Row.Cells[1].Attributes.Add("onmouseover", string.Format("Tip('<br>{0}', SHADOW, true, PADDING, 7, FOLLOWMOUSE, false);",
                  GetTextoComentarios(ac)));
        }
    }

    private static string GetTextoComentarios(AutorizacaoCompra ac)
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

    #region Nota Empenho
    void ucNotaEmpenho_NotaInformada(object sender, EventArgs e)
    {

        //NaturezaDespesa natureza = NaturezaDespesa.Get(Convert.ToInt32(ucNotaEmpenho.ID_NaturezaDespesa));
        //if (natureza != null && natureza.GetPai() == null)
        //{
        //    ShowMessage("A natureza de despeza selecionada não possui natureza principal.");
        //    return;
        //}
        _ac.Lista = ucNotaEmpenho.Lista;
        //_ac.NumeroLancamento = ucNotaEmpenho.NumeroLancamento;
        _ac.CodigoGestao = ucNotaEmpenho.CodigoGestao;
        _ac.Projeto = Projeto.Get(Convert.ToInt32(ucNotaEmpenho.ID_Projeto));
        _ac.PTRES = PTRES.Get(Convert.ToInt32(ucNotaEmpenho.ID_PTRES));
       // _ac.NaturezaDespesa = natureza;
        _ac.GerarNotaEmpenho(ID_Servidor, ucNotaEmpenho.NumeroNotaEmpenho, ucNotaEmpenho.Comentario);

        ucNotaEmpenho.Close();
        _ac = null;
        Bind();
    }

    void ucNotaEmpenho_OperacaoCancelada(object sender, EventArgs e)
    {
        _ac = null;
    }
    #endregion

    void ucNaturezaDespesa_NaturezaInformada(object sender, EventArgs e)
    {
        _ac.DesiginacaoDivisaoApoio(ID_Servidor, Convert.ToInt32(ucNaturezaDespesa.ID_NaturezaDespesa), Convert.ToInt32(ucNaturezaDespesa.ID_PTRES), ucNaturezaDespesa.Comentario);
        ucNaturezaDespesa.Close();
        _ac = null;
        Bind();
    }
    
}
