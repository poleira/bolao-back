namespace BolaoDaCopa.Models
{
    public class Bolao
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Logo { get; set; }
        public string TokenAcesso { get; set; }
        public string Aviso { get; set; }
        public Usuario UsuarioAdm { get; set; }
        public IEnumerable<Usuario> Usuarios { get; set; }
        public IEnumerable<BolaoRegra> Regras { get; set; }
    }
}
