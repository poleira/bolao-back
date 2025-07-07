using BolaoDaCopa.Dto.Notificacoes.Responses;
using BolaoDaCopa.Models;

namespace BolaoDaCopa.Aplicacao.Notificacoes.Servicos.Interfaces
{
    public interface INotificacoesServico
    {
        void AceitarSolicitacao(int idNotificacao, int idUsuarioLogado);
        void CriarNotificacao(Notificacao notificacao);
        void ExcluirNotificacao(int idNotificacao);
        IEnumerable<NotificacaoResponse> ListarNotificacoesPorUsuario(int idUsuario);
        void MarcarNotificacaoComoLida(int idNotificacao);
        bool ValidarSeUsuarioPossuiAlgumaNotificacaoNaoLida(int idUsuario);
    }
}
