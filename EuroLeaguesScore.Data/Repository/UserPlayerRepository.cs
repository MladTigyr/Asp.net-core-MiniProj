namespace EuroLeaguesScore.Data.Repository
{
    using System.Linq;

    using EuroLeaguesScore.Data.Models;
    using EuroLeaguesScore.Data.Repository.Contracts;
    using Microsoft.EntityFrameworkCore;

    public class UserPlayerRepository : BaseRepository<UserPlayer, string>, IUserPlayerRepository
    {
        private readonly EuroLeaguesScoreDbContext context;

        public UserPlayerRepository(EuroLeaguesScoreDbContext dbContext) : base(dbContext)
        {
            this.context = dbContext;
        }

        public async Task<IEnumerable<UserPlayer>> GetAllFavPlayersOrderedByPlayerNameWithUserIdParamAsync(string userId)
        {
            return await this.GetAllAttached()
                .AsNoTracking()
                .Include(up => up.Player)
                .Where(up => up.UserId.ToString() == userId)
                .OrderBy(up => up.Player.Name)
                .ToArrayAsync();
        }

        public async Task<IEnumerable<UserPlayer>> GetAllFavPlayersWithUserIdParamAsync(string userId)
        {
            return await this.GetAllAttached()
                .Include(up => up.Player)
                .Where(x => x.UserId.ToString() == userId)
                .ToArrayAsync();
        }

        public async Task OnlyDeleteAsync(UserPlayer userPlayer)
        {
            context.UserPlayers.Remove(userPlayer);
            await context.SaveChangesAsync();
        }
    }
}
