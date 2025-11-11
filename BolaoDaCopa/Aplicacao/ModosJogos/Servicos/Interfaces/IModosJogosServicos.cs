using BolaoDaCopa.Dto.Regras.Responses;

namespace BolaoDaCopa.Aplicacao.ModosJogos.Servicos.Interfaces
{
    public interface IModosJogosServicos
    {
        IEnumerable<RegraResponse> ListarRegrasModoJogo(int idModoJogo);
    }
}
