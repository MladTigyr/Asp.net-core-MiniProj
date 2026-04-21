namespace EuroLeaguesScore.ViewModels.Shared
{
    public class SearchBlockViewModel
    {
        public string? SearchTerm { get; set; }

        public string Controller { get; set; } = null!;

        public string Action { get; set; } = null!;

        public string Placeholder { get; set; } = "Search...";
    }
}
