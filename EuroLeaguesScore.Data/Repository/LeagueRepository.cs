
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

    public class LeagueRepository : BaseRepository<League, int>, ILeagueRepository
    {
        public LeagueRepository(EuroLeaguesScoreDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<League>> GetAllLeaguesWithTheirTeamsOrderedByLeagueNameAsync()
        {
            return await this.GetAllAttached()
                .AsNoTracking()
                .Include(l => l.Teams)
                .OrderBy(l => l.Name)
                .ToArrayAsync();
        }

        public async Task<League?> GetLeagueIfExistsWithIdParamAsync(int id)
        {
            return await this.GetAllAttached()
                .AsNoTracking()
                .Include(l => l.Teams)
                .ThenInclude(l => l.Players)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<IEnumerable<League>> GetLeaguesOrderedByLeagueNameAsync()
        {
            return await this.GetAllAttached()
                   .AsNoTracking()
                   .OrderBy(l => l.Name)
                   .ToArrayAsync();
        }
    }
}
