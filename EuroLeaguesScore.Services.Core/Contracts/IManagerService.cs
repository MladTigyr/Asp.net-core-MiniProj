namespace EuroLeaguesScore.Services.Core.Contracts
{
    using EuroLeaguesScore.Data.Models;
    using EuroLeaguesScore.ViewModels.Manager;

    public interface IManagerService
    {
        public Task<IEnumerable<AllManagerViewModel>> GetAllManagersWithTheirTeamIfTheyHaveAsync();

        public Task<IEnumerable<TeamInputModel>> GetTeamInputModelAsync();

        public Task AddManagerToDbAsync(AddManagerInputModel model);

        public Task<Manager?> GetManagerIfExistsAsync(int id);

        public Task<DetailsManagerViewModel?> GetDetailsManagerViewModelAsync(int id);

        public Task<EditManagerInputModel?> GetEditManagerViewModelAsync(int id);

        public Task<bool> EditManagerToDbAsync(int id, EditManagerInputModel model);

        public Task<DeleteManagerViewModel?> DeleteManagerViewModelAsync(int id);

        public Task<bool> DeleteManagerFromDbAsync(int id);
    }
}
