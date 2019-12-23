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

public partial class frmServicoMaterialPesquisa : SortingPageBase
{
   
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.btnPesquisar.Click += new EventHandler(btnPesquisar_Click);
		this.RegisterSortingControl(this.gvPesquisa);

        ddlTipoServicoMaterial.SelectedIndexChanged += delegate { TipoChanged(); };
    }

    private void TipoChanged()
    {
        Util.FillDropDownList(ddlClasseServicoMaterial, ClasseServicoMaterial.List(), "Todos");
        ddlClasseServicoMaterial.UpdateAfterCallBack = true;
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
			Anthem.AnthemClientMethods.Redirect("frmServicoMaterialCadastro.aspx", btnNovo);
			
			Util.FillDropDownList(ddlTipoServicoMaterial, typeof(TipoServicoMaterial), "Todos");
			Util.InsertDefaultItem(ddlClasseServicoMaterial, "Todas");
        }
    }
    #endregion  
    
    protected override void Bind()
    {
        List<ServicoMaterial> list = ServicoMaterial.Select(Convert.ToInt32(ddlTipoServicoMaterial.SelectedValue),
        Convert.ToInt32(ddlClasseServicoMaterial.SelectedValue), 
        Int32.MinValue,
        txtTexto.Text,
        Int32.MinValue,
        null,
        null,
        Int32.MinValue,
        Int32.MinValue,
        gvPesquisa.PageSize, gvPesquisa.PageIndex,
        null,
        new List<int>(),
        null);
		this.Sort(list);
		gvPesquisa.DataSource = list;
        gvPesquisa.DataBind();
        gvPesquisa.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();

        gvPesquisa.Visible = list.Count > 0;
        pnMensagem.Visible = list.Count == 0;
        pnMensagem.UpdateAfterCallBack = true;
    }  
}
