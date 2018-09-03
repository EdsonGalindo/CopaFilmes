using CopaFilmes.IO.Api.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CopaFilmes.IO.Api.DAO
{
    public sealed class FilmesDAO
    {
        private List<Filme> Filmes { get; set; }
        static readonly String endPointApiFilmes = "http://copafilmes.azurewebsites.net/";
        static readonly String rotaApiFilmes = "api/filmes";
        public Task InicializacaoAssinc { get; private set; }

        public List<Filme> ListarFilmes()
        {
            return Filmes;
        }

        public Filme ObterFilmePorId(string Id)
        {
            return Filmes.Where(f => f.Id == Id).FirstOrDefault();
        }

        public List<Filme> ObterFilmesPorId(string[] Id)
        {
            return Filmes.Where(f => Id.Contains(f.Id)).ToList();
        }

        public FilmesDAO()
        {
            Task.Run(() => InicializarBuscaFilmesAssinc()).Wait();
        }

        private async Task InicializarBuscaFilmesAssinc()
        {
            Filmes = await Task.Run(() => ObterDadosFilmes());
        }

        private static async Task<List<Filme>> ObterDadosFilmes()
        {
            List<Filme> retorno = new List<Filme>();

            using (var clienteHttp = new HttpClient())
            {
                clienteHttp.BaseAddress = new Uri(endPointApiFilmes);
                clienteHttp.DefaultRequestHeaders.Clear();
                clienteHttp.DefaultRequestHeaders.Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                await Task.Delay(100);

                HttpResponseMessage acessoApi = await clienteHttp.GetAsync(rotaApiFilmes);
                if (!acessoApi.IsSuccessStatusCode)
                    return retorno;

                var conteudoRetorno = await acessoApi.Content.ReadAsStringAsync();
                if (conteudoRetorno.Length == 0)
                    return retorno;

                retorno = JsonConvert.DeserializeObject<List<Filme>>(conteudoRetorno);
            }

            return retorno;
        }
    }
}
