namespace BolaoDaCopa.Models
{
    public class Regra
    {
        public virtual int Id { get; set; }
        public virtual string Descricao { get; set; }
        public virtual string Explicacao { get; set; }

        protected Regra() { }
    }
}
