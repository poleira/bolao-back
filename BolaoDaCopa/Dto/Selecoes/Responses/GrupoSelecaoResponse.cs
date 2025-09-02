namespace BolaoDaCopa.Dto.Selecoes.Responses
{
    public class GrupoSelecaoResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public GrupoResponse Grupo { get; set; }
        public string Logo { get; set; }
        public string Abreviacao { get; set; }
        public int? Pontuacao { get; set; }
        public int? PosicaoFaseDeGrupos { get; set; }
    }
}
