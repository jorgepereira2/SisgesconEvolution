using System;
using System.ComponentModel;

namespace Marinha.Business
{
	public enum StatusOMFEnum
	{
        [Description("Não Enviado")]
        NaoEnviado = 10,
        [Description("Emissão do TAV")]
		EmissaoTAV = 20,
        [Description("Solicitar Perícia Material")]
        SolicitarPericiaMaterial = 30,
        [Description("Aguardando Resultado da Perícia")]
        AguardandoResultadoPericia = 40,
        [Description("Aguardando Processamento")]
        AguardandoProcessamento = 50,
        [Description("Aguardando Encaminhamento pelo Fiel de Material")]
        AguardandoEncaminhamentoFielMaterial = 53,
        [Description("Aguardando Encaminhamento pelo Fiel de Armazenagem")]
        AguardandoEncaminhamentoFielArmazenagem = 56,
        [Description("Aguardando Encaminhamento pela Contabilidade")]
        AguardandoEncaminhamentoPelaContabilidade = 60,
        [Description("Aguardando Encaminhamento pela Sessão de Controle")]
        AguardandoEncaminhamentoPelaSessaoControle = 70,
        [Description("Enviado para o MATCFN")]
        EnviadoMATCFN = 80,
        [Description("Finalizado")]
        Finalizado = 100,
        [Description("Cancelado")]
	    Cancelado = 110
	}
}
