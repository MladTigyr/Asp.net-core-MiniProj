namespace EuroLeaguesScore.ViewModels.League
{
    using EuroLeaguesScore.ViewModels.Team;

    public class DetailsLeagueViewModel : AllLeagueViewModel
    {
        public int TotalPlayers { get; set; }

        public IEnumerable<DetailsTeamInputModel> Teams { get; set; } 
            = new HashSet<DetailsTeamInputModel>();
    }
}
