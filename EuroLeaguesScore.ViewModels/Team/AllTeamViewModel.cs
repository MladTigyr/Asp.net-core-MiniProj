namespace EuroLeaguesScore.ViewModels.Team
{
    using static GCommon.EntityValidations;
    public class AllTeamViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string City { get; set; } = null!;

        public string Country { get; set; } = null!;

        public string LeagueName { get; set; } = null!;

        public int Wins { get; set; }

        public int Losses { get; set; }

        public int Draws { get; set; }

        public string? ManagerName { get; set; }

        public bool IsFavourite { get; set; }
    }
}
