namespace BolaoDaCopa.Models
{
    public class BolaoRegra
    {
        public virtual int Id { get; set; }
        public virtual int Pontuacao { get; set; }
        public virtual Bolao Bolao { get; set; }
        public virtual Regra Regra { get; set; }

        protected BolaoRegra() { }

        public BolaoRegra(int pontuacao, Bolao bolao, Regra regra)
        {
            Pontuacao = pontuacao;
            Bolao = bolao;
            Regra = regra;
        }
    }
}
