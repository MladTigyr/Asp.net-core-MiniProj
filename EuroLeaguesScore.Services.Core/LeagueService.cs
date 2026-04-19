namespace EuroLeaguesScore.Services.Core
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EuroLeaguesScore.Data.Repository.Contracts;
    using EuroLeaguesScore.Data.Models;
    using EuroLeaguesScore.Services.Core.Contracts;
    using EuroLeaguesScore.ViewModels.League;
    using ViewModels.Team;

    public class LeagueService : ILeagueService
    {
        private readonly ILeagueRepository leagueRepository;

        public LeagueService(ILeagueRepository leagueRepository)
        {
            this.leagueRepository = leagueRepository;
        }

        public async Task<IEnumerable<AllLeagueViewModel>> GetAllLeagueViewModelsOrderedByLeagueNameAsync()
        {
            IEnumerable<League> entityLeagues = await leagueRepository
                .GetAllLeaguesWithTheirTeamsOrderedByLeagueNameAsync();

            IEnumerable<AllLeagueViewModel> models = entityLeagues
                .Select(l => new AllLeagueViewModel
                {
                    Id = l.Id,
                    Name = l.Name,
                    Country = l.Teams.Select(t => t.Country).FirstOrDefault() ?? "Unknown"
                });

            return models;
        }

        public async Task<DetailsLeagueViewModel?> GetDetailsLeagueViewModelAsync(int id)
        {
            League? league = await leagueRepository
                .GetLeagueIfExistsWithIdParamAsync(id);

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
    }
}
