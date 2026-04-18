namespace EuroLeaguesScore.Data.Models
{
    using Microsoft.AspNetCore.Identity;

    public class UserPlayer
    {
        public string UserId { get; set; } = null!;

        public virtual IdentityUser User { get; set; } = null!;

        public int PlayerId { get; set; }

        public virtual Player Player { get; set; } = null!;
    }
}
