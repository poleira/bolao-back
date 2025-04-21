namespace BolaoDaCopa.Dto.Boloes.Comandos
{
    public class BolaoUsuarioComando
    {
        public int IdUsuario { get; set; }
        public int IdBolao { get; set; }
        public BolaoUsuarioComando(int usuarioId, int bolaoId)
        {
            IdUsuario = usuarioId;
            IdBolao = bolaoId;
        }
    }
}
