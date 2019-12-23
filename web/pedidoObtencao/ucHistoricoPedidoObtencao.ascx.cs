using System;
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

public partial class pedidoObtencao_ucHistoricoPedidoObtencao : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
    
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        dlHistorico.ItemDataBound +=new DataListItemEventHandler(dlHistorico_ItemDataBound);
    }
    
    public object DataSource
    {
        set
        {
            dlHistorico.DataSource = value;
        }
    }

    public override void DataBind()
    {
        dlHistorico.DataBind();
        dlHistorico.UpdateAfterCallBack = true;
        
    }

    void dlHistorico_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            HistoricoPedidoObtencao historico = (HistoricoPedidoObtencao)e.Item.DataItem;
            if (historico.FlagReprovado)
                e.Item.ForeColor = Color.Red;
        }
    }
}
