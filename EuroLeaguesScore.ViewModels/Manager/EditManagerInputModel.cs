namespace EuroLeaguesScore.ViewModels.Manager
{
    using System.ComponentModel.DataAnnotations;

    public class EditManagerInputModel : AddManagerInputModel
    {
        [Required]
        public int Id { get; set; }
    }
}
