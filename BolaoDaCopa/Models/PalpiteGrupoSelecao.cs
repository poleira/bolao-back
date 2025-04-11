namespace BolaoDaCopa.Models
{
    public class PalpiteGrupoSelecao
    {
        public int Id { get; set; }
        public Grupo Grupo { get; set; }
        public Selecao Selecao { get; set; }
        public int PontuacaoSelecao { get; set; }
        public BolaoUsuario BolaoUsuario { get; set; }
    }
}
