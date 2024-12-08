using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WoodcarvingApp.Data.Models;
using WoodcarvingApp.Web.Data;
using WoodcarvingApp.Web.ViewModels.Woodcarver;

namespace WoodcarvingApp.Web.Controllers
{
    public class WoodcarverController(WoodcarvingDbContext dbContext) : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Woodcarver> allWoodcarvers = await dbContext
                .Woodcarvers
                .ToListAsync();

            return View(allWoodcarvers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var cities = dbContext.Cities;

            var model = new WoodcarverCreateViewModel()
            {
                CityList = new SelectList(cities, nameof(City.Id), nameof(City.CityName))
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WoodcarverCreateViewModel inputModel)
        {
            if (ModelState.ContainsKey(nameof(WoodcarverCreateViewModel.CityList)))
            {
                ModelState.Remove(nameof(WoodcarverCreateViewModel.CityList));
            }
            inputModel.CityList = new SelectList(await dbContext.Cities.ToListAsync(), nameof(City.Id), nameof(City.CityName));

            if (!ModelState.IsValid)
            {
                return View(inputModel);
            }

            if (!dbContext.Cities.Any(c => c.Id == inputModel.CityId))
            {
                ModelState.AddModelError(nameof(inputModel.CityId), "Invalid city selected.");
                return View(inputModel);
            }

            var woodcarver = new Woodcarver
            {
                FirstName = inputModel.FirstName,
                LastName = inputModel.LastName,
                CityId = inputModel.CityId,
                Age = inputModel.Age,
                PhoneNumber = inputModel.PhoneNumber,
                ImageUrl = inputModel.ImageUrl,
                IsDeleted = false
            };

            await dbContext.Woodcarvers.AddAsync(woodcarver);
            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
