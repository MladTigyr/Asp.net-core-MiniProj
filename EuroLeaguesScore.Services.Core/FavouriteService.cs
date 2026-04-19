namespace EuroLeaguesScore.Services.Core
{
    using EuroLeaguesScore.Data;
    using EuroLeaguesScore.Data.Models;
    using EuroLeaguesScore.Data.Repository.Contracts;
    using EuroLeaguesScore.Services.Core.Contracts;
    using EuroLeaguesScore.ViewModels.Favourite;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class FavouriteService : IFavouriteService
    {
        private readonly IFavouriteRepository favouriteRepository;
        private readonly IUserPlayerRepository userPlayerRepository;

        public FavouriteService(IFavouriteRepository favouriteRepository, IUserPlayerRepository userPlayerRepository)
        {
            this.favouriteRepository = favouriteRepository;
            this.userPlayerRepository = userPlayerRepository;
        }

        public async Task<IEnumerable<FavouritePlayerViewModel>?> GetAllFavPlayersIfExistAsync(string userId)
        {
            IEnumerable<UserPlayer> entityUserPlayers = await userPlayerRepository
                .GetAllFavPlayersOrderedByPlayerNameWithUserIdParamAsync(userId);

            IEnumerable<FavouritePlayerViewModel> models = entityUserPlayers
                .Select(u => new FavouritePlayerViewModel
                {
                    Id = u.Player.Id,
                    Name = u.Player.Name,
                    Age = u.Player.Age
                });

            return models;
        }

        public async Task<IEnumerable<FavouriteTeamViewModel>?> GetAllFavTeamsIfExistAsync(string userId)
        {
            IEnumerable<UserTeam> entityUserTeams = await favouriteRepository
                .GetAllTeamsByUserIdOrderedByLeagueNameThenByTeamNameAsync(userId);

            IEnumerable<FavouriteTeamViewModel> models = entityUserTeams
                .Select(u => new FavouriteTeamViewModel
                {
                    Id = u.TeamId,
                    Name = u.Team.Name,
                    Country = u.Team.Country,
                    LeagueName = u.Team.League.Name,
                });

            return models;
        }

        public async Task TogglePlayerFavouriteAsync(string userId, int playerId)
        {
            UserPlayer? player = await userPlayerRepository
                .FirstOrDefaultAsync(up => up.UserId == userId && up.PlayerId.ToString() == playerId.ToString());

            if (player != null)
            {
                await userPlayerRepository
                    .OnlyDeleteAsync(player);
            }
            else
            {
                UserPlayer userPlayer = new UserPlayer
                {
                    UserId = userId,
                    PlayerId = playerId
                };

                await userPlayerRepository.AddAsync(userPlayer);
            }
        }

        public async Task ToggleTeamFavouriteAsync(string userId, int teamId)
        {
            UserTeam? favourtieTeam = await favouriteRepository
                .FirstOrDefaultAsync(ut => ut.UserId == userId && ut.TeamId.ToString() == teamId.ToString());

            if (favourtieTeam != null)
            {
                await favouriteRepository
                    .OnlyDeleteAsync(favourtieTeam);
            }
            else
            {
                UserTeam userTeam = new UserTeam
                {
                    UserId = userId,
                    TeamId = teamId
                };

                await favouriteRepository.AddAsync(userTeam);
            }
        }
    }
}
