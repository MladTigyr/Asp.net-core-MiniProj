using Microsoft.AspNetCore.Mvc;

namespace EuroLeaguesScore.Controllers
{
    public class PlayerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
