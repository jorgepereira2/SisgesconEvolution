using System;
using System.ComponentModel;

namespace Marinha.Business
{
	public enum TipoValor
	{
        [Description("Material Indireto")]
		MaterialIndireto = 1,
        [Description("Serviço Indireto")]
		ServicoIndireto = 2,
        [Description("Mão-de-Obra Indireta")]
        MaoDeObraIndireta = 3
	}
}
