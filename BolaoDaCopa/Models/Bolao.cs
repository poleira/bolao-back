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
        public virtual bool Privado { get; set; }
        public virtual Usuario UsuarioAdm { get; set; }
        public virtual IEnumerable<Usuario> Usuarios { get; set; }
        public virtual IEnumerable<BolaoRegra> Regras { get; set; }
        public virtual IEnumerable<Premio> Premios { get; set; }

        public Bolao() { }

        public Bolao(string nome, string logo, string aviso, string senha, Usuario usuarioAdm, bool privado)
        {
            Nome = nome;
            Logo = logo;
            Aviso = aviso;
            Senha = senha;
            UsuarioAdm = usuarioAdm;
            Privado = privado;
        }

        public Bolao(int id, string nome, string logo, string tokenAcesso, string aviso, string? senha, bool privado, Usuario usuarioAdm)
        {
            Id = id;
            Nome = nome;
            Logo = logo;
            TokenAcesso = tokenAcesso;
            Aviso = aviso;
            Senha = senha;
            Privado = privado;
            UsuarioAdm = usuarioAdm;
        }

        public virtual void SetTokenAcesso(string tokenAcesso)
        {
            TokenAcesso = tokenAcesso;
        }
    }
}
