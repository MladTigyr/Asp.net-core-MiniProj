
namespace EuroLeaguesScore.Data.Models
{
    using Microsoft.AspNetCore.Identity;

    public class UserTeam
    {
        public string UserId { get; set; } = null!;

        public virtual IdentityUser User { get; set; } = null!;

        public int TeamId { get; set; }

        public virtual Team Team { get; set; } = null!;
    }
}
