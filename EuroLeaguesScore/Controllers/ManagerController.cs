namespace EuroLeaguesScore.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using EuroLeaguesScore.Services.Core.Contracts;
    using EuroLeaguesScore.ViewModels.Manager;

    using static GCommon.ViewModelsMessages;
    using Microsoft.AspNetCore.Authorization;

    public class ManagerController : BaseController
    {
        private readonly IManagerService managerService;

        public ManagerController(IManagerService managerService)
        {
             this.managerService = managerService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            IEnumerable<AllManagerViewModel> models = await managerService
                .GetAllManagersWithTheirTeamIfTheyHaveAsync();

            return View(models);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            AddManagerInputModel model = new AddManagerInputModel()
            {
                TeamNames = await managerService
                    .GetTeamInputModelAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddManagerInputModel model)
        {
            model.TeamNames = await managerService
                .GetTeamInputModelAsync();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await managerService
                    .AddManagerToDbAsync(model);

                return RedirectToAction(nameof(All));
            }
            catch
            {
                ModelState.AddModelError(string.Empty, AddError);

                return View(model);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            DetailsManagerViewModel? model = await managerService
                .GetDetailsManagerViewModelAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            EditManagerInputModel? model = await managerService
                .GetEditManagerViewModelAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditManagerInputModel model)
        {
            model.TeamNames = await managerService
                .GetTeamInputModelAsync();

            if (id <= 0)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                bool managerExists = await managerService
                    .EditManagerToDbAsync(id, model);

                if (!managerExists)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(All));
            }
            catch
            {
                ModelState.AddModelError(string.Empty, EditError);

                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            DeleteManagerViewModel? model = await managerService
                .DeleteManagerViewModelAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, DeleteManagerViewModel model)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            try
            {
                bool managerExists = await managerService
                    .DeleteManagerFromDbAsync(id);

                if (!managerExists)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(All));
            }
            catch
            {
                ModelState.AddModelError(string.Empty, DeleteError);

                return View(model);
            }
        }
    }
}
