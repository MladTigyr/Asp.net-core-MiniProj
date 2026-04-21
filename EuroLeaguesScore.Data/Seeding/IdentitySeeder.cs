namespace EuroLeaguesScore.Data.Seeding
{
    using Microsoft.AspNetCore.Identity;

    using EuroLeaguesScore.Data.Seeding.Contracts;
    using static GCommon.ExceptionMessages;

    public class IdentitySeeder : IIdentitySeeder
    {
        public static string[] DefaultRoles = new[]
        {
            "Admin",
            "User"
        };

        private readonly RoleManager<IdentityRole<Guid>> roleManager;

        public IdentitySeeder(RoleManager<IdentityRole<Guid>> roleManager)
        {
            this.roleManager = roleManager;
        }

        public async Task SeedRolesAsync()
        {
            foreach(string role in DefaultRoles)
            {
                bool roleExist = await roleManager.RoleExistsAsync(role);

                if (!roleExist)
                {
                    IdentityRole<Guid> newRole = new IdentityRole<Guid>(role);

                    IdentityResult identityRoleResult = await roleManager
                        .CreateAsync(newRole);

                    if (!identityRoleResult.Succeeded)
                    {
                        throw new InvalidOperationException(string.Format(RoleSeedingExceptionMessage, role));
                    }
                }
            }
        }
    }
}
