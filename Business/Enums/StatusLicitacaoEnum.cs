using System;
using System.ComponentModel;

namespace Marinha.Business
{
	public enum StatusLicitacaoEnum
	{
        [Description("Pedido de Licitação")]
        PedidoLicitacao = 5,
        [Description("Cadastro inicial da Licitação")]
		CadastroInicial = 10,
        [Description("Elaborando processos")]
		ElaborandoProcessos = 20,
        [Description("Nota técnica")]
        NotaTecnica = 30,
        [Description("Alterações pendentes de fiscalização")]
        AlteracoesPendentesFiscalizacao = 40,
        [Description("Parecer juridico - AGU")]
        ParecerJuridico = 50,
        [Description("Alterações após parecer juridico")]
        AlteracoesAposParecerJuridico = 60,
        [Description("Período de Divulgação")]
        PeriodoDivulgacao = 70,
        [Description("Analise de Impugnação ao edital")]
        AnaliseImpugnacaoEdital = 80,
        [Description("Sessão publica")]
        SessaoPublica = 90,
        [Description("Fornecedor Habilitado")]
        FornecedorHabilitado = 100,
        [Description("Periodo recursal")]
        PeriodoRecursal = 110,
        [Description("Fornecedor Adjudicado")]
        FornecedorAdjudicado = 120,
        [Description("Fornecedor homologado")]
        FornecedorHomologado = 130,
        [Description("Contrato Assinado")]
        ContratoAssinado = 140,
        [Description("Finalizado")]
        Finalizado = 150,
		[Description("Cancelado")]
		Cancelado = 200
	}
}
