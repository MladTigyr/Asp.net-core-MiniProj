
namespace EuroLeaguesScore.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;

    using EuroLeaguesScore.Data.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ManagerConfiguration : IEntityTypeConfiguration<Manager>
    {
        private static readonly Manager[] Managers =
        {
            new Manager { Id = 1, Name = "Pep Guardiola", Age = 55 },
            new Manager { Id = 2, Name = "Mikel Arteta", Age = 44 },
            new Manager { Id = 3, Name = "Arne Slot", Age = 47 },
            new Manager { Id = 4, Name = "Michael Carrick", Age = 44 },
            new Manager { Id = 5, Name = "Liam Rosenior", Age = 41 },

            new Manager { Id = 6, Name = "Álvaro Arbeloa", Age = 43 },
            new Manager { Id = 7, Name = "Hansi Flick", Age = 61 },
            new Manager { Id = 8, Name = "Diego Simeone", Age = 54 },
            new Manager { Id = 9, Name = "Quique Sanchez Flores", Age = 59 },
            new Manager { Id = 10, Name = "Imanol Alguacil", Age = 52 },

            new Manager { Id = 11, Name = "Vincent Kompany", Age = 40 },
            new Manager { Id = 12, Name = "Niko Kovač", Age = 54 },
            new Manager { Id = 13, Name = "Marco Rose", Age = 49 },
            new Manager { Id = 14, Name = "Kasper Hjulmand", Age = 54 },
            new Manager { Id = 15, Name = "Dieter Hecking", Age = 61 },

            new Manager { Id = 16, Name = "Luciano Spalletti", Age = 67 },
            new Manager { Id = 17, Name = "Massimiliano Allegri", Age = 58 },
            new Manager { Id = 18, Name = "Guillermo Hoyos", Age = 62 },
            new Manager { Id = 19, Name = "Antonio Conte", Age = 56 },
            new Manager { Id = 20, Name = "Gian Piero Gasperini", Age = 68 },

            new Manager { Id = 21, Name = "Luis Enrique", Age = 54 },
            new Manager { Id = 22, Name = "Habib Beye", Age = 48 },
            new Manager { Id = 23, Name = "Paulo Fonseca", Age = 53 },
            new Manager { Id = 24, Name = "Sébastien Pocognoli", Age = 38 },
            new Manager { Id = 25, Name = "Bruno Génésio", Age = 59 }
        };

        public void Configure(EntityTypeBuilder<Manager> entity)
        {
            entity.HasData(Managers);
        }
    }
}
