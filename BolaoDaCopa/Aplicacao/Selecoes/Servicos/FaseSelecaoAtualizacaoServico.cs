using BolaoDaCopa.Aplicacao.Selecoes.Servicos.Interfaces;
using BolaoDaCopa.Bibliotecas.Transacoes.Interfaces;
using BolaoDaCopa.Dto.ApiFootball;
using BolaoDaCopa.Infra.Repositorios.Selecoes.Interfaces;
using BolaoDaCopa.Models;
using BolaoDaCopa.Services.ApiFootball;
using NHibernate;
using ISession = NHibernate.ISession;

namespace BolaoDaCopa.Aplicacao.Selecoes.Servicos
{
    public class FaseSelecaoAtualizacaoServico : IFaseSelecaoAtualizacaoServico
    {
        // Mapeamento: strRound (normalizado) → faseId dos participantes dessa rodada.
        // Aparecer na chave de uma rodada confirma a classificação para aquela fase.
        // Avanço acumulativo: se a seleção aparece em R16, ela já está em R32 (inserção de faseId=1 já ocorreu antes).
        // Campeão (faseId=7) e terceiro lugar (faseId=8) são tratados separadamente com base no vencedor.
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
            ["3rd place"] = 6,
            ["third place"] = 6,
            ["third-place"] = 6,
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

            // Filtrar todos os eventos com round mapeável — resultado não é necessário para inserir participantes.
            // Tenta resolver o round por strRound → strStage → strFilename (fallback em cascata).
            var eventosEliminatorios = eventos
                .Where(e => ResolverRound(e) != null)
                .ToList();

            if (!eventosEliminatorios.Any())
            {
                logger.LogInformation("Nenhum evento eliminatório encontrado.");
                return;
            }

            // Carregar seleções indexadas por SportsDbId
            var selecoes = selecoesRepositorio.QuerySelecao()
                .Where(x => x.SportsDbId != null)
                .ToList();

            var selecaoPorSportsDbId = selecoes.ToDictionary(x => x.SportsDbId!.Value, x => x);

            logger.LogInformation(
                "Processando {Count} eventos eliminatórios.",
                eventosEliminatorios.Count);

            unitOfWork.BeginTransaction();
            try
            {
                int inseridos = 0;

                foreach (var evento in eventosEliminatorios)
                {
                    var faseId = ResolverRound(evento)!.Value;

                    // Ambos os participantes confirmados na fase (independente de resultado)
                    inseridos += await AdicionarSelecaoAFaseSeNecessario(faseId, evento.IdHomeTeam, selecaoPorSportsDbId);
                    inseridos += await AdicionarSelecaoAFaseSeNecessario(faseId, evento.IdAwayTeam, selecaoPorSportsDbId);

                    // Vencedor da final → faseId=7 (campeão) — só quando resultado confirmado
                    if (faseId == 5 && evento.TemResultado && evento.SportsDbIdVencedor.HasValue)
                    {
                        inseridos += await AdicionarSelecaoAFaseSeNecessario(7, evento.SportsDbIdVencedor.Value, selecaoPorSportsDbId);
                    }

                    // Vencedor da disputa de terceiro → faseId=8 — só quando resultado confirmado
                    if (faseId == 6 && evento.TemResultado && evento.SportsDbIdVencedor.HasValue)
                    {
                        inseridos += await AdicionarSelecaoAFaseSeNecessario(8, evento.SportsDbIdVencedor.Value, selecaoPorSportsDbId);
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

        // Mapeamento direto por intRound para valores conhecidos da API.
        // R32=32 confirmado. Demais serão adicionados à medida que os jogos ocorrerem.
        private static readonly Dictionary<int, int> IntRoundParaFaseId = new()
        {
            [32] = 1,  // Round of 32
            // [16] = 2, // Oitavas  — adicionar quando confirmado
            // [8]  = 3, // Quartas  — adicionar quando confirmado
            // [4]  = 4, // Semis    — adicionar quando confirmado
        };

        // Calendário fixo da Copa 2026 — fallback por data para fases após o R32.
        // R32 NÃO usa datas (overlap com fase de grupos em 28/06). Oitavas em diante são seguras.
        // faseId: 2=Oitavas, 3=Quartas, 4=Semis, 6=Disputa3°, 5=Final
        private static readonly (DateOnly Inicio, DateOnly Fim, int FaseId)[] RoundPorData = new[]
        {
            (new DateOnly(2026, 7,  5), new DateOnly(2026, 7,  8), 2),
            (new DateOnly(2026, 7, 11), new DateOnly(2026, 7, 12), 3),
            (new DateOnly(2026, 7, 15), new DateOnly(2026, 7, 16), 4),
            (new DateOnly(2026, 7, 18), new DateOnly(2026, 7, 18), 6),
            (new DateOnly(2026, 7, 19), new DateOnly(2026, 7, 19), 5),
        };

        /// <summary>
        /// Tenta resolver o faseId do evento em cascata:
        /// 0. intRound 1-3 → fase de grupos → descarta
        /// 1. intRound com valor direto conhecido (ex: 32 → faseId=1)
        /// 2. strRound → strStage → strFilename → data do jogo
        /// Retorna null se nenhum campo for mapeável.
        /// </summary>
        private static int? ResolverRound(SportsDbEventDto evento)
        {
            // intRound 1-3 = fase de grupos — descarta imediatamente
            if (evento.IntRound.HasValue && evento.IntRound.Value <= 3)
                return null;

            // Mapeamento direto por intRound (valores confirmados da API)
            if (evento.IntRound.HasValue && IntRoundParaFaseId.TryGetValue(evento.IntRound.Value, out var faseIdDireto))
                return faseIdDireto;

            // Fallbacks para rounds ainda não mapeados por intRound
            return MapearRound(evento.StrRound)
                ?? MapearRound(evento.StrStage)
                ?? MapearRoundDeFilename(evento.StrFilename)
                ?? MapearRoundPorData(evento.DateEvent);
        }

        private static int? MapearRoundPorData(string dateEvent)
        {
            if (!DateOnly.TryParse(dateEvent, out var data)) return null;

            foreach (var (inicio, fim, faseId) in RoundPorData)
            {
                if (data >= inicio && data <= fim) return faseId;
            }
            return null;
        }

        private static int? MapearRoundDeFilename(string strFilename)
        {
            if (string.IsNullOrWhiteSpace(strFilename)) return null;

            // strFilename costuma ser algo como:
            // "Soccer/International/FIFA_World_Cup/2026-07-04_Brazil_vs_England/Round_of_32/..."
            // Tenta encontrar qualquer segmento do path que bata no dicionário.
            var segmentos = strFilename.Replace('_', ' ').Split('/', StringSplitOptions.RemoveEmptyEntries);
            foreach (var segmento in segmentos)
            {
                var resultado = MapearRound(segmento.Trim());
                if (resultado.HasValue) return resultado;
            }
            return null;
        }

        private static int? MapearRound(string strRound)
        {
            if (string.IsNullOrWhiteSpace(strRound)) return null;
            var normalizado = strRound.Trim();
            return RoundParaFaseIdParticipantes.TryGetValue(normalizado, out var faseId) ? faseId : null;
        }
    }
}
