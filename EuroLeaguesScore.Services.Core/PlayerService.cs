
namespace EuroLeaguesScore.Services.Core
{
    using System.Threading.Tasks;

    using EuroLeaguesScore.Data.Models;
    using EuroLeaguesScore.Data.Repository.Contracts;
    using EuroLeaguesScore.Services.Core.Contracts;
    using EuroLeaguesScore.ViewModels.Player;

    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository playerRepository;
        private readonly IUserPlayerRepository userPlayerRepository;
        private readonly ITeamRepository teamRepository;

        public PlayerService(IPlayerRepository playerRepository, IUserPlayerRepository userPlayerRepository, ITeamRepository teamRepository)
        {
            this.playerRepository = playerRepository;
            this.userPlayerRepository = userPlayerRepository;
            this.teamRepository = teamRepository;
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

            await playerRepository.AddAsync(player);
        }

        public async Task<bool> DeletePlayerFromDbAsync(int playerId)
        {
            Player? player = await playerRepository
                .GetPlayerWithHisTeamIfExistsAsync(playerId);

            if (player == null)
            {
                return false;
            }

            bool isDeleted = await playerRepository
                .DeleteAsync(player);

            return isDeleted;
        }

        public async Task<bool> EditPlayerToDbAsync(int playerId, EditPlayerInputModel model)
        {
            Player? player = await playerRepository
                .GetPlayerWithHisTeamIfExistsAsync(playerId);

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

            await playerRepository
                .SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<AllPlayersViewModel>> GetAllPlayersOrderedByLeagueThenByTeamNameThenByNameAsync(string? userId)
        {
            IEnumerable<Player> entityPlayers = await playerRepository
                .GetAllPlayersOrderedByLeagueThenByTeamNameThenByNameAsync();

            IEnumerable<AllPlayersViewModel> models = entityPlayers
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
                .ToArray();

            if (userId != null)
            {
                IEnumerable<UserPlayer> players = await userPlayerRepository
                    .GetAllFavPlayersWithUserIdParamAsync(userId);

                if (players.Any())
                {
                    foreach (AllPlayersViewModel model in models)
                    {
                        if (players.Any(up => up.UserId.ToString() == userId && up.PlayerId == model.Id))
                        {
                            model.IsFavourite = true;
                        }
                    }
                }
            }

            return models;
        }

        public async Task<IEnumerable<TeamViewModel>> GetAllTeamsAsync()
        {
            IEnumerable<Team> teams = await teamRepository
                .GetAllAsync();

            return teams
                .Select(t => new TeamViewModel
                {
                    Id = t.Id,
                    TeamName = t.Name,
                });
        }

        public async Task<DeletePlayerViewModel?> GetDeletePlayerViewModelAsync(int playerId)
        {
            Player? player = await playerRepository
                .GetPlayerWithHisTeamIfExistsAsync(playerId);

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
            Player? player = await playerRepository
                .GetPlayerWithHisTeamIfExistsAsync(playerId);

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
                IEnumerable<UserPlayer> userPlayers = await userPlayerRepository
                    .GetAllFavPlayersWithUserIdParamAsync(userId);

                if (userPlayers.Any(up => up.UserId.ToString() == userId && up.PlayerId == detailsPlayerViewModel.Id))
                {
                    detailsPlayerViewModel.IsFavourite = true;
                }
            }

            return detailsPlayerViewModel;
        }

        public async Task<EditPlayerInputModel?> GetEditPlayerInputModelAsync(int playerId)
        {
            Player? player = await playerRepository
                .GetPlayerWithHisTeamIfExistsAsync(playerId);

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
    }
}
