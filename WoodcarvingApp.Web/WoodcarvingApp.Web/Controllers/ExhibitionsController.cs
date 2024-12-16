using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WoodcarvingApp.Services.Data.Interfaces;
using WoodcarvingApp.Web.ViewModels.Exhibition;

public class ExhibitionsController(IExhibitionService _exhibitionService) : Controller
{

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var exhibitions = await _exhibitionService.GetAllExhibitionsAsync();
        return View(exhibitions);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Create()
    {
        var model = await _exhibitionService.GetCreateExhibitionDataAsync();
        return View("CreateEdit", model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public async Task<IActionResult> Create(ExhibitionCreateEditViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Cities = (await _exhibitionService.GetCreateExhibitionDataAsync()).Cities;
            return View("CreateEdit", model);
        }

        bool result = await _exhibitionService.CreateExhibitionAsync(model);
        if (!result)
        {
            ModelState.AddModelError(string.Empty, "Failed to create the exhibition.");
            return View("CreateEdit", model);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Edit(Guid id)
    {
        var model = await _exhibitionService.GetEditExhibitionDataAsync(id);

        if (model == null)
        {
            return NotFound();
        }

        return View("CreateEdit", model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public async Task<IActionResult> Edit(ExhibitionCreateEditViewModel inputModel)
    {
        if (!ModelState.IsValid)
        {
            var model = await _exhibitionService.GetEditExhibitionDataAsync(inputModel.Id ?? throw new InvalidOperationException("The Guid is null at the GetEditExhibitionDataAsync method"));
            inputModel.Cities = model.Cities;
            inputModel.Woodcarvings = model.Woodcarvings;
            return View("CreateEdit", inputModel);
        }

        var success = await _exhibitionService.EditExhibitionAsync(inputModel);

        if (!success)
        {
            return NotFound();
        }

        return RedirectToAction(nameof(Index));
    }


    public async Task<IActionResult> Details(Guid id)
    {
        var model = await _exhibitionService.GetExhibitionDetailsAsync(id);

        if (model == null)
        {
            return NotFound();
        }

        return View(model);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Delete(Guid id)
    {
        var model = await _exhibitionService.GetExhibitionDeleteDataAsync(id);

        if (model == null)
        {
            return NotFound();
        }

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var success = await _exhibitionService.DeleteExhibitionAsync(id);

        if (!success)
        {
            return NotFound();
        }

        return RedirectToAction(nameof(Index));
    }
}
