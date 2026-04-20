
namespace EuroLeaguesScore.Data.Models
{
    using Microsoft.AspNetCore.Identity;

    public class UserTeam
    {
        public Guid UserId { get; set; }

        public virtual ApplicationUser User { get; set; } = null!;

        public int TeamId { get; set; }

        public virtual Team Team { get; set; } = null!;
    }
}
