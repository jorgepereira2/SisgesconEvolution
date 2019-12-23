using System;
using System.ComponentModel;

namespace Marinha.Business
{
	public enum TipoSanguineo
	{
        [Description("Não informado")]
        NaoInformado = 0,
		[Description("AB+")]
		ABPositivo = 1,
		[Description("A+")]
		APositivo = 2,
        [Description("B+")]
        BPositivo = 3,
        [Description("O+")]
		OPositivo = 4,
        [Description("AB-")]
		ABNegativo = 5,
        [Description("A-")]
		ANegativo = 6,
        [Description("B-")]
		BNegativo = 7,
        [Description("O-")]
        ONegativo = 8


	}
}
