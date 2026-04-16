namespace EuroLeaguesScore.ViewModels.Manager
{
    using System.ComponentModel.DataAnnotations;

    using static GCommon.EntityValidations;
    using static GCommon.ViewModelsMessages;

    public class AddManagerInputModel
    {
        [Required]
        [MinLength(ManagerNameMinLength)]
        [MaxLength(ManagerNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [Range(ManagerAgeMin, ManagerAgeMax, ErrorMessage = AgeMessage)]
        public int Age { get; set; }

        public int? TeamId { get; set; }

        public IEnumerable<TeamInputModel> TeamNames { get; set; }
            = new HashSet<TeamInputModel>();
    }
}
