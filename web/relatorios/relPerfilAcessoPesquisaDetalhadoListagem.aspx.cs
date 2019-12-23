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
        this.dlPerfilAcesso.ItemDataBound += new DataListItemEventHandler(dlPerfilAcesso_ItemDataBound);
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
        List<PerfilAcesso> list = new List<PerfilAcesso>();

        // List
        foreach (PerfilAcesso p in PerfilAcesso.SelectByString(Request["perfilAcesso"]))
        {
            list.Add(p);
        }

        // Bind
        dlPerfilAcesso.DataSource = list;
        dlPerfilAcesso.DataKeyField = "ID";
        dlPerfilAcesso.DataBind();
        dlPerfilAcesso.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();
    }

    void dlPerfilAcesso_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            // Id
            string ID = Convert.ToInt32(dlPerfilAcesso.DataKeys[e.Item.ItemIndex]).ToString();

            // List
            List<Servidor> list = Servidor.SelectFromPerfilAcesso(
                ID,
                Request["nome"]
            );

            // Populate
            Anthem.DataList dlPessoa = (Anthem.DataList)e.Item.FindControl("dlPessoa");
            dlPessoa.DataSource = list;
            dlPessoa.DataKeyField = "ID";
            dlPessoa.DataBind();
            dlPessoa.UpdateAfterCallBack = true;
            Anthem.AnthemClientMethods.ResizeIFrame();
        }
    }

    #endregion
}