namespace BolaoDaCopa.Models
{
    public class Usuario
    {
        public virtual int Id { get; set; }
        public virtual string Nome { get; set; }
        public virtual string Email { get; set; }
        public virtual string Senha { get; set; }
        public virtual string Logo { get; set; }
        public virtual IEnumerable<Bolao> Boloes { get; set; }
        protected Usuario() { }
    }
}
