namespace BolaoDaCopa.Dto.Boloes.Requests
{
    public class CriarBolaoRequest
    {
        public string Nome { get; set; }
        public string Logo { get; set; }
        public string Aviso { get; set; }
        public string HashUsuario { get; set; }
        public string Senha { get; set; } 
        public bool Privado { get; set; }
        public InserirRegraBolaoRequest[] InserirRegraBolaoRequests { get; set; }
        public InserirPremioBolaoRequest[] InserirPremioBolaoRequests { get; set; }

    }
}
