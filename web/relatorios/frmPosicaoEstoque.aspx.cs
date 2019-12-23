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

public partial class frmPosicaoEstoque : SortingPageBase
{

    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);       
		this.RegisterSortingControl(gvPesquisa);
        ucColumn.ColumnsChanged += new EventHandler(ucColumn_ColumnsChanged);
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
			ucColumn.SetValues();
        }
    }
    #endregion     

    
	protected override void Bind()
    {
		DataTable dt = MovimentoEstoque.SelectPosicaoEstoque(
            IsNull(Request["id_material"], Int32.MinValue),
            Convert.ToInt32(Request["id_origemMaterial"]),
			IsNull(HttpUtility.UrlDecode(Request["dataInicio"]), DateTime.MinValue),
            IsNull(HttpUtility.UrlDecode(Request["dataFim"]), DateTime.MinValue));
       
        this.Sort(dt);
        DataView dv = new DataView(dt);
        if (Convert.ToBoolean(Request["OcultarItensZerados"]))
            dv.RowFilter = "QuantidadeEntrada <> 0 OR QuantidadeSaida <> 0";
	    gvPesquisa.DataSource = dv; 		
        gvPesquisa.DataBind();
		pnGrid.UpdateAfterCallBack = true;
    }

    
}



