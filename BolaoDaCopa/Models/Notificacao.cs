using BolaoDaCopa.Models.Enums;

namespace BolaoDaCopa.Models
{
    public class Notificacao
    {
        public virtual int Id { get; set; }
        public virtual string Mensagem { get; set; } = string.Empty;
        public virtual TipoMensagemEnum Tipo { get; set; }
        public virtual bool Lida { get; set; }
        public virtual string? HashBolao { get; set; } = null;
        public virtual Usuario UsuarioRecebendo { get; set; }
        public virtual Usuario UsuarioEnviando { get; set; }

        protected Notificacao() { }

        public Notificacao(string mensagem, TipoMensagemEnum tipo, bool lida, Usuario usuario, Usuario usuarioEnviando, string hashBolao)
        {
            Mensagem = mensagem;
            Tipo = tipo;
            Lida = lida;
            UsuarioRecebendo = usuario;
            UsuarioEnviando = usuarioEnviando;
            HashBolao = hashBolao;
        }
    }
}
