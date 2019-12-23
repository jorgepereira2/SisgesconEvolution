using System;
using System.ComponentModel;

namespace Marinha.Business
{
	public enum TipoMovimentoFinanceiro
	{
		[Description("Entrada")]
		Entrada = 1,
		[Description("Comprometido")]
		SaidaComprometido = 2,
        [Description("Empenhado")]
        SaidaEmpenhado = 3,
        [Description("Finalizado")]
        SaidaFinalizado = 4
	}
}
