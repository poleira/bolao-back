namespace BolaoDaCopa.Dto.JogosGrupo.Responses
{
    public class JogoGrupoResponse
    {
        public int Id { get; set; }
        public GrupoJogoResponse Grupo { get; set; }
        public SelecaoJogoResponse Selecao1 { get; set; }
        public SelecaoJogoResponse Selecao2 { get; set; }
        public int? PlacarSelecao1 { get; set; }
        public int? PlacarSelecao2 { get; set; }
    }

    public class GrupoJogoResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }

    public class SelecaoJogoResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Abreviacao { get; set; }
        public string Logo { get; set; }
    }
}
