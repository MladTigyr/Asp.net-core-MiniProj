
namespace EuroLeaguesScore.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using EuroLeaguesScore.Data.Models;

    public class UserPlayerConfiguration : IEntityTypeConfiguration<UserPlayer>
    {
        public void Configure(EntityTypeBuilder<UserPlayer> entity)
        {
            entity.HasKey(up => new { up.UserId, up.PlayerId });

            entity.HasOne(u => u.User)
                .WithMany()
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(p => p.Player)
                .WithMany()
                .HasForeignKey(p => p.PlayerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
