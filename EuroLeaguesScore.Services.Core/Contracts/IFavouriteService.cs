
namespace EuroLeaguesScore.Services.Core.Contracts
{
    using EuroLeaguesScore.Data.Models;
    using EuroLeaguesScore.ViewModels.Favourite;

    public interface IFavouriteService
    {
        public Task<IEnumerable<FavouriteTeamViewModel>?> GetAllFavTeamsIfExistAsync(string userId);

        public Task ToggleTeamFavouriteAsync(string userId, int teamId);

        public Task<IEnumerable<FavouritePlayerViewModel>?> GetAllFavPlayersIfExistAsync(string userId);

        public Task TogglePlayerFavouriteAsync(string userId, int playerId);
    }
}
