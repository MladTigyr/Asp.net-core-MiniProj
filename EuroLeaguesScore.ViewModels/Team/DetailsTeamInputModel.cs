namespace EuroLeaguesScore.ViewModels.Team
{
    using EuroLeaguesScore.ViewModels.Player;

    public class DetailsTeamInputModel : AllTeamViewModel
    {


        public IEnumerable<DetailsPlayerViewModel> Players { get; set; } 
            = new HashSet<DetailsPlayerViewModel>();   
    }
}
