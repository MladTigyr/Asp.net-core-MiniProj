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
    }
}
