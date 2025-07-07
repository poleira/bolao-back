using BolaoDaCopa.Models.Enums;

namespace BolaoDaCopa.Dto.Notificacoes.Responses
{
    public class NotificacaoResponse
    {
        public int Id { get; set; }
        public string Mensagem { get; set; } = string.Empty;
        public TipoMensagemEnum Tipo { get; set; }
        public bool Lida { get; set; }
        public string? HashBolao { get; set; } = null;
    }
}
