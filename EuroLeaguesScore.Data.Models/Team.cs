namespace EuroLeaguesScore.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static GCommon.EntityValidations;

    public class Team
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(TeamNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(TeamCountryMaxLength)]
        public string Country { get; set; } = null!;

        [Required]
        [MaxLength(TeamCityMaxLength)]
        public string City { get; set; } = null!;

        public int Wins { get; set; }

        public int Losses { get; set; }

        public int Draws { get; set; }

        public int LeagueId { get; set; }

        [ForeignKey(nameof(LeagueId))]
        public virtual League League { get; set; } = null!;

        public int ManagerId { get; set; }

        [ForeignKey(nameof(ManagerId))]
        public virtual Manager Manager { get; set; } = null!;

        public virtual ICollection<Player> Players { get; set; } 
            = new HashSet<Player>();

    }
}
