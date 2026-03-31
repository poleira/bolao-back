using BolaoDaCopa.Dto.JogosGrupo.Requests;
using BolaoDaCopa.Dto.JogosGrupo.Responses;

namespace BolaoDaCopa.Aplicacao.JogosGrupo.Servicos.Interfaces
{
    public interface IJogosGrupoServico
    {
        IList<JogoGrupoResponse> ListarJogosGrupo(JogoGrupoRequest request);
        IList<JogoGrupoResponse> ListarJogosBrasil();
    }
}
