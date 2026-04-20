namespace EuroLeaguesScore.Data.Models
{
    using Microsoft.AspNetCore.Identity;

    public class UserPlayer
    {
        public Guid UserId { get; set; }

        public virtual ApplicationUser User { get; set; } = null!;

        public int PlayerId { get; set; }

        public virtual Player Player { get; set; } = null!;
    }
}
