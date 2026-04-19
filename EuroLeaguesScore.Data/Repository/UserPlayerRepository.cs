namespace EuroLeaguesScore.Data.Repository
{
    using System.Linq;

    using EuroLeaguesScore.Data.Models;
    using EuroLeaguesScore.Data.Repository.Contracts;
    using Microsoft.EntityFrameworkCore;

    public class UserPlayerRepository : BaseRepository<UserPlayer, string>, IUserPlayerRepository
    {
        public UserPlayerRepository(EuroLeaguesScoreDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<UserPlayer>> GetAllFavPlayersWithUserIdParamAsync(string userId)
        {
            return await this.GetAllAttached()
                .Where(x => x.UserId == userId)
                .ToArrayAsync();
        }
    }
}
