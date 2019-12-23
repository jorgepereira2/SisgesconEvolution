using System;
using System.ComponentModel;

namespace Marinha.Business
{
	public enum StatusPedidoServicoMergulhoEnum
	{
		[Description("Não Enviado")]
		NaoEnviado = 10,
		[Description("Aguardando Detalhamento p/ Servidor")]
        AguardandoDetalhamentoParaSupervidor = 20,
        [Description("Aguardando Execução do Serviço")]
        AguardandoExecuçãoServiço = 30,
        [Description("Emitir Faturamento")]
        EmitirFaturamento = 40,
        [Description("Aguardando Pagamento")]
        AguardandoPagamento = 50,
        [Description("Finalizado")]
        Finalizado = 200,
        [Description("Cancelado")]
        Cancelado = 210
	}
}
