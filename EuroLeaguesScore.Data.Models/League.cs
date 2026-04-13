namespace EuroLeaguesScore.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static GCommon.EntityValidations;

    public class League
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(LeagueNameMaxLength)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Team> Teams { get; set; } 
            = new HashSet<Team>();
    }
}