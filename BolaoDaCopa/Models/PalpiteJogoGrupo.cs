namespace BolaoDaCopa.Models
{
    public class PalpiteJogoGrupo
    {
        public virtual int Id { get; protected set; }
        public virtual Grupo Grupo { get; protected set; }
        public virtual Selecao Selecao1 { get; protected set; }
        public virtual Selecao Selecao2 { get; protected set; }
        public virtual int PlacarSelecao1 { get; protected set; }
        public virtual int PlacarSelecao2 { get; protected set; }
        public virtual BolaoUsuario BolaoUsuario { get; protected set; }

        protected PalpiteJogoGrupo() { }
    }
}
