namespace BolaoDaCopa.Models
{
    public class PalpiteArtilheiroBrasil
    {
        public virtual int Id { get; set; }
        public virtual Jogador Jogador { get; set; }
        public virtual BolaoUsuario BolaoUsuario { get; set; }

        protected PalpiteArtilheiroBrasil() { }

        public PalpiteArtilheiroBrasil(Jogador jogador, BolaoUsuario bolaoUsuario)
        {
            Jogador = jogador;
            BolaoUsuario = bolaoUsuario;
        }
    }
}
