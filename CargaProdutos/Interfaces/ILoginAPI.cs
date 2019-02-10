using System.Threading.Tasks;
using Refit;
using CargaProdutos.Models;

namespace CargaProdutos.Interfaces
{
    public interface ILoginAPI
    {
        [Post("/login")]
        Task<Token> PostCredentials(User user);
    }
}