namespace EuroLeaguesScore.Data.Repository.Contracts
{
    using EuroLeaguesScore.Data.Models;

    public interface IPlayerRepository : IRepository<Player, int>
    {
        Task<IEnumerable<Player>> GetDetailsPlayersOrderedByNameWithTeamIdAsync(int teamId);
    }
}
