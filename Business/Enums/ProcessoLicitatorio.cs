using System;
using System.ComponentModel;

namespace Marinha.Business
{
	public enum ProcessoLicitatorio
	{
        [Description("Processo Licitatório")]
        ProcessoLicitatorio = 1,
        [Description("Termo de justificativa de dispensa de licitação")]
		TermoJustificativaDispensaLicitação = 2,
        [Description("Termo de justificativa de inexibilidade de licitação")]
		TermoJustificativaInexibilidadeLicitação = 3
	}
}
