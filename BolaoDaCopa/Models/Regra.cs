namespace BolaoDaCopa.Models
{
    public class Regra
    {
        public virtual int Id { get; protected set; }
        public virtual string Descricao { get; protected set; }
        public virtual string Explicacao { get; protected set; }

        protected Regra() { }
    }
}
