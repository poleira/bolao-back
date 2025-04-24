using BolaoDaCopa.Dto.Autenticacao.Responses;
using BolaoDaCopa.Dto.Usuarios.Requests;

namespace BolaoDaCopa.Aplicacao.Usuarios.Servicos.Interfaces
{
    public interface IUsuariosServico
    {
        AutenticacaoResponse Inserir(UsuarioRequest request);
        Task<AutenticacaoResponse> Autenticar(LoginRequest request);
    }
}
