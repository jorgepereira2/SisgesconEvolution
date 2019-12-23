using System;
using System.Collections.Generic;
using System.Text;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
   
    public interface IPedido
    {
        int ID{ get;}
        StatusPedidoServico Status{ get; }
        //Equipamento Equipamento{ get;}
        string CodigoComAno{ get;}
        string CodigoPedidoCliente { get; }
        int CodigoInterno{ get;}
        Cliente Cliente{ get;}
        DateTime DataEmissao { get;}
        bool FlagRecusado{ get;}
        HistoricoPedidoServico UltimoHistorico{ get;}
        void Recusar(int id_servidor, string justificativa);
       

        Cliente ClientePagador{ get;}
        Servidor ServidorGerente{ get;}
        bool FlagProgem{ get;}
        Celula Celula{ get;}
        int ID_PedidoServico{ get;}
        string NumeroRegistro{ get;}
        CategoriaServico CategoriaServico{ get;}

        string DescricaoEquipamentos { get; }
        string DescricaoCliente { get; }
        string DescricaoStatus { get; }

        List<Etapa> GetEtapas();
    }
}
