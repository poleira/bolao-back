namespace BolaoDaCopa.Models
{
    public class Bolao
    {
        public virtual int Id { get; protected set; }
        public virtual string Nome { get; protected set; }
        public virtual string Logo { get; protected set; }
        public virtual string TokenAcesso { get; protected set; }
        public virtual string Aviso { get; protected set; }
        public virtual string? Senha { get; protected set; }
        public virtual bool Privado { get; protected set; }
        public virtual Usuario UsuarioAdm { get; protected set; }
        public virtual IEnumerable<Usuario> Usuarios { get; protected set; }
        public virtual IEnumerable<BolaoRegra> Regras { get; protected set; }

        protected Bolao() { }

        public Bolao(string nome, string logo, string aviso, string senha, Usuario usuarioAdm)
        {
            Nome = nome;
            Logo = logo;
            Aviso = aviso;
            Senha = senha;
            UsuarioAdm = usuarioAdm;
        }

        public virtual void SetTokenAcesso(string tokenAcesso)
        {
            TokenAcesso = tokenAcesso;
        }
    }
}
