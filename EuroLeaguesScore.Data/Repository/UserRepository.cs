namespace EuroLeaguesScore.Data.Repository
{
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EuroLeaguesScore.Data.Models;
    using EuroLeaguesScore.Data.Repository.Contracts;

    public class UserRepository : BaseRepository<ApplicationUser, Guid>, IUserRepository
    {
        public UserRepository(EuroLeaguesScoreDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersWithTheirRolesOrderedByUserNameAsync(Guid adminId)
        {
            return await this.GetAllAttached()
                .AsNoTracking()
                .Where(u => u.Id != adminId)
                .OrderBy(u => u.UserName)
                .ToArrayAsync();
        }

        public async Task<ApplicationUser?> GetApplicationUserByIdAsync(Guid userId)
        {
            return await this.FirstOrDefaultAsync(u => u.Id == userId);
        }
    }
}
