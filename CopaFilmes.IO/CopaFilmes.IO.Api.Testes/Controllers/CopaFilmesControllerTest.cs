using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CopaFilmes.IO.Api.Controllers;
using CopaFilmes.IO.Api.DAO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace CopaFilmes.IO.Api.Testes.Controllers
{
    [TestClass]
    public class CopaFilmesControllerTest
    {
        [TestMethod]
        public void ListarFilmes()
        {
            CopaFilmesController controller = new CopaFilmesController();

            JsonResult result = controller.ListarFilmesDisponiveis();

            Assert.IsTrue(result.Value != null && ((List<Models.Filme>)result.Value).Count > 0,
                "Não foram retornados filmes");
        }

        [TestMethod]
        public void RealizarPartidasCopa()
        {
            var Dao = new FilmesDAO();
            var filmesEscolhidos = string.Join(", ", (Dao.ListarFilmes().Take(8).Select(f => f.Id).ToArray<string>()));
            CopaFilmesController controller = new CopaFilmesController();

            JsonResult result = controller.RealizarPartidasCopa(filmesEscolhidos) as JsonResult;

            Assert.IsTrue(result.Value != null && ((List<Models.Filme>)result.Value).Count > 0, 
                "Não foram retornados filmes");
        }

    }
}
