namespace BolaoDaCopa.Models
{
    public class Grupo
    {
        public virtual int Id { get; set; }
        public virtual string Nome { get; set; }
        public virtual IEnumerable<GrupoSelecao> Selecoes { get; set; }

        protected Grupo() { }

    }
}
