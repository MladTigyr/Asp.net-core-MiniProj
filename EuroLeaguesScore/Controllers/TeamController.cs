namespace EuroLeaguesScore.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using EuroLeaguesScore.ViewModels.Team;
    using static GCommon.ViewModelsMessages;
    using EuroLeaguesScore.Services.Core.Contracts;
    using Microsoft.AspNetCore.Authorization;

    public class TeamController : BaseController
    {
        private readonly ITeamService teamService;

        public TeamController(ITeamService teamService)
        {
            this.teamService = teamService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            string? userId = GetUser();

            IEnumerable<AllTeamViewModel> teams = await teamService
                .AllTeamsOrderedByLeagueNameThenByNameAsync(userId);

            return View(teams);
        }

        [HttpGet]
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
