namespace EuroLeaguesScore.Controllers
{
    using EuroLeaguesScore.Data.Models;
    using EuroLeaguesScore.Services.Core.Contracts;
    using EuroLeaguesScore.ViewModels.Favourite;
    using Microsoft.AspNetCore.Mvc;

    public class FavouriteController : BaseController
    {
        private readonly IFavouriteService favouriteService;

        public FavouriteController(IFavouriteService favouriteService)
        {
            this.favouriteService = favouriteService;
        }

        [HttpGet]
        public async Task<IActionResult> Teams()
        {
            IEnumerable<FavouriteTeamViewModel>? teams = await favouriteService
                .GetAllFavTeamsIfExistAsync(GetUser());

            return View(teams);
        }

        [HttpPost]
        public async Task<IActionResult> ToggleTeam(int teamId)
        {
            if (teamId <= 0)
            {
                return BadRequest();
            }

            string userId = GetUser();

            await favouriteService
                .ToggleTeamFavouriteAsync(userId, teamId);

            return RedirectToAction("All", "Team");
        }

        [HttpPost]
        public async Task<IActionResult> AddOrRemoveTeam(int teamId)
        {
            if (teamId <= 0)
            {
                return BadRequest();
            }

            string userId = GetUser();

            await favouriteService
                .ToggleTeamFavouriteAsync(userId, teamId);

            return RedirectToAction(nameof(Teams));
        }

        [HttpPost]
        public async Task<IActionResult> TogglePlayer(int playerId)
        {
            if (playerId <= 0)
            {
                return BadRequest();
            }

            string userId = GetUser();

            await favouriteService
                .TogglePlayerFavouriteAsync(userId, playerId);

            return RedirectToAction("All", "Player");
        }

        [HttpGet]
        public async Task<IActionResult> Players()
        {
            IEnumerable<FavouritePlayerViewModel>? models = await favouriteService
                .GetAllFavPlayersIfExistAsync(GetUser());

            return View(models);
        } 

        [HttpPost]
        public async Task<IActionResult> AddOrRemovePlayer(int playerId)
        {
            if (playerId <= 0)
            {
                return BadRequest();
            }

            string userId = GetUser();

            await favouriteService
                .TogglePlayerFavouriteAsync(userId, playerId);

            return RedirectToAction(nameof(Players));
        }
    }
}
