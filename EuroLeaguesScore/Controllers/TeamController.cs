namespace EuroLeaguesScore.Controllers
{
    using EuroLeaguesScore.Services.Core.Contracts;
    using EuroLeaguesScore.ViewModels.Shared;
    using EuroLeaguesScore.ViewModels.Team;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;
    using static GCommon.ViewModelsMessages;

    public class TeamController : BaseController
    {
        private readonly ITeamService teamService;
        private readonly ILeagueService leagueService;

        public TeamController(ITeamService teamService, ILeagueService leagueService    )
        {
            this.teamService = teamService;
            this.leagueService = leagueService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All([FromQuery] string? searchTerm, [FromQuery] int? leagueId, int currentPage = 1)
        {
            int elementsPerPage = 12;

            TeamPaginationBlockViewModel model = await teamService.GetAllTeamsPaginated(
                User.FindFirstValue(ClaimTypes.NameIdentifier),
                searchTerm,
                leagueId,
                elementsPerPage,
                currentPage);

            ViewBag.Leagues = await leagueService.GetAllLeagueViewModelsOrderedByLeagueNameAsync();

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add()
        {
            AddTeamInputModel model = new AddTeamInputModel
            {
                Managers = await teamService.GetManagersWhichHaveNotTeamToManageAsync(),
                Leagues = await teamService.GetLeaguesOrderedByLeagueNameAsync()
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add(AddTeamInputModel model)
        {
            model.Leagues = await teamService.GetLeaguesOrderedByLeagueNameAsync();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await teamService.AddTeamToDbAsync(model);

                return RedirectToAction(nameof(All));
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "An error occured while creating the team.");
                return View(model);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            string userId = GetUser();

            DetailsTeamInputModel? model = await teamService.GetDetailsTeamViewModelAsync(id, userId);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }


            EditTeamInputModel? model = await teamService.GetEditTeamViewModelAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, EditTeamInputModel model)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            model.Managers = await teamService.GetManagersWhichHaveNotTeamToManageOrCurrentTeamManagerAsync(id);
            model.Leagues = await teamService.GetLeaguesOrderedByLeagueNameAsync();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                bool teamExist = await teamService.EditTeamToDbAsync(id, model);

                if (teamExist == false)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(All));
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "An error occured while editing this team. Please try again later");

                return View(model);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            DeleteTeamViewModel? model = await teamService.GetDeleteTeamViewModelAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id, DeleteTeamViewModel model)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            try
            {
                bool teamExists = await teamService.DeleteTeamFromDbAsync(id, model);

                return RedirectToAction(nameof(All));
            }
            catch
            {
                ModelState.AddModelError(string.Empty, DeleteError);

                return RedirectToAction(nameof(All));
            }
        }
    }
}
