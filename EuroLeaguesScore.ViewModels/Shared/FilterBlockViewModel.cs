using EuroLeaguesScore.ViewModels.League;

namespace EuroLeaguesScore.ViewModels.Shared
{
    public class FilterBlockViewModel
    {
        public IEnumerable<AllLeagueViewModel> Leagues { get; set; } 
            = new HashSet<AllLeagueViewModel>();
    }
}
