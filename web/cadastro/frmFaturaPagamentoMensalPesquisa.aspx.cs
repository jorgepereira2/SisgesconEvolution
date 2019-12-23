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

public partial class frmFaturaPagamentoMensalPesquisa : SortingPageBase
{
   
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.btnPesquisar.Click += new EventHandler(btnPesquisar_Click);
		this.RegisterSortingControl(this.gvPesquisa);
        gvPesquisa.RowDataBound += new GridViewRowEventHandler(gvPesquisa_RowDataBound);
    }

    void gvPesquisa_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btnDetalhes = (LinkButton)e.Row.FindControl("lnkDetalhes");
            LinkButton btnEditar = (LinkButton)e.Row.FindControl("lnkEditar");
            FaturaPagamentoMensal fatura = (FaturaPagamentoMensal)e.Row.DataItem;
            //Anthem.AnthemClientMethods.Popup(btnDetalhes, "fchPedidoServicoMergulho.aspx?id_pedido=" + pedido.ID.ToString(),
            //false, false, false, true, true, true, true, 10, 40, 700, 520, false);

            string address = "frmFaturaPagamentoMensalCadastro.aspx?id_fatura=" + fatura.ID;

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
            Anthem.AnthemClientMethods.Redirect("frmFaturaPagamentoMensalCadastro.aspx", btnNovo);

        }
    }
    #endregion  
    
    protected override void Bind()
    {

        List<FaturaPagamentoMensal> list = FaturaPagamentoMensal.Select(
                txtTexto.Text,
                IsNull(txtDataEmissaoInicio.Text, DateTime.MinValue),
                IsNull(txtDataEmissaoFim.Text, DateTime.MinValue),
                IsNull(txtDataVencimentoInicio.Text, DateTime.MinValue),
                IsNull(txtDataVencimentoFim.Text, DateTime.MinValue));
       
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
}
