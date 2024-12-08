using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WoodcarvingApp.Data.Models;
using WoodcarvingApp.Web.Data;
using WoodcarvingApp.Web.ViewModels.Woodcarver;

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
            var cities = dbContext.Cities;

            var model = new WoodcarverCreateViewModel()
            {
                CityList = new SelectList(cities, nameof(City.Id), nameof(City.CityName))
            };

            return View(model);
        }
    }
}
