
namespace EuroLeaguesScore.Services.Core.Contracts
{
    using EuroLeaguesScore.Data.Models;
    using EuroLeaguesScore.ViewModels.Player;

    public interface IPlayerService
    {
        Task<IEnumerable<AllPlayersViewModel>> GetAllPlayersOrderedByLeagueThenByTeamNameThenByNameAsync(string? userId, string? searchTerm, int elementsPerPage, int currentPage);

        Task<DetailsPlayerViewModel?> GetDetailsPlayerViewModelAsync(int playerId, string userId);

        Task<IEnumerable<TeamViewModel>> GetAllTeamsAsync();

        Task AddPlayerToDbAsync(AddPlayerInputModel model);

        Task<EditPlayerInputModel?> GetEditPlayerInputModelAsync(int playerId);

        Task<bool> EditPlayerToDbAsync(int playerId, EditPlayerInputModel model);

        Task<DeletePlayerViewModel?> GetDeletePlayerViewModelAsync(int playerId);

        Task<bool> DeletePlayerFromDbAsync(int playerId);

        Task<PlayerPaginationBlockViewModel> GetAllPlayersPaginated(string? userId, string? searchTerm, int elementsPerPage, int currentPage);
    }
}
