namespace BolaoDaCopa.Models
{
    public class Fase
    {
        public virtual int Id { get; set; }
        public virtual string Nome { get; set; }
        public virtual IEnumerable<FaseSelecao> Selecoes { get; set; }

        protected Fase() { }
    }
}
