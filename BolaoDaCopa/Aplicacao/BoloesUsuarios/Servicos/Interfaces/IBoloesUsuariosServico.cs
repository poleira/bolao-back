using BolaoDaCopa.Dto.Boloes.Requests;
using BolaoDaCopa.Dto.BoloesUsuarios.Responses;

namespace BolaoDaCopa.Aplicacao.BoloesUsuarios.Servicos.Interfaces
{
    public interface IBoloesUsuariosServico
    {
        BolaoUsuarioResponse Recuperar(int id);
        IEnumerable<BolaoUsuarioResponse> ListarBoloesPorUsuario(int idUsuario);
        void AssociarUsuarioBolaoViaHub(AssociarBolaoUsuarioViaHubRequest request);
    }
}
