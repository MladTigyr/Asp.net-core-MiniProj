namespace EuroLeaguesScore.Web.Infrastructure.Extensions
{
    using EuroLeaguesScore.Data.Seeding.Contracts;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;

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
    }
}
