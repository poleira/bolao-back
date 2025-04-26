namespace BolaoDaCopa.Models
{
    public class Jogador
    {
        public virtual int Id { get; set; }
        public virtual string Nome { get; set; }
        
        protected Jogador() { }
    }
}
