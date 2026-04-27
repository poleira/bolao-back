using System.Text.Json.Serialization;

namespace BolaoDaCopa.Dto.ApiFootball
{
    public class SportsDbEventsRoot
    {
        [JsonPropertyName("events")]
        public List<SportsDbEvent>? Events { get; set; }
    }

    public class SportsDbEvent
    {
        [JsonPropertyName("idEvent")]
        public string IdEvent { get; set; } = string.Empty;

        [JsonPropertyName("strRound")]
        public string StrRound { get; set; } = string.Empty;

        [JsonPropertyName("idHomeTeam")]
        public string IdHomeTeam { get; set; } = string.Empty;

        [JsonPropertyName("idAwayTeam")]
        public string IdAwayTeam { get; set; } = string.Empty;

        [JsonPropertyName("intHomeScore")]
        public string? IntHomeScore { get; set; }

        [JsonPropertyName("intAwayScore")]
        public string? IntAwayScore { get; set; }

        [JsonPropertyName("intHomeScorePenalty")]
        public string? IntHomeScorePenalty { get; set; }

        [JsonPropertyName("intAwayScorePenalty")]
        public string? IntAwayScorePenalty { get; set; }

        public bool TemResultado =>
            !string.IsNullOrWhiteSpace(IntHomeScore) &&
            !string.IsNullOrWhiteSpace(IntAwayScore) &&
            int.TryParse(IntHomeScore, out _) &&
            int.TryParse(IntAwayScore, out _);

        public int? IdHomeTeamInt =>
            int.TryParse(IdHomeTeam, out var v) ? v : null;

        public int? IdAwayTeamInt =>
            int.TryParse(IdAwayTeam, out var v) ? v : null;

        /// <summary>
        /// Retorna o SportsDbId do vencedor, ou null se empate sem penaltis.
        /// </summary>
        public int? SportsDbIdVencedor()
        {
            if (!TemResultado) return null;

            var homeScore = int.Parse(IntHomeScore!);
            var awayScore = int.Parse(IntAwayScore!);

            if (homeScore != awayScore)
                return homeScore > awayScore ? IdHomeTeamInt : IdAwayTeamInt;

            // Empate: decide nos penaltis
            if (!string.IsNullOrWhiteSpace(IntHomeScorePenalty) &&
                !string.IsNullOrWhiteSpace(IntAwayScorePenalty) &&
                int.TryParse(IntHomeScorePenalty, out var homePen) &&
                int.TryParse(IntAwayScorePenalty, out var awayPen))
            {
                return homePen > awayPen ? IdHomeTeamInt : IdAwayTeamInt;
            }

            return null;
        }
    }

    public class SportsDbEventDto
    {
        public string StrRound { get; set; } = string.Empty;
        public int IdHomeTeam { get; set; }
        public int IdAwayTeam { get; set; }
        public bool TemResultado { get; set; }
        public int? SportsDbIdVencedor { get; set; }
    }
}
