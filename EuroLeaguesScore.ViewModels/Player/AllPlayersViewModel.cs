namespace EuroLeaguesScore.ViewModels.Player
{
    public class AllPlayersViewModel : DetailsPlayerViewModel
    {
        public int LeagueId { get; set; }

        public string LeagueName { get; set; } = null!;

        public int TeamId { get; set; }
    }
}
