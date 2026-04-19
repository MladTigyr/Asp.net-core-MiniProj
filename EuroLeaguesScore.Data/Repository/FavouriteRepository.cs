
namespace EuroLeaguesScore.Data.Repository
{
    using EuroLeaguesScore.Data.Models;
    using EuroLeaguesScore.Data.Repository.Contracts;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;

    public class FavouriteRepository : BaseRepository<UserTeam, string>, IFavouriteRepository
    {
        public FavouriteRepository(EuroLeaguesScoreDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<UserTeam>> GetAllTeamsByUserIdOrderedByLeagueNameThenByTeamNameAsync(string userId)
        {
            return await this.GetAllAttached()
                .AsNoTracking()
                .Where(ut => ut.UserId == userId)
                .OrderBy(ut => ut.Team.League.Name)
                .ThenBy(ut => ut.Team.Name)
                .ToArrayAsync();
        }
    }
}
