using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


using Marinha.Business;
using Shared.NHibernateDAL;


public partial class fchImpressaoParaCotacao : MarinhaPageBase
{
    protected PedidoServico _pedido;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        dgOrcamentoItem.ItemDataBound += new DataGridItemEventHandler(dgOrcamentoItem_ItemDataBound);
    }

    void dgOrcamentoItem_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if(e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            PedidoServicoItemOrcamento item = (PedidoServicoItemOrcamento) e.Item.DataItem;
            if(!string.IsNullOrEmpty(item.Observacao))
            {
                Literal lit = new Literal();
                lit.Text = "<br>Obs.: " + item.Observacao;
                e.Item.Cells[0].Controls.Add(lit);
            }
        }
    }
    
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!this.IsPostBack)
		{
		    
            DelineamentoOrcamento orcamento = DelineamentoOrcamento.Get(Convert.ToInt32(Request["id_orcamento"]));
            _pedido = orcamento.PedidoServico;


            if(Request["tipo"] == "servico")
                dgOrcamentoItem.DataSource = orcamento.ItensOrcamento.Where(i => i.ServicoMaterial.TipoServicoMaterial == TipoServicoMaterial.Servico);
            else
                dgOrcamentoItem.DataSource = orcamento.ItensOrcamento.Where(i => i.ServicoMaterial.TipoServicoMaterial != TipoServicoMaterial.Servico);
		    
			Page.DataBind();
		}
	}
}
