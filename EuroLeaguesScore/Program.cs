namespace EuroLeaguesScore
{
    using EuroLeaguesScore.Data;
    using EuroLeaguesScore.Data.Models;
    using EuroLeaguesScore.Data.Repository;
    using EuroLeaguesScore.Data.Repository.Contracts;
    using EuroLeaguesScore.Data.Seeding;
    using EuroLeaguesScore.Data.Seeding.Contracts;
    using EuroLeaguesScore.Services.Core;
    using EuroLeaguesScore.Services.Core.Contracts;
    using EuroLeaguesScore.Web.Infrastructure.Extensions;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            string? connectionString = builder.Configuration.GetConnectionString("SqlDevConnection") ?? throw new InvalidOperationException("Connection string 'SqlDevConnection' not found.");
            builder.Services.AddDbContext<EuroLeaguesScoreDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddScoped<ITeamRepository, TeamRepository>();
            builder.Services.AddScoped<IManagerRepository, ManagerRepository>();
            builder.Services.AddScoped<ILeagueRepository, LeagueRepository>();
            builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();
            builder.Services.AddScoped<IFavouriteRepository, FavouriteRepository>();
            builder.Services.AddScoped<IUserPlayerRepository, UserPlayerRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            builder.Services.AddScoped<ITeamService, TeamService>();
            builder.Services.AddScoped<IPlayerService, PlayerService>();
            builder.Services.AddScoped<ILeagueService, LeagueService>();
            builder.Services.AddScoped<IManagerService, ManagerService>();
            builder.Services.AddScoped<IFavouriteService, FavouriteService>();
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddTransient<IIdentitySeeder, IdentitySeeder>();
            builder.Services.AddTransient<IAdminSeeder, AdminSeeder>();

            builder.Services.AddDefaultIdentity<ApplicationUser>(options => {
                ConfigureIdentity(options, builder);
            })
            .AddRoles<IdentityRole<Guid>>()
            .AddRoleManager<RoleManager<IdentityRole<Guid>>>()
            .AddEntityFrameworkStores<EuroLeaguesScoreDbContext>();
            builder.Services.AddControllersWithViews();

            WebApplication app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseRolesSeeder();
            app.UseAdminSeeder();

            app.UseStatusCodePagesWithRedirects("/Home/Error/{0}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }

        private static void ConfigureIdentity(IdentityOptions options, WebApplicationBuilder builder)
        {
            options.SignIn.RequireConfirmedAccount = builder
                .Configuration.GetValue<bool>("IdentitySettings:SignIn:RequireConfirmedAccount");

            options.User.RequireUniqueEmail = builder
                .Configuration.GetValue<bool>("IdentitySettings:User:RequireUniqueEmail");

            options.Password.RequiredLength = builder
                .Configuration.GetValue<int>("IdentitySettings:Password:RequiredLength");
            options.Password.RequireDigit = builder
                .Configuration.GetValue<bool>("IdentitySettings:Password:RequireDigit");
            options.Password.RequireNonAlphanumeric = builder
                .Configuration.GetValue<bool>("IdentitySettings:Password:RequireNonAlphanumeric");
            options.Password.RequireUppercase = builder
                .Configuration.GetValue<bool>("IdentitySettings:Password:RequireUppercase");
            options.Password.RequireLowercase = builder
                .Configuration.GetValue<bool>("IdentitySettings:Password:RequireLowercase");
        }
    }
}
