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

public partial class frmServicoMaterialBusca : SortingPageBase
{
   
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.btnPesquisar.Click += new EventHandler(btnPesquisar_Click);
		this.RegisterSortingControl(this.gvPesquisa);
        ddlClasseServicoMaterial.SelectedIndexChanged += new EventHandler(ddlClasseServicoMaterial_SelectedIndexChanged);
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

            Util.FillDropDownList(ddlTipoServicoMaterial, typeof(TipoServicoMaterial), "Todos");
            Util.FillDropDownList(ddlOrigem, typeof(OrigemMaterial), "Todas");
            Util.FillDropDownList(ddlUnidade, Unidade.List(), "Todas");
            Util.FillDropDownList(ddlClasseServicoMaterial, ClasseServicoMaterial.List(), "Todas");
            Util.InsertDefaultItem(ddlSubClasseServicoMaterial, "Todas");
            
            id_controle.Text = Request["id_controle"];

            MyClientScript.Close(btnFechar);
            
            if(Request["tipoMaterial"] != null)
            {
                ddlTipoServicoMaterial.Enabled = false;
                ddlTipoServicoMaterial.SelectedValue = Request["TipoMaterial"];
            }
        }
    }
    #endregion  
  
    void ddlClasseServicoMaterial_SelectedIndexChanged(object sender, EventArgs e)
    {
        Util.FillDropDownList(ddlSubClasseServicoMaterial, SubClasseServicoMaterial.List(Convert.ToInt32(ddlClasseServicoMaterial.SelectedValue)), "Todas");
        ddlSubClasseServicoMaterial.UpdateAfterCallBack = true;
    }
    
    protected override void Bind()
    {
        List<ServicoMaterial> list = ServicoMaterial.Select(Convert.ToInt32(ddlTipoServicoMaterial.SelectedValue),
        Convert.ToInt32(ddlClasseServicoMaterial.SelectedValue),
        Convert.ToInt32(ddlSubClasseServicoMaterial.SelectedValue),
         txtTexto.Text,
         Convert.ToInt32(ddlUnidade.SelectedValue),
         txtDescricaoSingra.Text,
         txtCodigoSiasg.Text,
         Int32.MinValue,
         Int32.MinValue,
         gvPesquisa.PageSize, gvPesquisa.PageIndex,
         true,
         this.SJBLiberados,
         this.FlagAcessaTodosMateriais);
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
