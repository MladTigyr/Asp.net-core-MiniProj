
namespace EuroLeaguesScore.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using EuroLeaguesScore.Services.Core.Contracts;
    using EuroLeaguesScore.ViewModels.Team;
    using EuroLeaguesScore.ViewModels.Shared;

    public class TeamManagementController : BaseController
    {
        public readonly ITeamService teamService;

        public TeamManagementController(ITeamService teamService)
        {
            this.teamService = teamService;
        }

        public async Task<IActionResult> All(int currentPage = 1)
        {
            string adminId = GetAdminUserId();

            TeamPaginationBlockViewModel models = await teamService
                .GetAllTeamsPaginated(adminId, null, null, 10, currentPage);

            return View(models);
        }
    }
}
