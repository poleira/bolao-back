using BolaoDaCopa.Dto.Regras.Responses;

namespace BolaoDaCopa.Aplicacao.ModoJogo.Servicos.Interfaces
{
    public interface IModoJogoServico
    {
        IEnumerable<RegraResponse> ListarRegrasModoJogo(int idModoJogo);
    }
}
