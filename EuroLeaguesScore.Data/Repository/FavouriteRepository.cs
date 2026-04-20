
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
        private readonly EuroLeaguesScoreDbContext context;
        public FavouriteRepository(EuroLeaguesScoreDbContext dbContext) : base(dbContext)
        {
            context = dbContext;
        }

        public async Task<IEnumerable<UserTeam>> GetAllTeamsByUserIdOrderedByLeagueNameThenByTeamNameAsync(string userId)
        {
            return await this.GetAllAttached()
                .AsNoTracking()
                .Include(ut => ut.Team)
                .ThenInclude(t => t.League)
                .Where(ut => ut.UserId.ToString() == userId)
                .OrderBy(ut => ut.Team.League.Name)
                .ThenBy(ut => ut.Team.Name)
                .ToArrayAsync();
        }

        public async Task OnlyDeleteAsync(UserTeam userTeam)
        {
            context.UserTeams.Remove(userTeam);
            await context.SaveChangesAsync();
        }
    }
}
