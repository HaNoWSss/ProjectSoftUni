using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WoodcarvingApp.Data.Models;
using WoodcarvingApp.Web.Data;
using WoodcarvingApp.Web.ViewModels.Woodcarving;

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
            var woodcarvers = dbContext.Woodcarvers;
            var woodTypes = dbContext.WoodTypes;

            var model = new WoodcarvingCreateViewModel()
            {
                WoodcarverList = new SelectList(woodcarvers, nameof(Woodcarver.Id), nameof(Woodcarver.FirstName)),
                WoodTypeList = new SelectList(woodTypes, nameof(WoodType.Id), nameof(WoodType.WoodTypeName))
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
