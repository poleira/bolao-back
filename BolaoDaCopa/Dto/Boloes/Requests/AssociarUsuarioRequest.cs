
namespace BolaoDaCopa.Dto.Boloes.Requests
{
    public class AssociarUsuarioRequest
    {
        public int? IdUsuario { get; set; }
        public string HashBolao { get; set; }
        public string Senha { get; set; }
        public string? HashUsuarioASerDeletado { get; set; } //Necessario para desassociar o usuario do bolao
    }
}
