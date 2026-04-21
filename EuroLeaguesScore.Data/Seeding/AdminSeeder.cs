namespace EuroLeaguesScore.Data.Seeding
{
    using Microsoft.AspNetCore.Identity;
    using System.Threading.Tasks;

    using EuroLeaguesScore.Data.Models;
    using EuroLeaguesScore.Data.Seeding.Contracts;

    using static GCommon.ExceptionMessages;
    using Microsoft.Extensions.Configuration;

    public class AdminSeeder : IAdminSeeder
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;

        public AdminSeeder(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }

        public async Task AssignAdminAsync()
        {
            string userName = configuration["AdminSettings:UserName"] ?? "admin";
            string userEmail = configuration["AdminSettings:Email"] ?? "admin@gmail.com";
            string userPassword = configuration["AdminSettings:Password"] ?? "1234567";

            ApplicationUser? adminUser = await userManager
                .FindByEmailAsync(userEmail);

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = userName,
                    Email = userEmail,
                    NormalizedUserName = userName.ToUpper(),
                    NormalizedEmail = userEmail.ToUpper()
                };

                IdentityResult createUserResult = await userManager
                    .CreateAsync(adminUser, userPassword);

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
