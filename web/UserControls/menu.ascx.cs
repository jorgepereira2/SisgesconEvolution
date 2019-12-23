using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Art = ComponentArt.Web.UI;
using Marinha.Business;


using NHibernate;
using Iesi.Collections.Generic;
using Shared.Common;

public partial class principal_menu : System.Web.UI.UserControl
{
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!this.Page.IsPostBack)
		{
			FillMenu();

		}
	}

	private void FillMenu()
	{
        int id_servidor = ((MarinhaPageBase)this.Page).ID_Servidor;
		ProcessoCollection processos = Processo.SelectMenu(id_servidor);
		foreach (Processo p in processos)
		{            
            if (p.ProcessoPai == null)
                Menu1.Items.Add(GetMenuItem(p));
            else
            {
                Art.MenuItem menuItem = Menu1.FindItemById(p.ProcessoPai.ID.ToString());
				if(menuItem != null)
					menuItem.Items.Add(GetMenuItem(p));
            }
		}
	}

	private Art.MenuItem GetMenuItem(Processo p)
	{
		Art.MenuItem item = new Art.MenuItem();        
        item.ID = p.ID.ToString();
		if (p.Processos.Count == 0)
		{
			item.ClientSideCommand = string.Format("direciona('{0}','false');", p.Link);
		}
		else if(p.ProcessoPai != null)
		{
			item.Look.RightIconUrl = "popup.gif";			
			item.Look.HoverRightIconUrl = "SelectedPopup.gif";			
            //item.LookId = "DefaultItemLook";
		}

        if (p.ProcessoPai == null)
        {
            item.LookId = "TopItem";
        }       
        item.Text = p.Nome;

		return item;
	}
}
