namespace BolaoDaCopa.Models
{
    public class PalpiteArtilheiro
    {
        public int Id { get; set; }
        public Jogador Jogador { get; set; }
        public BolaoUsuario BolaoUsuario { get; set; }
    }
}
