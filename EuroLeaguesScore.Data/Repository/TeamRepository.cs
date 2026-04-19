namespace EuroLeaguesScore.Data.Repository
{
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;

    using EuroLeaguesScore.Data.Models;
    using EuroLeaguesScore.Data.Repository.Contracts;

    public class TeamRepository : BaseRepository<Team, int>, ITeamRepository
    {
        private readonly EuroLeaguesScoreDbContext context;

        public TeamRepository(EuroLeaguesScoreDbContext dbContext) : base(dbContext)
        {
            this.context = dbContext;
        }

        public async Task<IEnumerable<Team>> AllTeamsOrderedByLeagueNameThenByNameAsync()
        {
            return await this.GetAllAttached()
                .AsNoTracking()
                .Include(t => t.League)
                .Include(t => t.Manager)
                .OrderBy(t => t.League.Name)
                .ThenBy(t => t.Name)
                .ToArrayAsync();
        }

        public async Task<Team?> GetTeamByIdTrackingWithoutIncludingAsync(int id)
        {
            return await this
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Team?> GetTeamByIdWithItsIncludesAsync(int id)
        {
            return await this.GetAllAttached()
                .AsNoTracking()
                .Include(t => t.League)
                .Include(t => t.Players)
                .Include(t => t.Manager)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Team?> GetTeamByIdWithItsIncludesTrackingAsync(int id)
        {
            return await this.GetAllAttached()
                .Include(t => t.League)
                .Include(t => t.Players)
                .Include(t => t.Manager)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Team>> GetTeamsWithNoManagerAsync()
        {
            return await this.GetAllAttached()
                .OrderBy(t => t.Name)
                .Where(t => t.Manager == null)
                .ToArrayAsync();
        }

        public async Task<Team?> TeamWithCurrentManager(int managerId)
        {
            return await this.GetAllAttached()
                .Include(t => t.League)
                .FirstOrDefaultAsync(m => m.Manager!.Id == managerId);
        }
    }
}
