using System;
using System.Collections.Generic;
using NHibernate;
using Shared.NHibernateDAL;

namespace Marinha.Business
{
	[Serializable]
	public partial class PedidoAquisicaoParsitManager : PedidoAquisicaoManager	
	{

        public PedidoAquisicaoParsitManager(PedidoAquisicao pa): base()
        {
            _pedidoAquisicao = pa;
        }
	}
}
