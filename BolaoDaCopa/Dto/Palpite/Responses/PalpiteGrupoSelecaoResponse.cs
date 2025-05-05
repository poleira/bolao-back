using BolaoDaCopa.Models;

namespace BolaoDaCopa.Dto.Palpite.Responses
{
    public class PalpiteGrupoSelecaoResponse
    {
        public int Id { get; set; }
        public Grupo Grupo { get; set; }
        public Selecao Selecao { get; set; }
        public int PontuacaoSelecao { get; set; }
    }
}
