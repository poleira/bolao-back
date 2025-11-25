using BolaoDaCopa.Dto.Selecoes.Responses;

namespace BolaoDaCopa.Dto.Palpite.Responses
{
    public class JogoEliminatoriaResponse
    {
        public int NumeroJogo { get; set; }
        public string Fase { get; set; }
        public GrupoSelecaoResponse? Selecao1 { get; set; }
        public GrupoSelecaoResponse? Selecao2 { get; set; }
        public int? ProximoJogoVencedor { get; set; }
    }
}
