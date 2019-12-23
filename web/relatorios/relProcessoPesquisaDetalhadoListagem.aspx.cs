using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Text;
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

public partial class relPessoaLista : SortingPageBase
{
    #region Initialization

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);       
        this.dlProcesso.ItemDataBound += new DataListItemEventHandler(dlProcesso_ItemDataBound);
    }

    void ucColumn_ColumnsChanged(object sender, EventArgs e)
    {
        Bind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
			Bind();
        }
    }

    #endregion  

    #region Imprimir

    protected override void Bind()
    {
        // List
        List<Processo> list = new List<Processo>();

        // List
        foreach (Processo p in Processo.SelectByString(Request["processo"]))
        {
            list.Add(p);
        }

        // Bind
        dlProcesso.DataSource = list;
        dlProcesso.DataKeyField = "ID";
        dlProcesso.DataBind();
        dlProcesso.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();
    }

    void dlProcesso_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            // Id
            string ID = Convert.ToInt32(dlProcesso.DataKeys[e.Item.ItemIndex]).ToString();

            // List
            List<Servidor> list = Servidor.SelectFromProcesso(
                ID,
                Request["nome"]
            );

            // Populate
            Anthem.DataList dlServidor = (Anthem.DataList)e.Item.FindControl("dlServidor");
            dlServidor.DataSource = list;
            dlServidor.DataKeyField = "ID";
            dlServidor.DataBind();
            dlServidor.UpdateAfterCallBack = true;
            Anthem.AnthemClientMethods.ResizeIFrame();
        }
    }

    #endregion
}