using BolaoDaCopa.Aplicacao.Boloes.Servicos.Interfaces;
using BolaoDaCopa.Aplicacao.Notificacoes.Servicos.Interfaces;
using BolaoDaCopa.Bibliotecas.Transacoes.Interfaces;
using BolaoDaCopa.Dto.Boloes.Requests;
using BolaoDaCopa.Dto.Notificacoes.Responses;
using BolaoDaCopa.Infra.Repositorios.BoloesUsuarios.Interfaces;
using BolaoDaCopa.Infra.Repositorios.Notificacoes.Interfaces;
using BolaoDaCopa.Infra.Repositorios.Palpites.Interface;
using BolaoDaCopa.Models;

namespace BolaoDaCopa.Aplicacao.Notificacoes.Servicos
{
    public class NotificacoesServico : INotificacoesServico
    {
        private readonly INotificacoesRepositorio notificacoesRepositorio;
        private readonly IUnitOfWork unitOfWork;
        private readonly IBoloesServico boloesServico;
        private readonly IBoloesUsuariosRepositorio boloesUsuariosRepositorio;
        private readonly IPalpitesRepositorio palpitesRepositorio;

        public NotificacoesServico(
            INotificacoesRepositorio notificacoesRepositorio,
            IUnitOfWork unitOfWork,
            IBoloesServico boloesServico,
            IBoloesUsuariosRepositorio boloesUsuariosRepositorio,
            IPalpitesRepositorio palpitesRepositorio)
        {
            this.notificacoesRepositorio = notificacoesRepositorio;
            this.unitOfWork = unitOfWork;
            this.boloesServico = boloesServico;
            this.boloesUsuariosRepositorio = boloesUsuariosRepositorio;
            this.palpitesRepositorio = palpitesRepositorio;
        }

        public IEnumerable<NotificacaoResponse> ListarNotificacoesPorUsuario(int idUsuario)
        {
            var query = notificacoesRepositorio.Query()
                .Where(n => n.UsuarioRecebendo.Id == idUsuario && n.Lida == false);

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
                    .Where(n => n.Lida == false && n.UsuarioRecebendo.Id == idUsuario);

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

        public void CriarNotificacaoBoasVindas(Usuario usuario)
        {
            var notificacao = new Notificacao(
                mensagem: $"Bem-vindo ao Bolão do Hexa, {usuario.Nome}! Os palpites estão abertos até dia 10/06/26. 🎉 Boas apostas!",
                tipo: Models.Enums.TipoMensagemEnum.Mensagem,
                lida: false,
                usuario: usuario,
                usuarioEnviando: null,
                hashBolao: null
            );

            notificacoesRepositorio.Inserir(notificacao);
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

        public void ValidarECriarNotificacoesPalpitesFaltantes()
        {
            // Phases: FaseId → (expected count, display name)
            var faseRegras = new Dictionary<int, (int Esperado, string Nome)>
            {
                { 1, (32, "Rodada de 16") },
                { 2, (16, "Oitavas") },
                { 3, (8,  "Quartas") },
                { 4, (4,  "Semis") },
                { 5, (2,  "Finais") },
                { 6, (2,  "Disputa de terceiro") },
                { 7, (1,  "Campeão") },
                { 8, (1,  "Terceiro") }
            };

            try
            {
                unitOfWork.BeginTransaction();

                var boloesUsuarios = boloesUsuariosRepositorio.Query().ToList();

                foreach (var bolaoUsuario in boloesUsuarios)
                {
                    var faltantes = new List<string>();

                    // Artilheiro do torneio
                    if (!palpitesRepositorio.RecuperarQueryPalpiteArtilheiroPorBolaoUsuarioId(bolaoUsuario.Id).Any())
                        faltantes.Add("Artilheiro do torneio");

                    // Artilheiro do Brasil
                    if (!palpitesRepositorio.RecuperarQueryPalpiteArtilheiroBrasilPorBolaoUsuarioId(bolaoUsuario.Id).Any())
                        faltantes.Add("Artilheiro do Brasil");

                    // Palpites por fase (32→16→8→4→2→1)
                    var fasePalpites = palpitesRepositorio
                        .RecuperarQueryPalpiteFaseSelecaoPorBolaoUsuarioId(bolaoUsuario.Id)
                        .ToList();

                    foreach (var (faseId, (esperado, nome)) in faseRegras)
                    {
                        int contagem = fasePalpites.Count(x => x.Fase.Id == faseId);
                        if (contagem < esperado)
                            faltantes.Add($"{nome}: {contagem}/{esperado} seleções");
                    }

                    // Grupos (48 seleções classificadas)
                    int contGrupo = palpitesRepositorio
                        .RecuperarQueryPalpiteGrupoSelecaoPorBolaoUsuarioId(bolaoUsuario.Id)
                        .Count();
                    if (contGrupo < 48)
                        faltantes.Add($"Classificação dos grupos: {contGrupo}/48 seleções");

                    // Jogos da fase de grupos (3 palpites)
                    int contJogoGrupo = palpitesRepositorio
                        .RecuperarQueryPalpiteJogoGrupoPorBolaoUsuarioId(bolaoUsuario.Id)
                        .Count();
                    if (contJogoGrupo < 3)
                        faltantes.Add($"Jogos do Brasil: {contJogoGrupo}/3 palpites");

                    // Terceiros lugares (12 seleções)
                    int contTerceiroLugar = palpitesRepositorio
                        .RecuperarQueryPalpiteTerceiroLugarPorBolaoUsuarioId(bolaoUsuario.Id)
                        .Count();
                    if (contTerceiroLugar < 12)
                        faltantes.Add($"Terceiros lugares: {contTerceiroLugar}/12 seleções");

                    if (faltantes.Count > 0)
                    {
                        var mensagem = $"Seu bolão \"{bolaoUsuario.Bolao.Nome}\" está incompleto! Faça seus palpites o quanto antes (limite dia 10/06). " +
                                       $"Palpites faltantes: {string.Join(", ", faltantes)}.";

                        var notificacao = new Notificacao(
                            mensagem: mensagem,
                            tipo: Models.Enums.TipoMensagemEnum.Mensagem,
                            lida: false,
                            usuario: bolaoUsuario.Usuario,
                            usuarioEnviando: null!,
                            hashBolao: bolaoUsuario.Bolao.TokenAcesso
                        );

                        notificacoesRepositorio.Inserir(notificacao);
                    }
                }

                unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                unitOfWork.Rollback();
                throw new Exception($"Erro ao validar palpites faltantes: {ex.Message}", ex);
            }
        }
    }
}
