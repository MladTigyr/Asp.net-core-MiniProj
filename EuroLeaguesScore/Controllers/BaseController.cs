namespace EuroLeaguesScore.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;

    [Authorize]
    public class BaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        protected bool IsAuthenticated()
        {
            return User.Identity?.IsAuthenticated ?? false;
        }

        protected string? GetUser()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        protected string? GetUsername()
        {
            return User.Identity?.Name;
        }
    }
}
