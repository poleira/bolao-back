namespace BolaoDaCopa.Dto.Palpite.Requests
{
    public class CriarPalpiteArtilheiroBrasilRequest
    {
        public int IdUsuario { get; set; }
        public string HashBolao { get; set; }
        public int JogadorId { get; set; }
    }
}
