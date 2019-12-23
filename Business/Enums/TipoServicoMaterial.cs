using System;
using System.ComponentModel;

namespace Marinha.Business
{
	public enum TipoServicoMaterial
	{
		[Description("Serviço")]
		Servico = 1,
		[Description("Material")]
		Material = 2,
        [Description("Material Não Estocável")]
        MaterialNaoEstocavel = 3,
        [Description("Patrimônio")]
        Patrimonio = 4
	}
}
