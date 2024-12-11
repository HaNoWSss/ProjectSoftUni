using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WoodcarvingApp.Data.Models;
using WoodcarvingApp.Services.Mapping;
using WoodcarvingApp.Web.Data;
using WoodcarvingApp.Web.ViewModels.Woodcarving;

namespace WoodcarvingApp.Web.Controllers
{
    public class WoodcarvingController(WoodcarvingDbContext dbContext) : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Woodcarving> allWoodcarving = await dbContext
                .Woodcarvings
                .Where(w => !w.IsDeleted)
                .ToListAsync();

            return View(allWoodcarving);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create()
        {

            var woodcarvers = await dbContext.Woodcarvers
                .Select(w => new WoodcarverViewModel
                {
                    Id = w.Id,
                    FullName = $"{w.FirstName} {w.LastName}"
                })
                .ToListAsync();
            var woodTypes = await dbContext.WoodTypes
                .Select(w => new WoodTypeViewModel
                {
                    Id = w.Id,
                    WoodTypeName = w.WoodTypeName
                })
                .ToListAsync();

            var model = new WoodcarvingCreateViewModel
            {
                Woodcarvers = woodcarvers,
                WoodTypes = woodTypes
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(WoodcarvingCreateViewModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                inputModel.Woodcarvers = await dbContext.Woodcarvers
                    .Select(w => new WoodcarverViewModel
                    {
                        Id = w.Id,
                        FullName = $"{w.FirstName} {w.LastName}"
                    })
                    .ToListAsync();
                inputModel.WoodTypes = await dbContext.WoodTypes
                    .Select(w => new WoodTypeViewModel
                    {
                        Id = w.Id,
                        WoodTypeName = w.WoodTypeName
                    })
                    .ToListAsync();

                return View(inputModel);
            }

            if (string.IsNullOrWhiteSpace(inputModel.ImageUrl))
                inputModel.ImageUrl = "/images/woodcarving-image-not-added.png";

            var woodcarving = new Woodcarving();
            AutoMapperConfig.MapperInstance.Map(inputModel, woodcarving);
            await dbContext.AddAsync(woodcarving);
            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var woodcarving = await dbContext.Woodcarvings
                .Include(w => w.Woodcarver)
                .Include(w => w.WoodType)
                .Include(w => w.WoodcarvingExhibitions)
                    .ThenInclude(we => we.Exhibition)
                .Where(w => !w.IsDeleted && w.Id == id)
                .FirstOrDefaultAsync();

            if (woodcarving == null)
            {
                return NotFound();
            }

            var model = new WoodcarvingDetailsViewModel
            {
                Id = woodcarving.Id,
                Title = woodcarving.Title,
                Description = woodcarving.Description,
                WoodcarverName = $"{woodcarving.Woodcarver.FirstName} {woodcarving.Woodcarver.LastName}",
                WoodTypeName = woodcarving.WoodType.WoodTypeName,
                ImageUrl = woodcarving.ImageUrl,
                IsAvailable = woodcarving.IsAvailable,
                Exhibitions = woodcarving.WoodcarvingExhibitions
                    .Where(w => !w.Exhibition.IsDeleted)
                    .Select(we => new ExhibitionViewModel
                    {
                        Id = we.Exhibition.Id,
                        ExhibitionName = we.Exhibition.ExhibitionName,
                        StartDate = we.Exhibition.StartDate,
                        EndDate = we.Exhibition.EndDate
                    })
                    .ToList()
            };

            return View(model);
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(Guid id)
        {

            var woodcarvers = await dbContext.Woodcarvers
                .Select(w => new WoodcarverViewModel
                {
                    Id = w.Id,
                    FullName = w.FirstName + w.LastName
                })
                .ToListAsync();
            var woodTypes = await dbContext.WoodTypes
                .Select(w => new WoodTypeViewModel
                {
                    Id = w.Id,
                    WoodTypeName = w.WoodTypeName
                })
                .ToListAsync();

            var model = await dbContext.Woodcarvings
                .Where(w => w.Id == id)
                .Select(w => new WoodcarvingEditViewModel
                {
                    Id = w.Id,
                    Title = w.Title,
                    Description = w.Description,
                    WoodcarverId = w.WoodcarverId,
                    Woodcarvers = woodcarvers,
                    WoodTypeId = w.WoodTypeId,
                    WoodTypes = woodTypes,
                    ImageUrl = w.ImageUrl,
                    IsAvailable = w.IsAvailable
                })
                .FirstOrDefaultAsync();

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(WoodcarvingEditViewModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                inputModel.Woodcarvers = await dbContext.Woodcarvers
                    .Select(w => new WoodcarverViewModel
                    {
                        Id = w.Id,
                        FullName = $"{w.FirstName} {w.LastName}"
                    })
                    .ToListAsync();
                inputModel.WoodTypes = await dbContext.WoodTypes
                    .Select(w => new WoodTypeViewModel
                    {
                        Id = w.Id,
                        WoodTypeName = w.WoodTypeName
                    })
                    .ToListAsync();

                return View(inputModel);
            }

            if (string.IsNullOrWhiteSpace(inputModel.ImageUrl))
                inputModel.ImageUrl = "/images/woodcarving-image-not-added.png";

            var woodcarving = await dbContext.Woodcarvings
                .FirstOrDefaultAsync(w => w.Id == inputModel.Id && !w.IsDeleted);

            if (woodcarving == null)
            {
                return NotFound();
            }

            woodcarving.Title = inputModel.Title;
            woodcarving.Description = inputModel.Description;
            woodcarving.WoodcarverId = inputModel.WoodcarverId ?? throw new InvalidOperationException("WoodcarverId is required.");
            woodcarving.WoodTypeId = inputModel.WoodTypeId ?? throw new InvalidOperationException("WoodTypeId is required.");
            woodcarving.ImageUrl = inputModel.ImageUrl;
            woodcarving.IsAvailable = inputModel.IsAvailable;

            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            var woodcarving = await dbContext.Woodcarvings
                .Where(w => !w.IsDeleted && w.Id == id)
                .Select(w => new
                {
                    w.Id,
                    w.Title,
                    w.ImageUrl
                })
                .FirstOrDefaultAsync();

            if (woodcarving == null)
            {
                return NotFound();
            }

            var model = new WoodcarvingDeleteViewModel
            {
                Id = woodcarving.Id,
                Title = woodcarving.Title,
                ImageUrl = woodcarving.ImageUrl
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var woodcarving = await dbContext.Woodcarvings
                .Where(w => !w.IsDeleted && w.Id == id)
                .FirstOrDefaultAsync();

            if (woodcarving == null)
            {
                return NotFound();
            }

            woodcarving.IsDeleted = true;

            dbContext.Woodcarvings.Update(woodcarving);
            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
