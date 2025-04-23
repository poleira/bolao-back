namespace BolaoDaCopa.Models
{
    public class GrupoSelecao
    {
        public virtual int Id { get; protected set; }
        public virtual Grupo Grupo { get; protected set; }
        public virtual Selecao Selecao { get; protected set; }
        public virtual int? PontuacaoSelecao { get; protected set; }

        protected GrupoSelecao() { }
    }
}
