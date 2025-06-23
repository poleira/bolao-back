namespace BolaoDaCopa.Dto.Boloes.Requests
{
    public class AssociarBolaoUsuarioViaHubRequest
    {
        public int? IdUsuario { get; set; }
        public string NomeBolao { get; set; }
        public string Senha { get; set; }
    }
}
