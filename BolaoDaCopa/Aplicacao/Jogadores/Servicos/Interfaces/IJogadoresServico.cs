using BolaoDaCopa.Dto.Jogadores.Requests;
using BolaoDaCopa.Models;

namespace BolaoDaCopa.Aplicacao.Jogadores.Servicos.Interfaces
{
    public interface IJogadoresServico
    {
        IEnumerable<Jogador> ListarJogadores(JogadoresListarRequest request);
    }
}
