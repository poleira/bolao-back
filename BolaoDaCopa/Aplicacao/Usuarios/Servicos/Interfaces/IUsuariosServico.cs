using BolaoDaCopa.Dto.Autenticacao.Responses;
using BolaoDaCopa.Dto.Usuarios;
using BolaoDaCopa.Dto.Usuarios.Requests;

namespace BolaoDaCopa.Aplicacao.Usuarios.Servicos.Interfaces
{
    public interface IUsuariosServico
    {
        AutenticacaoResponse Inserir(UsuarioRequest request);
        Task<AutenticacaoResponse> Autenticar(LoginRequest request);
        UsuarioResponse ObterUsuarioLogado(int idUsuario);
        void VerificarUsuarioExistente(VerificarUsuarioExistenteRequest request);
        void AlterarNome(int idUsuario, string novoNome);
    }
}
