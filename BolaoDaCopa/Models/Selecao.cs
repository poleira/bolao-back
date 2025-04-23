namespace BolaoDaCopa.Models
{
    public class Selecao
    {
        public virtual int Id { get; protected set; }
        public virtual string Nome { get; protected set; }
        public virtual string Logo { get; protected set; }
        public virtual string Abreviacao { get; protected set; }
        protected Selecao() { }
    }
}
