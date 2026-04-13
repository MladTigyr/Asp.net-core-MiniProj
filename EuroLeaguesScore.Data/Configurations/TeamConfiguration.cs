namespace EuroLeaguesScore.Data.Configurations
{
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        private static readonly Team[] Teams =
        {
            // 🇬🇧 Premier League (LeagueId = 1)
            new Team { Id = 1, Name = "Manchester City", Country = "England", City = "Manchester", LeagueId = 1, Wins = 0, Losses = 0, Draws = 0 },
            new Team { Id = 2, Name = "Arsenal", Country = "England", City = "London", LeagueId = 1, Wins = 0, Losses = 0, Draws = 0 },
            new Team { Id = 3, Name = "Liverpool", Country = "England", City = "Liverpool", LeagueId = 1, Wins = 0, Losses = 0, Draws = 0 },
            new Team { Id = 4, Name = "Manchester United", Country = "England", City = "Manchester", LeagueId = 1, Wins = 0, Losses = 0, Draws = 0 },
            new Team { Id = 5, Name = "Chelsea", Country = "England", City = "London", LeagueId = 1, Wins = 0, Losses = 0, Draws = 0 },
            // 🇪🇸 La Liga (LeagueId = 2)
            new Team { Id = 6, Name = "Real Madrid", Country = "Spain", City = "Madrid", LeagueId = 2, Wins = 0, Losses = 0, Draws = 0 },
            new Team { Id = 7, Name = "Barcelona", Country = "Spain", City = "Barcelona", LeagueId = 2, Wins = 0, Losses = 0, Draws = 0 },
            new Team { Id = 8, Name = "Atletico Madrid", Country = "Spain", City = "Madrid", LeagueId = 2, Wins = 0, Losses = 0, Draws = 0 },
            new Team { Id = 9, Name = "Sevilla", Country = "Spain", City = "Seville", LeagueId = 2, Wins = 0, Losses = 0, Draws = 0 },
            new Team { Id = 10, Name = "Real Sociedad", Country = "Spain", City = "San Sebastian", LeagueId = 2, Wins = 0, Losses = 0, Draws = 0 },

            // 🇩🇪 Bundesliga (LeagueId = 3)
            new Team { Id = 11, Name = "Bayern Munich", Country = "Germany", City = "Munich", LeagueId = 3, Wins = 0, Losses = 0, Draws = 0 },
            new Team { Id = 12, Name = "Borussia Dortmund", Country = "Germany", City = "Dortmund", LeagueId = 3, Wins = 0, Losses = 0, Draws = 0 },
            new Team { Id = 13, Name = "RB Leipzig", Country = "Germany", City = "Leipzig", LeagueId = 3, Wins = 0, Losses = 0, Draws = 0 },
            new Team { Id = 14, Name = "Bayer Leverkusen", Country = "Germany", City = "Leverkusen", LeagueId = 3, Wins = 0, Losses = 0, Draws = 0 },
            new Team { Id = 15, Name = "Wolfsburg", Country = "Germany", City = "Wolfsburg", LeagueId = 3, Wins = 0, Losses = 0, Draws = 0 },

            // 🇮🇹 Serie A (LeagueId = 4)
            new Team { Id = 16, Name = "Juventus", Country = "Italy", City = "Turin", LeagueId = 4, Wins = 0, Losses = 0, Draws = 0 },
            new Team { Id = 17, Name = "AC Milan", Country = "Italy", City = "Milan", LeagueId = 4, Wins = 0, Losses = 0, Draws = 0 },
            new Team { Id = 18, Name = "Inter Milan", Country = "Italy", City = "Milan", LeagueId = 4, Wins = 0, Losses = 0, Draws = 0 },
            new Team { Id = 19, Name = "Napoli", Country = "Italy", City = "Naples", LeagueId = 4, Wins = 0, Losses = 0, Draws = 0 },
            new Team { Id = 20, Name = "Roma", Country = "Italy", City = "Rome", LeagueId = 4, Wins = 0, Losses = 0, Draws = 0 },

            // 🇫🇷 Ligue 1 (LeagueId = 5)
            new Team { Id = 21, Name = "Paris Saint-Germain", Country = "France", City = "Paris", LeagueId = 5, Wins = 0, Losses = 0, Draws = 0 },
            new Team { Id = 22, Name = "Marseille", Country = "France", City = "Marseille", LeagueId = 5, Wins = 0, Losses = 0, Draws = 0 },
            new Team { Id = 23, Name = "Lyon", Country = "France", City = "Lyon", LeagueId = 5, Wins = 0, Losses = 0, Draws = 0 },
            new Team { Id = 24, Name = "Monaco", Country = "Monaco", City = "Monaco", LeagueId = 5, Wins = 0, Losses = 0, Draws = 0 },
            new Team { Id = 25, Name = "Lille", Country = "France", City = "Lille", LeagueId = 5, Wins = 0, Losses = 0, Draws = 0},
        };

        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasData(Teams);
        }
    }
}
