namespace BolaoDaCopa.Models
{
    public class Grupo
    {
        public virtual int Id { get; protected set; }
        public virtual string Nome { get; protected set; }
        public virtual IEnumerable<GrupoSelecao> Selecoes { get; protected set; }

        protected Grupo() { }

    }
}
