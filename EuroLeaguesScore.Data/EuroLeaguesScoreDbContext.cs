namespace EuroLeaguesScore.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    using Data.Models;

    public class EuroLeaguesScoreDbContext : IdentityDbContext
    {
        public EuroLeaguesScoreDbContext(DbContextOptions<EuroLeaguesScoreDbContext> options)
            : base(options)
        {

        }

        public virtual DbSet<Manager> Managers { get; set; } = null!;

        public virtual DbSet<Team> Teams { get; set; } = null!;

        public virtual DbSet<Player> Players { get; set; } = null!;

        public virtual DbSet<League> Leagues { get; set; } = null!;

        public virtual DbSet<UserTeam> UserTeams { get; set; } = null!;

        public virtual DbSet<UserPlayer> UserPlayers { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
                base.OnModelCreating(builder);
                
                builder.ApplyConfigurationsFromAssembly(typeof(EuroLeaguesScoreDbContext).Assembly);
        }

    }
}
