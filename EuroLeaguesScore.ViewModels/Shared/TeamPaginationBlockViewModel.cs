using EuroLeaguesScore.ViewModels.Team;

namespace EuroLeaguesScore.ViewModels.Shared
{
    public class TeamPaginationBlockViewModel
    {
        public int ElementsPerPage { get; set; }

        public int CurrentPage { get; set; }

        public IEnumerable<AllTeamViewModel> Teams { get; set; }
            = new List<AllTeamViewModel>();

        public int TotalCount { get; set; }

        public int Pages { get; set; }
    }
}
