namespace EuroLeaguesScore.Services.Core.Contracts
{
    using EuroLeaguesScore.Data.Models;
    using EuroLeaguesScore.ViewModels.League;

    public interface ILeagueService
    {
        Task<IEnumerable<AllLeagueViewModel>> GetAllLeagueViewModelsOrderedByLeagueNameAsync();

        Task<League?> GetLeagueIfExistsWithIdParamAsync(int id);

        Task<DetailsLeagueViewModel?> GetDetailsLeagueViewModelAsync(int id);
    }
}
