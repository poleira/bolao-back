namespace BolaoDaCopa.Models
{
    public class Selecao
    {
        public virtual int Id { get; set; }
        public virtual string Nome { get; set; }
        public virtual string Logo { get; set; }
        public virtual string Abreviacao { get; set; }
        public virtual int? PontuacaoSelecao { get; set; }
        public virtual int? PosicaoFaseDeGrupos { get; set; }
        public virtual Grupo Grupo { get; set; }
        protected Selecao() { }
    }
}
