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

public partial class frmOMFPendente : SortingPageBase
{
    protected NotaEntregaMaterialOMF _omf
    {
        get { return Session["frmOMFPendente._omf"] == null ? null : (NotaEntregaMaterialOMF)Session["frmOMFPendente._omf"]; }
        set { Session["frmOMFPendente._omf"] = value; }
    }
    
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
		this.RegisterSortingControl(this.gvPesquisa);
        this.gvPesquisa.RowDataBound += new GridViewRowEventHandler(gvPesquisa_RowDataBound);
        this.gvPesquisa.RowEditing += new GridViewEditEventHandler(gvPesquisa_RowEditing);
        ucMessageBox.MessageBoxClose += new UserControls_MessageBox.MessageBoxEventHandler(ucMessageBox_MessageBoxClose);
        ucInputBox.TextoInformado += new EventHandler(ucInputBox_TextoInformado);
        gvPesquisa.RowDeleting += new GridViewDeleteEventHandler(gvPesquisa_RowDeleting);
        ucJustificativa.JustificativaInformada += new EventHandler(ucJustificativa_JustificativaInformada);
    }

    void ucJustificativa_JustificativaInformada(object sender, EventArgs e)
    {
        NotaEntregaMaterialOMF omf = NotaEntregaMaterialOMF.Get(ucJustificativa.ID_Item);
        omf.Recusar(ID_Servidor, ucJustificativa.Justificativa);
        ucJustificativa.Close();
        Bind();
    }

    void gvPesquisa_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = Convert.ToInt32(gvPesquisa.DataKeys[e.RowIndex]["ID"]);
        ucJustificativa.Show(id);
    }

    

   

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            Anthem.Manager.Register(this);
           
            Bind();
        }
    }
    #endregion  
  
    protected override void Bind()
    {
        List<NotaEntregaMaterialOMF> list = NotaEntregaMaterialOMF.Select(this.ID_Servidor, Int32.MinValue);
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
        StatusOMFEnum status = (StatusOMFEnum)Convert.ToInt32(((Label)gvPesquisa.Rows[e.NewEditIndex].FindControl("lblID_Status")).Text);

        if (status == StatusOMFEnum.EmissaoTAV)
        {
            Anthem.AnthemClientMethods.Redirect("frmOMFItem.aspx?id_omf=" + id.ToString());
        }
        else if (status == StatusOMFEnum.SolicitarPericiaMaterial)
        {
            _omf = NotaEntregaMaterialOMF.Get(id);
            ucInputBox.Show(id, "Informe o número da mensagem de solicitação da perícia");
        }
        else if (status == StatusOMFEnum.AguardandoResultadoPericia)
        {
            Anthem.AnthemClientMethods.Redirect("frmOMFResponsavelPericia.aspx?id_omf=" + id.ToString());
        }
        else if (status == StatusOMFEnum.AguardandoProcessamento || 
            status == StatusOMFEnum.EnviadoMATCFN)
        {
            _omf = NotaEntregaMaterialOMF.Get(id);
            ucInputBox.Show(id, "Comentário");
        }
        else if (status == StatusOMFEnum.AguardandoEncaminhamentoFielMaterial ||
            status == StatusOMFEnum.AguardandoEncaminhamentoFielArmazenagem ||
            status == StatusOMFEnum.AguardandoEncaminhamentoPelaContabilidade ||
            status == StatusOMFEnum.AguardandoEncaminhamentoPelaSessaoControle)
        {
            _omf = NotaEntregaMaterialOMF.Get(id);
            ucMessageBox.Show("Confirma operação?", id);
        }
       
    }

    void ucMessageBox_MessageBoxClose(object sender, MessageBoxEventArgs e)
    {
        if (e.Result == MessageBoxResult.Sim)
        {

            if (_omf.Status.StatusOMFEnum == StatusOMFEnum.AguardandoEncaminhamentoFielMaterial ||
                _omf.Status.StatusOMFEnum == StatusOMFEnum.AguardandoEncaminhamentoFielArmazenagem ||
                _omf.Status.StatusOMFEnum == StatusOMFEnum.AguardandoEncaminhamentoPelaContabilidade ||
                _omf.Status.StatusOMFEnum == StatusOMFEnum.AguardandoEncaminhamentoPelaSessaoControle)
                
            {
                _omf.IrParaProximoStatus(this.ID_Servidor, null);
            }
            _omf = null;
            Bind();
        }
        _omf = null;
    }

    void ucInputBox_TextoInformado(object sender, EventArgs e)
    {
        if (_omf.Status.StatusOMFEnum == StatusOMFEnum.SolicitarPericiaMaterial)
        {
            _omf.SolicitarPericia(this.ID_Servidor, ucInputBox.Texto);
            
        }
        else if(_omf.Status.StatusOMFEnum == StatusOMFEnum.AguardandoProcessamento ||
            _omf.Status.StatusOMFEnum  == StatusOMFEnum.EnviadoMATCFN)
        {
            _omf.IrParaProximoStatus(this.ID_Servidor, ucInputBox.Texto);
        }
        _omf = null;
        ucInputBox.Close();
        Bind();
    }

    void gvPesquisa_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            NotaEntregaMaterialOMF omf = (NotaEntregaMaterialOMF)e.Row.DataItem;

            //if (ac.Status.StatusAutorizacaoCompraEnum == StatusAutorizacaoCompraEnum.Reprovado)
            //{
            //    e.Row.ForeColor = Color.Red;
            //    e.Row.ToolTip = ac.UltimoHistorico.Justificativa;

            //    e.Row.Attributes.Add("onmouseover", string.Format("Tip('<b>Justificativa:</b><br><br>{0}', SHADOW, true, PADDING, 7, FOLLOWMOUSE, false);",

            //    ac.UltimoHistorico.Justificativa));
            //}


            if (omf.UltimoHistorico != null && omf.UltimoHistorico.FlagReprovado)
            {
                e.Row.ForeColor = Color.Red;
                e.Row.ToolTip = omf.UltimoHistorico.Justificativa;

                e.Row.Attributes.Add("onmouseover", string.Format("Tip('<b>Justificativa:</b><br><br>{0}', SHADOW, true, PADDING, 7, FOLLOWMOUSE, false);",
                    omf.UltimoHistorico.Justificativa));
            }

            LinkButton btnDetalhes = (LinkButton)e.Row.FindControl("lnkDetalhes");
            Anthem.AnthemClientMethods.Popup(btnDetalhes, "fchOMF.aspx?id_omf=" + omf.ID, false, false, false, true, true, true, true, 10, 40, 700, 520, false);

            //string address;
            //if (ac.Status.StatusAutorizacaoCompraEnum == StatusAutorizacaoCompraEnum.AguardandoEntregaMercadoria || ac.Status.StatusAutorizacaoCompraEnum == StatusAutorizacaoCompraEnum.AguardandoImpressao)
            //    address = string.Format("fchAutorizacaoCompraAssinatura.aspx?id_ac={0}", ac.ID);
            //else
            //    address = string.Format("fchAutorizacaoCompraCompleto.aspx?id_ac={0}", ac.ID);
            //Anthem.AnthemClientMethods.Popup(btnDetalhes, address, false, false, false, true, true, true, true, 10, 40, 700, 520, false);

            //e.Row.Cells[1].Attributes.Add("onmouseover", string.Format("Tip('<br>{0}', SHADOW, true, PADDING, 7, FOLLOWMOUSE, false);",
            //      GetTextoComentarios(ac)));
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
 
    
}
