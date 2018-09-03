using Microsoft.VisualStudio.TestTools.UnitTesting;
using CopaFilmes.IO.Api.DAO;
using System.Threading.Tasks;

namespace CopaFilmes.IO.Api.Testes.DAO
{
    [TestClass]
    public class FilmesDAOTest
    {
        FilmesDAO Dao;

        [TestMethod]
        public void ListarFilmes()
        {
            Dao = new FilmesDAO();

            var result = Dao.ListarFilmes();

            Assert.IsTrue((result.Count > 0), "Nenhum filme foi retornado.");
        }

        [TestMethod]
        public void ObterFilmePorId()
        {
            Dao = new FilmesDAO();

            var result = Dao.ObterFilmePorId("tt2854926");

            Assert.IsTrue((result != null), "Nenhum filme encontrado com o Id informado.");
        }
    }
}