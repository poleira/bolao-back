using BolaoDaCopa.Dto.ApiFootball;

namespace BolaoDaCopa.Services.ApiFootball
{
    public interface IApiFootballService
    {
        Task<IList<SelecaoStandingDto>> ObterStandings();
    }
}
