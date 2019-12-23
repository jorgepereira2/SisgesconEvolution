using System;
using System.ComponentModel;

namespace Marinha.Business
{
	public enum TipoOperativo
	{
		[Description("Administrativo")]
		Administrativo = 1,
		[Description("Operativo")]
		Operativo = 2
	}
}
