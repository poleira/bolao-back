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

            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var root = JsonSerializer.Deserialize<SportsDbTableRoot>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var result = new List<SelecaoStandingDto>();

            if (root?.Table == null || root.Table.Count == 0)
            {
                logger.LogWarning("Resposta do TheSportsDB não contém dados de standings.");
                return result;
            }

            foreach (var standing in root.Table)
            {
                if (!int.TryParse(standing.IdTeam, out var teamId)) continue;
                int.TryParse(standing.IntRank, out var rank);
                int.TryParse(standing.IntPoints, out var points);

                result.Add(new SelecaoStandingDto
                {
                    TeamId = teamId,
                    Rank = rank,
                    Points = points
                });
            }

            logger.LogInformation("Standings obtidos: {Count} seleções.", result.Count);
            return result;
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

            foreach (var ev in root.Events)
            {
                if (ev.IdHomeTeamInt == null || ev.IdAwayTeamInt == null) continue;

                result.Add(new SportsDbEventDto
                {
                    StrRound = ev.StrRound,
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
