namespace EuroLeaguesScore.Services.Core.Contracts
{
    using EuroLeaguesScore.ViewModels.Admin.UserManagement;

    public interface IUserService
    {
        Task<IEnumerable<AllUserViewModel>> GetAllUsersViewModelOrderedByUserNameAsync(Guid adminId);

        Task<bool> AssignRoleAsync(Guid userId, string role);

        Task<bool> DeleteUserAsync(Guid userId);
    }
}
