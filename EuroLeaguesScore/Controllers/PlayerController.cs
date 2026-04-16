namespace EuroLeaguesScore.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using EuroLeaguesScore.Services.Core.Contracts;
    using EuroLeaguesScore.ViewModels.Player;

    using static GCommon.ViewModelsMessages;

    public class PlayerController : BaseController
    {
        private readonly IPlayerService playerService;

        public PlayerController(IPlayerService playerService)
        {
            this.playerService = playerService;
        }


        [HttpGet]
        public async Task<IActionResult> All()
        {
            IEnumerable<AllPlayersViewModel> model = await playerService.GetAllPlayersOrderedByLeagueThenByTeamNameThenByNameAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            DetailsPlayerViewModel? model = await playerService.GetDetailsPlayerViewModelAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            AddPlayerInputModel model = new AddPlayerInputModel()
            {
                TeamNames = await playerService.GetAllTeamsAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddPlayerInputModel model)
        {
            model.TeamNames = await playerService.GetAllTeamsAsync();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await playerService.AddPlayerToDbAsync(model);

                return RedirectToAction(nameof(All));
            }
            catch
            {
                ModelState.AddModelError(string.Empty, AddError);

                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            EditPlayerInputModel? model = await playerService
                .GetEditPlayerInputModelAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditPlayerInputModel model)
        {
            model.TeamNames = await playerService
                .GetAllTeamsAsync();

            if (id <= 0)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                bool playerExists = await playerService
                    .EditPlayerToDbAsync(id, model);
                if (!playerExists)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(All));
            }
            catch
            {
                ModelState.AddModelError(string.Empty, EditError);

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

            DeletePlayerViewModel? model = await playerService.GetDeletePlayerViewModelAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, DeletePlayerViewModel model)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            try
            {
                bool playerExists = await playerService
                    .DeletePlayerFromDbAsync(id);

                if (!playerExists)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(All));
            }
            catch
            {
                ModelState.AddModelError(string.Empty, DeleteError);

                return View(model);
            }
        }

    }
}
