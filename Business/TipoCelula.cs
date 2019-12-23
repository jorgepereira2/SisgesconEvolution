using System.ComponentModel;

namespace Marinha.Business
{
    public enum TipoCelula
    {
        [Description("Seção")]
        Secao = 1,
        [Description("Divisão")]
        Divisao = 2,
        [Description("Departamento")]
        Departamento = 3
        
    }
}