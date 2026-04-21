namespace EuroLeaguesScore.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using EuroLeaguesScore.Services.Core.Contracts;
    using EuroLeaguesScore.ViewModels.Manager;

    public class ManagerManagementController : BaseController
    {
        private readonly IManagerService managerService;

        public ManagerManagementController(IManagerService managerService)
        {
            this.managerService = managerService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            IEnumerable<AllManagerViewModel> models = await managerService
                .GetAllManagersWithTheirTeamIfTheyHaveAsync();

            return View(models);
        }
    }
}
