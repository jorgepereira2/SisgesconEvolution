using System;
using System.ComponentModel;

namespace Marinha.Business
{
	public enum StatusPedidoAquisicaoEnum
	{
		[Description("Não Enviado")]
		NaoEnviado = 10,
        [Description("Aguardando Aprovação Encarregado Divisão")]
        AguardandoAprovacaoEncarregadoDivisao = 20,
        [Description("Aguardando Aprovação Encarregado Departamento")]
        AguardandoAprovacaoEncarregadoDepartamento = 25,
        [Description("Aguardando Parecer Agente Financeiro")]
        AguardandoParecerAgenteFinanceiro = 30,
        [Description("Aguardando Aprovações")]
        AguardandoAprovacoes = 40,
        [Description("Aguardando Empenho")]
        AguardandoEmpenho = 50,
        [Description("Aguardando Parecer Divisão Suprimentos")]
        AguardandoParecerDivisaoSuprimentos = 60,
        [Description("Aguardando Liquidação")]
        AguardandoLiquidacao=65,
        [Description("Finalizado")]
        Finalizado = 70,
        [Description("Reprovado")]
        Reprovado = 80,
	    
	}
}
