
namespace EuroLeaguesScore.Data.Repository
{
    using EuroLeaguesScore.Data.Models;
    using EuroLeaguesScore.Data.Repository.Contracts;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ManagerRepository : BaseRepository<Manager, int>, IManagerRepository
    {
        private readonly EuroLeaguesScoreDbContext context;
        public ManagerRepository(EuroLeaguesScoreDbContext dbContext) : base(dbContext)
        {
            this.context = dbContext;
        }

        public async Task<IEnumerable<Manager>> GetAllManagersWithTheirTeamAsync()
        {
            return await this.GetAllAttached()
                .AsNoTracking()
                .OrderBy(m => m.Name)
                .ToArrayAsync();
        }

        public async Task<IEnumerable<Manager>> GetManagersWhichHaveNotTeamToManageAsync()
        {
            return await this.GetAllAttached()
                .AsNoTracking()
                .Where(m => !context.Teams.Any(t => t.ManagerId == m.Id)) // im doing this because i want to show only managers that are not currently managing a team. When i first seeded the db i made sure that all managers are not assigned to a team (i made their teamId null), so this will work fine.
                .OrderBy(m => m.Name)
                .Select(m => new Manager
                {
                    Id = m.Id,
                    Name = m.Name,
                    Age = m.Age
                })
                .ToArrayAsync();
        }

        public async Task<IEnumerable<Manager>> GetManagersWhichHaveNotTeamToManageOrCurrentTeamManagerAsync(int teamId)
        {
            return await this.GetAllAttached()
                    .AsNoTracking()
                    .Where(m => !context.Teams.Any(t => t.ManagerId == m.Id && t.Id != teamId))
                    .OrderBy(m => m.Name)
                    .ToArrayAsync();
        }
    }
}
