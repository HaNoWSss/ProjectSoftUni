using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WoodcarvingApp.Services.Data.Interfaces;
using WoodcarvingApp.Web.ViewModels.WoodType;

namespace WoodcarvingApp.Web.Controllers
{
    public class WoodTypeController(IWoodTypeService woodTypeService) : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var woodTypes = await woodTypeService.GetAllWoodTypesAsync();
            return View(woodTypes);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            var model = new WoodTypeCreateViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(WoodTypeCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var success = await woodTypeService.CreateWoodTypeAsync(model);

            if (!success)
            {
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var woodType = await woodTypeService.GetWoodTypeDetailsAsync(id);

            if (woodType == null)
            {
                return NotFound();
            }

            return View(woodType);
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(Guid id)
        {
            var woodType = await woodTypeService.GetWoodTypeForEditAsync(id);

            if (woodType == null)
            {
                return NotFound();
            }

            return View(woodType);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(WoodTypeEditViewModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return View(inputModel);
            }

            var success = await woodTypeService.UpdateWoodTypeAsync(inputModel);

            if (!success)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            var woodType = await woodTypeService.GetWoodTypeForDeleteAsync(id);

            if (woodType == null)
            {
                return NotFound();
            }

            return View(woodType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Delete(WoodTypeDeleteViewModel inputModel)
        {
            var success = await woodTypeService.DeleteWoodTypeAsync(inputModel.Id);

            if (!success)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
