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
            var standings = await apiFootballService.ObterStandings();

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
                    if (standing == null) continue;

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
