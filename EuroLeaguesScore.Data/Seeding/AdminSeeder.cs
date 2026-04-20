namespace EuroLeaguesScore.Data.Seeding
{
    using Microsoft.AspNetCore.Identity;
    using System.Threading.Tasks;

    using EuroLeaguesScore.Data.Models;
    using EuroLeaguesScore.Data.Seeding.Contracts;

    using static GCommon.ExceptionMessages;

    public class AdminSeeder : IAdminSeeder
    {
        private readonly string userName = "admin";
        private readonly string email = "admin@gmail.com";
        private readonly string password = "1234567";

        private readonly UserManager<ApplicationUser> userManager;

        public AdminSeeder(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task AssignAdminAsync()
        {
            ApplicationUser? adminUser = await userManager
                .FindByEmailAsync(email);

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = userName,
                    Email = email,
                    NormalizedUserName = userName.ToUpper(),
                    NormalizedEmail = email.ToUpper()
                };

                IdentityResult createUserResult = await userManager
                    .CreateAsync(adminUser, password);

                if (!createUserResult.Succeeded)
                {
                    throw new InvalidOperationException(string.Format(AdminCreationExceptionMessage, userName));
                }
            }

            bool isInRole = await userManager
                .IsInRoleAsync(adminUser, "Admin");

            if (!isInRole)
            {
                IdentityResult addRoleResult = await userManager
                    .AddToRoleAsync(adminUser, "Admin");

                if (!addRoleResult.Succeeded)
                {
                    throw new InvalidOperationException(string.Format(AdminAssignExceptionMessage, userName));
                }
            }
        }
    }
}
