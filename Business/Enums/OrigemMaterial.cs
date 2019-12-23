using System;
using System.ComponentModel;

namespace Marinha.Business
{
	public enum OrigemMaterial
	{
		[Description("Obtenção")]
		Obtencao = 1,

        [Description("Singra")]
        Rodizio = 2,

        [Description("Oficina")]
        Oficina = 3,

        [Description("PEP")]
        PEP = 4,

        [Description("Licitação")]
        Licitação = 5
	}
}