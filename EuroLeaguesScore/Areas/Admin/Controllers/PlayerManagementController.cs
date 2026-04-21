
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
        public async Task<IActionResult> All(int currentPage = 1)
        {
            string adminId = GetAdminUserId();

            PlayerPaginationBlockViewModel models = await playerService
                .GetAllPlayersPaginated(adminId, null, 10, currentPage);

            return View(models);
        }
    }
}
