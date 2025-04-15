namespace BolaoDaCopa.Models
{
    public class Selecao
    {
        public virtual int Id { get; set; }
        public virtual string Nome { get; set; }
        public virtual string Logo { get; set; }
        public virtual string Abreviacao { get; set; }
        protected Selecao() { }
    }
}
