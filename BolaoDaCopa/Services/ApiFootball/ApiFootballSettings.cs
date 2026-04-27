namespace BolaoDaCopa.Services.ApiFootball
{
    public class ApiFootballSettings
    {
        public string ApiKey { get; set; } = "123";
        public int LeagueId { get; set; }
        public string Season { get; set; } = string.Empty;
        public string JobCronUtc { get; set; } = "0 6 * * *";
        public DateTime DataInicioJobs { get; set; }
        public DateTime DataFimJobs { get; set; }
        public DateTime DataInicioJobsEliminatorias { get; set; }
        public DateTime DataFimJobsEliminatorias { get; set; }
    }
}
