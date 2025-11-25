namespace BolaoDaCopa.Dto.Palpite.Responses
{
    public class EliminatoriasResponse
    {
        public IList<JogoEliminatoriaResponse> RodadaDe16 { get; set; } = new List<JogoEliminatoriaResponse>();
        public IList<JogoEliminatoriaResponse> Oitavas { get; set; } = new List<JogoEliminatoriaResponse>();
        public IList<JogoEliminatoriaResponse> Quartas { get; set; } = new List<JogoEliminatoriaResponse>();
        public IList<JogoEliminatoriaResponse> Semis { get; set; } = new List<JogoEliminatoriaResponse>();
        public JogoEliminatoriaResponse? Finais { get; set; }
    }
}
