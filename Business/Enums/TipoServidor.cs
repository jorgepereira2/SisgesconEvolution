using System;
using System.ComponentModel;

namespace Marinha.Business
{
	public enum TipoServidor
	{
		[Description("Civil")]
		Civil = 1,
		[Description("Militar da Gola")]
		MilitarGola = 2,
		[Description("Fuzileiro Naval")]
		FuzileiroNaval
	}
}
