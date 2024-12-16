using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WoodcarvingApp.Services.Data.Interfaces;
using WoodcarvingApp.Web.ViewModels.Woodcarver;

namespace WoodcarvingApp.Web.Controllers
{
    public class WoodcarverController(IWoodcarverService woodcarverService) : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var woodcarvers = await woodcarverService.GetAllWoodcarversAsync();
            return View(woodcarvers);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create()
        {
            var model = await woodcarverService.GetWoodcarverCreateModelAsync();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(WoodcarverCreateViewModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                var model = await woodcarverService.GetWoodcarverCreateModelAsync();
                inputModel.Cities = model?.Cities;

                return View(inputModel);
            }

            var success = await woodcarverService.CreateWoodcarverAsync(inputModel);
            if (!success)
            {
                ModelState.AddModelError("", "Failed to create woodcarver. Please check your inputs.");
                var model = await woodcarverService.GetWoodcarverCreateModelAsync();
                inputModel.Cities = model?.Cities;
                return View(inputModel);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var woodcarverDetails = await woodcarverService.GetWoodcarverDetailsAsync(id);

            if (woodcarverDetails == null)
            {
                return NotFound();
            }

            return View(woodcarverDetails);
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(Guid id)
        {
            var woodcarverEditModel = await woodcarverService.GetWoodcarverForEditAsync(id);

            if (woodcarverEditModel == null)
            {
                return NotFound();
            }

            return View(woodcarverEditModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(WoodcarverEditViewModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                var model = await woodcarverService.GetWoodcarverCreateModelAsync();
                inputModel.Cities = model?.Cities;

                return View(inputModel);
            }

            var success = await woodcarverService.UpdateWoodcarverAsync(inputModel);
            if (!success)
            {
                ModelState.AddModelError("", "Failed to update woodcarver. Please check your inputs.");
                var model = await woodcarverService.GetWoodcarverCreateModelAsync();
                inputModel.Cities = model?.Cities;
                return View(inputModel);
            }

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleteModel = await woodcarverService.GetWoodcarverForDeleteAsync(id);

            if (deleteModel == null)
            {
                return NotFound();
            }

            return View(deleteModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var success = await woodcarverService.DeleteWoodcarverAsync(id);

            if (!success)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
