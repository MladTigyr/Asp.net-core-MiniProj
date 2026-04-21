namespace EuroLeaguesScore.Data.Repository.Contracts
{
    using EuroLeaguesScore.Data.Models;

    public interface IUserRepository
    {
        Task<IEnumerable<ApplicationUser>> GetAllUsersWithTheirRolesOrderedByUserNameAsync(Guid adminId);

        Task<ApplicationUser?> GetApplicationUserByIdAsync(Guid userId);
    }
}
