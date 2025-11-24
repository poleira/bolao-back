namespace BolaoDaCopa.Dto.Palpite.Requests
{
    public class CriarPalpiteTerceiroLugarRequest
    {
        public string HashBolao { get; set; }
        public int IdSelecao { get; set; }
        public int Posicao { get; set; }
    }
}
