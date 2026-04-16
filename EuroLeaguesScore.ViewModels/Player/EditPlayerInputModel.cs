
namespace EuroLeaguesScore.ViewModels.Player
{
    using System.ComponentModel.DataAnnotations;

    public class EditPlayerInputModel : AddPlayerInputModel
    {
        [Required]
        public int Id { get; set; }
    }
}
