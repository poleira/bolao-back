using NHibernate.Mapping.ByCode.Impl;

namespace BolaoDaCopa.Dto.Boloes.Responses
{
    public class BolaoListarResponse
    {
        public string Nome { get; set; } = string.Empty;
        public string[] Premio { get; set; } = Array.Empty<string>();
        public string Usuario { get; set; } = string.Empty;
        public bool? Privado { get; set; }
        public bool? TemSenha { get; set; }
    }
}
