namespace EuroLeaguesScore.Controllers
{
    using EuroLeaguesScore.Services.Core.Contracts;
    using EuroLeaguesScore.ViewModels.League;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class LeagueController : BaseController
    {
        private readonly ILeagueService leagueService;

        public LeagueController(ILeagueService leagueService)
        {
            this.leagueService = leagueService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            IEnumerable<AllLeagueViewModel> models = await leagueService
                .GetAllLeagueViewModelsOrderedByLeagueNameAsync();

            return View(models);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            DetailsLeagueViewModel? model = await leagueService.GetDetailsLeagueViewModelAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }
    }
}
