using BolaoDaCopa.Models;

namespace BolaoDaCopa.Dto.Boloes.Responses
{
    public class PremioResponse
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public int Colocacao { get; set; }
    }
}
