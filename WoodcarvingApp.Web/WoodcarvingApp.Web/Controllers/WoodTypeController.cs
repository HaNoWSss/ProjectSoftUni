using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WoodcarvingApp.Data.Models;
using WoodcarvingApp.Web.Data;
using WoodcarvingApp.Web.ViewModels.WoodType;

namespace WoodcarvingApp.Web.Controllers
{
    public class WoodTypeController(WoodcarvingDbContext dbContext) : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var woodTypes = await dbContext.WoodTypes
                .Where(w => !w.IsDeleted)
                .Select(w => new WoodTypeIndexViewModel
                {
                    Id = w.Id,
                    WoodTypeName = w.WoodTypeName,
                    ImageUrl = w.ImageUrl
                })
                .ToListAsync();

            return View(woodTypes);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new WoodTypeCreateViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WoodTypeCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var woodType = new WoodType
            {
                WoodTypeName = model.WoodTypeName,
                Description = model.Description,
                Hardness = model.Hardness,
                Color = model.Color,
                ImageUrl = model.ImageUrl
            };

            await dbContext.WoodTypes.AddAsync(woodType);
            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
