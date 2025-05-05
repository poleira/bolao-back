namespace BolaoDaCopa.Dto.Palpite.Requests
{
    public class CriarPalpiteGrupoSelecaoRequest
    {
        public string HashUsuario { get; set; }
        public string HashBolao { get; set; }
        public int IdGrupo { get; set; }
        public int IdSelecao { get; set; }
        public int PontuacaoSelecao { get; set; }
    }
}
