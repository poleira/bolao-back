namespace BolaoDaCopa.Models
{
    public class ModoJogo
    {
        public virtual int Id { get; set; }
        public virtual string NomeModoJogo { get; set; }
        public virtual IEnumerable<ModoJogoRegra> Regras { get; set; }

        protected ModoJogo() { }
    }
}
