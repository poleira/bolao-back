namespace BolaoDaCopa.Models
{
    public class JogoGrupo
    {  
        public virtual int Id { get; protected set; }
        public virtual Grupo Grupo { get; protected set; }
        public virtual Selecao Selecao1 { get; protected set; }
        public virtual Selecao Selecao2 { get; protected set; }
        public virtual int PlacarSelecao1 { get; protected set; }
        public virtual int PlacarSelecao2 { get; protected set; }

        protected JogoGrupo() { }
    }
}
