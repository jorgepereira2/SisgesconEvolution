using System;
using System.ComponentModel;

namespace Marinha.Business
{
	public enum OrigemPO
	{
		[Description("Direto")]
		Direto = 1,
		[Description("Pedido Serviço")]
        PS = 2,
        [Description("Gasto Extra PS")]
        GastoExtraPS = 3
	}
}
