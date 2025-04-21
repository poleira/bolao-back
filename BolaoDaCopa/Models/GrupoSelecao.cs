namespace BolaoDaCopa.Models
{
    public class GrupoSelecao
    {
        public virtual int Id { get; set; }
        public virtual Grupo Grupo { get; set; }
        public virtual Selecao Selecao { get; set; }
        public virtual int? PontuacaoSelecao { get; set; }

        protected GrupoSelecao() { }
    }
}
