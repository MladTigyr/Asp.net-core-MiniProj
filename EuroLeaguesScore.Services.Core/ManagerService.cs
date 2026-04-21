namespace EuroLeaguesScore.Services.Core
{
    using EuroLeaguesScore.Data.Models;
    using EuroLeaguesScore.Data.Repository.Contracts;
    using EuroLeaguesScore.Services.Core.Contracts;
    using EuroLeaguesScore.ViewModels.Manager;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ManagerService : IManagerService
    {
        private readonly IManagerRepository managerRepository;
        private readonly ITeamRepository teamRepository;

        public ManagerService(IManagerRepository managerRepository, ITeamRepository teamRepository)
        {
            this.managerRepository = managerRepository;
            this.teamRepository = teamRepository;
        }

        public async Task AddManagerToDbAsync(AddManagerInputModel model)
        {
            Manager manager = new Manager()
            {
                Name = model.Name,
                Age = model.Age,
                TeamId = model.TeamId,
            };

            await managerRepository.AddAsync(manager);
        }

        public async Task<bool> DeleteManagerFromDbAsync(int id)
        {
            Manager? manager = await GetManagerIfExistsAsync(id);

            if (manager == null)
            {
                return false;
            }

            bool isDeleted = await managerRepository
                .DeleteAsync(manager);

            return isDeleted;
        }

        public async Task<DeleteManagerViewModel?> DeleteManagerViewModelAsync(int id)
        {
            Manager? manager = await GetManagerIfExistsAsync(id);

            if (manager == null)
            {
                return null;
            }

            DeleteManagerViewModel model = new DeleteManagerViewModel
            {
                Id = manager.Id,
                Name = manager.Name,
            };

            return model;
        }

        public async Task<bool> EditManagerToDbAsync(int id, EditManagerInputModel model)
        {
            Manager? manager = await GetManagerIfExistsAsync(id);

            if (manager == null)
            {
                return false;
            }

            manager.Name = model.Name;
            manager.Age = model.Age;
            manager.TeamId = model.TeamId;

            await managerRepository.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<AllManagerViewModel>> GetAllManagersWithTheirTeamIfTheyHaveAsync(string? searchTerm = null)
        {
            IEnumerable<Manager> entityManagers = await managerRepository
                .GetAllManagersWithTheirTeamAsync(searchTerm);

            IEnumerable<AllManagerViewModel> models = entityManagers
                .Select(m => new AllManagerViewModel
                {
                    Id = m.Id,
                    Name = m.Name,
                    Age = m.Age,
                });

            return models;
        }

        public async Task<DetailsManagerViewModel?> GetDetailsManagerViewModelAsync(int id)
        {
            Manager? manager = await GetManagerIfExistsAsync(id);

            if (manager == null)
            {
                return null;
            }

            Team? teamWithCurrManager = await teamRepository
                .TeamWithCurrentManager(manager.Id);

            DetailsManagerViewModel model = new DetailsManagerViewModel
            {
                Id = manager.Id,
                Name = manager.Name,
                Age = manager.Age,
                TeamName = teamWithCurrManager?.Name,
                LeagueName = teamWithCurrManager?.League.Name,
            };

            return model;
        }

        public async Task<EditManagerInputModel?> GetEditManagerViewModelAsync(int id)
        {
            Manager? manager = await GetManagerIfExistsAsync(id);

            if (manager == null)
            {
                return null;
            }

            EditManagerInputModel model = new EditManagerInputModel
            {
                Id = manager.Id,
                Name = manager.Name,
                Age = manager.Age,
                TeamNames = await GetTeamInputModelAsync()
            };

            return model;
        }

        public async Task<Manager?> GetManagerIfExistsAsync(int id)
        {
            return await managerRepository
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<TeamInputModel>> GetTeamInputModelAsync()
        {
            IEnumerable<Team> entityTeams = await teamRepository
                .GetTeamsWithNoManagerAsync();

            return entityTeams
                .Select(t => new TeamInputModel
                {
                    Id = t.Id,
                    Name = t.Name,
                });
        }
    }
}
