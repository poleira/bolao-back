using BolaoDaCopa.Dto.BoloesRegras.Responses;

namespace BolaoDaCopa.Dto.Boloes.Responses
{
    public class BolaoResponse
    {
    public int? IdModoJogo { get; set; } // continua como int para response
        public string Nome { get; set; } = string.Empty;
        public string Logo { get; set; } = string.Empty;
        public string TokenAcesso { get; set; } = string.Empty;
        public string Aviso { get; set; } = string.Empty;
        public string Administrador { get; set; }
        public IEnumerable<PremioResponse> Premios { get; set; } = new List<PremioResponse>();
        public IEnumerable<BolaoRegraResponse> Regras { get; set; } = new List<BolaoRegraResponse>();
    }
}
