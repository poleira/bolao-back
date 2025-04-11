namespace BolaoTeste.Dto.Rank
{
    public class RankResponse
    {
        public RankResponse(string usuario, int pontuacao)
        {
            Usuario = usuario;
            Pontuacao = pontuacao;
        }

        public string Usuario { get; set; }
        public int Pontuacao { get; set; }
    }
}
