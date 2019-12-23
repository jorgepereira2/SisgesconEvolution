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

public partial class frmEntradaACPesquisa : SortingPageBase
{
    #region Initialization

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
		this.RegisterSortingControl(this.gvPesquisa);
        gvPesquisa.RowDataBound += new GridViewRowEventHandler(gvPesquisa_RowDataBound);
    }

    void gvPesquisa_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if(e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkAC = (LinkButton) e.Row.FindControl("lnkAC");

            PedidoObtencao ac = (PedidoObtencao)e.Row.DataItem;

            string address = string.Format("fchPedidoObtencaoCompleto.aspx?id_ac={0}", ac.ID);
            Anthem.AnthemClientMethods.Popup(lnkAC, address, false, false, false, true, true, true, true, 10, 40, 700, 520, false);
        }
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
   
    #region Bind

    protected override void Bind()
    {
        List<PedidoObtencao> list = PedidoObtencao.SelectEntradasPendentes();

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

    #endregion
}
