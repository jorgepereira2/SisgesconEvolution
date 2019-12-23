using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


using Marinha.Business;
using Shared.NHibernateDAL;


public partial class fchPedidoServico : MarinhaPageBase
{
    protected PedidoServico _pedido;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        dlOrcamento.ItemDataBound += new DataListItemEventHandler(dlOrcamento_ItemDataBound);
    }
    
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!this.IsPostBack)
		{
		    if(Request["id_pedido"] != null)
                _pedido = PedidoServico.Get(Convert.ToInt32(Request["id_pedido"]));
            else if (Request["id_orcamento"] != null)
            {
                DelineamentoOrcamento orcamento = DelineamentoOrcamento.Get(Convert.ToInt32(Request["id_orcamento"]));
                _pedido = orcamento.PedidoServico;
            }
            
            dgHistorico.DataSource = _pedido.Historico;
		    dgEquipamento.DataSource = _pedido.Equipamentos;
		    dlOrcamento.DataSource = _pedido.Orcamentos;
		    dlPO.DataSource = _pedido.SelectPOsAssociados();
		    
			Page.DataBind();
		}
	}

    void dlOrcamento_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if(e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            _orcamento = (DelineamentoOrcamento) e.Item.DataItem;
            DataGrid dgDelineamento = (DataGrid) e.Item.FindControl("dgDelineamento");
            DataGrid dgOrcamentoItem = (DataGrid)e.Item.FindControl("dgOrcamentoItem");
            HtmlTableRow trDevolucao = (HtmlTableRow)e.Item.FindControl("trDevolucao");

            trDevolucao.Visible = !string.IsNullOrEmpty(_orcamento.ComentarioDevolucao);
            dgOrcamentoItem.DataSource = _orcamento.ItensOrcamento;
            dgOrcamentoItem.DataBind();

            
            dgDelineamento.DataSource = _orcamento.ItensDelineamento;
            dgDelineamento.DataBind();
            
            if(_orcamento.Ocorrencias.Count > 0)
            {
                Panel pnOcorrencia = (Panel) e.Item.FindControl("pnOcorrencia");
                DataGrid dgOcorrencia = (DataGrid) e.Item.FindControl("dgOcorrencia");

                dgOcorrencia.DataSource = _orcamento.Ocorrencias;
                dgOcorrencia.DataBind();
                pnOcorrencia.Visible = true;
            }
        }
    }

    private DelineamentoOrcamento _orcamento;
    protected void dgDelineamento_ItemCreated(object sender, DataGridItemEventArgs e)
    {
        if(e.Item.ItemType == ListItemType.Footer)
        {
            Label lblTotalHH = (Label) e.Item.FindControl("lblTotalHH");
            lblTotalHH.Text = _orcamento.HomemHoraTotal.ToString();
        }
    }
}
