using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WoodcarvingApp.Data.Models;
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
                .ToListAsync();

            return View(allWoodcarving);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
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
        public async Task<IActionResult> Create(WoodcarvingCreateViewModel inputModel)
        {
            if (ModelState.ContainsKey(nameof(WoodcarvingCreateViewModel.WoodcarverList)))
            {
                ModelState.Remove(nameof(WoodcarvingCreateViewModel.WoodcarverList));
            }
            inputModel.WoodcarverList = new SelectList(await dbContext.Woodcarvers.ToListAsync(), nameof(Woodcarver.Id), nameof(Woodcarver.FirstName));

            if (ModelState.ContainsKey(nameof(WoodcarvingCreateViewModel.WoodTypeList)))
            {
                ModelState.Remove(nameof(WoodcarvingCreateViewModel.WoodTypeList));
            }
            inputModel.WoodTypeList = new SelectList(await dbContext.WoodTypes.ToListAsync(), nameof(WoodType.Id), nameof(WoodType.WoodTypeName));

            if (!ModelState.IsValid)
            {
                inputModel.WoodcarverList = new SelectList(await dbContext.Woodcarvers.ToListAsync(), nameof(Woodcarver.Id), nameof(Woodcarver.FirstName));
                return View(inputModel);
            }

            if (!dbContext.Woodcarvers.Any(w => w.Id == inputModel.WoodcarverId))
            {
                ModelState.AddModelError(nameof(inputModel.WoodcarverId), "Invalid woodcarver selected.");
                inputModel.WoodcarverList = new SelectList(await dbContext.Woodcarvers.ToListAsync(), nameof(Woodcarver.Id), nameof(Woodcarver.FirstName));
                return View(inputModel);
            }

            if (!dbContext.WoodTypes.Any(w => w.Id == inputModel.WoodTypeId))
            {
                ModelState.AddModelError(nameof(inputModel.WoodTypeId), "Invalid wood type selected.");
                inputModel.WoodTypeList = new SelectList(await dbContext.WoodTypes.ToListAsync(), nameof(WoodType.Id), nameof(WoodType.WoodTypeName));
                return View(inputModel);
            }

            var woodcarving = new Woodcarving
            {
                Title = inputModel.Title,
                Description = inputModel.Description,
                ImageUrl = inputModel.ImageUrl,
                IsAvailable = inputModel.IsAvailable,
                WoodcarverId = inputModel.WoodcarverId,
                WoodTypeId = inputModel.WoodTypeId,
                IsDeleted = false
            };

            await dbContext.Woodcarvings.AddAsync(woodcarving);
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
        public async Task<IActionResult> Edit(Guid id)
        {
            var woodcarving = await dbContext.Woodcarvings
                .Include(w => w.Woodcarver)
                .Include(w => w.WoodType)
                .Where(w => !w.IsDeleted && w.Id == id)
                .FirstOrDefaultAsync();

            if (woodcarving == null)
            {
                return NotFound();
            }

            var model = new EditWoodcarvingViewModel
            {
                Id = woodcarving.Id,
                Title = woodcarving.Title,
                Description = woodcarving.Description,
                WoodcarverId = woodcarving.WoodcarverId,
                WoodcarverList = new SelectList(await dbContext.Woodcarvers.ToListAsync(), nameof(Woodcarver.Id), nameof(Woodcarver.FirstName)),
                WoodTypeId = woodcarving.WoodTypeId,
                WoodTypeList = new SelectList(await dbContext.WoodTypes.ToListAsync(), nameof(WoodType.Id), nameof(WoodType.WoodTypeName)),
                ImageUrl = woodcarving.ImageUrl,
                IsAvailable = woodcarving.IsAvailable
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditWoodcarvingViewModel inputModel)
        {
            if (ModelState.ContainsKey(nameof(WoodcarvingCreateViewModel.WoodcarverList)))
            {
                ModelState.Remove(nameof(WoodcarvingCreateViewModel.WoodcarverList));
            }
            inputModel.WoodcarverList = new SelectList(await dbContext.Woodcarvers.ToListAsync(), nameof(Woodcarver.Id), nameof(Woodcarver.FirstName));

            if (ModelState.ContainsKey(nameof(WoodcarvingCreateViewModel.WoodTypeList)))
            {
                ModelState.Remove(nameof(WoodcarvingCreateViewModel.WoodTypeList));
            }
            inputModel.WoodTypeList = new SelectList(await dbContext.WoodTypes.ToListAsync(), nameof(WoodType.Id), nameof(WoodType.WoodTypeName));
            if (!ModelState.IsValid)
            {
                inputModel.WoodcarverList = new SelectList(await dbContext.Woodcarvers.ToListAsync(), nameof(Woodcarver.Id), nameof(Woodcarver.FirstName));
                inputModel.WoodTypeList = new SelectList(await dbContext.WoodTypes.ToListAsync(), nameof(WoodType.Id), nameof(WoodType.WoodTypeName));
                return View(inputModel);
            }

            var woodcarving = await dbContext.Woodcarvings
                .Where(w => !w.IsDeleted && w.Id == inputModel.Id)
                .FirstOrDefaultAsync();

            if (woodcarving == null)
            {
                return NotFound();
            }

            woodcarving.Title = inputModel.Title;
            woodcarving.Description = inputModel.Description;
            woodcarving.WoodcarverId = inputModel.WoodcarverId;
            woodcarving.WoodTypeId = inputModel.WoodTypeId;
            woodcarving.ImageUrl = inputModel.ImageUrl;
            woodcarving.IsAvailable = inputModel.IsAvailable;

            dbContext.Woodcarvings.Update(woodcarving);
            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
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

            var model = new DeleteWoodcarvingViewModel
            {
                Id = woodcarving.Id,
                Title = woodcarving.Title,
                ImageUrl = woodcarving.ImageUrl
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
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
