namespace BolaoDaCopa.Dto.Palpite.Requests
{
    public class CriarPalpiteArtilheiroRequest
    {
        public int IdUsuario { get; set; }
        public string HashBolao { get; set; }
        public int JogadorId { get; set; }
    }
}
