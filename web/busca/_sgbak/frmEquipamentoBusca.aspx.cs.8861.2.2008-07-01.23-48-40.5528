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

public partial class frmEquipamentoBusca : SortingPageBase
{
   
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.btnPesquisar.Click += new EventHandler(btnPesquisar_Click);
		this.RegisterSortingControl(this.gvPesquisa);
        this.ddlTipoEquipamento.SelectedIndexChanged += new EventHandler(ddlTipoEquipamento_SelectedIndexChanged);
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
            id_controle.Text = Request["id_controle"];
            Util.FillDropDownList(ddlTipoEquipamento, TipoEquipamento.List(), "Todos");
            Util.FillDropDownList(ddlTipoOperativo, typeof(TipoOperativo), "Todos");
            Util.InsertDefaultItem(ddlSubTipoEquipamento, "Todos");
        }
    }
    #endregion  
    
    protected override void Bind()
    {
        List<Equipamento> list = Equipamento.Select(Convert.ToInt32(ddlTipoEquipamento.SelectedValue), 
            txtDescricao.Text, 
            Convert.ToInt32(ddlSubTipoEquipamento.SelectedValue),
            Convert.ToInt32(ddlTipoOperativo.SelectedValue)
        );
		this.Sort(list);
		gvPesquisa.DataSource = list;
        gvPesquisa.DataBind();
        gvPesquisa.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();

        gvPesquisa.Visible = list.Count > 0;
        pnMensagem.Visible = list.Count == 0;
        pnMensagem.UpdateAfterCallBack = true;
    }

    void ddlTipoEquipamento_SelectedIndexChanged(object sender, EventArgs e)
    {
        Util.FillDropDownList(ddlSubTipoEquipamento, SubTipoEquipamento.List(Convert.ToInt32(ddlTipoEquipamento.SelectedValue)), "Todos");
        ddlSubTipoEquipamento.UpdateAfterCallBack = true;
    }
}
