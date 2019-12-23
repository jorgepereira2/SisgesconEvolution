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
using Marinha.Business.UI;
using Shared.SessionState;
using ComponentArt.Web.UI;
using Shared.Common;

public partial class frmFaturamento : SortingPageBase
{
    #region Initialization

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.btnPesquisar.Click += new EventHandler(btnPesquisar_Click);
        dlOrcamento.ItemCommand += new DataListCommandEventHandler(dlOrcamento_ItemCommand);
        ucFaturamento.FaturamentoInserido += new EventHandler(ucFaturamento_FaturamentoInserido);
        
    }

    void ucFaturamento_FaturamentoInserido(object sender, EventArgs e)
    {
        Bind();
    }

    protected void dlOrcamento_ItemCommand(object source, CommandEventArgs e)
    {
        if(e.CommandName == "Faturar")
        {
            ucFaturamento.Show(Convert.ToInt32(e.CommandArgument));
        }
    }


	[Anthem.Method]
    public void btnPesquisar_Click(object sender, EventArgs e)
    {
        Bind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            Anthem.Manager.Register(this);            
        
			Util.FillDropDownList(ddlStatus, StatusPedidoServico.List(), "Todos");
            Util.FillDropDownList(ddlAno, DateTimeManager.Anos(2008, DateTime.Today.AddYears(1).Year),"Todos");

            ddlAno.SelectedValue = DateTime.Today.Year.ToString();
            txtDataInicio.Text = DateTime.Today.AddMonths(-1).ToShortDateString();
            txtDataFim.Text = DateTime.Today.ToShortDateString();
            RegisterDeleteScript();
        }
    }

	#endregion     
    
    #region Bind

    protected override void Bind()
    {
        bool? apenasFaturados = null;
        if (ddlJaFaturados.SelectedValue != "0")
            apenasFaturados = Convert.ToBoolean(ddlJaFaturados.SelectedValue);
        List<DelineamentoOrcamento> list = DelineamentoOrcamento.SelectParaFaturamento(
            txtTexto.Text,
            IsNull(txtDataInicio.Text, DateTime.MinValue),
            IsNull(txtDataFim.Text, DateTime.MinValue),
            Convert.ToInt32(ddlStatus.SelectedValue),
            apenasFaturados,
            Convert.ToInt32(ddlAno.SelectedValue));

        dlOrcamento.DataSource = list;
        dlOrcamento.DataBind();

        dlOrcamento.Visible = list.Count > 0;
		pnMensagem.Visible = list.Count == 0;

        dlOrcamento.UpdateAfterCallBack = true;
		Anthem.AnthemClientMethods.ResizeIFrame();
	       
        pnMensagem.UpdateAfterCallBack = true;
        
    }

    #endregion

    #region Item Data Bound

    protected void dlOrcamento_ItemDataBound(object sender, DataListItemEventArgs e)
	{
		if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
		{
			GridView gvFaturamento = (GridView)e.Item.FindControl("gvFaturamento");
            DelineamentoOrcamento orcamento = (DelineamentoOrcamento)e.Item.DataItem;

		    gvFaturamento.DataSource = orcamento.Faturamentos;
            gvFaturamento.DataBind();

			//Atacha o script para expandir a tabela
			Image img = (Image)e.Item.FindControl("imgDetalheOrcamento");
			HtmlGenericControl div = (HtmlGenericControl)e.Item.FindControl("divFaturamento");
			//HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("trOrcamento");

			img.Attributes.Add("onclick", "Mostrar('" + div.ClientID + "', '" + img.ClientID + "');");
		}
	}

    protected void gvFaturamento_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DelineamentoOrcamentoFaturamento faturamento = (DelineamentoOrcamentoFaturamento)e.Row.DataItem;
            LinkButton lnkImprimir = (LinkButton) e.Row.FindControl("lnkImprimir");

            Anthem.AnthemClientMethods.Popup(lnkImprimir, "fchFaturamento.aspx?id_faturamento=" + faturamento.ID.ToString(),
                false, false, false, true, true, true, true, 40, 80, 700, 500, false);
        }
    }

	#endregion

    #region #xcluir

    [Anthem.Method]
    public void Excluir(int id_faturamento)
    {
        DelineamentoOrcamentoFaturamento fatura = DelineamentoOrcamentoFaturamento.Get(id_faturamento);
        fatura.Delete();
        Bind();
    }

    #endregion
}