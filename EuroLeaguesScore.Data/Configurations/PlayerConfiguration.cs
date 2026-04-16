namespace EuroLeaguesScore.Data.Configurations
{
    using Data.Models;
    using EuroLeaguesScore.Data.Models.Enums;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        private static readonly Player[] Players =
        {
            // Manchester City (TeamId = 1)
            new Player { Id = 1, Name = "Erling Haaland", Age = 24, Position = Position.Forward, Goals = 0, Assists = 0, TeamId = 1 },
            new Player { Id = 2, Name = "Ryan Cherki", Age = 21, Position = Position.Midfielder, Goals = 0, Assists = 0, TeamId = 1 },
            new Player { Id = 3, Name = "Phil Foden", Age = 25, Position = Position.Midfielder, Goals = 0, Assists = 0, TeamId = 1 },

            // Arsenal (TeamId = 2)
            new Player { Id = 4, Name = "Bukayo Saka", Age = 24, Position = Position.Forward, Goals = 0, Assists = 0, TeamId = 2 },
            new Player { Id = 5, Name = "Martin Odegaard", Age = 27, Position = Position.Midfielder, Goals = 0, Assists = 0, TeamId = 2 },
            new Player { Id = 6, Name = "Gabriel Jesus", Age = 29, Position = Position.Forward, Goals = 0, Assists = 0, TeamId = 2 },

            // Liverpool (TeamId = 3)
            new Player { Id = 7, Name = "Mohamed Salah", Age = 33, Position = Position.Forward, Goals = 0, Assists = 0, TeamId = 3 },
            new Player { Id = 8, Name = "Hugo Ekitike", Age = 23, Position = Position.Forward, Goals = 0, Assists = 0, TeamId = 3 },
            new Player { Id = 9, Name = "Virgil van Dijk", Age = 33, Position = Position.Defender, Goals = 0, Assists = 0, TeamId = 3 },

            // Real Madrid (TeamId = 6)
            new Player { Id = 10, Name = "Jude Bellingham", Age = 23, Position = Position.Midfielder, Goals = 0, Assists = 0, TeamId = 6 },
            new Player { Id = 11, Name = "Vinicius Jr", Age = 25, Position = Position.Forward, Goals = 0, Assists = 0, TeamId = 6},
            new Player { Id = 12, Name = "Rodrygo", Age = 25, Position = Position.Forward, Goals = 0, Assists = 0, TeamId = 6 },

            // Barcelona (TeamId = 7)
            new Player { Id = 13, Name = "Robert Lewandowski", Age = 37, Position = Position.Forward, Goals = 0, Assists = 0, TeamId = 7, },
            new Player { Id = 14, Name = "Pedri", Age = 23, Position = Position.Midfielder, Goals = 0, Assists = 0, TeamId = 7 },
            new Player { Id = 15, Name = "Gavi", Age = 21, Position = Position.Midfielder, Goals = 0, Assists = 0, TeamId = 7 },

            // Bayern Munich (TeamId = 11)
            new Player { Id = 16, Name = "Harry Kane", Age = 32, Position = Position.Forward, Goals = 0, Assists = 0, TeamId = 11 },
            new Player { Id = 17, Name = "Jamal Musiala", Age = 23, Position = Position.Midfielder, Goals = 0, Assists = 0, TeamId = 11 },
            new Player { Id = 18, Name = "Serge Gnabry", Age = 30, Position = Position.Forward, Goals = 0, Assists = 0, TeamId = 11 },

            // Borussia Dortmund (TeamId = 12)
            new Player { Id = 19, Name = "Marco Reus", Age = 34, Position = Position.Midfielder, Goals = 0, Assists = 0, TeamId = 12 },
            new Player { Id = 20, Name = "Karim Adeyemi", Age = 24, Position = Position.Forward, Goals = 0, Assists = 0, TeamId = 12 },
            new Player { Id = 21, Name = "Julian Brandt", Age = 29, Position = Position.Midfielder, Goals = 0, Assists = 0, TeamId = 12 },

            // Juventus (TeamId = 16)
            new Player { Id = 22, Name = "Dusan Vlahovic", Age = 26, Position = Position.Forward, Goals = 0, Assists = 0, TeamId = 16 },
            new Player { Id = 23, Name = "Kenan Yildiz", Age = 21, Position = Position.Forward, Goals = 0, Assists = 0, TeamId = 16 },
            new Player { Id = 24, Name = "Jonathan David", Age = 26, Position = Position.Forward, Goals = 0, Assists = 0, TeamId = 16 },

            // Inter Milan (TeamId = 18)
            new Player { Id = 25, Name = "Lautaro Martinez", Age = 28, Position = Position.Forward, Goals = 0, Assists = 0, TeamId = 18 },
            new Player { Id = 26, Name = "Nicolo Barella", Age = 29, Position = Position.Midfielder, Goals = 0, Assists = 0, TeamId = 18 },
            new Player { Id = 27, Name = "Marcus Thuram", Age = 28, Position = Position.Forward, Goals = 0, Assists = 0, TeamId = 18 },

            // PSG (TeamId = 21)
            new Player { Id = 28, Name = "Desire Doue", Age = 21, Position = Position.Forward, Goals = 0, Assists = 0, TeamId = 21 },
            new Player { Id = 29, Name = "Ousmane Dembele", Age = 27, Position = Position.Forward, Goals = 0, Assists = 0, TeamId = 21 },
            new Player { Id = 30, Name = "Vitinha", Age = 24, Position = Position.Midfielder, Goals = 0, Assists = 0, TeamId = 21 },
        };

        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.HasData(Players);
        }
    }
}
