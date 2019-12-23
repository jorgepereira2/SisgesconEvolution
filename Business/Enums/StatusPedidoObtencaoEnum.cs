using System;
using System.ComponentModel;

namespace Marinha.Business
{
	public enum StatusPedidoObtencaoEnum
	{
        [Description("Cadastro Inicial do AC")]
        NaoEnviado = 10,

        //[Description("Aguardando Verificação Paiol")]
        //AguardandoVerificacaoPaiol = 20,

        //[Description("Aguardando Ap. Enc. Dep. Material")]
        //AguardandoAprovacaoEncarregadoDepartamentoMaterial = 30,

        [Description("Aguardando Aprovação Chefe Dep. (DPCP)")]
        AguardandoAprovacaoChefeDepartamento_Servidor = 40,

        [Description("Aprovação Enc. De Divisão")]
        AprovacaoEncDivisão = 50,

        [Description("Aguardando Aprovação Chefe Dep.")]
        AguardandoAprovacaoChefeDepartamento_Departamento = 55,

        [Description("Aguardando Designação Comprador")]
        AguardandoDesignacaoComprador = 58,

        [Description("Aguardando Cotações")]
        AguardandoAvaliacaoEncarregadoObtencao = 64,

        [Description("Conferência execução financeira")]
        DefiniçãoFinanceiraRelator = 150,

        [Description("Aguardando Aprovação do Agente Fiscal")]
        AguardandoAprovacaoAgenteFiscal = 155,

        [Description("Aguardando Aprovação Comandante")]
        AguardandoAprovacaoOrdenadorDespesa = 160,

        [Description("Aguardando Nota de Empenho")]
        AguardandoEnvioEmpenho = 185,

        [Description("Aguardando Impressão AC")]
        AguardandoEntregaExecucao = 190,

        [Description("Aguardando Entrega de Mercadoria")]
        AguardandoEntregaMercadoria = 200,

        [Description("Finalizado")]
        Finalizado = 210,

        [Description("Reprovado")]
        Reprovado = 220,

        [Description("Cancelado")]
        Cancelado = 230,

        // EXCLUIDOS
        [Description("AguardandoAprovacaoEncarregadoDepartamento")]
        AguardandoAprovacaoEncarregadoDepartamento = 100010,

        [Description("AguardandoAprovacaoPAR")]
        AguardandoAprovacaoPAR = 100020,

        [Description("AguardandoAprovacaoEncarregadoDivisao")]
        AguardandoAprovacaoEncarregadoDivisao = 100030,

        [Description("AguardandoLiquidacao")]
        AguardandoLiquidacao = 100040,

        [Description("AguardandoAprovacaoSupervisaoCotacao")]
        AguardandoAprovacaoSupervisaoCotacao = 100050,

        [Description("AguardandoCreditoEmpenho")]
        AguardandoCreditoEmpenho = 100060,

        [Description("AguardandoPagamentoPO")]
        AguardandoPagamentoPO = 100070,
	}
}