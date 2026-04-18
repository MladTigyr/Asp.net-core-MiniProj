namespace EuroLeaguesScore
{
    using EuroLeaguesScore.Data;
    using EuroLeaguesScore.Services.Core;
    using EuroLeaguesScore.Services.Core.Contracts;
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

            builder.Services.AddScoped<ITeamService, TeamService>();
            builder.Services.AddScoped<IPlayerService, PlayerService>();
            builder.Services.AddScoped<ILeagueService, LeagueService>();
            builder.Services.AddScoped<IManagerService, ManagerService>();
            builder.Services.AddScoped<IFavouriteService, FavouriteService>();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => {
                ConfigureIdentity(options, builder);
                }).AddEntityFrameworkStores<EuroLeaguesScoreDbContext>();
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
