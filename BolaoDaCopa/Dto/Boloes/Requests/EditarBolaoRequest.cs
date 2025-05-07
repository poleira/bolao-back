namespace BolaoDaCopa.Dto.Boloes.Requests
{
    public class EditarBolaoRequest
    {
            public string HashBolao { get; set; } = string.Empty;
            public string Nome { get; set; } = string.Empty;
            public string Logo { get; set; } = string.Empty;
            public string Aviso { get; set; } = string.Empty;
            public string HashUsuario { get; set; } = string.Empty;
            public string Senha { get; set; } = string.Empty;
            public bool Privado { get; set; } = false;
            public InserirRegraBolaoRequest[] InserirRegrasBoloes { get; set; } = Array.Empty<InserirRegraBolaoRequest>();
            public InserirPremioBolaoRequest[] InserirPremiosBoloes { get; set; } = Array.Empty<InserirPremioBolaoRequest>();
    }
}
