namespace BolaoDaCopa.Models
{
    public class Artilheiro
    {
        public virtual int Id { get; set; }
        public virtual Jogador Jogador { get; set; }

        protected Artilheiro() { }
    }
}
