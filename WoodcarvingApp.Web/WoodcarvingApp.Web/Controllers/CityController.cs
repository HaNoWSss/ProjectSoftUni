using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WoodcarvingApp.Data.Models;
using WoodcarvingApp.Web.Data;
using WoodcarvingApp.Web.ViewModels.City;

namespace WoodcarvingApp.Web.Controllers
{
    public class CityController(WoodcarvingDbContext dbContext) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<City> allCities = await dbContext
                .Cities
                .ToListAsync();

            return View(allCities);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CityCreateViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CityCreateViewModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return View(inputModel);
            }

            var city = new City
            {
                CityName = inputModel.CityName,
                ImageUrl = inputModel.ImageUrl
            };

            await dbContext.Cities.AddAsync(city);
            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
