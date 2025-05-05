namespace BolaoDaCopa.Models
{
    public class Fase
    {
        public virtual int Id { get; protected set; }
        public virtual string Nome { get; protected set; }
        public virtual IEnumerable<FaseSelecao> Selecoes { get; protected set; }

        protected Fase() { }

        public Fase(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }
    }
}
