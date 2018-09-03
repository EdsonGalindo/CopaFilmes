using Microsoft.VisualStudio.TestTools.UnitTesting;
using CopaFilmes.IO.Api.Business;
using CopaFilmes.IO.Api.DAO;
using System.Linq;

namespace CopaFilmes.IO.Api.Testes.Business
{
    [TestClass]
    public class CopaFilmesTest
    {
        FilmesDAO Dao;
        CopaFilmesBusiness CopaFilmesBusiness;

        [TestMethod]
        public void IniciarCopa()
        {
            Dao = new FilmesDAO();
            var filmesCopa = Dao.ListarFilmes().Take(8).Select(f => f.Id).ToArray<string>();
            CopaFilmesBusiness = new CopaFilmesBusiness(filmesCopa);

            var result = CopaFilmesBusiness.IniciarCopa();

            Assert.IsTrue(result != null, "Nenhum filme vencedor, houve um erro!");
        }
    }
}
