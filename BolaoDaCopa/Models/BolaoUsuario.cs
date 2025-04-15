namespace BolaoDaCopa.Models
{
    public class BolaoUsuario
    {
        public virtual int Id { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual Bolao Bolao { get; set; }

        protected BolaoUsuario() { }

    }
}
