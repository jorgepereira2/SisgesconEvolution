using System;
using System.ComponentModel;

namespace Marinha.Business
{
	public enum StatusPedidoServicoEnum
    {
        [Description("Nenhum")]
        Nenhum = 1,

        [Description("Cadastro Inicial do PS")]
        NaoEnviado = 10,

        [Description("Aguardando DAC")]
        AguardandoDAC = 20,

        [Description("Aprovar Envio Para Delineamento")]
        AguardandoEnvioParaDelineamento = 30,

        [Description("Aguardando Envio Para Delineamento")]
        AguardandoEnvioParaDelineamentoAprovar = 35,

        [Description("Aguardando Delineamento")]
        EmDelineamento = 40,

        [Description("Aguardando Orçamento")]
        AguardandoOrcamento = 50,

        [Description("Aguardando Aprovação Encarregado DPCP")]
        OrcamentoFinalizado = 60,

        //[Description("Aguardando Aprovação Comandante DCPC")]
        //AguardandoAprovacaoComandanteDCPC = 70,

        [Description("Aguardando Aprovação Comandante Geral")]
        AguardandoAprovacaoComandanteGeral = 80,

        [Description("Aguardando Envio Mensagem Cliente")]
        AguardandoEnvioMensagemCliente = 90,

        [Description("Aguardando Aprovação Cliente")]
        AguardandoAprovacaoCliente = 100,

        [Description("Aguardando Verificação do Estoque")]
        AguardandoVerificacaoEstoque = 105,

        [Description("Aguardando Comprometimento Cliente")]
        AguardandoComprometimentoCliente = 110,

        // ENTRE ESTAS ETAPAS CRIR O "PO/PS"

        [Description("Aguardando Planejamento")]
        AguardandoPlanejamento = 120,

        [Description("Aguardando Chamada Meio")]
        AguardandoChamadaMeio = 130,

        [Description("Aguardando Meio")]
        AguardandoMeio = 140,

        [Description("Aguardando Inicio Execução")]
        AguardandoInicioExecucao = 150,

        [Description("Em Execução")]
        EmExecucao = 160,

        [Description("Aguardando Mensagem Prontificação")]
        AguardandoMensagemProntificacao = 170,

        [Description("Aguardando Emissão Faturamento Final ")]
        AguardandoEmissaoFaturamentoFinal = 180,

        [Description("Aguardando Devolução Meio")]
        AguardandoDevolucaoMeio = 190,

        [Description("Aguardando Satisfeito")]
        AguardandoSatisfeito = 200,

        [Description("Aguardando Liquidação")]
        AguardandoLiquidacao = 210,

        [Description("Cancelado")]
        Cancelado = 250,

        [Description("Finalizado")]
        Finalizado = 260,

        [Description("Em Garantia")]
        EmGarantia = 270,










        // ETAPAS DO POSEIDON

        //      [Description("Não Enviado")]
        //      NaoEnviado = 10,

        //      [Description("Aguardando Triagem")]
        //      AguardandoTriagem = 20,

        //      [Description("Aguardando Delineamento")]
        //      AguardandoDelineamento = 30,

        //      [Description("Aguardando Aprovação Delineamento")]
        //      AguardandoAprovacaoDelineamento = 40,

        //      [Description("Aguardando Designação do Cotador")]
        //      AguardandoDesignacaoCotador = 50,

        //      [Description("Aguardando Cotação")]
        //      AguardandoCotacao = 60,

        //      [Description("Aguardando Aprovação Cotação")]
        //      AguardandoAprovacaoCotacao = 70,

        //      [Description("Aguardando Envio Mensagem Cliente")]
        //      AguardandoEnvioMensagemCliente = 90,

        //      //[Description("Aguardando Aprovação Cliente")]
        //      //AguardandoAprovacaoCliente  = 100,

        //      [Description("Aguardando Indicação do recurso")]
        //      AguardandoIndicacaoRecurso = 110,

        //      [Description("Aguardando Planejamento")]
        //      AguardandoPlanejamento = 120,

        //      [Description("Aguardando Chamada Meio")]
        //      AguardandoChamadaMeio = 130,

        //      [Description("Aguardando o Meio")]
        //      AguardandoMeio = 140,

        //      [Description("Aguardando Início Execução")]
        //      AguardandoInicioExecucao = 150,

        //      [Description("Em Execução")]
        //      EmExecucao = 160,

        //      [Description("Aguardando Satisfeito")]
        //      AguardandoSatisfeito = 170,

        //      [Description("Aguardando Emissão Faturamento Final")]
        //      AguardandoEmissaoFaturamentoFinal = 180,

        //      [Description("Cancelado")]
        //      Cancelado = 250,

        //      [Description("Finalizado")]
        //      Finalizado = 260,

        //      [Description("Em Garantia")]
        //      EmGarantia = 270
    }
}