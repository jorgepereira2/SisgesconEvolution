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

public partial class frmPedidoServicoMergulhoPesquisa : SortingPageBase
{
   
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.btnPesquisar.Click += new EventHandler(btnPesquisar_Click);
		this.RegisterSortingControl(this.gvPesquisa);
        gvPesquisa.RowDataBound += new GridViewRowEventHandler(gvPesquisa_RowDataBound);
        this.btnEnviar.Click += BtnEnviar_OnClick;
    }

    void gvPesquisa_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btnDetalhes = (LinkButton)e.Row.FindControl("lnkDetalhes");
            LinkButton btnEditar = (LinkButton)e.Row.FindControl("lnkEditar");
            PedidoServicoMergulho pedido = (PedidoServicoMergulho)e.Row.DataItem;
            Anthem.AnthemClientMethods.Popup(btnDetalhes, "fchPedidoServicoMergulho.aspx?id_pedido=" + pedido.ID.ToString(),
            false, false, false, true, true, true, true, 10, 40, 700, 520, false);

            string address = "frmPedidoServicoMergulhoCadastro.aspx?id_pedido=" + pedido.ID.ToString();

            Anthem.AnthemClientMethods.Redirect(address, btnEditar);
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
            Anthem.AnthemClientMethods.Redirect("frmPedidoServicoMergulhoCadastro.aspx", btnNovo);

        }
    }
    #endregion  
    
    protected override void Bind()
    {

        List<PedidoServicoMergulho> list = PedidoServicoMergulho.Select(
                txtTexto.Text,
                IsNull(txtDataInicio.Text, DateTime.MinValue),
                IsNull(txtDataFim.Text, DateTime.MinValue),
                Convert.ToInt32(StatusPedidoServicoMergulhoEnum.NaoEnviado),
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

    private void BtnEnviar_OnClick(object sender, EventArgs e)
    {
        int count = 0;
        foreach (GridViewRow gridViewRow in gvPesquisa.Rows)
        {
            if (gridViewRow.RowType == DataControlRowType.DataRow)
            {
                CheckBox chk = (CheckBox)gridViewRow.FindControl("chkEnviar");
                if (chk.Checked)
                {
                    count++;
                    PedidoServicoMergulho ps = PedidoServicoMergulho.Get(Convert.ToInt32(gvPesquisa.DataKeys[gridViewRow.RowIndex][0]));
                    ps.Enviar(this.ID_Servidor);
                }
            }
        }
        if (count == 0)
        {
            ShowMessage("Nenhum pedido foi selecionado.");
            return;
        }
        Bind();
    }
}
