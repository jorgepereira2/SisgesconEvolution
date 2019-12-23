using System;
using System.ComponentModel;

namespace Marinha.Business
{
    public enum TipoPedidoAquisicaoEnum
	{
		[Description("Serviço")]
		Servico = 1,
		[Description("Material")]
		Material = 2,
        [Description("Obra")]
        Obra = 3
	}
}
