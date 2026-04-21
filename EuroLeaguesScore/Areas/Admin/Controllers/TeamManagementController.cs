
namespace EuroLeaguesScore.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using EuroLeaguesScore.Services.Core.Contracts;
    using EuroLeaguesScore.ViewModels.Team;

    public class TeamManagementController : BaseController
    {
        public readonly ITeamService teamService;

        public TeamManagementController(ITeamService teamService)
        {
            this.teamService = teamService;
        }

        public async Task<IActionResult> All()
        {
            string adminId = GetAdminUserId();

            IEnumerable<AllTeamViewModel> models = await teamService
                .AllTeamsOrderedByLeagueNameThenByNameAsync(adminId);

            return View(models);
        }
    }
}
