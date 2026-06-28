using System.Text.Json;
using BolaoDaCopa.Dto.ApiFootball;
using Microsoft.Extensions.Options;

namespace BolaoDaCopa.Services.ApiFootball
{
    public class ApiFootballService : IApiFootballService
    {
        private readonly HttpClient httpClient;
        private readonly ApiFootballSettings settings;
        private readonly ILogger<ApiFootballService> logger;

        public ApiFootballService(
            IHttpClientFactory httpClientFactory,
            IOptions<ApiFootballSettings> options,
            ILogger<ApiFootballService> logger)
        {
            this.httpClient = httpClientFactory.CreateClient("SportsDb");
            this.settings = options.Value;
            this.logger = logger;
        }

        public async Task<IList<SelecaoStandingDto>> ObterStandings()
        {
            var url = $"{settings.ApiKey}/lookuptable.php?l={settings.LeagueId}&s={settings.Season}";

            logger.LogInformation("Consultando standings no TheSportsDB: {Url}", url);

            try
            {
                var response = await httpClient.GetAsync(url);
                logger.LogInformation("Resposta da API: StatusCode={StatusCode}", response.StatusCode);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                logger.LogInformation("Conteúdo retornado (primeiros 500 chars): {Content}", 
                    content.Length > 500 ? content.Substring(0, 500) : content);

                var root = JsonSerializer.Deserialize<SportsDbTableRoot>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                var result = new List<SelecaoStandingDto>();

                if (root?.Table == null || root.Table.Count == 0)
                {
                    logger.LogWarning("Resposta do TheSportsDB não contém dados de standings. Root é null? {RootNull}, Table é null? {TableNull}", 
                        root == null, root?.Table == null);
                    return result;
                }

                foreach (var standing in root.Table)
                {
                    if (!int.TryParse(standing.IdTeam, out var teamId)) continue;
                    int.TryParse(standing.IntRank, out var rank);
                    int.TryParse(standing.IntPoints, out var points);
                    int.TryParse(standing.IntPlayed, out var played);

                    result.Add(new SelecaoStandingDto
                    {
                        TeamId = teamId,
                        Rank = rank,
                        Points = points,
                        Played = played
                    });
                }

                logger.LogInformation("Standings obtidos com sucesso: {Count} seleções.", result.Count);
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao consultar standings no TheSportsDB.");
                throw;
            }
        }

        public async Task<IList<SportsDbEventDto>> ObterEventosEliminatorias()
        {
            var url = $"{settings.ApiKey}/eventsseason.php?id={settings.LeagueId}&s={settings.Season}";

            logger.LogInformation("Consultando eventos da temporada no TheSportsDB: {Url}", url);

            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var root = JsonSerializer.Deserialize<SportsDbEventsRoot>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var result = new List<SportsDbEventDto>();

            if (root?.Events == null || root.Events.Count == 0)
            {
                logger.LogWarning("Resposta do TheSportsDB não contém eventos.");
                return result;
            }

            // Log temporário: exibe o JSON bruto dos primeiros 3 eventos para diagnóstico dos campos disponíveis
            var primeiros = root.Events.Where(e => e.IdHomeTeamInt != null && e.IdAwayTeamInt != null).Take(3);
            foreach (var ev in primeiros)
            {
                logger.LogWarning(
                    "[RAW EVENT] idEvent={IdEvent} | strRound='{StrRound}' | strStage='{StrStage}' | strFilename='{StrFilename}' | strEvent='{StrEvent}'",
                    ev.IdEvent, ev.StrRound, ev.StrStage, ev.StrFilename, ev.StrEvent);
            }

            foreach (var ev in root.Events)
            {
                if (ev.IdHomeTeamInt == null || ev.IdAwayTeamInt == null) continue;

                if (string.IsNullOrWhiteSpace(ev.StrRound))
                {
                    logger.LogDebug(
                        "[DEBUG] idEvent={IdEvent} strRound='{StrRound}' strStage='{StrStage}' strFilename='{StrFilename}' strEvent='{StrEvent}'",
                        ev.IdEvent, ev.StrRound, ev.StrStage, ev.StrFilename, ev.StrEvent);
                }

                result.Add(new SportsDbEventDto
                {
                    StrRound = ev.StrRound,
                    StrStage = ev.StrStage,
                    StrFilename = ev.StrFilename,
                    StrEvent = ev.StrEvent,
                    DateEvent = ev.DateEvent,
                    IntRound = int.TryParse(ev.IntRound, out var intRound) ? intRound : null,
                    IdHomeTeam = ev.IdHomeTeamInt.Value,
                    IdAwayTeam = ev.IdAwayTeamInt.Value,
                    TemResultado = ev.TemResultado,
                    SportsDbIdVencedor = ev.SportsDbIdVencedor()
                });
            }

            logger.LogInformation("Eventos obtidos: {Count} partidas.", result.Count);
            return result;
        }
    }
}
