using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Refit;
using CargaProdutos.Models;
using CargaProdutos.Interfaces;

namespace CargaProdutos
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile($"appsettings.json");
            var config = builder.Build();

            string urlBase = config.GetSection("APIProdutos_Access:UrlBase").Value;

            // Envio da requisição a fim de autenticar
            // e obter o token de acesso
            var loginAPI = RestService.For<ILoginAPI>(urlBase);
            Token token = loginAPI.PostCredentials(
                new User()
                {
                    UserID = config.GetSection("APIProdutos_Access:UserID").Value,
                    Password = config.GetSection("APIProdutos_Access:Password").Value
                }).Result;
            Console.WriteLine(JsonConvert.SerializeObject(token));

            if (token.Authenticated)
            {
                // Associar o token aos headers do objeto
                // gerado via Refit
                var produtosAPI = RestService.For<IProdutosAPI>(urlBase,
                    new RefitSettings()
                    {
                        AuthorizationHeaderValueGetter = () =>
                            Task.FromResult(token.AccessToken)
                    });

                Console.WriteLine(JsonConvert.SerializeObject(
                    produtosAPI.IncluirProduto(
                        new Produto()
                        {
                            CodigoBarras = "00005",
                            Nome = "Teste Produto 05",
                            Preco = 5.05
                        }).Result));


                Console.WriteLine("Produtos cadastrados: " +
                    JsonConvert.SerializeObject(
                        produtosAPI.ListarProdutos().Result));
            }

            Console.WriteLine("\nFinalizado!");
            Console.ReadKey();
        }
    }
}