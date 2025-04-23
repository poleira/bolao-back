namespace BolaoDaCopa.Models
{
    public class FaseSelecao
    {
        public virtual int Id { get; protected set; }
        public virtual Selecao Selecao { get; protected set; }
        public virtual Fase Fase { get; protected set; }

        protected FaseSelecao() { }

    }
}
