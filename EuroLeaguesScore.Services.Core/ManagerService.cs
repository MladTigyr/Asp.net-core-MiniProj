namespace EuroLeaguesScore.Services.Core
{
    using EuroLeaguesScore.Data;
    using EuroLeaguesScore.Data.Models;
    using EuroLeaguesScore.Services.Core.Contracts;
    using EuroLeaguesScore.ViewModels.Manager;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ManagerService : IManagerService
    {
        private readonly EuroLeaguesScoreDbContext dbContext;

        public ManagerService(EuroLeaguesScoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddManagerToDbAsync(AddManagerInputModel model)
        {
            Manager manager = new Manager()
            {
                Name = model.Name,
                Age = model.Age,
                TeamId = model.TeamId,
            };

            await dbContext.AddAsync(manager);
            await dbContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteManagerFromDbAsync(int id)
        {
            Manager? manager = await GetManagerIfExistsAsync(id);

            if (manager == null)
            {
                return false;
            }

            dbContext.Remove(manager);
            await dbContext.SaveChangesAsync();

            return true;
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
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<AllManagerViewModel>> GetAllManagersWithTheirTeamIfTheyHaveAsync()
        {
            IEnumerable<AllManagerViewModel> models = await dbContext.Managers
                .AsNoTracking()
                .OrderBy(m => m.Name)
                .Select(m => new AllManagerViewModel
                {
                    Id = m.Id,
                    Name = m.Name,
                    Age = m.Age,
                })
                .ToArrayAsync();

            return models;
        }

        public async Task<DetailsManagerViewModel?> GetDetailsManagerViewModelAsync(int id)
        {
            Manager? manager = await GetManagerIfExistsAsync(id);

            if (manager == null)
            {
                return null;
            }

            Team? teamWithCurrManager = await dbContext.Teams
                .Include(t => t.League)
                .FirstOrDefaultAsync(m => m.Manager.Id == manager.Id);

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
            return await dbContext.Managers
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<TeamInputModel>> GetTeamInputModelAsync()
        {
            return await dbContext.Teams
                .OrderBy(t => t.Name)
                .Where(t => t.Manager == null)
                .Select(t => new TeamInputModel
                {
                    Id = t.Id,
                    Name = t.Name,
                })
                .ToArrayAsync();
        }
    }
}
