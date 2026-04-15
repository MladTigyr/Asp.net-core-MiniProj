namespace EuroLeaguesScore.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    using EuroLeaguesScore.Data;
    using EuroLeaguesScore.ViewModels.Team;
    using EuroLeaguesScore.Data.Models;
    using EuroLeaguesScore.ViewModels.Player;
    using static GCommon.ViewModelsMessages;

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
                .ToArray();

            return View(teams);
        }

        [HttpGet]
        public IActionResult Add()
        {
            AddTeamInputModel model = new AddTeamInputModel
            {
                Managers = GetManagers(),
                Leagues = GetLeagues()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Add(AddTeamInputModel model)
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
                .Include(t => t.Manager)
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
                Draws = team.Draws,
                ManagerName = team.Manager != null ? team.Manager.Name : "No manager",
                Players = GetPlayers(team.Id)
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            Team? team = dbContext.Teams
                .FirstOrDefault(t => t.Id == id);

            if (team == null)
            {
                return NotFound();
            }

            IEnumerable<Manager> managers = dbContext.Managers
                    .AsNoTracking()
                    .Where(m => !dbContext.Teams.Any(t => t.ManagerId == m.Id && t.Id != team.Id))
                    .OrderBy(m => m.Name)
                    .ToArray();

            EditTeamInputModel model = new EditTeamInputModel
            {
                Id = id,
                Name = team.Name,
                Country = team.Country,
                City = team.City,
                LeagueId = team.LeagueId,
                ManagerId = team.ManagerId,
                Wins = team.Wins,
                Losses = team.Losses,
                Draws = team.Draws,
                Managers = managers,
                Leagues = GetLeagues()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(int id, EditTeamInputModel model)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            IEnumerable<Manager> managers = dbContext.Managers
                    .AsNoTracking()
                    .Where(m => !dbContext.Teams.Any(t => t.ManagerId == m.Id && t.Id != id))
                    .OrderBy(m => m.Name)
                    .ToArray();


            model.Managers = managers;
            model.Leagues = GetLeagues();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Team? team = dbContext.Teams
                .FirstOrDefault(t => t.Id == id);

            if (team == null)
            {
                return NotFound();
            }

            try
            {
                team.Name = model.Name;
                team.City = model.City;
                team.Country = model.Country;
                team.Wins = model.Wins;
                team.Draws = model.Draws;
                team.Losses = model.Losses;
                team.LeagueId = model.LeagueId;
                team.ManagerId = model.ManagerId;

                dbContext.SaveChanges();

                return RedirectToAction(nameof(All));
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "An error occured while editing this team. Please try again later");

                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            Team? team = dbContext.Teams
                .FirstOrDefault(t => t.Id == id);

            if (team == null)
            {
                return NotFound();
            }

            DetailsTeamInputModel model = new DetailsTeamInputModel
            {
                Id = team.Id,
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int id, DetailsTeamInputModel model)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            Team? team = dbContext.Teams
                .FirstOrDefault(t => t.Id == id);

            if (team == null)
            {
                return NotFound();
            }

            try
            {
                dbContext.Teams.Remove(team);
                dbContext.SaveChanges();

                return RedirectToAction(nameof(All));
            }
            catch
            {
                ModelState.AddModelError(string.Empty, DeleteError);

                return RedirectToAction(nameof(All));
            }
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

        private IEnumerable<Manager> GetManagers()
        {
            return dbContext.Managers
                .AsNoTracking()
                .Where(m => !dbContext.Teams.Any(t => t.ManagerId == m.Id)) // im doing this because i want to show only managers that are not currently managing a team. When i first seeded the db i made sure that all managers are not assigned to a team (i made their teamId null), so this will work fine.
                .OrderBy(m => m.Name)
                .Select(m => new Manager
                {
                    Id = m.Id,
                    Name = m.Name,
                    Age = m.Age
                })
                .ToArray();
        }

        private IEnumerable<DetailsPlayerViewModel?> GetPlayers(int teamId)
        {
            return dbContext.Players
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
                .ToArray();
        }
    }
}
