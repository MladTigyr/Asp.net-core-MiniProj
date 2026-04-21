
namespace EuroLeaguesScore.Areas.Admin.Controllers
{
    using EuroLeaguesScore.Services.Core.Contracts;

    using EuroLeaguesScore.ViewModels.Player;
    using Microsoft.AspNetCore.Mvc;

    public class PlayerManagementController : BaseController
    {
        private readonly IPlayerService playerService;

        public PlayerManagementController(IPlayerService playerService)
        {
            this.playerService = playerService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            string adminId = GetAdminUserId();

            IEnumerable<AllPlayersViewModel> models = await playerService
                .GetAllPlayersOrderedByLeagueThenByTeamNameThenByNameAsync(adminId);

            return View(models);
        }
    }
}
