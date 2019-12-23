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
            BindServidor();
        }
    }

    #endregion  

    #region Menus

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
        dlProcesso.DataBind();
        dlProcesso.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();
    }

    #endregion

    #region Servidores

    public void BindServidor()
    {
        // List
        List<Servidor> list = Servidor.SelectFromProcesso(
            Request["processo"],
            Request["nome"]
        );

        // Populate
        gvServidor.DataSource = list;
        gvServidor.DataBind();
        Anthem.AnthemClientMethods.ResizeIFrame();
    }

    #endregion
}