namespace BolaoDaCopa.Models
{
    public class PalpiteGrupoSelecao
    {
        public virtual int Id { get; protected set; }
        public virtual Grupo Grupo { get; protected set; }
        public virtual Selecao Selecao { get; protected set; }
        public virtual int PontuacaoSelecao { get; protected set; }
        public virtual BolaoUsuario BolaoUsuario { get; protected set; }

        protected PalpiteGrupoSelecao() { }
    }
}
