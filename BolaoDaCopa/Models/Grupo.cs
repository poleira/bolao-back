namespace BolaoDaCopa.Models
{
    public class Grupo
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public IEnumerable<GrupoSelecao> Selecoes { get; set; }

    }
}
