using System;
using System.ComponentModel;

namespace Marinha.Business
{
	public enum StatusPedidoServicoAtividadeSecundariaEnum
	{
        [Description("Cadastro Inicial")]
		NaoEnviado = 10,
        [Description("Aguardando Pagamento")]
        AguardandoPagamento = 20,
        [Description("Finalizado")]
        Finalizado = 100,
        [Description("Cancelado")]
        Cancelado = 110
	}
}
