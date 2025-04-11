namespace BolaoDaCopa.Models
{
    public class PalpiteFaseSelecao
    {
        public int Id { get; set; }
        public Fase Fase { get; set; }
        public Selecao Selecao { get; set; }
        public BolaoUsuario BolaoUsuario { get; set; }
    }
}
