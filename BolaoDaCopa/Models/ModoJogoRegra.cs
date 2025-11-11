namespace BolaoDaCopa.Models
{
    public class ModoJogoRegra
    {
        public virtual int Id { get; protected set; }
        public virtual ModoJogo ModoJogo { get; protected set; }
        public virtual Regra Regra { get; protected set; }

        protected ModoJogoRegra() { }

        public ModoJogoRegra(ModoJogo modoJogo, Regra regra)
        {
            ModoJogo = modoJogo;
            Regra = regra;
        }
    }
}
