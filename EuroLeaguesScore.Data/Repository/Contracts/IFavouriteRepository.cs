namespace EuroLeaguesScore.Data.Repository.Contracts
{
    using EuroLeaguesScore.Data.Models;

    public interface IFavouriteRepository : IRepository<UserTeam, string>
    {
        Task<IEnumerable<UserTeam>> GetAllTeamsByUserIdOrderedByLeagueNameThenByTeamNameAsync(string userId);
    }
}
