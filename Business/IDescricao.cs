using System;
using System.Collections.Generic;
using System.Text;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
    public interface IDescricao : IBusinessObject
    {
        string Descricao{ get; set;}
        bool FlagAtivo{ get; set;}
        
        
    }
}
