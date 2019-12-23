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

public partial class frmOMFPorPericia : MarinhaPageBase
{  
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        dlOMF.ItemDataBound += new DataListItemEventHandler(dlOMF_ItemDataBound);
    }
  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
			Bind();
        }
    }
    #endregion     

    
	protected void Bind()
    {
        List<NotaEntregaMaterialOMF> list = NotaEntregaMaterialOMF.Select(
             Convert.ToInt32(Request["id_status"]),
             Convert.ToInt32(Request["id_tipoEmprego"]),
             Convert.ToInt32(Request["id_fornecedor"]),
             Convert.ToInt32(Request["id_recebedor"]),
             HttpUtility.UrlDecode(Request["NumeroNota"]),
             HttpUtility.UrlDecode(Request["NumeroEmpenho"]),
             IsNull(HttpUtility.UrlDecode(Request["dataInicio"]), DateTime.MinValue),
             IsNull(HttpUtility.UrlDecode(Request["dataFim"]), DateTime.MinValue)
             );

        
        dlOMF.DataSource = list;
        dlOMF.DataBind();
       
    }

    void dlOMF_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataGrid dgPericia = (DataGrid)e.Item.FindControl("dgPericia");
            NotaEntregaMaterialOMF omf = (NotaEntregaMaterialOMF)e.Item.DataItem;

            dgPericia.DataSource = omf.ResponsaveisPericia;
            dgPericia.DataBind();
        }
    }

}


