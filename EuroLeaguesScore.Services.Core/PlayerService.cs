
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

        public async Task AddPlayerToDbAsync(AddPlayerInputModel model)
        {
            Player player = new Player
            {
                Name = model.Name,
                Age = model.Age,
                Position = model.Position,
                Goals = model.Goals,
                Assists = model.Assists,
                TeamId = model.TeamId,
            };

            await dbContext.AddAsync(player);
            await dbContext.SaveChangesAsync();
        }

        public async Task<bool> DeletePlayerFromDbAsync(int playerId)
        {
            Player? player = await GetPlayerWithHisTeamNameIfExistsAsync(playerId);

            if (player == null)
            {
                return false;
            }

            dbContext.Remove(player);
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> EditPlayerToDbAsync(int playerId, EditPlayerInputModel model)
        {
            Player? player = await GetPlayerWithHisTeamNameIfExistsAsync(playerId);

            if (player == null)
            {
                return false;
            }

            player.Name = model.Name;
            player.Age = model.Age;
            player.Goals = model.Goals;
            player.Assists = model.Assists;
            player.Position = model.Position;
            player.TeamId = model.TeamId;

            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<AllPlayersViewModel>> GetAllPlayersOrderedByLeagueThenByTeamNameThenByNameAsync(string? userId)
        {
            IEnumerable<AllPlayersViewModel> models = await dbContext.Players
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
                    TeamId = p.Team.Id,
                    TeamName = p.Team.Name,
                    LeagueId = p.Team.League.Id,
                    LeagueName = p.Team.League.Name,
                })
                .ToArrayAsync();

            if (userId != null)
            {
                IEnumerable<UserPlayer> players = await dbContext.UserPlayers
                    .Where(up => up.UserId == userId)
                    .ToArrayAsync();

                if (players.Any())
                {
                    foreach (AllPlayersViewModel model in models)
                    {
                        if (players.Any(up => up.UserId == userId && up.PlayerId == model.Id))
                        {
                            model.IsFavourite = true;
                        }
                    }
                }
            }

            return models;
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

        public async Task<DeletePlayerViewModel?> GetDeletePlayerViewModelAsync(int playerId)
        {
            Player? player = await GetPlayerWithHisTeamNameIfExistsAsync(playerId);

            if (player == null)
            {
                return null;
            }

            DeletePlayerViewModel model = new DeletePlayerViewModel
            {
                Id = player.Id,
            };

            return model;
        }

        public async Task<DetailsPlayerViewModel?> GetDetailsPlayerViewModelAsync(int playerId, string userId)
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

            if (userId != null)
            {
                IEnumerable<UserPlayer> userPlayers = await dbContext.UserPlayers
                    .Where(up => up.UserId == userId)
                    .ToArrayAsync();

                if (userPlayers.Any(up => up.UserId == userId && up.PlayerId == detailsPlayerViewModel.Id))
                {
                    detailsPlayerViewModel.IsFavourite = true;
                }
            }

            return detailsPlayerViewModel;
        }

        public async Task<EditPlayerInputModel?> GetEditPlayerInputModelAsync(int playerId)
        {
            Player? player = await GetPlayerWithHisTeamNameIfExistsAsync(playerId);

            if (player == null)
            {
                return null;
            }

            EditPlayerInputModel model = new EditPlayerInputModel
            {
                Id = player.Id,
                Name = player.Name,
                Age = player.Age,
                Goals = player.Goals,
                Assists = player.Assists,
                TeamId = player.Team.Id,
                TeamNames = await GetAllTeamsAsync(),
                Position = player.Position
            };

            return model;
        }

        public async Task<Player?> GetPlayerWithHisTeamNameIfExistsAsync(int playerId)
        {
            return await dbContext.Players
                .Include(p => p.Team)
                .FirstOrDefaultAsync(p => p.Id == playerId);
        }
    }
}
