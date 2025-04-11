namespace BolaoDaCopa.Models
{
    public class Fase
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public IEnumerable<FaseSelecao> Selecoes { get; set; }
    }
}
