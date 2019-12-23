using System;
using System.ComponentModel;

namespace Marinha.Business
{
	public enum SistemaLicitatorio
	{
        [Description("SPP - Sistema de Preço Praticado")]
		SPP = 1,
        [Description("SRP - Sistema de Registro de Preços")]
		SRP = 2
	}
}
