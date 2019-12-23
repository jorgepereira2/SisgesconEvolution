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

public partial class frmLicitacaoPesquisa : SortingPageBase
{
   
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.btnPesquisar.Click += new EventHandler(btnPesquisar_Click);
		this.RegisterSortingControl(this.gvPesquisa);
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

            if (Request["pedido"] == null)
            {
                Anthem.AnthemClientMethods.Redirect("frmLicitacaoCadastro.aspx", btnNovo);
                gvPesquisa.Columns[gvPesquisa.Columns.Count - 1].Visible = false;
            }
            else
            {
                Anthem.AnthemClientMethods.Redirect("frmLicitacaoCadastro.aspx?pedido=true", btnNovo);
                gvPesquisa.Columns[gvPesquisa.Columns.Count - 2].Visible = false;
            }
        }
    }
    #endregion  
    
    protected override void Bind()
    {
        List<Licitacao> list;
        if(Request["pedido"] == null)
		    list = Licitacao.Select(IsNull(txtDataInicio.Text, DateTime.MinValue), IsNull(txtDataFim.Text, DateTime.MinValue), null);
        else
            list = Licitacao.Select(IsNull(txtDataInicio.Text, DateTime.MinValue), IsNull(txtDataFim.Text, DateTime.MinValue), StatusLicitacaoEnum.PedidoLicitacao);

		this.Sort(list);
		gvPesquisa.DataSource = list;
        gvPesquisa.DataBind();
        gvPesquisa.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();

        gvPesquisa.Visible = list.Count > 0;
        pnMensagem.Visible = list.Count == 0;
        pnMensagem.UpdateAfterCallBack = true;
    }  
}
