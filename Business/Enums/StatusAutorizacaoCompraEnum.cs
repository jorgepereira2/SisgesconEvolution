using System;
using System.ComponentModel;

namespace Marinha.Business
{
	public enum StatusAutorizacaoCompraEnum
	{
        [Description("Aguardando Aprovação Encarregado Obtenção")]
        AguardandoAprovacaoEncarregadoObtencao = 10,
        [Description("Aguardando Aprovação Comandante Geral")]
		AguardandoAprovacaoComandanteGeral = 20,
        [Description("Aguardando Designação da Divisão de Apoio")]
        AguardandoDesignacaoDivisaoApoio = 25,
        [Description("Aguardando Nota de Empenho")]
        AguardandoNotaEmpenho = 30,
        [Description("Aguardando Impressão")]
        AguardandoImpressao = 35,
        [Description("Aguardando Entrega da Mercadoria")]
        AguardandoEntregaMercadoria = 40,
		[Description("Finalizado")]
		Finalizado = 50,
        [Description("Reprovado")]
	    Reprovado = 60,
        [Description("Cancelado")]
	    Cancelado = 70
	}
}
