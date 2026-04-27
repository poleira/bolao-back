using System.Text.Json.Serialization;

namespace BolaoDaCopa.Dto.ApiFootball
{
    public class SportsDbTableRoot
    {
        [JsonPropertyName("table")]
        public List<SportsDbTeamStanding> Table { get; set; } = new();
    }

    public class SportsDbTeamStanding
    {
        [JsonPropertyName("idTeam")]
        public string IdTeam { get; set; } = string.Empty;

        [JsonPropertyName("strTeam")]
        public string StrTeam { get; set; } = string.Empty;

        [JsonPropertyName("intRank")]
        public string IntRank { get; set; } = "0";

        [JsonPropertyName("intPoints")]
        public string IntPoints { get; set; } = "0";
    }

    public class SelecaoStandingDto
    {
        public int TeamId { get; set; }
        public int Rank { get; set; }
        public int Points { get; set; }
    }
}
