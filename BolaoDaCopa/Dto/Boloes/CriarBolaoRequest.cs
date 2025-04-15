namespace BolaoDaCopa.Dto.Boloes
{
    public class CriarBolaoRequest
    {
        public string Nome { get; set; }
        public string Logo { get; set; }
        public string TokenAcesso { get; set; }
        public string Aviso { get; set; }
        public int IdUsuario { get; set; }
        public string Senha { get; set; } 

    }
}
