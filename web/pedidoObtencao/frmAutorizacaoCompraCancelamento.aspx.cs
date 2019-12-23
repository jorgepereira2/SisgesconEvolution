using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Drawing;
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

public partial class frmAutorizacaoCompraCancelamento : SortingPageBase
{
   
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.btnPesquisar.Click += new EventHandler(btnPesquisar_Click);
		this.RegisterSortingControl(this.gvPesquisa);
		this.gvPesquisa.RowDataBound += GvPesquisa_OnRowDataBound;
        this.ucMotivoCancelamento.OkClick += new EventHandler(ucMotivoCancelamento_OkClick);
        gvPesquisa.RowDeleting += new GridViewDeleteEventHandler(gvPesquisa_RowDeleting);   
    }

    private void GvPesquisa_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if(e.Row.RowType == DataControlRowType.DataRow)
        {
            AutorizacaoCompra ac = (AutorizacaoCompra) e.Row.DataItem;
            LinkButton btnDetalhes = (LinkButton)e.Row.FindControl("lnkDetalhes");
            
            Anthem.AnthemClientMethods.Popup(btnDetalhes, "fchAutorizacaoCompra.aspx?id_ac=" + ac.ID.ToString(),
            false, false, false, true, true, true, true, 10, 40, 700, 520, false);

            if (ac.Status.StatusAutorizacaoCompraEnum == StatusAutorizacaoCompraEnum.Cancelado)
            {
                string str = string.Format("<b>Motivo:</b><br>{0}", ac.MotivoCancelamento);

                e.Row.Cells[3].Attributes.Add("onmouseover",
                                              string.Format("Tip('{0}', SHADOW, true, PADDING, 7, FOLLOWMOUSE, false);",
                                                            str));
            }
        }
    }

    void btnPesquisar_Click(object sender, EventArgs e)
    {
        Bind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            Anthem.Manager.Register(this);

            Util.FillDropDownList(ddlComprador, Servidor.List(FuncaoServidor.Comprador), "Todos");
            Util.FillDropDownList(ddlStatus, StatusAutorizacaoCompra.List(), "Todos");
        }
    }
    #endregion  
    
    protected override void Bind()
    {
        List<AutorizacaoCompra> list = AutorizacaoCompra.Select(
            txtTexto.Text,
            IsNull(txtDataInicio.Text, DateTime.MinValue),
            IsNull(txtDataFim.Text, DateTime.MinValue),
            Convert.ToInt32(ddlStatus.SelectedValue),
            Convert.ToInt32(ddlComprador.SelectedValue));
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

    void gvPesquisa_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = Convert.ToInt32(gvPesquisa.DataKeys[e.RowIndex]["ID"]);
        ucMotivoCancelamento.Show(id);
    }

    void ucMotivoCancelamento_OkClick(object sender, EventArgs e)
    {
        AutorizacaoCompra ac = AutorizacaoCompra.Get(ucMotivoCancelamento.ID_Objeto);
        ac.Cancelar(ID_Servidor, ucMotivoCancelamento.ID_MotivoCancelamento);
        ucMotivoCancelamento.Close();
        Bind();
    }
}
