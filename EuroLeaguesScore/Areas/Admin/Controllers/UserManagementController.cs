namespace EuroLeaguesScore.Areas.Admin.Controllers
{
    using EuroLeaguesScore.Data.Seeding;
    using EuroLeaguesScore.Services.Core.Contracts;
    using EuroLeaguesScore.ViewModels.Admin.UserManagement;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class UserManagementController : BaseController
    {
        private readonly IUserService userService;

        public UserManagementController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            Guid adminId = Guid.Parse(GetAdminUserId());

            ViewData["AllRoles"] = IdentitySeeder.DefaultRoles;

            IEnumerable<AllUserViewModel> models = await userService
                .GetAllUsersViewModelOrderedByUserNameAsync(adminId);

            return View(models);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(Guid userId, string role)
        {
            if (userId == Guid.Empty || string.IsNullOrWhiteSpace(role))
            {
                return BadRequest();
            }

            bool result = await userService.AssignRoleAsync(userId, role);

            if (!result)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            string? currentUserId = GetAdminUserId();

            if (currentUserId == id.ToString())
            {
                return BadRequest("You cannot delete yourself.");
            }

            bool result = await userService.DeleteUserAsync(id);

            if (!result)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(All));
        }
    }
}
