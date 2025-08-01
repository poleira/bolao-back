namespace BolaoDaCopa.Dto.Palpite.Requests
{
    public class CriarPalpiteJogoGrupoRequest
    {
        public string HashBolao { get; set; }
        public int IdGrupo { get; set; }
        public int IdSelecao1 { get; set; }
        public int IdSelecao2 { get; set; }
        public int PlacarSelecao1 { get; set; }
        public int PlacarSelecao2 { get; set; }

    }
}
