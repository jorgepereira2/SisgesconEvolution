using System;
using System.ComponentModel;

namespace Marinha.Business
{
	public enum TipoCalculoLicitacaoItem
	{
		[Description("Valor Médio")]
		ValorMedio = 1,
		[Description("Menor Valor")]
		MenorValor = 2
	}
}
