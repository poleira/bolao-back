
namespace BolaoDaCopa.Dto.Boloes.Requests
{
    public class AssociarUsuarioRequest
    {
        public string HashUsuario { get; set; }
        public string HashBolao { get; set; }
        public string Senha { get; set; }
        public string? HashUsuarioLogado { get; set; } //Necessario para desassociar o usuario do bolao
    }
}
