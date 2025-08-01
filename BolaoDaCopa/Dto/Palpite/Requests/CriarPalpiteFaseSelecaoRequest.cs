namespace BolaoDaCopa.Dto.Palpite.Requests
{
    public class CriarPalpiteFaseSelecaoRequest
    {
        public int IdFase { get; set; }
        public int IdSelecao { get; set; }
        public string HashBolao { get; set; }
    }
}
