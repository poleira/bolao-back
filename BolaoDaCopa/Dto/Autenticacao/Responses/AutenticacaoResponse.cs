using BolaoDaCopa.Dto.Usuarios;

namespace BolaoDaCopa.Dto.Autenticacao.Responses
{
    public class AutenticacaoResponse
    {
        public UsuarioResponse Usuario { get; set; }
        public string Token { get; set; }
    }
}
