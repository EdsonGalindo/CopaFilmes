using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CopaFilmes.IO.Api.DAO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CopaFilmes.IO.Api.Business;
using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json;

namespace CopaFilmes.IO.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CopaFilmesController : ControllerBase
    {
        FilmesDAO DadosFilmes;

        public CopaFilmesController()
        {
            DadosFilmes = new FilmesDAO();
        }

        [Route("")]
        [Route("filmes-disponiveis")]
        [HttpGet]
        [EnableCors("PermiteAcessoSites")]
        public JsonResult ListarFilmesDisponiveis()
        {
            return new JsonResult(DadosFilmes.ListarFilmes());
        }

        [Route("")]
        [Route("filmes-selecionados")]
        [HttpGet]
        [EnableCors("PermiteAcessoSites")]
        public JsonResult RealizarPartidasCopa(string FilmesCopa)
        {
            string[] FilmesCopaID = FilmesCopa.Split(", ");

            CopaFilmesBusiness CopaFilmes = new CopaFilmesBusiness(FilmesCopaID);
            var resultadoFinal = CopaFilmes.IniciarCopa();

            return new JsonResult(resultadoFinal);
        }
    }
}