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

public partial class frmResponsavelEtapaPesquisa : SortingPageBase
{
   
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
		this.RegisterSortingControl(this.gvPesquisa);
        this.gvPesquisa.RowDataBound += new GridViewRowEventHandler(gvPesquisa_RowDataBound);
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
		List<StatusPedidoServico> list = StatusPedidoServico.Select();
		this.Sort(list);
		gvPesquisa.DataSource = list;
        gvPesquisa.DataBind();
        gvPesquisa.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();

        gvPesquisa.Visible = list.Count > 0;
        pnMensagem.Visible = list.Count == 0;
        pnMensagem.UpdateAfterCallBack = true;
    }

    void gvPesquisa_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if(e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton link = (LinkButton) e.Row.FindControl("btnEditar");
            StatusPedidoServico status = (StatusPedidoServico) e.Row.DataItem;
            string address = "";
            if(status.FlagVinculoPorDivisao)
                address = "frmStatusPedidoServicoDivisao.aspx";
            else
                address = "frmStatusPedidoServicoResponsavel.aspx";
                
            
            Anthem.AnthemClientMethods.Redirect(address + "?id_status=" + status.ID.ToString(), link);
        }
    }
}
