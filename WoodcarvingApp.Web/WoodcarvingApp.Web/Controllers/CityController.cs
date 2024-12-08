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

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var city = await dbContext.Cities
                .Include(c => c.Woodcarvers)
                .Include(c => c.Exhibitions)
                .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);

            if (city == null)
            {
                return NotFound();
            }

            var model = new CityDetailsViewModel
            {
                Id = city.Id,
                CityName = city.CityName,
                ImageUrl = city.ImageUrl,
                Woodcarvers = city.Woodcarvers
                    .Where(w => !w.IsDeleted)
                    .Select(w => new CityWoodcarverViewModel
                    {
                        Id = w.Id,
                        FullName = $"{w.FirstName} {w.LastName}",
                        ImageUrl = w.ImageUrl
                    })
                    .ToList(),
                Exhibitions = city.Exhibitions
                    .Where(e => !e.IsDeleted)
                    .Select(e => new CityExhibitionViewModel
                    {
                        Id = e.Id,
                        Name = e.ExhibitionName,
                        ImageUrl = e.ImageUrl
                    })
                    .ToList()
            };

            return View(model);
        }

    }
}
