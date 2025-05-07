using BolaoDaCopa.Dto.BoloesUsuarios.Responses;

namespace BolaoDaCopa.Aplicacao.BoloesUsuarios.Servicos.Interfaces
{
    public interface IBoloesUsuariosServico
    {
        BolaoUsuarioResponse Recuperar(int id);
        IEnumerable<BolaoUsuarioResponse> ListarBoloesUsuario(string hashUsuario);
    }
}
