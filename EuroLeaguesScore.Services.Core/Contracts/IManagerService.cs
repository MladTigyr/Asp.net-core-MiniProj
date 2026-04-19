namespace EuroLeaguesScore.Services.Core.Contracts
{
    using EuroLeaguesScore.Data.Models;
    using EuroLeaguesScore.ViewModels.Manager;

    public interface IManagerService
    {
        Task<IEnumerable<AllManagerViewModel>> GetAllManagersWithTheirTeamIfTheyHaveAsync();

        Task<IEnumerable<TeamInputModel>> GetTeamInputModelAsync();

        Task AddManagerToDbAsync(AddManagerInputModel model);

        Task<Manager?> GetManagerIfExistsAsync(int id);

        Task<DetailsManagerViewModel?> GetDetailsManagerViewModelAsync(int id);

        Task<EditManagerInputModel?> GetEditManagerViewModelAsync(int id);

        Task<bool> EditManagerToDbAsync(int id, EditManagerInputModel model);

        Task<DeleteManagerViewModel?> DeleteManagerViewModelAsync(int id);

        Task<bool> DeleteManagerFromDbAsync(int id);
    }
}
