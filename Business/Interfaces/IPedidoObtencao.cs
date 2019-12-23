using System;
using System.Collections.Generic;
using System.Text;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
   
    public interface IPedidoObtencao
    {
        int ID{ get;}
        int ID_PedidoObtencao { get; }
        StatusPedidoObtencao Status{ get; }
        string CodigoComAno{ get;}
        string TipoPedidoSigla { get; }
        string Aplicacao { get; }
        string DescricaoStatus { get; }
        int Numero { get; }
        DelineamentoOrcamento DelineamentoOrcamento { get; }
        DateTime DataEmissao { get; }
        decimal ValorTotal { get; }
        Celula Celula{ get;}
        HistoricoPedidoObtencao UltimoHistorico { get; }
        ICustomList<HistoricoPedidoObtencao> Historico { get; }
        
    }
}
