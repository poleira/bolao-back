namespace BolaoDaCopa.Models
{
    public class BolaoRegra
    {
        public virtual int Id { get; protected set; }
        public virtual int Pontuacao { get; protected set; }
        public virtual Bolao Bolao { get; protected set; }
        public virtual Regra Regra { get; protected set; }

        protected BolaoRegra() { }

        public BolaoRegra(int pontuacao, Bolao bolao, Regra regra)
        {
            Pontuacao = pontuacao;
            Bolao = bolao;
            Regra = regra;
        }
    }
}
