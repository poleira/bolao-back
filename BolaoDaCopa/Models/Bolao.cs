namespace BolaoDaCopa.Models
{
    public class Bolao
    {
        public virtual int Id { get; set; }
        public virtual string Nome { get; set; }
        public virtual string Logo { get; set; }
        public virtual string TokenAcesso { get; set; }
        public virtual string Aviso { get; set; }
        public virtual string? Senha { get; set; }
        public virtual Usuario UsuarioAdm { get; set; }
        public virtual IEnumerable<Usuario> Usuarios { get; set; }
        public virtual IEnumerable<BolaoRegra> Regras { get; set; }

        protected Bolao() { }

        public Bolao(string nome, string logo, string aviso, string senha, Usuario usuarioAdm)
        {
            Nome = nome;
            Logo = logo;
            Aviso = aviso;
            Senha = senha;
            UsuarioAdm = usuarioAdm;
        }
    }
}
