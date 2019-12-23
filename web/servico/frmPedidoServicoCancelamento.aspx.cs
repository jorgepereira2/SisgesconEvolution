using System;
using System.Data;
using System.Configuration;
using System.Collections;
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

public partial class frmPedidoServicoCancelamento : SortingPageBase
{
   
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.btnPesquisar.Click += new EventHandler(btnPesquisar_Click);
		this.RegisterSortingControl(this.gvPesquisa);
        gvPesquisa.RowDataBound += new GridViewRowEventHandler(gvPesquisa_RowDataBound);
        gvPesquisa.RowDeleting += new GridViewDeleteEventHandler(gvPesquisa_RowDeleting);
        this.ucMotivoCancelamento.OkClick += new EventHandler(ucMotivoCancelamento_OkClick);
        gvPesquisa.RowEditing += new GridViewEditEventHandler(gvPesquisa_RowEditing);
        this.ucReativar.OkClick += ucReativar_OkClick;
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            Anthem.Manager.Register(this);                        
        }
    }
    #endregion  
    
    void gvPesquisa_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btnDetalhes = (LinkButton)e.Row.FindControl("lnkDetalhes");
            PedidoServico pedido = (PedidoServico)e.Row.DataItem;
            Anthem.AnthemClientMethods.Popup(btnDetalhes, "fchPedidoServico.aspx?id_pedido=" + pedido.ID.ToString(),
            false, false, false, true, true, true, true, 10, 40, 700, 520, false);

            if (pedido.Status.StatusPedidoServicoEnum == StatusPedidoServicoEnum.Cancelado)
            {
                string str = string.Format("<b>Motivo:</b><br>{0}", pedido.MotivoCancelamento);
               
                e.Row.Cells[3].Attributes.Add("onmouseover",
                                              string.Format("Tip('{0}', SHADOW, true, PADDING, 7, FOLLOWMOUSE, false);",
                                                            str));
            }
            else
            {
                LinkButton lnkAtivar = (LinkButton)e.Row.FindControl("lnkAtivar");
                lnkAtivar.Visible = false;
            }
        }
    }

    void btnPesquisar_Click(object sender, EventArgs e)
    {
        Bind();
    }
    
    protected override void Bind()
    {
        List<PedidoServico> list = PedidoServico.Select(
            txtTexto.Text,
            IsNull(txtDataInicio.Text, DateTime.MinValue),
            IsNull(txtDataFim.Text, DateTime.MinValue),
            Int32.MinValue,
            Int32.MinValue,
            Int32.MinValue,
            "",
            "",
            Int32.MinValue);
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
        PedidoServico ps = PedidoServico.Get(ucMotivoCancelamento.ID_Objeto);
        ps.Cancelar(ID_Servidor, ucMotivoCancelamento.ID_MotivoCancelamento, ucMotivoCancelamento.Comentario);
        ucMotivoCancelamento.Close();
        Bind();
    }

    void gvPesquisa_RowEditing(object sender, GridViewEditEventArgs e)
    {
        ucReativar.Show(Convert.ToInt32(gvPesquisa.DataKeys[e.NewEditIndex]["ID"]));
    }

    void ucReativar_OkClick(object sender, EventArgs e)
    {
        PedidoServico ps = PedidoServico.Get(ucReativar.ID_Objeto);
        ps.Reativar(ID_Servidor, ucReativar.ExcluirOrcamentos);

        Bind();
        ucReativar.Close();
        ShowMessage(string.Format("O novo código do PS reativado é {0}.", ps.CodigoComAno));
    }
}
