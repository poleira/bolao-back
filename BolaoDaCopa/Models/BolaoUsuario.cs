namespace BolaoDaCopa.Models
{
    public class BolaoUsuario
    {
        public virtual int Id { get; protected set; }
        public virtual Usuario Usuario { get; protected set; }
        public virtual Bolao Bolao { get; protected set; }

        protected BolaoUsuario() { }

        public BolaoUsuario(Usuario usuario, Bolao bolao)
        {
            SetUsuario(usuario);
            SetBolao(bolao);
        }

        private void SetUsuario(Usuario usuario)
        {
            Usuario = usuario;
        }

        private void SetBolao(Bolao bolao)
        {
            Bolao = bolao;
        }
    }
}
