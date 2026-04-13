namespace EuroLeaguesScore.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Match
    {
        [Key]
        public int Id { get; set; }

        public int HomeTeamId { get; set; }

        public virtual Team HomeTeam { get; set; } = null!;

        public int AwayTeamId { get; set; }

        public virtual Team AwayTeam { get; set; } = null!;

        public DateTime MatchDate { get; set; }

        public int HomeTeamGoals { get; set; }

        public int AwayTeamGoals { get; set; }
    }
}
