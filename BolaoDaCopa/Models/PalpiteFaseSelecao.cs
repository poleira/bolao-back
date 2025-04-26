namespace BolaoDaCopa.Models
{
    public class PalpiteFaseSelecao
    {
        public virtual int Id { get; protected set; }
        public virtual Fase Fase { get; protected set; }
        public virtual Selecao Selecao { get; protected set; }
        public virtual BolaoUsuario BolaoUsuario { get; protected set; }

        protected PalpiteFaseSelecao() { }
    }
}
