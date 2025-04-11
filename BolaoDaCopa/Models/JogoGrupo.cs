namespace BolaoDaCopa.Models
{
    public class JogoGrupo
    {  
        public int Id { get; set; }
        public Grupo Grupo { get; set; }
        public Selecao Selecao1 { get; set; }
        public Selecao Selecao2 { get; set; }
        public int PlacarSelecao1 { get; set; }
        public int PlacarSelecao2 { get; set; }
    }
}
