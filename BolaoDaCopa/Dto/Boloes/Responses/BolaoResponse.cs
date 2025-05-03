namespace BolaoDaCopa.Dto.Boloes.Responses
{
    public class BolaoResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Logo { get; set; }
        public string TokenAcesso { get; set; }
        public string Aviso { get; set; }
        public string? Senha { get; set; }
    }
}
