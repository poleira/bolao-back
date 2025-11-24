using BolaoDaCopa.Dto.Selecoes.Responses;

namespace BolaoDaCopa.Dto.Palpite.Responses
{
    public class PalpiteTerceiroLugarResponse
    {
        public int Id { get; set; }
        public GrupoSelecaoResponse Selecao { get; set; }
        public int Posicao { get; set; }
    }
}
