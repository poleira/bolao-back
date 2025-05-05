using BolaoDaCopa.Dto.Fases.Responses;
using BolaoDaCopa.Models;

namespace BolaoDaCopa.Dto.Palpite.Responses
{
    public class PalpiteFaseSelecaoResponse
    {
        public int Id { get; set; }
        public FaseResponse Fase { get; set; }
        public Selecao Selecao { get; set; }

    }
}
