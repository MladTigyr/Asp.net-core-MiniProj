namespace EuroLeaguesScore.Data.Repository.Contracts
{
    using EuroLeaguesScore.Data.Models;

    public interface IPlayerRepository : IRepository<Player, int>
    {
        Task<IEnumerable<Player>> GetDetailsPlayersOrderedByNameWithTeamIdAsync(int teamId);

        Task<IEnumerable<Player>> GetAllPlayersOrderedByLeagueThenByTeamNameThenByNameAsync();

        Task<Player?> GetPlayerWithHisTeamIfExistsAsync(int id);
    }
}
