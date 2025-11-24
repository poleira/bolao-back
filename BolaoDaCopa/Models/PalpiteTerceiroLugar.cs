namespace BolaoDaCopa.Models
{
    public class PalpiteTerceiroLugar
    {
        public virtual int Id { get; protected set; }
        public virtual Selecao Selecao { get; protected set; }
        public virtual BolaoUsuario BolaoUsuario { get; protected set; }
        public virtual int Posicao { get; protected set; }

        protected PalpiteTerceiroLugar() { }

        public PalpiteTerceiroLugar(Selecao selecao, BolaoUsuario bolaoUsuario, int posicao)
        {
            SetSelecao(selecao);
            SetBolaoUsuario(bolaoUsuario);
            SetPosicao(posicao);
        }

        private void SetSelecao(Selecao selecao)
        {
            Selecao = selecao;
        }

        private void SetBolaoUsuario(BolaoUsuario bolaoUsuario)
        {
            BolaoUsuario = bolaoUsuario;
        }

        private void SetPosicao(int posicao)
        {
            Posicao = posicao;
        }
    }
}
