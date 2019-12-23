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

public partial class frmPedidoObtencaoEdicao : SortingPageBase
{

   
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.btnPesquisar.Click += new EventHandler(btnPesquisar_Click);
		this.RegisterSortingControl(this.gvPesquisa);
		this.gvPesquisa.RowDataBound += GvPesquisa_OnRowDataBound;

        gvPesquisa.RowEditing += new GridViewEditEventHandler(gvPesquisa_RowEditing);
        gvPesquisa.RowCancelingEdit += new GridViewCancelEditEventHandler(gvPesquisa_RowCancelingEdit);
        gvPesquisa.RowUpdating += new GridViewUpdateEventHandler(gvPesquisa_RowUpdating);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            Anthem.Manager.Register(this);

			Util.FillDropDownList(ddlStatus, StatusPedidoObtencao.List(), "Todos");
            Util.FillDropDownList(ddlAno, DateTimeManager.Anos(DateTime.Today.AddYears(-6).Year, DateTime.Today.Year), "Todos");
            ddlAno.SelectedValue = DateTime.Today.Year.ToString();
            
            Util.FillDropDownList(ddlCelula, Celula.List(), "Todas");
            

          
        }
    }

   
    #endregion  
    
    protected override void Bind()
    {
        TipoPedido? tipoPedido = null;
        if(Request["pm"] == null)
            tipoPedido = TipoPedido.PedidoObtencao;
        else 
            tipoPedido = TipoPedido.PedidoMaterial;

        List<PedidoObtencao> list = PedidoObtencao.Select(
            txtTexto.Text,
		    IsNull(txtDataInicio.Text, DateTime.MinValue), 
		    IsNull(txtDataFim.Text, DateTime.MinValue),
		    Convert.ToInt32(ddlStatus.SelectedValue),
		    Convert.ToInt32(ddlCelula.SelectedValue),
		    txtAplicacao.Text,
            tipoPedido,
            0,
            Convert.ToInt32(ddlAno.SelectedValue));
		this.Sort(list);
		gvPesquisa.DataSource = list;
        gvPesquisa.DataKeyNames = new string[] { "ID" };
        gvPesquisa.DataBind();
        gvPesquisa.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();

        gvPesquisa.Visible = list.Count > 0;
        pnMensagem.Visible = list.Count == 0;
        pnMensagem.UpdateAfterCallBack = true;
    }

    private void GvPesquisa_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowState == DataControlRowState.Edit)
        {
            PedidoObtencao pedido = (PedidoObtencao)e.Row.DataItem;
            
            Anthem.DropDownList ddlServidor = (Anthem.DropDownList)e.Row.FindControl("ddlServidor");
            Util.FillDropDownList(ddlServidor, Servidor.List(null));
            ddlServidor.UpdateAfterCallBack = true;
            ddlServidor.SelectedValue = pedido.Servidor.ID.ToString();
        }
    }

    void btnPesquisar_Click(object sender, EventArgs e)
    {
        Bind();
    }

    void gvPesquisa_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        PedidoObtencao po = PedidoObtencao.Get(Convert.ToInt32(gvPesquisa.DataKeys[e.RowIndex]["ID"]));

        DropDownList ddlServidor = (DropDownList)gvPesquisa.Rows[e.RowIndex].FindControl("ddlServidor");

        po.Servidor = Servidor.Get(Convert.ToInt32(ddlServidor.SelectedValue));
        po.Alterar();

        gvPesquisa.EditIndex = -1;
        Bind();
    }

    void gvPesquisa_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvPesquisa.EditIndex = -1;
        Bind();
    }

    void gvPesquisa_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvPesquisa.EditIndex = e.NewEditIndex;
        Bind();
    }
}
