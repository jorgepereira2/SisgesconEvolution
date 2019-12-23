using System;
using System.Data;
using System.Configuration;
using System.Collections;
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

public partial class frmImpressaoCodigoBarrasPesquisa : SortingPageBase
{
   
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.btnPesquisar.Click += new EventHandler(btnPesquisar_Click);
		this.RegisterSortingControl(this.gvPesquisa);

        ddlTipoServicoMaterial.SelectedIndexChanged += delegate { TipoChanged(); };
        btnImprimir.Click += new EventHandler(btnImprimir_Click);
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
			
			Util.FillDropDownList(ddlTipoServicoMaterial, typeof(TipoServicoMaterial), "Todos");
            Util.FillDropDownList(ddlMedidaEtiqueta, MedidaEtiqueta.List(), ESCOLHA_OPCAO);
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
        5000, gvPesquisa.PageIndex, true, new List<int>(), null);
		this.Sort(list);
		gvPesquisa.DataSource = list;
        gvPesquisa.DataKeyNames = new string[]{"ID"};
        gvPesquisa.DataBind();
        gvPesquisa.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();

        gvPesquisa.Visible = list.Count > 0;
        pnMensagem.Visible = list.Count == 0;
        pnMensagem.UpdateAfterCallBack = true;
    }

    void btnImprimir_Click(object sender, EventArgs e)
    {
        StringBuilder str = new StringBuilder();
        foreach (GridViewRow row in gvPesquisa.Rows)
        {
            CheckBox chkImprimir = (CheckBox) row.FindControl("chkImprimir");
            if(chkImprimir.Checked)
            {
                str.Append(gvPesquisa.DataKeys[row.RowIndex]["ID"].ToString()).Append(",");
            }
        }

        string address = string.Format("frmImpressaoCodigoBarraPopup.aspx?listaMaterialID={0}&id_medidaEtiqueta={1}&etiquetaInicial={2}", str.ToString(), ddlMedidaEtiqueta.SelectedValue, txtEtiquetaInicial.Text);
        Anthem.AnthemClientMethods.Popup(this, address,
            false, false, false, true, true, true, true, 20, 30, 700, 600);
    }

    protected void chkTodos_CheckChanged(object sender, EventArgs e)
    {
        CheckBox chkTodos = (CheckBox) sender;
        foreach (GridViewRow row in gvPesquisa.Rows)
        {
            Anthem.CheckBox chkImprimir = (Anthem.CheckBox)row.FindControl("chkImprimir");
            chkImprimir.Checked = chkTodos.Checked;
            chkImprimir.UpdateAfterCallBack = true;
        }
    }
}
