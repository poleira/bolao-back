using BolaoDaCopa.Dto.Boloes.Responses;

namespace BolaoDaCopa.Dto.BoloesRegras.Responses
{
    public class BolaoRegraResponse
    {
        public int Id { get; set; }
        public RegraResponse Regra { get; set; }
        public string Descricao { get; set; }
        public string Explicacao { get; set; }
        public int Pontuacao { get; set; }
    }
}
