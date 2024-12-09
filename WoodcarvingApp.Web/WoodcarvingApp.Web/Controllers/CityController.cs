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
                .Where(w => !w.IsDeleted)
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
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var city = await dbContext.Cities
                .Where(c => !c.IsDeleted && c.Id == id)
                .FirstOrDefaultAsync();

            if (city == null)
            {
                return NotFound();
            }

            var viewModel = new CityEditViewModel
            {
                Id = city.Id,
                CityName = city.CityName,
                ImageUrl = city.ImageUrl
            };

            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CityEditViewModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return View(inputModel);
            }

            var city = await dbContext.Cities
                .Where(c => !c.IsDeleted && c.Id == inputModel.Id)
                .FirstOrDefaultAsync();

            if (city == null)
            {
                return NotFound();
            }

            city.CityName = inputModel.CityName;
            city.ImageUrl = inputModel.ImageUrl;

            dbContext.Cities.Update(city);
            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var city = await dbContext.Cities
                .Where(c => !c.IsDeleted && c.Id == id)
                .Select(c => new CityDeleteViewModel
                {
                    Id = c.Id,
                    CityName = c.CityName,
                    ImageUrl = c.ImageUrl
                })
                .FirstOrDefaultAsync();

            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(CityDeleteViewModel inputModel)
        {
            var city = await dbContext.Cities
                .Where(c => !c.IsDeleted && c.Id == inputModel.Id)
                .FirstOrDefaultAsync();

            if (city == null)
            {
                return NotFound();
            }

            city.IsDeleted = true;
            dbContext.Cities.Update(city);
            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
