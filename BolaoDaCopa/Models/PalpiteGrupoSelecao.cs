namespace BolaoDaCopa.Models
{
    public class PalpiteGrupoSelecao
    {
        public virtual int Id { get; set; }
        public virtual Grupo Grupo { get; set; }
        public virtual Selecao Selecao { get; set; }
        public virtual int PontuacaoSelecao { get; set; }
        public virtual BolaoUsuario BolaoUsuario { get; set; }

        protected PalpiteGrupoSelecao() { }
    }
}
