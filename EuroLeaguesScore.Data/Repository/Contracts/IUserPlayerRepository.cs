namespace EuroLeaguesScore.Data.Repository.Contracts
{
    using EuroLeaguesScore.Data.Models;

    public interface IUserPlayerRepository : IRepository<UserPlayer, string>
    {
        Task<IEnumerable<UserPlayer>> GetAllFavPlayersWithUserIdParamAsync(string userId);

        Task<IEnumerable<UserPlayer>> GetAllFavPlayersOrderedByPlayerNameWithUserIdParamAsync(string userId);

        Task OnlyDeleteAsync(UserPlayer userPlayer);
    }
}
