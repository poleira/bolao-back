using BolaoDaCopa.Models.Enums;
using BolaoDaCopa.Models;

namespace BolaoDaCopa.Dto.Notificacoes.Requests
{
    public class NotificacaoRequest
    {
        public string Mensagem { get; set; } = string.Empty;
        public TipoMensagemEnum Tipo { get; set; }
        public bool Lida { get; set; }
        public int IdUsuario { get; set; }
    }
}
