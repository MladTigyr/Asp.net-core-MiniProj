namespace EuroLeaguesScore.ViewModels.Team
{
    using EuroLeaguesScore.Data.Models;
    using System.ComponentModel.DataAnnotations;
    using static GCommon.EntityValidations;

    public class CreateTeamInputModel
    {
        [Required]
        [MinLength(TeamNameMinLength)]
        [MaxLength(TeamNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MinLength(TeamCountryMinLength)]
        [MaxLength(TeamCountryMaxLength)]
        public string Country { get; set; } = null!;

        [Required]
        [MinLength(TeamCityMinLength)]
        [MaxLength(TeamCityMaxLength)]
        public string City { get; set; } = null!;

        [Required]
        public int LeagueId { get; set; }

        [Required]
        public IEnumerable<League> Leagues { get; set; } = 
            new HashSet<League>();

        [Required]
        [Range(MinWinsLossesDraws, MaxWinsLossesDraws)]
        public int Wins { get; set; }

        [Required]
        [Range(MinWinsLossesDraws, MaxWinsLossesDraws)]
        public int Losses { get; set; }

        [Required]
        [Range(MinWinsLossesDraws, MaxWinsLossesDraws)]
        public int Draws { get; set; }
    }
}
