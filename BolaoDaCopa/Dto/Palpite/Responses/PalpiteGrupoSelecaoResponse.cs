using BolaoDaCopa.Dto.Selecoes.Responses;
using BolaoDaCopa.Models;

namespace BolaoDaCopa.Dto.Palpite.Responses
{
    public class PalpiteGrupoSelecaoResponse
    {
        public int Id { get; set; }
        public GrupoResponse Grupo { get; set; }
        public GrupoSelecaoResponse Selecao { get; set; }
        public int? PontuacaoSelecao { get; set; }
        public int PosicaoSelecao { get; set; }
    }
}
