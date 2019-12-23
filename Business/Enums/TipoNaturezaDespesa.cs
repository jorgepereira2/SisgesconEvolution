using System;
using System.ComponentModel;

namespace Marinha.Business
{
	public enum TipoNaturezaDespesa
	{
		[Description("Financeiro")]
		Financeiro = 1,
		[Description("Patrimonial")]
		Patrimonial = 2,
        [Description("Serviço")]
        Servico = 3
	}
}
