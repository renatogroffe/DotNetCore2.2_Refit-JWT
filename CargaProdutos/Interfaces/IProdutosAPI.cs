using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;
using CargaProdutos.Models;

namespace CargaProdutos.Interfaces
{
    public interface IProdutosAPI
    {
        [Get("/Produtos")]
        [Headers("Authorization: Bearer")]
        Task<List<Produto>> ListarProdutos();

        [Post("/Produtos")]
        [Headers("Authorization: Bearer")]
        Task<ResultadoAPIProdutos> IncluirProduto([Body]Produto produto);
    }
}