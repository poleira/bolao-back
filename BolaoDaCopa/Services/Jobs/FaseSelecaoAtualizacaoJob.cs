using BolaoDaCopa.Aplicacao.Selecoes.Servicos.Interfaces;
using BolaoDaCopa.Services.ApiFootball;
using Cronos;
using Microsoft.Extensions.Options;

namespace BolaoDaCopa.Services.Jobs
{
    public class FaseSelecaoAtualizacaoJob : BackgroundService
    {
        private readonly IServiceScopeFactory scopeFactory;
        private readonly ApiFootballSettings settings;
        private readonly ILogger<FaseSelecaoAtualizacaoJob> logger;
        private readonly CronExpression cron;

        public FaseSelecaoAtualizacaoJob(
            IServiceScopeFactory scopeFactory,
            IOptions<ApiFootballSettings> options,
            ILogger<FaseSelecaoAtualizacaoJob> logger)
        {
            this.scopeFactory = scopeFactory;
            this.settings = options.Value;
            this.logger = logger;
            this.cron = CronExpression.Parse(settings.JobCronUtc, CronFormat.Standard);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation(
                "FaseSelecaoAtualizacaoJob iniciada. Executará diariamente às {Cron} UTC entre {Inicio} e {Fim}.",
                settings.JobCronUtc,
                settings.DataInicioJobsEliminatorias.ToString("dd/MM/yyyy"),
                settings.DataFimJobsEliminatorias.ToString("dd/MM/yyyy"));

            while (!stoppingToken.IsCancellationRequested)
            {
                var agora = DateTime.UtcNow;
                var proxima = cron.GetNextOccurrence(agora, TimeZoneInfo.Utc);

                if (proxima == null) break;

                var espera = proxima.Value - agora;
                logger.LogInformation("Próxima execução agendada para {Proxima} UTC.", proxima.Value);

                try
                {
                    await Task.Delay(espera, stoppingToken);
                }
                catch (OperationCanceledException)
                {
                    break;
                }

                if (stoppingToken.IsCancellationRequested) break;

                var dataExecucao = DateTime.UtcNow.Date;
                var dataInicio = settings.DataInicioJobsEliminatorias.Date;
                var dataFim = settings.DataFimJobsEliminatorias.Date;

                if (dataExecucao < dataInicio || dataExecucao > dataFim)
                {
                    logger.LogInformation(
                        "Execução ignorada: data atual {Data} fora do intervalo [{Inicio}, {Fim}].",
                        dataExecucao.ToString("dd/MM/yyyy"),
                        dataInicio.ToString("dd/MM/yyyy"),
                        dataFim.ToString("dd/MM/yyyy"));
                    continue;
                }

                await ExecutarAtualizacao(stoppingToken);
            }

            logger.LogInformation("FaseSelecaoAtualizacaoJob encerrada.");
        }

        private async Task ExecutarAtualizacao(CancellationToken stoppingToken)
        {
            logger.LogInformation("Iniciando atualização das fases eliminatórias.");
            try
            {
                using var scope = scopeFactory.CreateScope();
                var servico = scope.ServiceProvider.GetRequiredService<IFaseSelecaoAtualizacaoServico>();
                await servico.AtualizarFasesEliminatorias();
                logger.LogInformation("Atualização das fases eliminatórias concluída com sucesso.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro durante a atualização das fases eliminatórias.");
            }
        }
    }
}
