
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

    public class PlayerRepository : BaseRepository<Player, int>, IPlayerRepository
    {
        public PlayerRepository(EuroLeaguesScoreDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Player>> GetDetailsPlayersOrderedByNameWithTeamIdAsync(int teamId)
        {
            return await this.GetAllAttached()
                .AsNoTracking()
                .Where(p => p.TeamId == teamId)
                .OrderBy(p => p.Name)
                .ToArrayAsync();
        }
    }
}
