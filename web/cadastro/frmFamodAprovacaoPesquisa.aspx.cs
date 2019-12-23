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

public partial class frmFamodAprovacaoPesquisa : SortingPageBase
{

    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        btnAprovar.Click += new EventHandler(btnAprovar_Click);
        btnPesquisar.Click += new EventHandler(btnPesquisar_Click);
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
            Util.FillDropDownList(ddlOficina, Celula.List(null, true), "Todas");
            Bind();
        }
    }
	#endregion     
    
    protected override void Bind()
    {
        List<FamodOficina> list = Famod.SelectAgrupadoParaAprovacao(this.ID_Servidor, Convert.ToInt32(ddlOficina.SelectedValue));

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
            img.Attributes.Add("onclick", "Mostrar('" + div.ClientID + "', '" + img.ClientID + "');");
		}
	}

	protected void dlServidor_ItemDataBound(object sender, DataListItemEventArgs e)
	{
		if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
		{
			GridView gvFamod = (GridView)e.Item.FindControl("gvFamod");
            FamodServidor servidor = (FamodServidor)e.Item.DataItem;

			gvFamod.DataSource = servidor.Famods;
			gvFamod.DataKeyNames = new string[]{"ID"};
            gvFamod.DataBind();

			//Atacha o script para expandir a tabela
			System.Web.UI.WebControls.Image img = (System.Web.UI.WebControls.Image)e.Item.FindControl("imgDetalheServidor");
			System.Web.UI.HtmlControls.HtmlGenericControl div = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("divFamod");
			System.Web.UI.HtmlControls.HtmlTableRow tr = (System.Web.UI.HtmlControls.HtmlTableRow)e.Item.FindControl("trServidor");

            img.Attributes.Add("onclick", "Mostrar('" + div.ClientID + "', '" + img.ClientID + "');");
		}
	}
	#endregion

    void btnAprovar_Click(object sender, EventArgs e)
    {
        Parametro parametro = Parametro.Get();
        
        int count = 0;
        bool tudoOk = true;
        foreach (DataListItem dlItem in dlOficina.Items)
        {
            DataList dlServidor = (DataList)dlItem.FindControl("dlServidor");
            Label lblData = (Label)dlItem.FindControl("lblData");
            foreach (DataListItem dlServidorItem in dlServidor.Items)
            {
                CheckBox chkAprovar = (CheckBox)dlServidorItem.FindControl("chkAprovar");
                
                if (chkAprovar.Checked)
                {
                    count++;
                    //Verifica se as horas sao suficientes
                    Label lblHoras = (Label)dlServidorItem.FindControl("lblHoras");
                    bool ehMeioExpediente = Feriado.EhMeioExpediente(Convert.ToDateTime(lblData.Text));
                    if((Convert.ToInt32(lblHoras.Text) != parametro.MaximoHorasFAMOD && !ehMeioExpediente)
                        || (Convert.ToInt32(lblHoras.Text) != parametro.MaximoHorasMeioExpedienteFAMOD && ehMeioExpediente))
                    {
                        tudoOk = false;
                        continue;
                    }

                    GridView gvFamod = (GridView)dlServidorItem.FindControl("gvFamod");
                    foreach (GridViewRow gridViewRow in gvFamod.Rows)
                    {
                        if (gridViewRow.RowType == DataControlRowType.DataRow)
                        {

                            int id_famod = Convert.ToInt32(gvFamod.DataKeys[gridViewRow.RowIndex][0]);
                            Famod famod = Famod.Get(id_famod);
                            famod.SituacaoFAMOD = SituacaoFAMOD.Aprovado;
                            famod.Save();
                        }
                    }
                    
                }
            }
        }


        if (count == 0)
        {
            ShowMessage("Nenhum pedido foi selecionado.");
            return;
        }

        Bind();

        if(!tudoOk)
        {
            ShowMessage("Algumas Famods não podem ser aprovadas pois a soma das horas não equivalem à quantidade permitida.");
        }
    }

    protected void gvFamod_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if(e.Row.RowType == DataControlRowType.DataRow)
        {
            Famod famod = (Famod)e.Row.DataItem;
            if (famod.PedidoServico != null)
            {
                LinkButton lnkPS = (LinkButton) e.Row.FindControl("lnkPS");


                Anthem.AnthemClientMethods.Popup(lnkPS, "../servico/fchPedidoServico.aspx?id_pedido=" + famod.PedidoServico.ID,
                                                 false, false, false, true, true, true, true, 10, 40, 700, 520, false);
            }
        }
    }
}
