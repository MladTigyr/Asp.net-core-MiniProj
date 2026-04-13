namespace EuroLeaguesScore.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    using EuroLeaguesScore.Data;
    using EuroLeaguesScore.ViewModels.Team;
    using EuroLeaguesScore.Data.Models;

    public class TeamController : BaseController
    {
        private readonly EuroLeaguesScoreDbContext dbContext;

        public TeamController(EuroLeaguesScoreDbContext context)
        {
            this.dbContext = context;
        }

        [HttpGet]
        public IActionResult All()
        {
            IEnumerable<AllTeamViewModel> teams = dbContext.Teams
                .AsNoTracking()
                .Include(t => t.League)
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
                    Draws = t.Draws
                })
                .ToArray();

            return View(teams);
        }

        [HttpGet]
        public IActionResult Create()
        {
            CreateTeamInputModel model = new CreateTeamInputModel
            {
                Leagues = GetLeagues()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CreateTeamInputModel model)
        {
            model.Leagues = GetLeagues();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                Team team = new Team
                {
                    Name = model.Name,
                    Country = model.Country,
                    City = model.City,
                    LeagueId = model.LeagueId,
                    Wins = model.Wins,
                    Losses = model.Losses,
                    Draws = model.Draws
                };

                dbContext.Teams.Add(team);
                dbContext.SaveChanges();

                return RedirectToAction(nameof(All));
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "An error occured while creating the team.");
                return View(model);
            }
        }

        public IActionResult Details(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            Team? team = dbContext.Teams
                .AsNoTracking()
                .Include(t => t.League)
                .Include(t => t.Players)
                .FirstOrDefault(t => t.Id == id);

            if (team == null)
            {
                return NotFound();
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
                Draws = team.Draws
            };

            return View(model);
        }

        private IEnumerable<League> GetLeagues()
        {
            return dbContext.Leagues
                .AsNoTracking()
                .OrderBy(l => l.Name)
                .Select(l => new League
                {
                    Id = l.Id,
                    Name = l.Name,
                })
                .ToArray();
        }
    }
}
