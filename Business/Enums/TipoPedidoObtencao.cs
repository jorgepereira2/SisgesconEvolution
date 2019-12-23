using System;
using System.ComponentModel;

namespace Marinha.Business
{
	public enum TipoPedidoObtencao
	{
        [Description("Material")]
		MaterialServico = 1,
        [Description("Reposição Estoque")]
		ReposicaoEstoque = 2,
        [Description("Serviço Terceiro")]
        ServicoTerceiro = 3,
        [Description("Compra Patrimonial")]
		CompraPatrimonial = 4
	}
}
