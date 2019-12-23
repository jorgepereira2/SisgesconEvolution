using System;
using System.Collections.Generic;
using System.Text;

namespace Marinha.Business
{
    public interface IItemServicoMaterial
    {
        ServicoMaterial ServicoMaterial{ get;}
        decimal Quantidade { get; }
        OrigemMaterial OrigemMaterial{ get;}
        decimal Valor{ get;}
        string Especificacao { get; }
        Fornecedor Fornecedor { get; }
       
    }
}
