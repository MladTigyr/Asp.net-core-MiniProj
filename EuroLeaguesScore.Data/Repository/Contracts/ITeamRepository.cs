namespace EuroLeaguesScore.Data.Repository.Contracts
{
    using EuroLeaguesScore.Data.Models;

    public interface ITeamRepository : IRepository<Team, int>
    {
        Task<IEnumerable<Team>> AllTeamsOrderedByLeagueNameThenByNameAsync(string? searchTerm = null, int? leagueId = null);

        Task<Team?> GetTeamByIdWithItsIncludesAsync(int id);

        Task<Team?> GetTeamByIdWithItsIncludesTrackingAsync(int id);

        Task<Team?> GetTeamByIdTrackingWithoutIncludingAsync(int id);

        Task<IEnumerable<Team>> GetTeamsWithNoManagerAsync();

        Task<Team?> TeamWithCurrentManager(int managerId);
    }
}
