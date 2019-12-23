using System;
using System.ComponentModel;

namespace Marinha.Business
{
	public enum EstadoCivil
	{
        [Description("Não informado")]
        NaoInformado = 0,
        [Description("Solteiro(a)")]
		Solteiro = 1,
        [Description("Casado(a)")]
		Casado = 2,
        [Description("Separado(a) Judicialmente")]
        Separado = 3,
        [Description("Divorciado(a)")]
		Divorciado = 4,
        [Description("Viúvo(a)")]
		Viuvo = 5,
        [Description("União Estável")]
		UniaoEstavel = 6

	}
}
