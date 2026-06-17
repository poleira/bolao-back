using BolaoDaCopa.Aplicacao.Selecoes.Servicos.Interfaces;
using BolaoDaCopa.Bibliotecas.Transacoes.Interfaces;
using BolaoDaCopa.Infra.Repositorios.Selecoes.Interfaces;
using BolaoDaCopa.Services.ApiFootball;

namespace BolaoDaCopa.Aplicacao.Selecoes.Servicos
{
    public class SelecaoAtualizacaoServico : ISelecaoAtualizacaoServico
    {
        private readonly ISelecoesRepositorio selecoesRepositorio;
        private readonly IApiFootballService apiFootballService;
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<SelecaoAtualizacaoServico> logger;

        public SelecaoAtualizacaoServico(
            ISelecoesRepositorio selecoesRepositorio,
            IApiFootballService apiFootballService,
            IUnitOfWork unitOfWork,
            ILogger<SelecaoAtualizacaoServico> logger)
        {
            this.selecoesRepositorio = selecoesRepositorio;
            this.apiFootballService = apiFootballService;
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        public async Task AtualizarStandings()
        {
            logger.LogInformation("Iniciando AtualizarStandings...");
            var standings = await apiFootballService.ObterStandings();

            logger.LogInformation("Standings retornados: {Count}", standings.Count);
            if (standings.Count == 0)
            {
                logger.LogWarning("Nenhum standing retornado pela api-football. Atualização ignorada.");
                return;
            }

            var selecoes = selecoesRepositorio.QuerySelecao()
                .Where(x => x.SportsDbId != null)
                .ToList();

            logger.LogInformation("Atualizando standings de {Count} seleções.", selecoes.Count);

            unitOfWork.BeginTransaction();
            try
            {
                int atualizadas = 0;
                foreach (var selecao in selecoes)
                {
                    var standing = standings.FirstOrDefault(s => s.TeamId == selecao.SportsDbId);
                    if (standing == null)
                    {
                        logger.LogDebug("Standing não encontrado para seleção {SeleId} (SportsDbId: {SportsDbId})", selecao.Id, selecao.SportsDbId);
                        continue;
                    }
                    if (standing.Played == 0)
                    {
                        logger.LogDebug("Standing com 0 jogos para seleção {SeleId} (SportsDbId: {SportsDbId})", selecao.Id, selecao.SportsDbId);
                        continue;
                    }

                    logger.LogDebug("Atualizando seleção {SeleId}: Posição={Rank}, Pontos={Points}", selecao.Id, standing.Rank, standing.Points);
                    selecao.PosicaoFaseDeGrupos = standing.Rank;
                    selecao.PontuacaoSelecao = standing.Points;
                    selecoesRepositorio.Atualizar(selecao);
                    atualizadas++;
                }

                unitOfWork.Commit();
                logger.LogInformation("Standings atualizados com sucesso: {Count} seleções.", atualizadas);
            }
            catch (Exception ex)
            {
                unitOfWork.Rollback();
                logger.LogError(ex, "Erro ao atualizar standings das seleções.");
                throw;
            }
        }
    }
}
