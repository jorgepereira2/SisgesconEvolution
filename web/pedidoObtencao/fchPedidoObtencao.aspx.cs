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


public partial class fchPedidoObtencao : MarinhaPageBase
{
    protected PedidoObtencao _pedido;
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!this.IsPostBack)
		{
            _pedido = PedidoObtencao.Get(Convert.ToInt32(Request["id_pedido"]));

            dgItem.DataSource = _pedido.GetItens(OrigemMaterial.Obtencao);
            dgItemPEP.DataSource = _pedido.GetItens(OrigemMaterial.PEP);
            dgItemRodizio.DataSource = _pedido.GetItens(OrigemMaterial.Rodizio);
                        
			Page.DataBind();

            pnItem.Visible = dgItem.Items.Count > 0;
            pnItemPEP.Visible = dgItemPEP.Items.Count > 0;
            pnItemRodizio.Visible = dgItemRodizio.Items.Count > 0;

            if (_pedido.TipoPedido == TipoPedido.PedidoMaterial)
                lblTitulo.Text = "Pedido Material";

            if(_pedido.DelineamentoOrcamento != null)
            {
                lnkPS.Text = "(detalhes)";
                Anthem.AnthemClientMethods.Popup(lnkPS, "../servico/fchPedidoServico.aspx?id_pedido=" + _pedido.DelineamentoOrcamento.ID_PedidoServico.ToString(),
                    "ps",false, false, false, true, true, true, true, 20,40, 700, 500, false);
            }

            if (_pedido.TipoCompra.TipoCompraEnum == TipoCompraEnum.Licitacao && _pedido.Licitacao != null)
            {
                lnkLicitacao.Text = _pedido.Licitacao.NumeroPregao;
                Anthem.AnthemClientMethods.Popup(lnkLicitacao, "../licitacao/fchLicitacao.aspx?id_licitacao=" + _pedido.Licitacao.ID.ToString(),
                    "licitacao", false, false, false, true, true, true, true, 20, 40, 700, 500, false);
            }
		}
	}
}
