using BolaoDaCopa.Dto.Regras.Responses;
using BolaoDaCopa.Dto.ModosJogos.Responses;

namespace BolaoDaCopa.Aplicacao.ModosJogos.Servicos.Interfaces
{
    public interface IModosJogosServicos
    {
        IEnumerable<RegraResponse> ListarRegrasModoJogo(int idModoJogo);
        ModoJogoResponse? ObterModoPorHashBolao(string hashBolao);
    }
}
