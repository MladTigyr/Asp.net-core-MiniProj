namespace EuroLeaguesScore.Services.Core
{
    using EuroLeaguesScore.Data;
    using EuroLeaguesScore.Data.Models;
    using EuroLeaguesScore.Services.Core.Contracts;
    using EuroLeaguesScore.ViewModels.Favourite;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class FavouriteService : IFavouriteService
    {
        private readonly EuroLeaguesScoreDbContext dbContext;

        public FavouriteService(EuroLeaguesScoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<FavouritePlayerViewModel>?> GetAllFavPlayersIfExistAsync(string userId)
        {
            return await dbContext.UserPlayers
                .AsNoTracking()
                .Where(u => u.UserId == userId)
                .OrderBy(u => u.Player.Name)
                .Select(u => new FavouritePlayerViewModel
                {
                    Id = u.Player.Id,
                    Name = u.Player.Name,
                    Age = u.Player.Age,
                })
                .ToArrayAsync();
        }

        public async Task<IEnumerable<FavouriteTeamViewModel>?> GetAllFavTeamsIfExistAsync(string userId)
        {
            return await dbContext.UserTeams
                .AsNoTracking()
                .Where(u => u.UserId == userId)
                .OrderBy(t => t.Team.League.Name)
                .ThenBy(t => t.Team.Name)
                .Select(u => new FavouriteTeamViewModel
                {
                    Id = u.TeamId,
                    Name = u.Team.Name,
                    Country = u.Team.Country,
                    LeagueName = u.Team.League.Name,
                })
                .ToArrayAsync();
        }

        public async Task TogglePlayerFavouriteAsync(string userId, int playerId)
        {
            UserPlayer? player = await dbContext.UserPlayers
                .FirstOrDefaultAsync(up => up.UserId == userId && up.PlayerId == playerId);

            if (player != null)
            {
                dbContext.UserPlayers.Remove(player);
            }
            else
            {
                UserPlayer userPlayer = new UserPlayer
                {
                    UserId = userId,
                    PlayerId = playerId
                };

                await dbContext.UserPlayers.AddAsync(userPlayer);
            }

            await dbContext.SaveChangesAsync();
        }

        public async Task ToggleTeamFavouriteAsync(string userId, int teamId)
        {
            UserTeam? favourtieTeam = await dbContext.UserTeams
                .FirstOrDefaultAsync(u => u.UserId == userId && u.TeamId == teamId);

            if (favourtieTeam != null)
            {
                dbContext.UserTeams.Remove(favourtieTeam);
            }
            else
            {
                UserTeam userTeam = new UserTeam
                {
                    UserId = userId,
                    TeamId = teamId
                };

                await dbContext.AddAsync(userTeam);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
