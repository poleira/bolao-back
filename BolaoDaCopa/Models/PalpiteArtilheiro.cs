namespace BolaoDaCopa.Models
{
    public class PalpiteArtilheiro
    {
        public virtual int Id { get; protected set; }
        public virtual Jogador Jogador { get; protected set; }
        public virtual BolaoUsuario BolaoUsuario { get; protected set; }
    }
}
