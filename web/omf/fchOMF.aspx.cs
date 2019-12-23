using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Drawing;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


using Marinha.Business;
using Shared.NHibernateDAL;


public partial class fchOMF : MarinhaPageBase
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        dlHistorico.ItemDataBound += new DataListItemEventHandler(dlHistorico_ItemDataBound);
    }

    void dlHistorico_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if(e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            HistoricoOMF historico = (HistoricoOMF) e.Item.DataItem;
            if(historico.FlagReprovado)
                e.Item.ForeColor = Color.Red;
        }
    }
    protected NotaEntregaMaterialOMF _omf;
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!this.IsPostBack)
		{
            _omf = NotaEntregaMaterialOMF.Get(Convert.ToInt32(Request["id_omf"]));
           
            dgItem.DataSource = _omf.Itens;
		    dgResponsavel.DataSource = _omf.ResponsaveisPericia;
		    dlHistorico.DataSource = _omf.Historico;
            
			Page.DataBind();

		    pnResponsavel.Visible = _omf.ResponsaveisPericia.Count > 0;

		}
	}
}
