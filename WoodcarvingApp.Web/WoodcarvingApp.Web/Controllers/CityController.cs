using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WoodcarvingApp.Services.Data.Interfaces;
using WoodcarvingApp.Web.ViewModels.City;

namespace WoodcarvingApp.Web.Controllers
{
    public class CityController(ICityService cityService) : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<CityIndexViewModel> cities = await cityService.GetAllCitiesAsync();
            return View(cities);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            return View(new CityCreateViewModel());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CityCreateViewModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return View(inputModel);
            }

            await cityService.CreateCityAsync(inputModel);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var cityDetails = await cityService.GetCityDetailsAsync(id);

            if (cityDetails == null)
            {
                return NotFound();
            }

            return View(cityDetails);
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(Guid id)
        {
            var cityEditModel = await cityService.GetCityEditModelAsync(id);

            if (cityEditModel == null)
            {
                return NotFound();
            }

            return View(cityEditModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(CityEditViewModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return View(inputModel);
            }

            var success = await cityService.EditCityAsync(inputModel);

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
            var cityDeleteModel = await cityService.GetCityDeleteModelAsync(id);

            if (cityDeleteModel == null)
            {
                return NotFound();
            }

            return View(cityDeleteModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Delete(CityDeleteViewModel inputModel)
        {
            var success = await cityService.DeleteCityAsync(inputModel.Id);

            if (!success)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
