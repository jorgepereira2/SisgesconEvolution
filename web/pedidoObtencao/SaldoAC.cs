using System;
using System.Collections.Generic;
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

public partial class SaldoAC : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
    }   
    
    //public void Atualizar(PedidoObtencao po)
    //{
    //    if (po == null) return;

    //    //pega os ids dos itens selecionados
    //    Atualizar(po);
    //}

    public void Atualizar(PedidoObtencao po)
    {
        if (po.Itens.Any(x => x.SubNaturezaDespesa == null)) return;

        //pega os ids dos itens selecionados
        var subNaturezas =
            po.Itens.GroupBy(g => new { g.SubNaturezaDespesa.ID, g.SubNaturezaDespesa.Descricao }).Select(x => new { x.Key.ID, x.Key.Descricao, Valor = x.Sum(t => t.ValorTotal) }).ToList();

        var saldos = new List<SaldoDto>();

        foreach (var subNatureza in subNaturezas)
        {
            var saldo = new SaldoDto();
            saldo.Id = subNatureza.ID;
            saldo.Descricao = subNatureza.Descricao;
            saldo.ValorAc = subNatureza.Valor;

            var saldoDb = PedidoObtencao.GetSaldoComprasUtilizado(saldo.Id, DateTime.Today.Year);
            saldo.SaldoDisponivel = saldoDb.SaldoDisponivel;
            saldo.SaldoUtilizado = saldoDb.ValorUtilizadoTotal;
            saldo.SaldoComprometido = saldoDb.ValorComprometido;

            saldos.Add(saldo);
        }

        dgSaldo.DataSource = saldos;
        dgSaldo.DataBind();
        dgSaldo.UpdateAfterCallBack = true;
    }

    public class SaldoDto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public decimal SaldoDisponivel { get; set; }
        public decimal SaldoUtilizado { get; set; }
        public decimal SaldoComprometido { get; set; }
        public decimal ValorAc { get; set; }
    }
}
