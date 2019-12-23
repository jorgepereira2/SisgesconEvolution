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

public partial class frmFamodPorApropriacao : MarinhaPageBase
{
    private DataSet _ds;
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        dlApropriacao.ItemDataBound += new DataListItemEventHandler(dlApropriacao_ItemDataBound);
        btnExportar.Click += new EventHandler(btnExportar_Click);
    }

    void btnExportar_Click(object sender, EventArgs e)
    {
        App_Code.WebUtil.ExportToExcel(dlApropriacao, this, false);
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
        _ds = Famod.SelectHorasPorApropriacao(
             Convert.ToInt32(Request["ID_Servidor"]),
             Convert.ToInt32(Request["ID_Atividade"]),
             Convert.ToInt32(Request["ID_Apropriacao"]),
             IsNull(HttpUtility.UrlDecode(Request["dataInicio"]), DateTime.MinValue),
             IsNull(HttpUtility.UrlDecode(Request["dataFim"]), DateTime.MinValue)
             );
       
        dlApropriacao.DataSource = _ds.Tables[0];
        dlApropriacao.DataBind();
    }

    void dlApropriacao_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataGrid dgAtividade = (DataGrid)e.Item.FindControl("dgAtividade");
            DataRowView row = (DataRowView)e.Item.DataItem;

            DataView dvStatus = new DataView(_ds.Tables[1], "Atividade like '" + row["ID_Apropriacao"].ToString() + "|%'","" , DataViewRowState.CurrentRows);

            _totais = new List<int>();
            dgAtividade.DataSource = dvStatus;
            dgAtividade.DataBind();
        }
    }

    private List<int> _totais;
    protected void dgAtividade_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Header)
        {
            
            DataRowView row = (DataRowView)e.Item.DataItem;
            e.Item.Cells[1].Visible = false;

            if (e.Item.ItemType != ListItemType.Header)
            {
                for(int i = 2; i < e.Item.Cells.Count; i++)
                {
                    if (e.Item.ItemIndex == 0)
                        _totais.Add(Convert.ToInt32(e.Item.Cells[i].Text));
                    else
                        _totais[i - 2] += Convert.ToInt32(e.Item.Cells[i].Text);
                }
            }
        }
    }

    protected void dgAtividade_ItemCreated(object sender, DataGridItemEventArgs e)
    {
        if (_totais == null) return;
        if (e.Item.ItemType == ListItemType.Footer)
        {
            e.Item.Cells[1].Visible = false;
            e.Item.Cells[0].Text = "Totais";
            for (int i = 2; i < e.Item.Cells.Count; i++)
            {
                e.Item.Cells[i].Text = _totais[i - 2].ToString();
            }
        }
    }
}


