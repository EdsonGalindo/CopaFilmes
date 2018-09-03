using CopaFilmes.IO.Api.DAO;
using CopaFilmes.IO.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CopaFilmes.IO.Api.Business
{
    public class CopaFilmesBusiness
    {
        private FilmesDAO FilmesCopaDAO = new FilmesDAO();
        private List<Filme> FilmesCopa = new List<Filme>();
        private List<Filme> VencedoresEliminatorias = new List<Filme>();
        private List<Filme> VencedoresSemifinais = new List<Filme>();
        private List<Filme> ResultadoFinal = new List<Filme>();

        public List<Filme> IniciarCopa()
        {
            if (FilmesCopa == null || !(FilmesCopa.Count == 8))
                return ResultadoFinal;

            ExecutarPartidasEliminatorias();
            ExecutarPartidasSemifinais();
            ExecutarPartidaFinal();

            return ResultadoFinal;
        }

        private void ExecutarPartidasEliminatorias()
        {
            do
            {
                var filmesPartida = ObterFilmesPartida(FilmesCopa);

                var vencedor = ExecutarPartida(filmesPartida);

                VencedoresEliminatorias.Add(vencedor);

                RemoverFilmesPartida(FilmesCopa, filmesPartida);
            }
            while (FilmesCopa.Count > 0);
        }

        private void ExecutarPartidasSemifinais()
        {
            do
            {
                var filmesPartida = VencedoresEliminatorias.Take(2).ToList();

                var vencedor = ExecutarPartida(filmesPartida);

                VencedoresSemifinais.Add(vencedor);

                RemoverFilmesPartida(VencedoresEliminatorias, filmesPartida);
            }
            while (VencedoresEliminatorias.Count > 0);
        }

        private void ExecutarPartidaFinal()
        {
            var vencedor = ExecutarPartida(VencedoresSemifinais);

            var segundoLugar = VencedoresSemifinais.Where(f => f.Id != vencedor.Id).FirstOrDefault();

            ResultadoFinal = new List<Filme>() { vencedor, segundoLugar };
        }

        private Filme ExecutarPartida(List<Filme> filmesPartida)
        {
            Filme vencedor = new Filme();

            var filmeUp = filmesPartida[0];
            var filmeDown = filmesPartida[1];

            if (filmeUp.Nota > filmeDown.Nota)
                vencedor = filmeUp;

            if (filmeUp.Nota < filmeDown.Nota)
                vencedor = filmeDown;

            if (filmeUp.Nota == filmeDown.Nota)
            {
                vencedor = (new List<Filme> {
                        filmeUp,
                        filmeDown
                    }).OrderBy(f => f.Titulo).FirstOrDefault();
            }

            return vencedor;
        }

        private List<Filme> ObterFilmesPartida(List<Filme> FilmesDisputa)
        {
            var filmeUp = ObterFilmeUp(FilmesDisputa);
            var filmeDown = ObterFilmeDown(FilmesDisputa);

            return new List<Filme> { filmeUp, filmeDown };
        }

        private void RemoverFilmesPartida(List<Filme> FilmesDisputa, List<Filme> FilmesPartida)
        {
            FilmesDisputa.Remove(FilmesPartida[0]);
            FilmesDisputa.Remove(FilmesPartida[1]);
        }

        private Filme ObterFilmeUp(List<Filme> filmesPartida)
        {
            return filmesPartida.First();
        }

        private Filme ObterFilmeDown(List<Filme> filmesPartida)
        {
            return filmesPartida.Last();
        }

        public CopaFilmesBusiness(string[] FilmesCopaID)
        {
            FilmesCopa = FilmesCopaDAO.ObterFilmesPorId(FilmesCopaID)
                .OrderBy(F => F.Titulo).ToList();
        }
    }
}
