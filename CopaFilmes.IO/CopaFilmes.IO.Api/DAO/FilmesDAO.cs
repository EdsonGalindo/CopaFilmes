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
    public class FilmesDAO
    {
        private List<Filme> Filmes { get; set; }
        private readonly string urlApiFilmes = @"http://copafilmes.azurewebsites.net/api/filmes";

        public List<Filme> ListarFilmes()
        {
            return Filmes;
        }

        public FilmesDAO()
        {
            Filmes = ObterFilmes();
        }

        private List<Filme> ObterFilmes()
        {
            List<Filme> retorno = new List<Filme>();

            using (var clienteHttp = new HttpClient())
            {
                clienteHttp.DefaultRequestHeaders.Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var resultado = clienteHttp.GetAsync(urlApiFilmes).Result;

                if (resultado.StatusCode != HttpStatusCode.OK)
                    return retorno;

                if (resultado.Content.ToString().Length == 0)
                    return retorno;

                retorno = JsonConvert.DeserializeObject<List<Filme>>(resultado.Content.ToString()).ToList();
            }

            return retorno;
        }
    }
}
