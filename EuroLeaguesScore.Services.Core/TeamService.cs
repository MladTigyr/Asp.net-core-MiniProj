namespace EuroLeaguesScore.Services.Core
{
    using System.Threading.Tasks;

    using EuroLeaguesScore.Data;
    using EuroLeaguesScore.Services.Core.Contracts;
    using EuroLeaguesScore.ViewModels.Team;
    using EuroLeaguesScore.Data.Models;
    using EuroLeaguesScore.ViewModels.Player;
    using EuroLeaguesScore.Data.Repository.Contracts;

    public class TeamService : ITeamService
    {
        private readonly ITeamRepository teamRepository;
        private readonly IFavouriteRepository favouriteRepository;
        private readonly IManagerRepository managerRepository;
        private readonly ILeagueRepository leagueRepository;
        private readonly IPlayerRepository playerRepository;

        public TeamService(EuroLeaguesScoreDbContext dbContext, ITeamRepository teamRepository, IFavouriteRepository favouriteRepository, IManagerRepository managerRepository, ILeagueRepository leagueRepository, IPlayerRepository playerRepository)
        {
            this.teamRepository = teamRepository;
            this.favouriteRepository = favouriteRepository;
            this.managerRepository = managerRepository;
            this.leagueRepository = leagueRepository;
            this.playerRepository = playerRepository;
        }

        public async Task AddTeamToDbAsync(AddTeamInputModel model)
        {
            Team team = new Team
            {
                Name = model.Name,
                Country = model.Country,
                City = model.City,
                LeagueId = model.LeagueId,
                Wins = model.Wins,
                Losses = model.Losses,
                Draws = model.Draws,
                ManagerId = model.ManagerId,
            };

            await teamRepository.AddAsync(team);
        }

        public async Task<IEnumerable<AllTeamViewModel>> AllTeamsOrderedByLeagueNameThenByNameAsync(string? userId)
        {
            IEnumerable<Team> entityTeams = await teamRepository
                .AllTeamsOrderedByLeagueNameThenByNameAsync();

            IEnumerable<AllTeamViewModel> models = entityTeams
                .Select(t => new AllTeamViewModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    City = t.City,
                    Country = t.Country,
                    LeagueName = t.League.Name,
                    Wins = t.Wins,
                    Losses = t.Losses,
                    Draws = t.Draws,
                    ManagerName = t.Manager != null ? t.Manager.Name : "No manager"
                })
                .ToArray();

            if (userId != null)
            {
                IEnumerable<UserTeam> teams = await favouriteRepository
                    .GetAllTeamsByUserIdOrderedByLeagueNameThenByTeamNameAsync(userId);

                foreach (var model in models)
                {
                    if (teams.Any(t => t.TeamId == model.Id && t.UserId == userId))
                    {
                        model.IsFavourite = true;
                    }
                }
            }

            return models;
        }

        public async Task<bool> DeleteTeamFromDbAsync(int teamId, DeleteTeamViewModel model)
        {
            Team? team = await teamRepository
                .GetTeamByIdTrackingWithoutIncludingAsync(teamId);

            if (team == null)
            {
                return false;
            }

            bool isDeleted = await teamRepository
                .DeleteAsync(team);

            return isDeleted;
        }

        public async Task<bool> EditTeamToDbAsync(int teamId, EditTeamInputModel model)
        {
            Team? team = await teamRepository
                .GetTeamByIdWithItsIncludesTrackingAsync(teamId);

            if (team == null)
            {
                return false;
            }

            team.Name = model.Name;
            team.City = model.City;
            team.Country = model.Country;
            team.Wins = model.Wins;
            team.Draws = model.Draws;
            team.Losses = model.Losses;
            team.LeagueId = model.LeagueId;
            team.ManagerId = model.ManagerId;

            await teamRepository.SaveChangesAsync();

            return true;
        }

        public async Task<DeleteTeamViewModel?> GetDeleteTeamViewModelAsync(int teamId)
        {
            Team? team = await teamRepository
                .GetTeamByIdTrackingWithoutIncludingAsync(teamId);

            if (team == null) 
            { 
                return null;
            }

            DeleteTeamViewModel model = new DeleteTeamViewModel
            {
                Id = team.Id,
            };

            return model;
        }

        public async Task<IEnumerable<DetailsPlayerViewModel>> GetDetailsPlayersOrderedByNameWithTeamIdAsync(int teamId)
        {
            IEnumerable<Player> entityPlayers = await playerRepository
                .GetDetailsPlayersOrderedByNameWithTeamIdAsync(teamId);

            IEnumerable<DetailsPlayerViewModel> models = entityPlayers
                .Select(p => new DetailsPlayerViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Age = p.Age,
                    Position = p.Position.ToString(),
                    Goals = p.Goals,
                    Assists = p.Assists
                });

            return models;
        }

        public async Task<DetailsTeamInputModel?> GetDetailsTeamViewModelAsync(int teamId, string userId)
        {
            Team? team = await teamRepository
                .GetTeamByIdWithItsIncludesAsync(teamId);

            if (team == null)
            {
                return null;
            }

            DetailsTeamInputModel model = new DetailsTeamInputModel
            {
                Id = team.Id,
                Name = team.Name,
                Country = team.Country,
                City = team.City,
                LeagueName = team.League.Name,
                Wins = team.Wins,
                Losses = team.Losses,
                Draws = team.Draws,
                ManagerName = team.Manager != null ? team.Manager.Name : "No manager",
                Players = await GetDetailsPlayersOrderedByNameWithTeamIdAsync(team.Id)
            };

            if (userId != null)
            {
                IEnumerable<UserTeam> teams = await favouriteRepository
                    .GetAllTeamsByUserIdOrderedByLeagueNameThenByTeamNameAsync(userId);

                if(teams.Any(t => t.UserId == userId && t.TeamId == model.Id))
                {
                    model.IsFavourite = true;
                }
            }

            return model;
        }

        public async Task<EditTeamInputModel?> GetEditTeamViewModelAsync(int teamId)
        {
            Team? team = await teamRepository
                .GetTeamByIdWithItsIncludesTrackingAsync(teamId);

            if (team == null)
            {
                return null;
            }

            EditTeamInputModel model = new EditTeamInputModel
            {
                Id = teamId,
                Name = team.Name,
                Country = team.Country,
                City = team.City,
                LeagueId = team.LeagueId,
                ManagerId = team.ManagerId,
                Wins = team.Wins,
                Losses = team.Losses,
                Draws = team.Draws,
                Managers = await GetManagersWhichHaveNotTeamToManageOrCurrentTeamManagerAsync(teamId),
                Leagues = await GetLeaguesOrderedByLeagueNameAsync()
            };

            return model;
        }

        public async Task<IEnumerable<LeagueViewModel>> GetLeaguesOrderedByLeagueNameAsync()
        {
            IEnumerable<League> entityLeagues = await leagueRepository
                .GetLeaguesOrderedByLeagueNameAsync();

            IEnumerable<LeagueViewModel> models = entityLeagues
                   .Select(l => new LeagueViewModel
                   {
                       Id = l.Id,
                       Name = l.Name,
                   });

            return models;
        }

        public async Task<IEnumerable<ManagerViewModel>> GetManagersWhichHaveNotTeamToManageAsync()
        {
            IEnumerable<Manager> entityModels = await managerRepository
                .GetManagersWhichHaveNotTeamToManageAsync();

            IEnumerable<ManagerViewModel> models = entityModels
                .Select(m => new ManagerViewModel
                {
                    Id = m.Id,
                    Name = m.Name,
                    Age = m.Age,
                });

            return models;
        }

        public async Task<IEnumerable<ManagerViewModel>> GetManagersWhichHaveNotTeamToManageOrCurrentTeamManagerAsync(int teamId)
        {
            IEnumerable<Manager> entityModels = await managerRepository
                            .GetManagersWhichHaveNotTeamToManageOrCurrentTeamManagerAsync(teamId);

            IEnumerable<ManagerViewModel> models = entityModels
                .Select(m => new ManagerViewModel
                {
                    Id = m.Id,
                    Name = m.Name,
                    Age = m.Age,
                });

            return models;
        }
    }
}
