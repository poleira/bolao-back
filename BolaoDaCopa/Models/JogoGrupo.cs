namespace BolaoDaCopa.Models
{
    public class JogoGrupo
    {  
        public virtual int Id { get; set; }
        public virtual Grupo Grupo { get; set; }
        public virtual Selecao Selecao1 { get; set; }
        public virtual Selecao Selecao2 { get; set; }
        public virtual int PlacarSelecao1 { get; set; }
        public virtual int PlacarSelecao2 { get; set; }

        protected JogoGrupo() { }
    }
}
