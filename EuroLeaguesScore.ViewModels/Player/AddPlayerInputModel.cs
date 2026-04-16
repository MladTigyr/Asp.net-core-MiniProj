namespace EuroLeaguesScore.ViewModels.Player
{
    using System.ComponentModel.DataAnnotations;
    using static GCommon.EntityValidations;
    using static GCommon.ViewModelsMessages;
    using Data.Models;
    using EuroLeaguesScore.Data.Models.Enums;

    public class AddPlayerInputModel
    {
        [Required]
        [MinLength(PlayerNameMinLength)]
        [MaxLength(PlayerNameMaxLength)]
        public string Name { get; set; } = null!;
        
        [Required]
        [Range(PlayerAgeMin, PlayerAgeMax, ErrorMessage = AgeMessage)]
        public int Age { get; set; }

        [Required]
        public Position Position { get; set; }

        [Required]
        [Range(MinWinsLossesDraws, MaxWinsLossesDraws, ErrorMessage = GoalsAssistsMessage)]
        public int Goals { get; set; }

        [Required]
        [Range(MinWinsLossesDraws, MaxWinsLossesDraws, ErrorMessage = GoalsAssistsMessage)]
        public int Assists { get; set; }

        [Required]
        public int TeamId { get; set; }

        [Required]
        public IEnumerable<Team> TeamNames { get; set; }
            = new HashSet<Team>();
    }
}
