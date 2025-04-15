namespace BolaoDaCopa.Models
{
    public class FaseSelecao
    {
        public virtual int Id { get; set; }
        public virtual Selecao Selecao { get; set; }
        public virtual Fase Fase { get; set; }

        protected FaseSelecao() { }

    }
}
