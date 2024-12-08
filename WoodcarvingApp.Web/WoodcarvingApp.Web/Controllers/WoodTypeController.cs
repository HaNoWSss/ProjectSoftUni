using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    }
}
