using EuroLeaguesScore.ViewModels.Player;

public class PlayerPaginationBlockViewModel
{
    public IEnumerable<AllPlayersViewModel> Players { get; set; } = new List<AllPlayersViewModel>();

    public int CurrentPage { get; set; }

    public int ElementsPerPage { get; set; }

    public int TotalCount { get; set; }

    public int Pages { get; set; }
}