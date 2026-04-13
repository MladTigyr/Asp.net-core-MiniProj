namespace EuroLeaguesScore.ViewModels.Team
{
    using EuroLeaguesScore.Data.Models;
    using System.ComponentModel.DataAnnotations;

    public class DetailsTeamInputModel : AllTeamViewModel
    {


        public IEnumerable<Player> Players { get; set; } 
            = new HashSet<Player>();   
    }
}
