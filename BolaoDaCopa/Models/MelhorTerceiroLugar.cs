namespace BolaoDaCopa.Models
{
    public class MelhorTerceiroLugar
    {
        public virtual int Id { get; protected set; }
        public virtual Selecao Selecao { get; protected set; }
        public virtual int Posicao { get; protected set; }

        protected MelhorTerceiroLugar() { }

        public MelhorTerceiroLugar(Selecao selecao, int posicao)
        {
            SetSelecao(selecao);
            SetPosicao(posicao);
        }

        private void SetSelecao(Selecao selecao)
        {
            Selecao = selecao;
        }

        private void SetPosicao(int posicao)
        {
            Posicao = posicao;
        }
    }
}
