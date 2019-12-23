using System;
using System.ComponentModel;

namespace Marinha.Business
{
	public enum TipoOperacaoFinanceira
	{
		[Description("Entrada")]
		Entrada = 1,
		[Description("Transferência")]
		Transferencia = 2,
        [Description("Saída")]
        Saida = 3,
        [Description("Saída Direta")]
        SaidaDireta = 4
	}
}
