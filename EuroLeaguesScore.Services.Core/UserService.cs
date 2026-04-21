namespace EuroLeaguesScore.Services.Core
{
    using EuroLeaguesScore.Data.Models;
    using EuroLeaguesScore.Data.Repository.Contracts;
    using EuroLeaguesScore.Services.Core.Contracts;
    using EuroLeaguesScore.ViewModels.Admin.UserManagement;
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole<Guid>> roleManager;

        public UserService(IUserRepository userRepository, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            this.userRepository = userRepository;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<IEnumerable<AllUserViewModel>> GetAllUsersViewModelOrderedByUserNameAsync(Guid adminId)
        {
            IEnumerable<ApplicationUser> entityUsers = await userRepository
                .GetAllUsersWithTheirRolesOrderedByUserNameAsync(adminId);

            ICollection<AllUserViewModel> result = new List<AllUserViewModel>();

            foreach (ApplicationUser user in entityUsers)
            {
                IEnumerable<string> roles = await userManager.GetRolesAsync(user);

                result.Add(new AllUserViewModel
                {
                    Id = user.Id.ToString(),
                    UserName = user.UserName!,
                    Email = user.Email!,
                    Role = roles.FirstOrDefault() ?? "No role"
                });
            }

            return result;
        }

        public async Task<bool> AssignRoleAsync(Guid userId, string role)
        {
            ApplicationUser? appUser = await userManager.FindByIdAsync(userId.ToString());
            if (appUser == null)
            {
                return false;
            }

            bool roleExists = await roleManager.RoleExistsAsync(role);
            if (!roleExists)
            {
                return false;
            }

            IEnumerable<string>? currentRoles = await userManager.GetRolesAsync(appUser);

            if (currentRoles.Any())
            {
                IdentityResult removeResult = await userManager.RemoveFromRolesAsync(appUser, currentRoles);
                if (!removeResult.Succeeded)
                {
                    return false;
                }
            }

            IdentityResult addResult = await userManager.AddToRoleAsync(appUser, role);
            if (!addResult.Succeeded)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            ApplicationUser? user = await userRepository
                .GetApplicationUserByIdAsync(id);

            if (user == null)
            {
                return false;
            }

            IdentityResult isDeleted = await userManager
                .DeleteAsync(user);
                
            if (!isDeleted.Succeeded)
            {
                return false;
            }

            return true;
        }
    }
}
