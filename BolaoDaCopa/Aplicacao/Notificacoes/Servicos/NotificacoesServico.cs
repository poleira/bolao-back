using BolaoDaCopa.Aplicacao.Boloes.Servicos.Interfaces;
using BolaoDaCopa.Aplicacao.Notificacoes.Servicos.Interfaces;
using BolaoDaCopa.Bibliotecas.Transacoes.Interfaces;
using BolaoDaCopa.Dto.Boloes.Requests;
using BolaoDaCopa.Dto.Notificacoes.Responses;
using BolaoDaCopa.Infra.Repositorios.Notificacoes.Interfaces;
using BolaoDaCopa.Models;

namespace BolaoDaCopa.Aplicacao.Notificacoes.Servicos
{
    public class NotificacoesServico : INotificacoesServico
    {
        private readonly INotificacoesRepositorio notificacoesRepositorio;
        private readonly IUnitOfWork unitOfWork;
        private readonly IBoloesServico boloesServico;

        public NotificacoesServico(INotificacoesRepositorio notificacoesRepositorio, IUnitOfWork unitOfWork, IBoloesServico boloesServico)
        {
            this.notificacoesRepositorio = notificacoesRepositorio;
            this.unitOfWork = unitOfWork;
            this.boloesServico = boloesServico;
        }

        public IEnumerable<NotificacaoResponse> ListarNotificacoesPorUsuario(int idUsuario)
        {
            var query = notificacoesRepositorio.Query()
                .Where(n => n.UsuarioRecebendo.Id == idUsuario && !n.Lida);

            var projecao = query.Select(x => new NotificacaoResponse
            {
                Id = x.Id,
                Tipo = x.Tipo,
                Lida = x.Lida,
                Mensagem = x.Mensagem,
            }).ToList();

            return projecao;
        }

        public void MarcarNotificacaoComoLida(int idNotificacao)
        {
            try
            {
                unitOfWork.BeginTransaction();
                Notificacao? notificacao = notificacoesRepositorio.Recuperar(idNotificacao) ?? throw new Exception("Notificação não encontrada.");
                notificacao.Lida = true;
                notificacoesRepositorio.Editar(notificacao);
                unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                unitOfWork.Rollback();
                throw new Exception($"Erro ao marcar notificação como lida: {ex.Message}", ex);
            }
        }

        public bool ValidarSeUsuarioPossuiAlgumaNotificacaoNaoLida(int idUsuario)
        {
            try
            {
                var query = notificacoesRepositorio.Query()
                    .Where(n => !n.Lida && n.UsuarioRecebendo.Id == idUsuario);

                return query.Any();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao validar notificações: {ex.Message}", ex);
            }
        }

        public void CriarNotificacao(Notificacao notificacao)
        {
            if (notificacao == null)
            {
                throw new ArgumentNullException(nameof(notificacao), "Notificação não pode ser nula.");
            }

            try
            {
                var query = notificacoesRepositorio.Query()
                    .Where(x => x.UsuarioRecebendo.Id == notificacao.UsuarioRecebendo.Id &&
                                x.UsuarioEnviando.Id == notificacao.UsuarioEnviando.Id);

                if (!query.Any())
                {
                    notificacoesRepositorio.Inserir(notificacao);
                }
                else
                {
                    throw new InvalidOperationException("Você já enviou um pedido de entrada para este bolão.");
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ExcluirNotificacao(int idNotificacao)
        {
            try
            {
                unitOfWork.BeginTransaction();
                Notificacao? notificacao = notificacoesRepositorio.Recuperar(idNotificacao) ?? throw new Exception("Notificação não encontrada.");
                notificacoesRepositorio.Remover(notificacao);
                unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                unitOfWork.Rollback();
                throw new Exception($"Erro ao excluir notificação: {ex.Message}", ex);
            }
        }

        public void AceitarSolicitacao(int idNotificacao, int idUsuarioLogado)
        {
            Notificacao? notificacao = notificacoesRepositorio.Recuperar(idNotificacao) ?? throw new Exception("Notificação não encontrada.");
            if (notificacao.Tipo != Models.Enums.TipoMensagemEnum.Solicitacao)
            {
                throw new InvalidOperationException("A notificação não é do tipo solicitação.");
            }
            if (notificacao.UsuarioRecebendo.Id != idUsuarioLogado)
            {
                throw new InvalidOperationException("Você não tem permissão para aceitar esta solicitação.");
            }

            boloesServico.AssociarUsuarioBolao(new AssociarUsuarioRequest() { IdUsuario = notificacao.UsuarioEnviando.Id, HashBolao = notificacao.HashBolao, Senha = "" });
        }
    }
}
