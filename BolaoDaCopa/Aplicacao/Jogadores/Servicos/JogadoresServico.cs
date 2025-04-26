using BolaoDaCopa.Aplicacao.Jogadores.Servicos.Interfaces;
using BolaoDaCopa.Dto.Jogadores.Requests;
using BolaoDaCopa.Infra.Repositorios.NovaPasta.Interfaces;
using BolaoDaCopa.Models;

namespace BolaoDaCopa.Aplicacao.Jogadores.Servicos
{
    public class JogadoresServico : IJogadoresServico
    {
        public IJogadoresRepositorio jogadoresRepositorio;
        public JogadoresServico(IJogadoresRepositorio jogadoresRepositorio)
        {
            this.jogadoresRepositorio = jogadoresRepositorio;
        }

        public IEnumerable<Jogador> ListarJogadores(JogadoresListarRequest request)
        {
            var jogadores = jogadoresRepositorio.Query();

            if (!string.IsNullOrEmpty(request.Nome))
            {
                jogadores = jogadores.Where(x => x.Nome.Contains(request.Nome));
            }

            jogadores = jogadores.Take(10);

            return jogadores.ToList();
        }

    }
}

