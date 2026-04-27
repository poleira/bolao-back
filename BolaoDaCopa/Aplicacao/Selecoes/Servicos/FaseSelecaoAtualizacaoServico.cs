using BolaoDaCopa.Aplicacao.Selecoes.Servicos.Interfaces;
using BolaoDaCopa.Bibliotecas.Transacoes.Interfaces;
using BolaoDaCopa.Infra.Repositorios.Selecoes.Interfaces;
using BolaoDaCopa.Models;
using BolaoDaCopa.Services.ApiFootball;
using NHibernate;
using ISession = NHibernate.ISession;

namespace BolaoDaCopa.Aplicacao.Selecoes.Servicos
{
    public class FaseSelecaoAtualizacaoServico : IFaseSelecaoAtualizacaoServico
    {
        // Mapeamento: strRound (normalizado) → faseId dos participantes dessa rodada
        // A lógica é: quem JOGOU na rodada X está confirmado na fase X.
        // Vencedores da rodada X avançam para a fase X+1 (tratado separadamente para o campeão).
        private static readonly Dictionary<string, int> RoundParaFaseIdParticipantes = new(StringComparer.OrdinalIgnoreCase)
        {
            ["round of 32"] = 1,
            ["1/16"] = 1,
            ["round of 16"] = 2,
            ["1/8"] = 2,
            ["quarter-final"] = 3,
            ["quarter final"] = 3,
            ["quarterfinal"] = 3,
            ["semi-final"] = 4,
            ["semi final"] = 4,
            ["semifinal"] = 4,
            ["final"] = 5,
        };

        private readonly ISession session;
        private readonly ISelecoesRepositorio selecoesRepositorio;
        private readonly IApiFootballService apiFootballService;
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<FaseSelecaoAtualizacaoServico> logger;

        public FaseSelecaoAtualizacaoServico(
            ISession session,
            ISelecoesRepositorio selecoesRepositorio,
            IApiFootballService apiFootballService,
            IUnitOfWork unitOfWork,
            ILogger<FaseSelecaoAtualizacaoServico> logger)
        {
            this.session = session;
            this.selecoesRepositorio = selecoesRepositorio;
            this.apiFootballService = apiFootballService;
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        public async Task AtualizarFasesEliminatorias()
        {
            var eventos = await apiFootballService.ObterEventosEliminatorias();

            if (eventos.Count == 0)
            {
                logger.LogWarning("Nenhum evento retornado pelo TheSportsDB. Atualização ignorada.");
                return;
            }

            // Filtrar apenas eventos eliminatórios com resultado confirmado
            var eventosEliminatorios = eventos
                .Where(e => e.TemResultado && MapearRound(e.StrRound) != null)
                .ToList();

            if (!eventosEliminatorios.Any())
            {
                logger.LogInformation("Nenhum evento eliminatório com resultado encontrado.");
                return;
            }

            // Carregar seleções indexadas por SportsDbId
            var selecoes = selecoesRepositorio.QuerySelecao()
                .Where(x => x.SportsDbId != null)
                .ToList();

            var selecaoPorSportsDbId = selecoes.ToDictionary(x => x.SportsDbId!.Value, x => x);

            logger.LogInformation(
                "Processando {Count} eventos eliminatórios com resultado.",
                eventosEliminatorios.Count);

            unitOfWork.BeginTransaction();
            try
            {
                int inseridos = 0;

                foreach (var evento in eventosEliminatorios)
                {
                    var faseId = MapearRound(evento.StrRound)!.Value;

                    // Ambos os participantes confirmados na fase
                    inseridos += await AdicionarSelecaoAFaseSeNecessario(faseId, evento.IdHomeTeam, selecaoPorSportsDbId);
                    inseridos += await AdicionarSelecaoAFaseSeNecessario(faseId, evento.IdAwayTeam, selecaoPorSportsDbId);

                    // Vencedor da final → faseId=7 (campeão)
                    if (faseId == 5 && evento.SportsDbIdVencedor.HasValue)
                    {
                        inseridos += await AdicionarSelecaoAFaseSeNecessario(7, evento.SportsDbIdVencedor.Value, selecaoPorSportsDbId);
                    }
                }

                unitOfWork.Commit();
                logger.LogInformation("Fases eliminatórias atualizadas. Novas entradas: {Count}.", inseridos);
            }
            catch (Exception ex)
            {
                unitOfWork.Rollback();
                logger.LogError(ex, "Erro ao atualizar fases eliminatórias.");
                throw;
            }
        }

        private async Task<int> AdicionarSelecaoAFaseSeNecessario(
            int faseId,
            int sportsDbId,
            Dictionary<int, Selecao> selecaoPorSportsDbId)
        {
            if (!selecaoPorSportsDbId.TryGetValue(sportsDbId, out var selecao))
            {
                logger.LogWarning("SportsDbId {Id} não encontrado na tabela selecao.", sportsDbId);
                return 0;
            }

            var fase = await session.GetAsync<Fase>(faseId);
            if (fase == null)
            {
                logger.LogWarning("Fase com id={FaseId} não encontrada no banco.", faseId);
                return 0;
            }

            // Inicializar a coleção lazy se necessário
            if (!NHibernateUtil.IsInitialized(fase.Selecoes))
            {
                await NHibernateUtil.InitializeAsync(fase.Selecoes);
            }

            // Verificar se já existe para evitar duplicatas
            if (fase.Selecoes != null && fase.Selecoes.Any(fs => fs.Selecao.Id == selecao.Id))
            {
                return 0;
            }

            var novaFaseSelecao = new FaseSelecao(selecao);
            ((IList<FaseSelecao>)fase.Selecoes!).Add(novaFaseSelecao);

            logger.LogInformation(
                "Adicionando {Selecao} à fase {FaseId}.",
                selecao.Abreviacao, faseId);

            return 1;
        }

        private static int? MapearRound(string strRound)
        {
            if (string.IsNullOrWhiteSpace(strRound)) return null;
            var normalizado = strRound.Trim();
            return RoundParaFaseIdParticipantes.TryGetValue(normalizado, out var faseId) ? faseId : null;
        }
    }
}
