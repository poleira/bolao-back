namespace BolaoDaCopa.Models
{
    public class PalpiteFaseSelecao
    {
        public virtual int Id { get; set; }
        public virtual Fase Fase { get; set; }
        public virtual Selecao Selecao { get; set; }
        public virtual BolaoUsuario BolaoUsuario { get; set; }

        protected PalpiteFaseSelecao() { }
    }
}
