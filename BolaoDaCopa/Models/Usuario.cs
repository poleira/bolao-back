namespace BolaoDaCopa.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Logo { get; set; }
        public IEnumerable<Bolao> Boloes { get; set; }
    }
}
