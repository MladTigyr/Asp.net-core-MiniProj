
namespace EuroLeaguesScore.Services.Core
{
    using EuroLeaguesScore.Data;
    using EuroLeaguesScore.Data.Models;
    using EuroLeaguesScore.Services.Core.Contracts;
    using EuroLeaguesScore.ViewModels.Player;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    public class PlayerService : IPlayerService
    {
        private readonly EuroLeaguesScoreDbContext dbContext;

        public PlayerService(EuroLeaguesScoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<AllPlayersViewModel>> GetAllPlayersOrderedByLeagueThenByTeamNameThenByNameAsync()
        {
            return await dbContext.Players
                .AsNoTracking()
                .Include(p => p.Team)
                .ThenInclude(p => p.League)
                .OrderBy(p => p.Team.League.Name)
                .ThenBy(p => p.Team.Name)
                .ThenBy(p => p.Name)
                .Select(p => new AllPlayersViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Age = p.Age,
                    Position = p.Position.ToString(),
                    Goals = p.Goals,
                    Assists = p.Assists,
                    TeamName = p.Team.Name,
                    LeagueName = p.Team.League.Name
                })
                .ToArrayAsync();
        }

        public async Task<IEnumerable<Team>> GetAllTeamsAsync()
        {
            return await dbContext.Teams
                .Select(t => new Team
                {
                    Id = t.Id,
                    Name = t.Name,
                })
                .ToArrayAsync();
        }

        public async Task<DetailsPlayerViewModel?> GetDetailsPlayerViewModelAsync(int playerId)
        {
            Player? player = await GetPlayerWithHisTeamNameIfExistsAsync(playerId);

            if (player == null)
            {
                return null;
            }

            DetailsPlayerViewModel detailsPlayerViewModel = new DetailsPlayerViewModel
            {
                Id = player.Id,
                Name = player.Name,
                Age = player.Age,
                Position = player.Position.ToString(),
                Goals = player.Goals,
                Assists = player.Assists,
                TeamName = player.Team.Name,
            };

            return detailsPlayerViewModel;
        }

        public async Task<Player?> GetPlayerWithHisTeamNameIfExistsAsync(int playerId)
        {
            return await dbContext.Players
                .Include(p => p.Team)
                .FirstOrDefaultAsync(p => p.Id == playerId);
        }
    }
}
