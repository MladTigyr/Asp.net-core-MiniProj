namespace EuroLeaguesScore.ViewModels.Team
{
    using EuroLeaguesScore.Data.Models;
    using Microsoft.Extensions.Logging;
    using System.ComponentModel.DataAnnotations;

    using static GCommon.EntityValidations;
    using static GCommon.ViewModelsMessages;

    public class AddTeamInputModel
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

        public int? ManagerId { get; set; }

        public IEnumerable<Manager> Managers { get; set; } =
            new HashSet<Manager>();

        [Required]
        public int LeagueId { get; set; }

        [Required]
        public IEnumerable<League> Leagues { get; set; } = 
            new HashSet<League>();

        [Required]
        [Range(MinWinsLossesDraws, MaxWinsLossesDraws, ErrorMessage = WinsLossesDrawsMessage)]
        public int Wins { get; set; }

        [Required]
        [Range(MinWinsLossesDraws, MaxWinsLossesDraws, ErrorMessage = WinsLossesDrawsMessage)]
        public int Losses { get; set; }

        [Required]
        [Range(MinWinsLossesDraws, MaxWinsLossesDraws, ErrorMessage = WinsLossesDrawsMessage)]
        public int Draws { get; set; }
    }
}
