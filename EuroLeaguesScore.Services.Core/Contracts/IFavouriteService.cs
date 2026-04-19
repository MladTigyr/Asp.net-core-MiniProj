
namespace EuroLeaguesScore.Services.Core.Contracts
{
    using EuroLeaguesScore.Data.Models;
    using EuroLeaguesScore.ViewModels.Favourite;

    public interface IFavouriteService
    {
        Task<IEnumerable<FavouriteTeamViewModel>?> GetAllFavTeamsIfExistAsync(string userId);

        Task ToggleTeamFavouriteAsync(string userId, int teamId);

        Task<IEnumerable<FavouritePlayerViewModel>?> GetAllFavPlayersIfExistAsync(string userId);

        Task TogglePlayerFavouriteAsync(string userId, int playerId);
    }
}
