
namespace EuroLeaguesScore.Services.Core.Contracts
{
    using EuroLeaguesScore.Data.Models;
    using EuroLeaguesScore.ViewModels.Player;

    public interface IPlayerService
    {
        public Task<IEnumerable<AllPlayersViewModel>> GetAllPlayersOrderedByLeagueThenByTeamNameThenByNameAsync();

        public Task<Player?> GetPlayerWithHisTeamNameIfExistsAsync(int playerId);

        public Task<DetailsPlayerViewModel?> GetDetailsPlayerViewModelAsync(int playerId);

        public Task<IEnumerable<Team>> GetAllTeamsAsync();
    }
}
