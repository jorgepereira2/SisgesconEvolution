using System;
using System.Collections.Generic;
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
using Shared.Common;
using Shared.NHibernateDAL;


public partial class fchAutorizacaoCompraCompleto : MarinhaPageBase
{
    protected AutorizacaoCompra _ac;
    protected PedidoCotacao _cotacao;
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!this.IsPostBack)
		{
            _ac = AutorizacaoCompra.Get(Convert.ToInt32(Request["id_ac"]));
		    _cotacao = _ac.Itens[0].PedidoCotacao;


            dgItem.DataSource = _ac.Itens;
		    dgPagamento.DataSource = _ac.Pagamentos;
			Page.DataBind();

		    dgCotacao.DataSource = _cotacao.Itens;
		    dgCotacao.DataKeyField = "ID";
            dgCotacao.DataBind();

            dlHistorico.DataSource = _ac.Historico;
            dlHistorico.DataKeyField = "ID";
            dlHistorico.DataBind();
            
		    pnPagamento.Visible = _ac.Pagamentos.Count > 0;
            AtualizarSaldos();

		    repPO.DataSource = _ac.POs;
            repPO.DataBind();

            repPS.DataSource = _ac.PSs;
            repPS.DataBind();

		}
	}

    private void AtualizarSaldos()
    {
        lblFornecedorLimite1.Text = _cotacao.FornecedorCotacao1 != null ? _cotacao.FornecedorCotacao1.ToString() : "";
        lblFornecedorLimite2.Text = _cotacao.FornecedorCotacao2 != null ? _cotacao.FornecedorCotacao2.ToString() : "";
        lblFornecedorLimite3.Text = _cotacao.FornecedorCotacao3 != null ? _cotacao.FornecedorCotacao3.ToString() : "";
        lblFornecedorLimite4.Text = _cotacao.FornecedorCotacao4 != null ? _cotacao.FornecedorCotacao4.ToString() : "";

        
        SaldoFornecedor saldo =
           AutorizacaoCompra.GetSaldoComprasUtilizado(_cotacao.FornecedorCotacao1.ID, _cotacao.TipoCompra.ID, DateTime.Today.Year);
        lblSaldoReal1.Text = saldo.SaldoReal.ToString("N2");
        lblARealizar1.Text = saldo.SaldoARealizar.ToString("N2");
        lblSaldoTotal1.Text = saldo.SaldoTotal.ToString("N2");

        if (_cotacao.FornecedorCotacao2 != null)
        {
            saldo =
                AutorizacaoCompra.GetSaldoComprasUtilizado(_cotacao.FornecedorCotacao2.ID, _cotacao.TipoCompra.ID,
                                                           DateTime.Today.Year);
            lblSaldoReal2.Text = saldo.SaldoReal.ToString("N2");
            lblARealizar2.Text = saldo.SaldoARealizar.ToString("N2");
            lblSaldoTotal2.Text = saldo.SaldoTotal.ToString("N2");
        }

        if (_cotacao.FornecedorCotacao3 != null)
        {
            saldo =
                AutorizacaoCompra.GetSaldoComprasUtilizado(_cotacao.FornecedorCotacao3.ID, _cotacao.TipoCompra.ID,
                                                           DateTime.Today.Year);
            lblSaldoReal3.Text = saldo.SaldoReal.ToString("N2");
            lblARealizar3.Text = saldo.SaldoARealizar.ToString("N2");
            lblSaldoTotal3.Text = saldo.SaldoTotal.ToString("N2");
        }

        if (_cotacao.FornecedorCotacao4 != null)
        {
            saldo =
                AutorizacaoCompra.GetSaldoComprasUtilizado(_cotacao.FornecedorCotacao4.ID, _cotacao.TipoCompra.ID,
                                                           DateTime.Today.Year);
            lblSaldoReal4.Text = saldo.SaldoReal.ToString("N2");
            lblARealizar4.Text = saldo.SaldoARealizar.ToString("N2");
            lblSaldoTotal4.Text = saldo.SaldoTotal.ToString("N2");
        }
    }

    protected void repPO_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if(e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            KeyValuePair<int, string> pair = (KeyValuePair<int, string>) e.Item.DataItem;
            LinkButton lnkPO = (LinkButton) e.Item.FindControl("lnkPO");
            Anthem.AnthemClientMethods.Popup(lnkPO, "fchPedidoObtencao.aspx?id_pedido=" + pair.Key.ToString(), "po", false, false, false, true, true, true, true, 20, 40, 700, 500, false);
        }
    }

    protected void repPS_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            KeyValuePair<int, string> pair = (KeyValuePair<int, string>)e.Item.DataItem;
            LinkButton lnkPS = (LinkButton)e.Item.FindControl("lnkPS");
            Anthem.AnthemClientMethods.Popup(lnkPS, "../servico/fchPedidoServico.aspx?id_pedido=" + pair.Key.ToString(), "ps", false, false, false, true, true, true, true, 20, 40, 700, 500, false);
        }
    }
}
