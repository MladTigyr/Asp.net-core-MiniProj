namespace EuroLeaguesScore.ViewModels.Player
{
    public class DetailsPlayerViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int Age { get; set; }

        public string Position { get; set; } = null!;

        public int Goals { get; set; }

        public int Assists { get; set; }

        public string TeamName { get; set; } = null!;
    }
}
