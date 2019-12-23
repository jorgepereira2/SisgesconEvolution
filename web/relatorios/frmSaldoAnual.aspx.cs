using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Linq;
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

public partial class frmSaldoAnual : SortingPageBase
{

    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);       
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
			
        }
    }
    #endregion

    protected decimal PSAnoAnterior { get; set; }
    protected decimal PSAnoAtual { get; set; }
    protected decimal POIndustrial { get; set; }
    protected decimal PSMAnoAtual { get; set; }
    protected decimal PSMAnoAnterior { get; set; }
    protected decimal PODireto { get; set; }
    protected decimal PagamentoMensal { get; set; }
    protected decimal AtividadeSecundariaAnoAnterior { get; set; }
    protected decimal AtividadeSecundariaAnoAtual { get; set; }

    protected decimal TotalAtividadePrincipalRecebimento
    {
        get { return this.PSAnoAnterior + this.PSAnoAtual + this.PSMAnoAnterior + this.PSMAnoAtual; }
    }

    protected decimal TotalAtividadePrincipal
    {
        get { return this.TotalAtividadePrincipalRecebimento - this.POIndustrial; }
    }

    protected decimal TotalAtividadeSecundariaRecebimento
    {
        get { return this.AtividadeSecundariaAnoAnterior + this.AtividadeSecundariaAnoAtual; }
    }

    protected decimal TotalAtividadeSecundariaPagamento
    {
        get { return this.PODireto + this.PagamentoMensal; }
    }

    protected decimal TotalAtividadeSecundaria
    {
        get { return this.TotalAtividadeSecundariaRecebimento - this.TotalAtividadeSecundariaPagamento; }
    }

    protected decimal TotalGeral
    {
        get { return this.TotalAtividadeSecundaria + this.TotalAtividadePrincipal; }
    }


    protected override void Bind()
    {
		DataSet ds = MovimentoConta.SelectSaldoAnual(Convert.ToInt32(Request["ano"]));
	    DataRow row = ds.Tables[0].Rows[0];

	    this.PSAnoAnterior = Convert.ToDecimal(row["PSAnoAnterior"]);
        this.PSAnoAtual = Convert.ToDecimal(row["PSAnoAtual"]);
        this.POIndustrial = Convert.ToDecimal(row["POIndustrial"]);
        this.PODireto = Convert.ToDecimal(row["PODireto"]);
        this.PagamentoMensal = Convert.ToDecimal(row["PagamentoMensal"]);

        DateTime dataInicio = new DateTime(Convert.ToInt32(Request["ano"]), 1, 1);
        DateTime dataFim = new DateTime(Convert.ToInt32(Request["ano"]), 12, 31);
        List<PedidoServicoMergulho> psmAnoAnteriorList = PedidoServicoMergulho.Select("", dataInicio.AddYears(-1), dataFim.AddYears(-1), Convert.ToInt32(StatusPedidoServicoMergulhoEnum.Finalizado));

        this.PSMAnoAnterior = psmAnoAnteriorList.Where(p => p.Historico.Where(h => h.StatusPosterior.ID == Convert.ToInt32(StatusPedidoServicoMergulhoEnum.Finalizado) && h.Data.Year == dataInicio.Year).Count() > 0).Sum(p => p.TotalAPagar);
        
        List<PedidoServicoMergulho> psmAnoAtualList = PedidoServicoMergulho.Select("", dataInicio, dataFim, Convert.ToInt32(StatusPedidoServicoMergulhoEnum.Finalizado));
	    this.PSMAnoAtual = psmAnoAtualList.Sum(p => p.TotalAPagar);

        List<PedidoServicoAtividadeSecundaria> asAnoAnterior = PedidoServicoAtividadeSecundaria.Select("", dataInicio.AddYears(-1), dataFim.AddYears(-1), Convert.ToInt32(StatusPedidoServicoAtividadeSecundariaEnum.Finalizado), Int32.MinValue);
        this.AtividadeSecundariaAnoAnterior = asAnoAnterior.Where(p => p.Historico.Where(h => h.StatusPosterior.ID == Convert.ToInt32(StatusPedidoServicoAtividadeSecundariaEnum.Finalizado) && h.Data.Year == dataInicio.Year).Count() > 0).Sum(p => p.TotalGeral);

        List<PedidoServicoAtividadeSecundaria> asAnoAtual = PedidoServicoAtividadeSecundaria.Select("", dataInicio, dataFim, Convert.ToInt32(StatusPedidoServicoAtividadeSecundariaEnum.Finalizado), Int32.MinValue);
        this.AtividadeSecundariaAnoAtual = asAnoAtual.Sum(p => p.TotalGeral);

        this.DataBind();
    }

    
}



