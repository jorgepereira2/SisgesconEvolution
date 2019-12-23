using System;
using System.Collections.Generic;
using System.Text;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
    public interface IPedidoAquisicaoItem : IBusinessObject
    {
        string DescricaoItem{ get;}
        int Quantidade{ get;}
        decimal Valor{ get;}
        decimal? Valor2 { get; }
        decimal? Valor3 { get; }
        decimal ValorTotal{ get;}
    }
}
