namespace EuroLeaguesScore.Services.Core.Contracts
{
    using EuroLeaguesScore.Data.Models;
    using EuroLeaguesScore.ViewModels.League;

    public interface ILeagueService
    {
        public Task<IEnumerable<AllLeagueViewModel>> GetAllLeagueViewModelsOrderedByLeagueNameAsync();

        public Task<League?> GetLeagueIfExistsWithIdParamAsync(int id);

        public Task<DetailsLeagueViewModel?> GetDetailsLeagueViewModelAsync(int id);
    }
}
