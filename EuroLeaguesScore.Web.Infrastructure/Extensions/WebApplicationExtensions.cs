namespace EuroLeaguesScore.Web.Infrastructure.Extensions
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;

    using EuroLeaguesScore.Data.Seeding.Contracts;

    public static class WebApplicationExtensions
    {
        public static IApplicationBuilder UseRolesSeeder(this IApplicationBuilder app)
        {
            using IServiceScope scope =  app.ApplicationServices
                .CreateScope();

            IIdentitySeeder identitySeeder = scope
                .ServiceProvider
                .GetRequiredService<IIdentitySeeder>();

            identitySeeder.SeedRolesAsync()
                .GetAwaiter()
                .GetResult();

            return app;
        }

        public static IApplicationBuilder UseAdminSeeder(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices
                .CreateScope();

            IAdminSeeder userManager = scope
                .ServiceProvider
                .GetRequiredService<IAdminSeeder>();

            userManager.AssignAdminAsync()
                .GetAwaiter()
                .GetResult();

            return app;
        }
    }
}
