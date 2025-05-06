namespace BolaoDaCopa.Dto.Boloes.Responses
{
    public class BolaoResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Logo { get; set; } = string.Empty;
        public string TokenAcesso { get; set; } = string.Empty;
        public string Aviso { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public IEnumerable<PremioResponse> Premios { get; set; } = new List<PremioResponse>();
        public IEnumerable<BolaoRegraResponse> Regras { get; set; } = new List<BolaoRegraResponse>();
    }
}
