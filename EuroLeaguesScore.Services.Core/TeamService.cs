namespace EuroLeaguesScore.Services.Core
{
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    using EuroLeaguesScore.Data;
    using EuroLeaguesScore.Services.Core.Contracts;
    using EuroLeaguesScore.ViewModels.Team;
    using EuroLeaguesScore.Data.Models;
    using EuroLeaguesScore.ViewModels.Player;

    public class TeamService : ITeamService
    {
        private readonly EuroLeaguesScoreDbContext dbContext;

        public TeamService(EuroLeaguesScoreDbContext dbContext)
        {
            this.dbContext = dbContext;
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

            await dbContext.Teams.AddAsync(team);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<AllTeamViewModel>> AllTeamsOrderedByLeagueNameThenByNameAsync(string? userId)
        {
            IEnumerable<AllTeamViewModel> models = await dbContext.Teams
                .AsNoTracking()
                .Include(t => t.League)
                .Include(t => t.Manager)
                .OrderBy(t => t.League.Name)
                .ThenBy(t => t.Name)
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
                .ToArrayAsync();

            if (userId != null)
            {
                IEnumerable<UserTeam> teams = await dbContext.UserTeams
                .AsNoTracking()
                .ToArrayAsync();

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
            Team? team = await GetTeamByIdTrackingWithoutIncludingAsync(teamId);

            if (team == null)
            {
                return false;
            }

            dbContext.Teams.Remove(team);
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> EditTeamToDbAsync(int teamId, EditTeamInputModel model)
        {
            Team? team = await GetTeamByIdTrackingAsync(teamId);

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

            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<DeleteTeamViewModel?> GetDeleteTeamViewModelAsync(int teamId)
        {
            Team? team = await GetTeamByIdTrackingWithoutIncludingAsync(teamId);

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
            return await dbContext.Players
                .AsNoTracking()
                .Where(p => p.TeamId == teamId)
                .OrderBy(p => p.Name)
                .Select(p => new DetailsPlayerViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Age = p.Age,
                    Position = p.Position.ToString(),
                    Goals = p.Goals,
                    Assists = p.Assists
                })
                .ToArrayAsync();
        }

        public async Task<DetailsTeamInputModel?> GetDetailsTeamViewModelAsync(int teamId, string userId)
        {
            Team? team = await GetTeamByIdAsync(teamId);

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
                IEnumerable<UserTeam> teams = await dbContext.UserTeams
                    .AsNoTracking()
                    .ToArrayAsync();

                if(teams.Any(t => t.UserId == userId && t.TeamId == model.Id))
                {
                    model.IsFavourite = true;
                }
            }

            return model;
        }

        public async Task<EditTeamInputModel?> GetEditTeamViewModelAsync(int teamId)
        {
            Team? team = await GetTeamByIdTrackingAsync(teamId);

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

        public async Task<IEnumerable<League>> GetLeaguesOrderedByLeagueNameAsync()
        {
            return await dbContext.Leagues
               .AsNoTracking()
               .OrderBy(l => l.Name)
               .Select(l => new League
               {
                   Id = l.Id,
                   Name = l.Name,
               })
               .ToArrayAsync();
        }

        public async Task<IEnumerable<Manager>> GetManagersWhichHaveNotTeamToManageAsync()
        {
            return await dbContext.Managers
                .AsNoTracking()
                .Where(m => !dbContext.Teams.Any(t => t.ManagerId == m.Id)) // im doing this because i want to show only managers that are not currently managing a team. When i first seeded the db i made sure that all managers are not assigned to a team (i made their teamId null), so this will work fine.
                .OrderBy(m => m.Name)
                .Select(m => new Manager
                {
                    Id = m.Id,
                    Name = m.Name,
                    Age = m.Age
                })
                .ToArrayAsync();
        }

        public async Task<IEnumerable<Manager>> GetManagersWhichHaveNotTeamToManageOrCurrentTeamManagerAsync(int teamId)
        {
           return await dbContext.Managers
                    .AsNoTracking()
                    .Where(m => !dbContext.Teams.Any(t => t.ManagerId == m.Id && t.Id != teamId))
                    .OrderBy(m => m.Name)
                    .ToArrayAsync();
        }

        public async Task<Team?> GetTeamByIdAsync(int teamId)
        {
            Team? team = await dbContext.Teams
                .AsNoTracking()
                .Include(t => t.League)
                .Include(t => t.Players)
                .Include(t => t.Manager)
                .FirstOrDefaultAsync(t => t.Id == teamId);

            return team;
        }

        public async Task<Team?> GetTeamByIdTrackingAsync(int teamId)
        {
            Team? team = await dbContext.Teams
                .Include(t => t.League)
                .Include(t => t.Players)
                .Include(t => t.Manager)
                .FirstOrDefaultAsync(t => t.Id == teamId);

            return team;
        }

        public async Task<Team?> GetTeamByIdTrackingWithoutIncludingAsync(int teamId)
        {
            Team? team = await dbContext.Teams
                .FirstOrDefaultAsync(t => t.Id == teamId);

            return team;
        }
    }
}
