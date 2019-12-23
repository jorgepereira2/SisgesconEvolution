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

public partial class frmFamodPesquisa : SortingPageBase
{

    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.btnPesquisar.Click += new EventHandler(btnPesquisar_Click);
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
        
			Util.FillDropDownList(ddlServidor, Servidor.List(null),"Todos");
            Util.FillDropDownList(ddlOficina, Celula.ListCelulasSubordinadas(this.ID_Servidor, true));
            Util.FillDropDownList(ddlSituacao, typeof(SituacaoFAMOD), "Todas");
            Anthem.AnthemClientMethods.Redirect("frmFamodCadastro.aspx", btnNovo);

            txtDataInicio.Text = DateTime.Today.AddMonths(-1).ToShortDateString();
            txtDataFim.Text = DateTime.Today.ToShortDateString();
        }
    }
	#endregion     
    
    protected override void Bind()
    {
        Servidor servidor = Servidor.Get(this.ID_Servidor);
        List<FamodOficina> list = Famod.SelectAgrupado(
            Convert.ToInt32(ddlOficina.SelectedValue),
            Convert.ToInt32(ddlServidor.SelectedValue),
            IsNull(txtDataInicio.Text, DateTime.MinValue),
            IsNull(txtDataFim.Text, DateTime.MinValue),
            Convert.ToInt32(ddlSituacao.SelectedValue));

        dlOficina.DataSource = list;
        dlOficina.DataBind();

        dlOficina.Visible = list.Count > 0;
		pnMensagem.Visible = list.Count == 0;
		
		dlOficina.UpdateAfterCallBack = true;
		Anthem.AnthemClientMethods.ResizeIFrame();
	       
        pnMensagem.UpdateAfterCallBack = true;
        
    }

	#region Item Data Bound
	
	protected void dlOficina_ItemDataBound(object sender, DataListItemEventArgs e)
	{
		if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
		{			
			DataList dlServidor = (DataList)e.Item.FindControl("dlServidor");
            FamodOficina oficina = (FamodOficina)e.Item.DataItem;

			dlServidor.DataSource = oficina.Servidores;
            dlServidor.DataBind();

			//Atacha o script para expandir a tabela
			Image img = (System.Web.UI.WebControls.Image)e.Item.FindControl("imgDetalheOficina");
			HtmlGenericControl div = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("divServidor");
			HtmlTableRow tr = (System.Web.UI.HtmlControls.HtmlTableRow)e.Item.FindControl("trOficina");
			tr.Attributes.Add("onclick", "Mostrar('" + div.ClientID + "', '" + img.ClientID + "');");
		}
	}

	protected void dlServidor_ItemDataBound(object sender, DataListItemEventArgs e)
	{
		if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
		{
			GridView gvFamod = (GridView)e.Item.FindControl("gvFamod");
            FamodServidor servidor = (FamodServidor)e.Item.DataItem;

			gvFamod.DataSource = servidor.Famods;
            gvFamod.DataBind();

			//Atacha o script para expandir a tabela
			System.Web.UI.WebControls.Image img = (System.Web.UI.WebControls.Image)e.Item.FindControl("imgDetalheServidor");
			System.Web.UI.HtmlControls.HtmlGenericControl div = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("divFamod");
			System.Web.UI.HtmlControls.HtmlTableRow tr = (System.Web.UI.HtmlControls.HtmlTableRow)e.Item.FindControl("trServidor");

			tr.Attributes.Add("onclick", "Mostrar('" + div.ClientID + "', '" + img.ClientID + "');");
		}
	}
	#endregion
}
