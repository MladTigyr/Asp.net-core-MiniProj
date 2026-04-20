namespace EuroLeaguesScore.Data.Models
{
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser<Guid>
    {
        public virtual ICollection<UserPlayer> FavPlayers { get; set; }
            = new List<UserPlayer>();

        public virtual ICollection<UserTeam> FavTeams { get; set; }
            = new List<UserTeam>();


    }
}
