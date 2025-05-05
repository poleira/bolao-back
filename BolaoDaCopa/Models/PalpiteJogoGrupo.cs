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

        public PalpiteJogoGrupo(Grupo grupo, Selecao selecao1, Selecao selecao2, int placarSelecao1, int placarSelecao2, BolaoUsuario bolaoUsuario)
        {
            Grupo = grupo;
            Selecao1 = selecao1;
            Selecao2 = selecao2;
            PlacarSelecao1 = placarSelecao1;
            PlacarSelecao2 = placarSelecao2;
            BolaoUsuario = bolaoUsuario;
        }
    }
}
