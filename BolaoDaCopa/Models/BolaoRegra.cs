namespace BolaoDaCopa.Models
{
    public class BolaoRegra
    {
        public int Id { get; set; }
        public int Pontuacao { get; set; }
        public Bolao Bolao { get; set; }
        public Regra Regra { get; set; }
    }
}
