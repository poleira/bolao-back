using BolaoDaCopa.Aplicacao.Selecoes.Servicos.Interfaces;
using BolaoDaCopa.Services.ApiFootball;
using Cronos;
using Microsoft.Extensions.Options;

namespace BolaoDaCopa.Services.Jobs
{
    public class SelecaoAtualizacaoJob : BackgroundService
    {
        private readonly IServiceScopeFactory scopeFactory;
        private readonly ApiFootballSettings settings;
        private readonly ILogger<SelecaoAtualizacaoJob> logger;
        private readonly CronExpression cron;

        public SelecaoAtualizacaoJob(
            IServiceScopeFactory scopeFactory,
            IOptions<ApiFootballSettings> options,
            ILogger<SelecaoAtualizacaoJob> logger)
        {
            this.scopeFactory = scopeFactory;
            this.settings = options.Value;
            this.logger = logger;
            this.cron = CronExpression.Parse(settings.JobCronUtc, CronFormat.Standard);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation(
                "SelecaoAtualizacaoJob iniciada. Executará diariamente às {Cron} UTC entre {Inicio} e {Fim}.",
                settings.JobCronUtc,
                settings.DataInicioJobs.ToString("dd/MM/yyyy"),
                settings.DataFimJobs.ToString("dd/MM/yyyy"));

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
                var dataInicio = settings.DataInicioJobs.Date;
                var dataFim = settings.DataFimJobs.Date;

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

            logger.LogInformation("SelecaoAtualizacaoJob encerrada.");
        }

        private async Task ExecutarAtualizacao(CancellationToken stoppingToken)
        {
            logger.LogInformation("Iniciando atualização de standings das seleções.");
            try
            {
                using var scope = scopeFactory.CreateScope();
                var servico = scope.ServiceProvider.GetRequiredService<ISelecaoAtualizacaoServico>();
                await servico.AtualizarStandings();
                logger.LogInformation("Atualização de standings concluída com sucesso.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro durante a atualização de standings das seleções.");
            }
        }
    }
}
