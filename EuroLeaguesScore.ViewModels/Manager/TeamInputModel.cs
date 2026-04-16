namespace EuroLeaguesScore.ViewModels.Manager
{
    using System.ComponentModel.DataAnnotations;
    public class TeamInputModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;
    }
}
