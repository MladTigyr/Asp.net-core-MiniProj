namespace EuroLeaguesScore.ViewModels.Team
{
    using System.ComponentModel.DataAnnotations;

    public class EditTeamInputModel : AddTeamInputModel
    {
        [Required]
        public int Id { get; set; }
    }
}
