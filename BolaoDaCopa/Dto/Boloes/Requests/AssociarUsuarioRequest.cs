
namespace BolaoDaCopa.Dto.Boloes.Requests
{
    public class AssociarUsuarioRequest
    {
        public int IdUsuario { get; set; }
        public string HashBolao { get; set; }
        public string Senha { get; set; }
    }
}
