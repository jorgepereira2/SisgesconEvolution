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

public partial class frmPedidoAquisicaoPesquisa : SortingPageBase
{
   
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.btnPesquisar.Click += new EventHandler(btnPesquisar_Click);
		this.RegisterSortingControl(this.gvPesquisa);
		this.gvPesquisa.RowDataBound += GvPesquisa_OnRowDataBound;
    }

    private void GvPesquisa_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if(e.Row.RowType == DataControlRowType.DataRow)
        {
            PedidoAquisicao pedido = (PedidoAquisicao) e.Row.DataItem;

            if (pedido.FlagRecusado)
            {
                e.Row.ForeColor = Color.Red;
                e.Row.ToolTip = pedido.UltimoHistorico.Justificativa;
                e.Row.Attributes.Add("onmouseover",
                                     string.Format(
                                         "Tip('<b>Justificativa:</b><br><br>{0}', SHADOW, true, PADDING, 7, FOLLOWMOUSE, false);",
                                         pedido.UltimoHistorico.Justificativa));
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
			Anthem.AnthemClientMethods.Redirect("frmPedidoAquisicaoCadastro.aspx", btnNovo);

            BindPedidosRecusados();

            Util.FillDropDownList(ddlStatus, StatusPedidoAquisicao.List(), "Todos");
        }
    }

    private void BindPedidosRecusados()
    {
        List<PedidoAquisicao> list = PedidoAquisicao.SelectPedidosRecusados(ID_Servidor);
        if (list.Count == 0) return;
        gvPesquisa.DataSource = list;
        gvPesquisa.DataBind();
    }

    #endregion  
    
    protected override void Bind()
    {
        List<PedidoAquisicao> list = PedidoAquisicao.Select(
            txtTexto.Text,
		    IsNull(txtDataInicio.Text, DateTime.MinValue), 
		    IsNull(txtDataFim.Text, DateTime.MinValue),
            this.ID_Servidor,
            Convert.ToInt32(ddlStatus.SelectedValue));
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
