namespace EuroLeaguesScore.Services.Core
{
    using EuroLeaguesScore.Data;
    using EuroLeaguesScore.Data.Models;
    using EuroLeaguesScore.Services.Core.Contracts;
    using EuroLeaguesScore.ViewModels.League;
    using ViewModels.Team;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class LeagueService : ILeagueService
    {
        private readonly EuroLeaguesScoreDbContext dbContext;

        public LeagueService(EuroLeaguesScoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<AllLeagueViewModel>> GetAllLeagueViewModelsOrderedByLeagueNameAsync()
        {
            IEnumerable<AllLeagueViewModel> models = await dbContext.Leagues
                .AsNoTracking()
                .Include(l => l.Teams)
                .OrderBy(l => l.Name)
                .Select(l => new AllLeagueViewModel
                {
                    Id = l.Id,
                    Name = l.Name,
                    Country = l.Teams.Select(t => t.Country).FirstOrDefault() ?? "Unknown"
                })
                .ToArrayAsync();

            return models;
        }

        public async Task<DetailsLeagueViewModel?> GetDetailsLeagueViewModelAsync(int id)
        {
            League? league = await GetLeagueIfExistsWithIdParamAsync(id);

            if (league == null)
            {
                return null;
            }

            DetailsLeagueViewModel model = new DetailsLeagueViewModel
            {
                Id = league.Id,
                Name = league.Name,
                Country = league.Teams.Select(t => t.Country).FirstOrDefault() ?? "Unknown",
                Teams = league.Teams
                    .Select(lt => new DetailsTeamInputModel
                    {
                        Id = lt.Id,
                        Name = lt.Name,
                        City = lt.City,
                        Wins = lt.Wins,
                        Losses = lt.Losses,
                        Draws = lt.Draws,
                    })
                    .OrderByDescending(t => t.Wins)
                    .ThenByDescending(t => t.Draws)
                    .ThenBy(t => t.Losses),
                TotalPlayers = league.Teams.SelectMany(t => t.Players).Count(),
            };

            return model;
        }

        public async Task<League?> GetLeagueIfExistsWithIdParamAsync(int id)
        {
            return await dbContext.Leagues
                .AsNoTracking()
                .Include(l => l.Teams)
                .ThenInclude(l => l.Players)
                .FirstOrDefaultAsync(l => l.Id == id);
        }
    }
}
