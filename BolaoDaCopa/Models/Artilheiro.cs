namespace BolaoDaCopa.Models
{
    public class Artilheiro
    {
        public virtual int Id { get; protected set; }
        public virtual Jogador Jogador { get; protected set; }
    }
}
