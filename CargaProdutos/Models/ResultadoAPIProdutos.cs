using System.Collections.Generic;

namespace CargaProdutos.Models
{
    public class ResultadoAPIProdutos
    {
            public string Acao { get; set; }
            public bool Sucesso { get; set; }
            public List<string> Inconsistencias { get; set; }
    }
}