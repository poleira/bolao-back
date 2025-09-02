namespace BolaoDaCopa.Models
{
    public class PalpiteGrupoSelecao
    {
        public virtual int Id { get; protected set; }
        public virtual Grupo Grupo { get; protected set; }
        public virtual Selecao Selecao { get; protected set; }
        public virtual int? PontuacaoSelecao { get; protected set; }
        public virtual int PosicaoSelecao { get; protected set; }
        public virtual BolaoUsuario BolaoUsuario { get; protected set; }

        protected PalpiteGrupoSelecao() { }

        public PalpiteGrupoSelecao(Grupo grupo, Selecao selecao, int? pontuacaoSelecao, BolaoUsuario bolaoUsuario, int posicaoSelecao)
        {
            Grupo = grupo;
            Selecao = selecao;
            PontuacaoSelecao = pontuacaoSelecao;
            PosicaoSelecao = posicaoSelecao;
            BolaoUsuario = bolaoUsuario;
        }
    }
}
