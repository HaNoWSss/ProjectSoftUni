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
        public async Task<IActionResult> Create(Woodcarver model)
        {
            if (ModelState.IsValid)
            {
                dbContext.Woodcarvers.Add(model);
                await dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }
    }
}
