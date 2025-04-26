namespace BolaoDaCopa.Models
{
    public class BolaoUsuario
    {
        public virtual int Id { get; protected set; }
        public virtual Usuario Usuario { get; protected set; }
        public virtual Bolao Bolao { get; protected set; }

        protected BolaoUsuario() { }

    }
}
